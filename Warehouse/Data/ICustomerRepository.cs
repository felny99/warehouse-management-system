using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Warehouse.Data;

interface ICustomerRepository
{
    /// En metod som returnerar en lista med Customer-objekt.
    // Task<List<Customer>> GetCustomersAsync();


    /// En metod som tar emot ett objekt av typen Customer adderar objektet till 
    /// en lista av Customer-objekt och sparar förändringen i databasen. Annars kastas ett exeption. 
    /// Sedan returneras listan Customer-objekt.
    // Task<List<Customer>> AddCustomerAsync(Customer x);

    /// En metod som sparar förändringar som gjorts i databasen och returnerar en lista med Customer-objekt. 
    /// Task<List<Customer>> UpdateCustomerAsync(Customer x)

    /// En metod som tar emot ett objekt av typen Customer, raderar objektet
    /// från ett DbSet med Customer-objekt och och sparar ändringarna i databasen. 
    /// Annars kastas ett Exception.
    /// Sedan returneras en lista med objekt av typen Customer. 
    // Task<List<Customer>> DeleteCustomersAsync(Customer x)

    /// En metod som tar emot en lista med Order-objekt och en integer CustomerId. 
    /// Metoden returnerar en lista med de ordrar som är pending (dvs de ordrar som inte är Dispatched). 
    // List<Order> DisplayPending(List<Order> x, int y);

    /// En publik metod som tar emot en lista med Order-objekt och en integer CustomerId. 
    /// Metoden returnerar en lista med de ordrar som är Dispatched. 
    // List<Order> DisplayDispatched(List<Order> x, int y);
}
