using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Data
{
        public class CustomerService : ICustomerRepository
        {
            private readonly Context _context;

            /// Konstruktor som tar emot ett objekt av typen Context och tilldelar objektet 
            /// värdet av det privata objektet _context.
            public CustomerService(Context context)
            {
                _context = context;
            }

            /// Publik asynkronisk metod GetCustomerAsync som returnerar en lista med Customer-objekt.
            public async Task<List<Customer>> GetCustomersAsync()
            {
                return await _context.Customers.ToListAsync();
            }

            /// Publik asynkronisk metod som tar emot ett objekt av typen Customer adderar objektet till 
            /// en lista av Customer-objekt och sparar förändringen i databasen. Annars kastas ett exeption. 
            /// Sedan returneras listan Customer-objekt.
            public async Task<List<Customer>> AddCustomerAsync(Customer cust)
            {
                try
                {                   
                    _context.Customers.Add(cust);
                    await _context.SaveChangesAsync();
                    //changeEvent?.Invoke();
               }
                catch (Exception)
                {
                    throw;
                }
                return await _context.Customers.ToListAsync();
            }

            /// En publik asynkronisk metod som sparar förändringar som gjorts i databasen och returnerar 
            /// en lista med Customer-objekt. 
            public async  Task<List<Customer>> UpdateCustomerAsync(Customer cust)
            {
                _context.SaveChangesAsync();
                return await _context.Customers.ToListAsync();
            }

            /// En publik asynkronisk metod som tar emot ett objekt av typen Customer, raderar objektet
            /// från ett DbSet med Customer-objekt och och sparar ändringarna i databasen. Annars kastas ett Exception.
            /// Sedan returneras en lista med objekt av typen Customer. 
            public async Task<List<Customer>> DeleteCustomersAsync(Customer cust)
            {
                try
                {
                    _context.Customers.Remove(cust);
                    _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw;
                }
                return await  _context.Customers.ToListAsync();
            }

            /// En publik metod som tar emot en lista med Order-objekt och en integer CustomerId. 
            /// Metoden returnerar en lista med de ordrar som är pending (dvs de ordrar som inte är Dispatched). 
            public List<Order> DisplayPending(List<Order> Orders, int CustomerId)
            {

                    IEnumerable<Order> pending =
                    from ord in Orders
                    where ord.Dispatched == false
                    && ord.CustomerId == CustomerId
                    select ord;

                    return pending.ToList();

            }
            /// En publik metod som tar emot en lista med Order-objekt och en integer CustomerId. 
            /// Metoden returnerar en lista med de ordrar som är Dispatched. 
            public List<Order> DisplayDispatched(List<Order> Orders, int CustomerId)
            {
                    IEnumerable<Order> dispatched =
                    from ord in Orders
                    where ord.Dispatched == true
                    && ord.CustomerId == CustomerId
                    select ord;
                    return dispatched.ToList();
            }
        }




}
