using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affarssystemet
{
    class Shop
    {
        private List<Order> shopOrders;
        private List<Customer> shopCustomers;
        private List<Product> shopProducts;

        public Shop()
        {
            shopOrders = new List<Order>();
            shopCustomers = new List<Customer>();
            shopProducts = new List<Product>();
        }

        public void OrderAddProduct(Order item)
        {
            shopOrders.Add(item);
        }

        public void CustomersAddCustomer(Customer item)
        {
            shopCustomers.Add(item);
        }

        public void ProductsAddProduct(Product item)
        {
            shopProducts.Add(item);
        }
    }
}
