using System;
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

        private bool populated { get; set; } = false;  // For control when loading sample data


        //Counts created products in menu. To prevent duplicate products being created.
        public int CountProductCreation1 { get; set; }
        public int CountProductCreation2 { get; set; }
        public int CountProductCreation3 { get; set; }
        public int CountProductCreation4 { get; set; }

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

        public string CustomersShortList()
        {
            string returnStr = "Välj någon av kunderna:\n";

            foreach (Customer item in shopCustomers)
            {
                returnStr += item.customerNumber + ", " + item.customerName + "; ";
            }

            return returnStr;
        }

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

        /* Adds a few customers and a couple of orders to start with.
         */
        public string PopulateShop()
        {
            string returnStr = "Nu är: \n";

            if (!populated)
            {
                if (CountProductCreation1 == 0)
                {
                    ProductsAddProduct(new Product(ProductGetNextNumber(), "Dator", 4999m, 70));
                    CountProductCreation1++;
                }

                if (CountProductCreation2 == 0)
                {
                    ProductsAddProduct(new Product(ProductGetNextNumber(), "Skrivare", 2199m, 50));
                    CountProductCreation2++;
                }

                if (CountProductCreation3 == 0)
                {
                    ProductsAddProduct(new Product(ProductGetNextNumber(), "Telefon", 3299m, 50));
                    CountProductCreation3++;
                }

                if (CountProductCreation4 == 0)
                {
                    ProductsAddProduct(new Product(ProductGetNextNumber(), "Datorskärm", 999m, 50));
                    CountProductCreation4++;
                }
                returnStr += "- 4 produkter finns tillagda\n";

                if (shopProducts.Count < 4)
                {
                    return "Det finns inte tillräckligt många produkter, du måste börja med att registrera fyra olika.";
                }
                else
                {
                    CustomersAddCustomer(new Customer(101, "Lars Larsson", "Strågatan 34, Billerud"));
                    CustomersAddCustomer(new Customer(102, "Carina Persson", "Linsvägen 9, Östersund"));
                    CustomersAddCustomer(new Customer(103, "Maria Johansson", "Sturegatan 99, Stockholm"));
                    returnStr += "- 3 kunder finns tillagda\n";

                    OrderAdd(new Order(OrderGetNextNumber(), CustomerGetByNumber(103), new List<OrderRow>() { new OrderRow(ProductGetByNumber(201), 3), new OrderRow(ProductGetByNumber(203), 3), new OrderRow(ProductGetByNumber(204), 5) }));
                    OrderAdd(new Order(OrderGetNextNumber(), CustomerGetByNumber(101), new List<OrderRow>() { new OrderRow(ProductGetByNumber(201), 2), new OrderRow(ProductGetByNumber(202), 2) }));
                    OrderAdd(new Order(OrderGetNextNumber(), CustomerGetByNumber(103), new List<OrderRow>() { new OrderRow(ProductGetByNumber(204), 5) }));
                    OrderAdd(new Order(OrderGetNextNumber(), CustomerGetByNumber(102), new List<OrderRow>() { new OrderRow(ProductGetByNumber(202), 1) }));
                    returnStr += "- 4 order finns tillagda\n";

                    populated = true;

                    return returnStr;
                }
            }
            else
                return "Exempeldata är redan inläst.";
        }
    }
}
