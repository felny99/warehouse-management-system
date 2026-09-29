using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Warehouse.Data;

interface IProductRepository
{
     /// En metod som returnerar en lista med objekt av typen Product
      Task<List<Product>> GetProductsAsync();

     /// En metod som tar emot ett objekt av typen Product. 
     /// Metoden lägger till objektet i ett DbSet med Product-objekt och sparar förändringen i databasen. 
     /// Annars kastas ett exception. 
     /// Metoden returnerar en lista med objekt av typen Product.
      Task<List<Product>> AddProductAsync(Product x);

     /// En metod som tar emot en integer (id).
     /// Metoden raderar ett objekt av typen Product från ett DbSet om objektets id 
     /// överensstämmer med argumentet int id.
     /// Annars kastas ett exception.
     /// Metoden returnerar en lista med objekt av typen Product. 
      Task<List<Product>> DeleteProductAsync(int x);

     /// En metod som sparar förändringar i databasen.
     /// Metoden returnerar en lista med objekt av typen Product.
      Task<List<Product>> UpdateProductAsync(Product x);

     /// En metod som tar emot en lista med objekt av typen Order.
     /// Metoden returnerar en lista med de ordrar som är Dispatched. 

      List<Product> DisplayEmpty(List<Product> x);
}