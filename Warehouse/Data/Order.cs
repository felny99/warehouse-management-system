using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;

namespace Warehouse.Data
{
    public class Order
    {
        private int id;
        private int customerId;
        private Customer customer;
        private DateTime orderdate;
        private string deliveryAdress;
        private bool paymentCompleted;
        private bool dispatched;
        private List<OrderLine> items;


        /*[Required]//Fick konstiga errors av detta
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}."
            , MinimumLength = 3)]*/

        /// Publikt objekt av typen DateTime som tilldelas värdet av det privata objektet orderdate 
        public DateTime OrderDate
        {
            get { return orderdate; }
            set { orderdate = value; }
        }
        /// Publik int som tilldelas värdet av den privata stringen id
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        /// Publik int som tilldelas värdet av den privata stringen customerId
        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }
        /// Publikt objekt av typen Customer som tilldelas värdet av det privata objektet customer
        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        /*[Required]
        [Range(0, 999)]*/
        /// Publik string som tilldelas värdet av den privata stringen deliveryAdress
        public string DeliveryAdress
        {
            get { return deliveryAdress; }
            set { deliveryAdress = value; }
        }
        /// Publik bool som tilldelas värdet av den privata boolen paymentCompleted
        public bool PaymentCompleted
        {
            get { return paymentCompleted; }
            set { paymentCompleted = value; }
        }
        /// Publik bool som tilldelas värdet av den privata boolen dispatched
        public bool Dispatched
        {
            get { return dispatched; }
            set { dispatched = value; }
        }
        /// Publik lista av OrderLine-objekt som tilldelas värdena av den privata listan items
        public List<OrderLine> Items
        {
            get { return items;   }
            set { items = value; }
        }

        /// Objektklassens publika konstruktor i vilken den privata listan av OrderLine-objekt (items) 
        /// tilldelas värdet av en ny instansiering av en OrderLine-lista 
        public Order()
        {
            items = new List<OrderLine>();
        }

        /*public Order(int id, int customerId, string deliveryAdress, /*DateTime orderdate,
            bool dispatched, DateTime orderDate, bool paymentCompleted)
        {
            Id = id;
            CustomerId = customerId;
            //this.orderdate = orderdate;
            DeliveryAdress = deliveryAdress;
            PaymentCompleted = paymentCompleted;
            Dispatched = dispatched;
            OrderDate = orderDate;
         
        }*/
    }
}
