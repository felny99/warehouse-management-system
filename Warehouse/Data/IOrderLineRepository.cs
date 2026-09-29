using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Warehouse.Data;

interface IOrderlineRepository
{
    /// En metod som returnerar en lista med orderline-objekt. 
    // Task<List<OrderLine>> GetOrderLinesAsync();

    /// En  metod som tar emot ett objekt av typen OrderLine samt en int OrderId.
    /// Metoden tilldelar objektets id värdet av medskickad parameter OrderId samt lägger till objektet i ett DbSet
    /// av OrderLine-objektoch sprar förändringen i databasen. 
    /// Annars kastas ett exception. 
    /// Metoden returnerar en lista med orderline-objekt.
    // Task<List<OrderLine>> AddOrderLineAsync(OrderLine x, int y);

    /// En metod som tar emot ett objekt av typen OrderLine.
    /// Metoden raderar objektet från ett DbSet av OrderLine-objektoch och sprar förändringen i databasen. 
    /// Annars kastas ett exception. 
    /// Metoden returnerar en lista med orderline-objekt.
    // Task<List<OrderLine>> DeleteOrderLineAsync(OrderLine x);
}