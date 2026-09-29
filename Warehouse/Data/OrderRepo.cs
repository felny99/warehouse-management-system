using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Data
{
    public class OrderRepo : IOrderRepository
    {
        private readonly Context _context;

        /// Konstruktor som tar emot ett objekt av typen Context och tilldelar objektet 
        /// värdet av det privata objektet _context.
        public OrderRepo(Context context)
        {
            _context = context;
        }

        /// Publik asynkronisk metod som returnerar en lista av Order-objekt.
        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        /// Publik asynkronisk metod som tar emot ett objekt av typen Order.
        /// Metoden tilldelar objektets OrderDate till tiden för exekvering 
        /// lägger sedan till objektet i ett DbSet av order-objekt och sparar förändringarna i databasen.
        /// Annars kastas ett exception.
        /// Metoden returnerar en lista med order-objekt. 
        public async Task<List<Order>> AddOrderAsync(Order ord)
        {
            try
            {
                ord.OrderDate = DateTime.Now;
                _context.Orders.Add(ord);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return await _context.Orders.ToListAsync();
        }

        /// Publik asynkronisk metod som tar emot ett objekt av typen Order.
        /// Metoden raderar objektet från en lista med objekt av typen Order. 
        /// och sparar sedan ändringarna i databasen.
        /// Annars kastas ett exception.
        /// Metoden returnerar en lista med order-objekt. 
        public async Task<List<Order>> DeleteOrdersAsync(Order ord)
        {
            try
            {
                _context.Orders.Remove(ord);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return await _context.Orders.ToListAsync();
        }

        /// Publik asynkronisk metod som sparar ändringar i databasen och sedan returnerar en lista med order-objekt. 
        public async Task<List<Order>> UpdateOrderAsync(Order ord)
        {
            await _context.SaveChangesAsync();
            return await _context.Orders.ToListAsync();
        }

        /// Publik asynkronisk metod som tar emot en lista med objekt av typen Order.
        /// Metoden returnerar en lista med de ordrar som är Dispatched. 
        public List<Order> DisplayDispatched(List<Order> Orders)
        {
            IEnumerable<Order> dispatched =
                        from ord in Orders
                        where ord.Dispatched == true
                        select ord;

            return dispatched.ToList();
        }

        /// Publik asynkronisk metod som tar emot en lista med objekt av typen Order.
        /// Metoden returnerar en lista med de ordrar som är pending (inte dispatched). 
        public List<Order> DisplayPending(List<Order> Orders)
        {
            IEnumerable<Order> pending =
                        from ord in Orders
                        where ord.Dispatched == false
                        select ord;

            return pending.ToList();
        }

        public string GetRestockingDate(Order ord, List<OrderLine> OrderLines, List<Product> Products)
        {
            if (ord.Dispatched == true)
            {
                string Message = "Dispatched";
                return Message;
            }
            else
            {
                IEnumerable<OrderLine> orderlines =
                    from OrdLines in OrderLines
                    where OrdLines.OrderId == ord.Id
                    select OrdLines;

                foreach(var lines in orderlines)
                {
                    IEnumerable<Product> prod =
                    from prods in Products
                    where prods.Id == lines.ProductId
                    select prods;

                    foreach(var prods in prod)
                    {
                        if(prods.RestockingDate > DateTime.Now)
                        {
                            string message = prods.RestockingDate.ToString();
                            return message;
                        }
                        else
                        {
                            string message = "No restocking date set.";
                            return message;
                        }

                    }


                }
                string NoOrderLines = "No OrderLines in Order";
                return NoOrderLines;
            }

        }


        /// Public metod som tar emot tre listor med objekt av typerna Order Product och OrderLine.
        /// Metoden sorterar ordrar efter OrderDate.
        /// Metoden undersöker den totala kvantiteten av beställda produkter samt om denna kvantitet 
        /// tillåts av tillgänligt stock hos produkterna i fråga.
        /// Om ja: ta bort antalet i stock och ändra orderns status på dispatched till true
        /// Om nej: ta ej bort i stock och gå vidare till nästa order? Sätt restockingDate på DateTime.now + 10 dagar.
        /// Metoden returnerar void. 
        public void DispatchOrders(List<Order> Orders, List<Product> Products, List<OrderLine> OrderLines)
        {
            IEnumerable<Order> sortedOrders =
                from ord in Orders
                where ord.Dispatched == false
                && ord.PaymentCompleted == true
                orderby ord.OrderDate
                select ord;

            foreach (var ord in sortedOrders)
            {
                IEnumerable<OrderLine> specificOrderLines =
                    from line in OrderLines
                    where line.OrderId == ord.Id
                    select line;

                bool EnoughStock = true;

                foreach (var prod in Products)
                {
                    //Subtrahera rätt produkts stock med linens kvantite
                    //om stock är 0 eller mindre så finns för lite för dispatch, breaka loopen sät enough stock som för falsk och nästa order
                    // om tillräckligt med stock är enoughstock true och nästa loop drar av stock och ändrar till dispatched
                    int NewStock = prod.Stock;
                    foreach (var line in specificOrderLines)
                    {

                        if (line.ProductId == prod.Id)
                        {
                            NewStock = NewStock - line.Quantity;

                            if (NewStock <= 0)
                            {

                                EnoughStock = false;
                                break;
                            }

                        }

                    }
                    if (EnoughStock == false)
                    {
                        if(prod.RestockingDate < DateTime.Now)
                        {
                        prod.RestockingDate = DateTime.Now.AddDays(10);//Ej ändra datetime om det redan är satt. If(om datum är 001 eller ett paserat datum) kan vi ändra
                        _context.SaveChangesAsync();
                        }
                        
                        break;
                    }
                    //om kvantiten är mindre än 0 spara EJ nya kvantiten och sätt restockingdate datetime.now+10 dagar.

                }
                if (EnoughStock == true)
                {
                    foreach (var prod in Products)
                    {
                        //Subtrahera rätt produkts stock med linens kvantite
                        //om stock är 0 eller mer efter spara den nya stock och sätt odern som dispatched
                        // bool stockChanged = false;
                        int NewStock2;
                        foreach (var line in specificOrderLines)
                        {

                            if (line.ProductId == prod.Id)
                            {
                                NewStock2 = prod.Stock - line.Quantity;

                                prod.Stock = NewStock2;

                            }

                        }
                        //om kvantiten är mindre än 0 spara EJ nya kvantiten och sätt restockingdate datetime.now+10 dagar.
                    }

                    ord.Dispatched = true; //ändrar ordern till dispatched då det fanns tillräckligt med stock av alla produkter
                    _context.SaveChangesAsync();//rätt ställe?

                }

            }


        }
    }
}





