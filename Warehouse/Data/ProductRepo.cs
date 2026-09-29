using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Data
{
    public class ProductRepo : IProductRepository
    {
        private readonly Context _context;

        /// Konstruktor som tar emot ett objekt av typen Context och tilldelar objektet 
        /// värdet av det privata objektet _context.
        public ProductRepo(Context context)
        {
            _context = context;
        }

        /// Publik asynkronisk metod som returnerar en lista med objekt av typen Product
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        /// Publik asynkronisk metod som tar emot ett objekt av typen Product. 
        /// Metoden lägger till objektet i ett DbSet med Product-objekt och sparar förändringen i databasen. 
        /// Annars kastas ett exception. 
        /// Metoden returnerar en lista med objekt av typen Product.
        public async Task<List<Product>> AddProductAsync(Product prod)
        {
            try
            {
                _context.Products.Add(prod);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return await _context.Products.ToListAsync();
        }
        
        /// En publik asynkronisk metod som tar emot en integer (id).
        /// Metoden raderar ett objekt av typen Product från ett DbSet om objektets id 
        /// överensstämmer med argumentet int id.
        /// Annars kastas ett exception.
        /// Metoden returnerar en lista med objekt av typen Product. 
        public async Task<List<Product>> DeleteProductAsync(int id)
        {
            try
            {
                var prod = _context.Products.Find(id);
                _context.Products.Remove(prod);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return await _context.Products.ToListAsync();
        }

        /// En publik asynkronisk metod som sparar förändringar i databasen.
        /// Metoden returnerar en lista med objekt av typen Product.
        public async Task<List<Product>> UpdateProductAsync(Product prod)
        {
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }

        /// Publik asynkronisk metod som tar emot en lista med objekt av typen Order.
        /// Metoden returnerar en lista med de ordrar som är Dispatched. 
       

        public List<Product> DisplayEmpty(List<Product> Products)
        {
            IEnumerable<Product> pending =
                        from prod in Products
                        where prod.Stock == 0
                        select prod;

            return pending.ToList();

           /* List<Product> newProducts = new List<Product>();

            foreach(var p in Products)
            {
                if(p.Stock == 0)
                {
                    newProducts.Add(p);
                }

            }

            return newProducts;*/
        }
    }
}