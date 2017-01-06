using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affarssystemet
{
    class Shop
    {
        private List<Order> shopOrders; // List of orders in the shop
        private List<Customer> shopCustomers; // List of customers in the shop
        private List<Product> shopProducts; // List of products in the shop

        /* Constructor creating the various lists used by the shop.
         */
        public Shop()
        {
            shopOrders = new List<Order>();
            shopCustomers = new List<Customer>();
            shopProducts = new List<Product>();
        }

        /* Adds a new order, returns true if successful otherwise false.
         * Checks that the ordernumber is not taken.
         */
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

        /* Find an order by ordernumber. Returns found order otherwise null.
         */
        public Order OrderGetByNumber(int orderNo)
        {
            Order findOrder = null;  // Defaults return to null.

            for (int i = 0; i < shopOrders.Count; i++)
            {
                // Find the order that equals the argument and if found it is returned.
                if (shopOrders[i].orderNumber == orderNo)
                    findOrder = shopOrders[i];
            }

            return findOrder;
        }

        // Find all order for a customer by customernumber. Returns a list of orders otherwise null.         

        public List<Order> OrderGetByCustomerNumber(int custNo)
        {
            try
            {
                List<Order> customerOrders = new List<Order>();
                foreach (var order in shopOrders)
                {
                    if (order.customer.customerNumber == custNo)
                    {                        
                        customerOrders.Add(order);
                    }                    
                }                              

                if (customerOrders.Any())
                    return customerOrders;
                else
                    return null; 
            }
            catch (InvalidOperationException)
            {
                return null;
            }

        }

        /* Find out the next available ordernumber. Returns next number. If the shops
         * orderlist is empty it returns the first number, the default sequense starts with 301.
         */
        public int OrderGetNextNumber()
        {
            int returnNumber = 0;

            if (shopOrders.Count == 0)
                returnNumber = 301;  // If shops order list is empty, return first number in default sequense.
            else
            {
                for (int i = 0; i < shopOrders.Count; i++)
                {
                    // Finds the highest ordernumber taken. Needed when orders are removed.
                    if (shopOrders[i].orderNumber > returnNumber)
                    {
                        returnNumber = shopOrders[i].orderNumber;  
                    }
                }
                returnNumber += 1;  // +1 to the highest taken ordernumber       
            }

            return returnNumber;
        }


        /* Find out the next available customernumber. Returns next number. If the shops
         * customerlist is empty it returns the first number, the default sequense starts with 101.
         */
        public int CustomerGetNextNumber()
        {
            int returnNumber = 0;

            if (shopCustomers.Count == 0)
                returnNumber = 101;  // If shops order list is empty, return first number in default sequense.
            else
            {
                for (int i = 0; i < shopCustomers.Count; i++)
                {
                    // Finds the highest Customernumber taken. Needed if customers are removed.
                    if (shopCustomers[i].customerNumber > returnNumber)
                    {
                        returnNumber = shopCustomers[i].customerNumber;
                    }
                }
                returnNumber += 1;  // +1 to the highest taken customernumber       
            }

            return returnNumber;
        }
        
        /* Adds a new customer. Not complete, no checks are done.
         * Needed for creating orders.
         */
        public bool CustomersAddCustomer(Customer item)
        {

            if (CustomerGetByNumber(item.customerNumber) != null)
            {
                Console.WriteLine("Kunden finns redan, välj annat kundnummer");
                return false;
            }
            else
            {                
                shopCustomers.Add(item);
                return true;
            }
                
        }

        /* Find a customer by customer number. Returns found customer otherwise null.
        */
        public Customer CustomerGetByNumber(int customerNo)
        {
            Customer findCustomer = null;  // Defaults return to null.

            for (int i = 0; i < shopCustomers.Count; i++)
            {
                // Find the customer that equals the argument and if found it is returned.
                if (shopCustomers[i].customerNumber == customerNo)
                    findCustomer = shopCustomers[i];
            }
            return findCustomer;
        }
        

        /* Adds a new product. Not complete, no checks are done.
         * Needed for creating orders.
         */
        public void ProductsAddProduct(Product item)
        {
            shopProducts.Add(item);
        }
        
        /* Deletes an order from shopOrder by Ordernumber.
         * Returns the number of products in order by adding the products to shopProducts.
         * Returns true if order found and deleted.
         * Returns false if order not found our exception thrown.
         */
        public bool DeleteOrder(int orderNumber)
        {
            try
            {
                foreach (var order in shopOrders)
                {
                    if (order.orderNumber == orderNumber)
                    {
                        foreach (var item  in order.orderRows)
                        {
                            foreach (var product in shopProducts)
                            {
                                if (item.product == product)
                                {
                                    product.updateProdInStorage(item.numberOf);
                                    Console.WriteLine("Nytt antal " + product.productName + " i lager: "+ product.productsInStorage);
                                }
                                
                            }                           
                        }                        
                        shopOrders.Remove(order);        

                        return true;
                    }                        
                }
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }



    }
}
