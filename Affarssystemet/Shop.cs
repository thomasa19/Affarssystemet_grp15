﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affarssystemet
{
    /* Represents the shop, handling the products, customers and orders.
     */
    class Shop
    {
        private List<Order> shopOrders;       // List of orders in the shop
        private List<Customer> shopCustomers; // List of customers in the shop
        private List<Product> shopProducts;   // List of products in the shop
        public int OrderNextNumber { get; private set; } = 301; // Stores next ordernumber to be used

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
                // Update storage for each product in orderrows.
                foreach (OrderRow part in item.orderRows)
                {
                    foreach (Product product in shopProducts)
                    {
                        if (part.product == product)
                        {
                            product.productsInStorage -= part.numberOf;
                        }
                    }
                }
                // Add the order to the orders list.
                shopOrders.Add(item);
                OrderNextNumber += 1;
                success = true;
            }

            return success;
        }

        /* Returns the number of placed orders
         */
        public int NumberOfPlacedOrders()
        {
            return shopOrders.Count();
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

        /* Find out the next available customernumber. Returns next number. If the shops
         * customerlist is empty it returns the first number, the default sequense starts with 101.
         */
        public int CustomerGetNextNumber()
        {
            int returnNumber = 0;

            if (shopCustomers.Count == 0)
                returnNumber = 101;  // If shops customer list is empty, return first number in default sequense.
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

        /* Find a customer by customer number. Returns found customer otherwise null.
         */
        public Customer CustomerGetByName(string customerName)
        {
            Customer findCustomer = null;  // Defaults return to null.

            for (int i = 0; i < shopCustomers.Count; i++)
            {
                // Find the customer that equals the argument and if found it is returned.
                if (shopCustomers[i].customerName == customerName)
                    findCustomer = shopCustomers[i];
            }
            return findCustomer;
        }

        /* Find out the next available product number. Returns next number. If the shops
         * productlist is empty it returns the first number, the default sequense starts with 201.
         */
        public int ProductGetNextNumber()
        {
            int returnNumber = 0;

            if (shopProducts.Count == 0)
                returnNumber = 201;  // If shops product list is empty, return first number in default sequense.
            else
            {
                for (int i = 0; i < shopProducts.Count; i++)
                {
                    // Finds the highest Product number taken. Needed if products are removed.
                    if (shopProducts[i].productNumber > returnNumber)
                    {
                        returnNumber = shopProducts[i].productNumber;
                    }
                }
                returnNumber += 1;  // +1 to the highest taken product number       
            }

            return returnNumber;
        }

        /* Find a product by product number. Returns found product otherwise null.
         */
        public Product ProductGetByNumber(int productNo)
        {
            Product findProduct = null;  // Defaults return to null.

            for (int i = 0; i < shopProducts.Count; i++)
            {
                // Find the product that equals the argument and if found it is returned.
                if (shopProducts[i].productNumber == productNo)
                    findProduct = shopProducts[i];
            }
            return findProduct;
        }

        /* Adds a new product.
         * Needed for creating orders.
         */
        public bool ProductsAddProduct(Product item)
        {
            
            if (ProductGetByNumber(item.productNumber) != null)
            {
                Console.WriteLine("Produkten finns redan, välj annat artikelnummer");
                return false;
            }
           
                shopProducts.Add(item);
                return true;
            
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
                                    product.productsInStorage += item.numberOf;
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

        /* Show all orders in shop.
         */
        public string ListAllOrders()
        {
            string returnStr = "";

            if (shopOrders.Count != 0)
            {
                foreach (Order item in shopOrders)
                {
                    returnStr += item.ToString() + "\n";
                }
            }
            else
                returnStr += "Det saknas order.\n";

            return returnStr;
        }

        /* Show all products in shop.
         */
        public string ListAllProducts()
        {
            string returnStr = "";

            if (shopProducts.Count != 0)
            {
                foreach (var item in shopProducts)
                {
                    returnStr += item + "\n";
                }
            }
            else
                returnStr += "Det saknas produkter.\n";

            return returnStr;
        }

        /* Show all customers in shop.
         */
        public string ListAllCustomers()
        {
            string returnStr = "";

            if (shopCustomers.Count != 0)
            {
                foreach (var item in shopCustomers)
                {
                    returnStr += item + "\n";
                }
            }
            else
                returnStr += "Det saknas kunder.\n";

            return returnStr;
        }

        // A shortformed list of all customers.
        public string CustomersShortList()
        {
            string returnStr = "Välj någon av kunderna:\n";

            foreach (Customer item in shopCustomers)
            {
                returnStr += item.customerNumber + ", " + item.customerName + "; ";
            }

            return returnStr;
        }

        // A shortformed list of all products.
        public string ProductsShortList()
        {
            string returnStr = "Produkter:\n";

            foreach (Product item in shopProducts)
            {
                returnStr += item.productNumber + ", " + item.productName + "; ";
            }

            return returnStr;
        }

        // Updates a row in an order with a number of a product bought.
        public string OrderUpdate(int orderNo, int prodNo, int newNumberOf)
        {
            Order updateOrder = OrderGetByNumber(orderNo);

            return updateOrder.UpdateOrderRows(prodNo, newNumberOf);
        }

        /* Returns a customer total of products and sum of cost.
         */
        public string CustomerTotalCost(int customerNo)
        {   
            decimal productTotalSum=0;

            foreach (var item in this.OrderGetByCustomerNumber(customerNo))
            {                
                foreach (var orderRow in item.orderRows)
                {
                    productTotalSum += (orderRow.product.productPrice * orderRow.numberOf);
                }
            }
            return productTotalSum.ToString("C");
        }
        
        // Checks to see if a product is in an order
        public bool ProductInOrder(int productNo, int orderNo)
        {
            Product findProduct = ProductGetByNumber(productNo);
            Order searchOrder = OrderGetByNumber(orderNo);

            return searchOrder.FindProductRow(findProduct);
        }

        /* Adds a few customers and a couple of orders to start with.
         */
        public void PopulateShop()
        {
            ProductsAddProduct(new Product(201, "DATOR", 4999m, 70));
            ProductsAddProduct(new Product(202, "SKRIVARE", 2199m, 50));
            ProductsAddProduct(new Product(203, "TELEFON", 3299m, 50));
            ProductsAddProduct(new Product(204, "DATORSKÄRM", 999m, 50));

            CustomersAddCustomer(new Customer(101, "Lars Larsson", "Strågatan 34, Billerud"));
            CustomersAddCustomer(new Customer(102, "Carina Persson", "Linsvägen 9, Östersund"));
            CustomersAddCustomer(new Customer(103, "Maria Johansson", "Sturegatan 99, Stockholm"));

            OrderAdd(new Order(OrderNextNumber, CustomerGetByNumber(103), new List<OrderRow>() { new OrderRow(ProductGetByNumber(201), 3), new OrderRow(ProductGetByNumber(203), 3), new OrderRow(ProductGetByNumber(204), 5) }));
            OrderAdd(new Order(OrderNextNumber, CustomerGetByNumber(101), new List<OrderRow>() { new OrderRow(ProductGetByNumber(201), 2), new OrderRow(ProductGetByNumber(202), 2) }));
            OrderAdd(new Order(OrderNextNumber, CustomerGetByNumber(103), new List<OrderRow>() { new OrderRow(ProductGetByNumber(204), 5) }));
            OrderAdd(new Order(OrderNextNumber, CustomerGetByNumber(102), new List<OrderRow>() { new OrderRow(ProductGetByNumber(202), 1) }));
        }
    }
}
