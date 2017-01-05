using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affarssystemet
{
    class Program
    {
        // Creates the shop. Placed here to be accessible by menu method.
        private static Shop shop = new Shop();

        static void Main(string[] args)
        {
            int menuAlternative;
            bool runAgain = true;

            // Loop for showing the menu. When 0 the loop exits.
            while (runAgain)
            {
                Console.Clear();
                Console.WriteLine(PrintMenu());
                Console.Write("Välj vad du vill göra: ");

                // Controls if input is an integer if not it defaults to 99.
                if (int.TryParse(Console.ReadLine(), out menuAlternative))
                {
                    MenuChoice(out runAgain, menuAlternative);
                }
                else
                {
                    MenuChoice(out runAgain, 99);
                }
            }

            // Creates a couple of sample customers
            Customer cust1 = new Customer(123, "Thomas Arnqvist", "Uppsala");
            Customer cust2 = new Customer(124, "Karl Larsson", "Uppsala");
            // Adds customers to the list in Shop
            shop.CustomersAddCustomer(cust1);
            shop.CustomersAddCustomer(cust2);

            // Prints the customers
            Console.WriteLine(cust1.ToString());
            Console.WriteLine(cust2.ToString());

            // Creates a couple of sample products
            Product prod1 = new Product(233, "Äpple", 5m, 150);
            Product prod2 = new Product(234, "Apelsin", 10m, 32);
            // Adds products to the list in Shop
            shop.ProductsAddProduct(prod1);
            shop.ProductsAddProduct(prod2);

            // Prints the products
            Console.WriteLine(prod1.ToString());
            Console.WriteLine(prod2.ToString());

            // Creates a couple of sample orderrows
            OrderRow ordRow1 = new OrderRow(prod1, 23);
            OrderRow ordRow2 = new OrderRow(prod2, 4);

            // Creates a list for the orderrows and adds sample rows
            List<OrderRow> rows = new List<OrderRow>();
            rows.Add(ordRow1);
            rows.Add(ordRow2);

            // Creates a sample order and adds it to the list in Shop
            Order ord1 = new Order(shop.OrderGetNextNumber(), cust1, rows);
            bool test = shop.OrderAdd(ord1);
            if (test)
               Console.WriteLine("Toppen!");

            // Prints the order
            //Console.WriteLine(ord1);
            
            // Create second sample order with same customer and adds it to the list in Shop
            Order ord2 = new Order(shop.OrderGetNextNumber(), cust1, rows);

            test = shop.OrderAdd(ord2);
            if (test)
                Console.WriteLine("Toppen");

            // Prints the orders
            //Console.WriteLine(ord2);

            if (shop.OrderGetByNumber(301) != null)
                Console.WriteLine("Tummen upp!");
            else
                Console.WriteLine("Tummen ner!");
                           
            List<Order> customerOrders = new List<Order>();
            customerOrders = shop.OrderGetByCustomerNumber(123);

            if (customerOrders != null)
            {
                Console.WriteLine("Tummen upp!");

                foreach (var item in customerOrders)
                {
                    Console.WriteLine(item.ToString() +"\n");
                }                
            }
                
            else
                Console.WriteLine("Tummen ner!");

            // Delete order by ordernumber
            //Order delOrder = new Order(100, cust1, rows);
            Order delOrder = ord2;
            if (shop.DeleteOrder(delOrder.orderNumber))
                Console.WriteLine("Ordernr " + delOrder.orderNumber + " borttagen.");
            else
                Console.WriteLine("Ingen order borttagen");

            Console.ReadLine();
        }



        /* The functionality for the menu.
         */
        public static void MenuChoice(out bool runAgain, int menuAlternative)
        {
            runAgain = true; // Needs to be set for return, false only for exit alternative (0).

            switch (menuAlternative)
            {
                case 1:

                    Console.WriteLine("");
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 2:

                    Console.WriteLine("");
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 3:

                    Console.WriteLine("");
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 4:

                    Console.WriteLine("");
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 5:

                    Console.WriteLine("");
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 6:

                    Console.WriteLine("");
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 7:

                    Console.WriteLine("");
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 0:
                    // End and exit the program
                    runAgain = false;
                    break;
                default:
                    // In case of nothing else
                    Console.WriteLine("");
                    Console.WriteLine("Det alternativet finns inte.");
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
            }
        }

        /* Shows the menu.
         */
        public static string PrintMenu()
        {
            return "Meny för affären:\n" +
                   "1. Lägg till produkt\n" +
                   "2. Ändra produkt\n" +
                   "3. Lägg till kund\n" +
                   "4. Lägg till order\n" +
                   "5. Ändra order\n" +
                   "6. Ta bort order\n" +
                   "7. Visa order för kund(nummer)\n" +
                   "0. Avsluta\n";
        }
    }
}
