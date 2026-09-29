using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using System.ComponentModel.DataAnnotations;

namespace Warehouse.Data
{
    public class Customer//Ändra kopian av Product nedan!!!!!!!!!!
    {
        private int id;
        private string name;
        private string phone;
        private string email;
        private List<Order> orders;


        /// Publik string som tilldelas värdet av den privata stringen name
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
        /// Publik string som tilldelas värdet av den privata stringen phone
        [Required]
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        /// Publik string som tilldelas värdet av den privata stringen email
        [Required]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        /// Publik lista av orderobjekt som tilldelas värdena av den privata listan orders 
        [Required]
        public List<Order> Orders//Är detta korrekt?
        {
            get { return orders; }
            set { orders = value; }
        }
        /*public Customer(int id, string name, string phone, string email)//Göra konstruktor
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            Orders = new List<Order>();
        }*/

    }
}
