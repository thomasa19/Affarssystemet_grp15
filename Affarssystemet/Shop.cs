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

        public bool OrderAdd(Order item)
        {
            bool success = false;
            if (OrderGetByNumber(item.orderNumber) != null)
            {
                Console.WriteLine("Ordern är redan inlagd. Ange ett annat ordernummer.");
                success = false;
            }
            else
            {
                shopOrders.Add(item);
                success = true;
            }

            return success;
        }

        public Order OrderGetByNumber(int orderNo)
        {
            try
            {
                return shopOrders.Single(x => x.orderNumber == orderNo);
            }
            catch (InvalidOperationException)
            {
                return null;
            }                
        }

        public List<Order> OrderGetByCustomerNumber(int custNo)
        {
            try
            {
                return null; //shopOrders. (x => x.customer.customerNumberTA == custNo);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public int OrderGetNextNumber()
        {
            int returnNumber = 0;

            if (shopOrders.Count == 0)
                returnNumber = 301;
            else
            {
                int lastNumber = shopOrders.Max(x => x.orderNumber);
                returnNumber += 1;
            }

            return returnNumber;
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
