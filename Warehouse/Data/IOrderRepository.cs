using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Warehouse.Data;

interface IOrderRepository
{
        /// En metod som returnerar en lista av Order-objekt.
          Task<List<Order>> GetOrdersAsync();

        /// En metod som tar emot ett objekt av typen Order.
        /// Metoden tilldelar objektets OrderDate till tiden för exekvering 
        /// lägger sedan till objektet i ett DbSet av order-objekt och sparar förändringarna i databasen.
        /// Annars kastas ett exception.
        /// Metoden returnerar en lista med order-objekt.
          Task<List<Order>> AddOrderAsync(Order x);

        /// En metod som tar emot ett objekt av typen Order.
        /// Metoden raderar objektet från en lista med objekt av typen Order. 
        /// och sparar sedan ändringarna i databasen.
        /// Annars kastas ett exception.
        /// Metoden returnerar en lista med order-objekt. 
        //  Task<List<Order>> DeleteOrdersAsync(Order x);

        /// En metod som sparar ändringar i databasen och sedan returnerar en lista med order-objekt. 
          Task<List<Order>> UpdateOrderAsync(Order x);

        /// En metod som tar emot en lista med objekt av typen Order.
        /// Metoden returnerar en lista med de ordrar som är Dispatched.
         List<Order> DisplayDispatched(List<Order> x);

        /// En metod som tar emot en lista med objekt av typen Order.
        /// Metoden returnerar en lista med de ordrar som är pending (inte dispatched).
          List<Order> DisplayPending(List<Order> x);

    /// En metod som tar emot tre listor med objekt av typerna Order, Product och OrderLine.
    /// Metoden sorterar ordrar efter OrderDate.
    /// Metoden undersöker den totala kvantiteten av beställda produkter samt om denna kvantitet 
    /// tillåts av tillgänligt stock hos produkterna i fråga.
    /// Om ja: ta bort antalet i stock och ändra orderns status på dispatched till true
    /// Om nej: ta ej bort i stock och gå vidare till nästa order? Sätt restockingDate på DateTime.now + 10 dagar.
    /// Metoden returnerar void. 
    void DispatchOrders(List<Order> Orders, List<Product> Products, List<OrderLine> OrderLines);
}