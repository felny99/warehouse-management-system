using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Data
{
    public class OrderLineRepo : IOrderlineRepository 
    {
        private readonly Context _context;

        /// Konstruktor som tar emot ett objekt av typen Context och tilldelar objektet 
        /// värdet av det privata objektet _context.
        public OrderLineRepo(Context context) 
        {
            _context = context;
        }

        /// En publik asynkronisk metod som returnerar en lista med orderline-objekt. 
        public async Task<List<OrderLine>> GetOrderLinesAsync()
        {
            return await _context.OrderLines.ToListAsync();
        }

        /// En publik asynkronisk metod som tar emot ett objekt av typen OrderLine samt en int OrderId.
        /// Metoden tilldelar objektets id värdet av medskickad parameter OrderId samt lägger till objektet i ett DbSet
        /// av OrderLine-objektoch sprar förändringen i databasen. 
        /// Annars kastas ett exception. 
        /// Metoden returnerar en lista med orderline-objekt.
        public async Task<List<OrderLine>> AddOrderLineAsync(OrderLine ord, int OrderId)
        {
            try
            {
                ord.OrderId = OrderId;
                _context.OrderLines.Add(ord);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return await _context.OrderLines.ToListAsync();//Obs error CS0029
        }

        /// En publik asynkronisk metod som tar emot ett objekt av typen OrderLine.
        /// Metoden raderar objektet från ett DbSet av OrderLine-objektoch och sprar förändringen i databasen. 
        /// Annars kastas ett exception. 
        /// Metoden returnerar en lista med orderline-objekt.
        public async Task<List<OrderLine>> DeleteOrderLineAsync(OrderLine ord)
        {
            try
            {
                _context.OrderLines.Remove(ord);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return await _context.OrderLines.ToListAsync();
        }
    }
}