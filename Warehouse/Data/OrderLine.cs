using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;

namespace Warehouse.Data
{
    public class OrderLine//Ändra kopian av product nedan!!!!!!!!!!!!!!!!
    {
        private int id;
        private int productId;
        private Product product;
        private int orderId;
        private Order order;
        private int quantity;
        

        /// Publik int som tilldelas värdet av den privata integern id
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        /// Publik int som tilldelas värdet av den privata integern producentId
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        /// Publikt objekt av typen Product som tilldelas värdet av det privata objektet product
        public Product Product
        {
            get { return product; }
            set { product = value; }
        }
        /// Publik int som tilldelas värdet av den privata integern orderId
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        /// Publikt objekt av typen Order som tilldelas värdet av det privata objektet order
        public Order Order
        {
            get { return order; }
            set { order = value; }
        }
        /// Publik int som tilldelas värdet av den privata integern quantity
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        /*public OrderLine(int id, int quantity, int productId, int orderId)
        {
            Id = id;
            ProductId = productId;
            OrderId = orderId;
            Quantity = quantity;
        }*/

    }
}
