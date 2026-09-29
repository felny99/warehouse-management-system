using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;

namespace Warehouse.Data
{
    public class Product 
    {
        private int id;
        private double price;
        private string name;
        private int stock;
        private string description;
        private DateTime restockingdate;

        /*[Required]//Fick konstiga errors av detta
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}."
            , MinimumLength = 3)]*/
            
        /// Publik int som tilldelas värdet av den privata integern name    
        [Required]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// Publik int som tilldelas värdet av den privata integern id
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The value has to be bigger than {0}.")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        /// Publik int som tilldelas värdet av den privata integern stock
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The value has to be bigger than {0}.")]
        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }
        /*[Required]
        [Range(0, 999)]*/
        /// Publik double som tilldelas värdet av den privata doublen price
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The value has to be bigger than {0}.")]
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        /// Publik string som tilldelas värdet av den privata stringen description
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        /// Publikt objekt av typen DateTime som tilldelas värdet av det privata objektet restockingdate
        public DateTime RestockingDate
        {
            get { return restockingdate; }
            set { restockingdate = value; }
        }
        /*public Product(int id, string name, double price, int stock, string description)//Göra konstruktor
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
        }*/
    }
}