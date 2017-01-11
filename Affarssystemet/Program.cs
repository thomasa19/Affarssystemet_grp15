using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            // Creates sample customers and add to the list in Shop
            Customer cust1 = new Customer(shop.CustomerGetNextNumber(), "Thomas Arnqvist", "Uppsala");
            bool success = shop.CustomersAddCustomer(cust1);
            if (success)
                Console.WriteLine("Kund tillagd, happy..");

            // test fel typ av kund
            Customer cust3 = new Customer(123, "Thomas Arnqvist", "Uppsala");
            success = shop.CustomersAddCustomer(cust1);
            if (success)
                Console.WriteLine("Kund tillagd, happy..");
            else
                Console.WriteLine("Kundnr krock");
            
            Customer cust2 = new Customer(shop.CustomerGetNextNumber(), "Karl Larsson", "Uppsala");          
            success = shop.CustomersAddCustomer(cust2);
            if (success)
                Console.WriteLine("En till Kund tillagd, happy..");

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
               Console.WriteLine("Första ordern registrerad\n");

            // Prints the order
            //Console.WriteLine(ord1);
            
            // Create second sample order with same customer and adds it to the list in Shop
            Order ord2 = new Order(shop.OrderGetNextNumber(), cust1, rows);

            test = shop.OrderAdd(ord2);
            if (test)
                Console.WriteLine("Andra ordern registrerad\n");

            // Prints the orders
            //Console.WriteLine(ord2);

            if (shop.OrderGetByNumber(303) != null)
                Console.WriteLine("Tummen upp! Ordern finns\n");
            else
                Console.WriteLine("Tummen ner! Ordern saknas\n");
                           
            List<Order> customerOrders = new List<Order>();
            customerOrders = shop.OrderGetByCustomerNumber(123);

            if (customerOrders != null)
            {
                Console.WriteLine("Kunden har beställt:");

                foreach (var item in customerOrders)
                {
                    Console.WriteLine(item.ToString() +"\n");
                }                
            }                
            else
                Console.WriteLine("Tummen ner!");

            // Delete order by ordernumber           
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
                    Console.WriteLine(
                   "1. Lägg till en Dator\n" +
                   "2. Lägg till en Skrivare\n" +
                   "3. Lägg till en Telefon\n" +
                   "4. Lägg till en Datorskärm\n");
              
                    int menuSelection = 0;
                    

                    try
                    {
                        menuSelection = int.Parse(Console.ReadLine()); //Input of the menuchoices.
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Ett fel inträffade!");
                        Console.WriteLine("Tryck valfri knapp för att komma till huvudmenyn.");
                        Console.ReadLine();
                    }

                    switch (menuSelection)
                    {
                        case 1:
                          //If CountProductCreating1 is beyond 0 product will not be created
                            if (shop.CountProductCreation1 == 0)
                            {
                                Product computer = new Product(shop.ProductGetNextNumber(), "Dator", 4999m, 70);
                                shop.ProductsAddProduct(computer);
                                Console.WriteLine("Produkten är nu registrerad");
                                    Console.WriteLine(computer.ToString());
                                shop.CountProductCreation1++; 
                            }
                            else
                            {
                                Console.WriteLine("Produkten är redan registrerad");
                            }
                            
                            Console.WriteLine("");
                            Console.WriteLine("Tryck enter för att fortsätta.");
                            Console.ReadLine();
                            break;
                        case 2:
                            if (shop.CountProductCreation2 == 0)
                                {
                                Product printer = new Product(shop.ProductGetNextNumber(), "Skrivare", 2199m, 50);
                                shop.ProductsAddProduct(printer);
                                Console.WriteLine("Produkten är nu registrerad");
                                Console.WriteLine(printer.ToString());
                                shop.CountProductCreation2++;
                            }
                            else
                            {
                                Console.WriteLine("Produkten är redan registrerad");
                            }

                            Console.WriteLine("");
                            Console.WriteLine("Tryck enter för att fortsätta.");
                            Console.ReadLine();
                            break;
                        case 3:                 
                            if (shop.CountProductCreation3 == 0)
                            {
                                Product telepphone = new Product(shop.ProductGetNextNumber(), "Telefon", 3299, 50);
                                shop.ProductsAddProduct(telepphone);
                                Console.WriteLine("Produkten är nu registrerad");
                                Console.WriteLine(telepphone.ToString());
                                shop.CountProductCreation3++;
                            }
                            else
                            {
                                Console.WriteLine("Produkten är redan registrerad");
                            }
                            Console.WriteLine("");
                            Console.WriteLine("Tryck enter för att fortsätta.");
                            Console.ReadLine();
                            break;
                        case 4:
                            if (shop.CountProductCreation4 == 0)
                            {
                                Product computerScreen = new Product(shop.ProductGetNextNumber(), "Datorskärm", 999m, 50);
                                shop.ProductsAddProduct(computerScreen);
                                Console.WriteLine("Produkten är nu registrerad");
                                Console.WriteLine(computerScreen.ToString());
                                shop.CountProductCreation4++;
                            }
                            else
                            {
                                Console.WriteLine("Produkten är redan registrerad");
                            }
                            Console.WriteLine("");
                            Console.WriteLine("Tryck enter för att fortsätta.");
                            Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("Du måste välja något av nedanstående."); // Om användaren använder någonting annat än ovan tillgängliga alternativ.
                            break;
                    }
                    break;
                case 2:

                    int choiceProduct=99, choice=99;
                    Console.WriteLine("Ändra produkt\n");                    
                    List<Product> productList = shop.productList();
                    foreach (var item in productList)
                    {
                        Console.WriteLine(item);
                    }

                    Console.Write("\n\nVilken produkt vill du ändra (Ange artikelnummer) : ");

                    // Controls if input is an integer if not it defaults to 99.
                    if (int.TryParse(Console.ReadLine(), out choiceProduct))
                    {
                        if (shop.ProductGetByNumber(choiceProduct)!=null)
                        {
                            Console.WriteLine("Vill du ändra:\n" + 
                                              "1. antal\n"+
                                              "2. pris\n");

                            if (int.TryParse(Console.ReadLine(), out choice))
                            {
                                if (choice == 1)
                                {
                                    Console.Write("Ge ett nytt antal:");
                                    try
                                    {
                                        shop.ProductGetByNumber(choiceProduct).productsInStorage =
                                            Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Du har angett ett val som antingen inte finns eller har fel format");
                                    }
                                }
                                if (choice == 2)
                                {
                                    Console.Write("Ge ett nytt pris:");
                                    try
                                    {
                                        shop.ProductGetByNumber(choiceProduct).productPrice =
                                            Convert.ToInt32(Console.ReadLine());
                                                                                
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Du har angett ett val som antingen inte finns eller har fel format");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Du har angett ett val som antingen inte finns eller har fel format");
                            }
                        }
                        else                        
                            Console.WriteLine("Du har angett ett val som antingen inte finns eller har fel format");
                    }
                    else
                    {
                        Console.WriteLine("Du har angett ett val som antingen inte finns eller har fel format");
                    }                   

                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;

                case 3:
                    Console.WriteLine("Lägg till en kund");

                    Console.Write("\nAnge kundnamn: ");
                    string custName = Console.ReadLine();

                    Console.Write("\nAnge kundadress: ");
                    string custAddress = Console.ReadLine();

                    Customer customer = new Customer(shop.CustomerGetNextNumber(), custName, custAddress);
                    shop.CustomersAddCustomer(customer);

                    Console.WriteLine("\nKund tillagd med nedan uppgifter:");
                    Console.WriteLine(customer.ToString());
                    
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 4:
                    // Add an order
                    int customerNo = 0;
                    int productNo = 0;
                    int numberOf = 0;

                    if (shop.ProductGetNextNumber() == 201)
                        Console.WriteLine("Det finns inga produkter, du måste börja med att registrera några.");
                    else if (shop.CustomerGetNextNumber() == 101)
                        Console.WriteLine("Det finns inga kunder, du måste börja med att registrera några.");
                    else
                    {
                        Console.Write("Ange kundnummer: ");
                        while (!int.TryParse(Console.ReadLine(), out customerNo) || shop.CustomerGetByNumber(customerNo) == null)
                            Console.Write("Du angav inte korrekt kundnr, försök igen: ");
                        List<OrderRow> myNewOrder = new List<OrderRow>();

                        ConsoleKeyInfo cki;
                        do
                        {
                            Console.Write("Ange produktnr: ");
                            while (!int.TryParse(Console.ReadLine(), out productNo) || shop.ProductGetByNumber(productNo) == null || myNewOrder.Find(x => x.product.productNumber == productNo) != null)
                                Console.Write("Du angav ett felaktigt produktnr eller har redan lagt till produkten, försök igen: ");

                            Console.Write("Ange antal: ");
                            //cki = Console.ReadKey(true);
                            while (!int.TryParse(Console.ReadLine(), out numberOf))
                                Console.Write("Du angav inte ett korrkt antal, försök igen: ");
                            myNewOrder.Add(new OrderRow(shop.ProductGetByNumber(productNo), numberOf));

                            Console.Write("Lägg till en rad till? (j/n)");
                            cki = Console.ReadKey(false);  // show the key as you read it
                            Console.WriteLine();
                        } while (cki.Key == ConsoleKey.J);

                        Console.WriteLine("Du håller på att skapa följande order:");
                        Console.WriteLine("Ordernr: " + shop.OrderGetNextNumber());
                        Console.WriteLine("Kund: " + customerNo + ", " + shop.CustomerGetByNumber(customerNo).customerName);
                        Console.WriteLine("Produkter:");
                        foreach (var item in myNewOrder)
                        {
                            Console.WriteLine(" " + item.product.productNumber + ", " + item.product.productName + ", " + item.numberOf + " st" +
                                ((item.product.productsInStorage - item.numberOf < 0) ? "\n OBS att denna produkt restnoteras och leveransen försenas." : ""));
                        }

                        Console.WriteLine("Spara order? (j/n)");
                        Console.WriteLine();
                        cki = Console.ReadKey(false);
                        if (cki.Key == ConsoleKey.J)
                        {
                            int saveOrderNo = shop.OrderGetNextNumber();
                            shop.OrderAdd(new Order(saveOrderNo, shop.CustomerGetByNumber(customerNo), myNewOrder));
                            Console.WriteLine("\nOrdern är sparad.\n" + shop.OrderGetByNumber(saveOrderNo).ToString());
                        }
                        else
                            Console.WriteLine("Ordern registrerades inte.");
                    }

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
                case 8:
                    // Print all placed orders
                    Console.WriteLine(shop.ListAllOrders());

                    Console.WriteLine("");
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 9:
                    // Print all placed products
                    Console.WriteLine(shop.ListAllProducts());

                    Console.WriteLine("");
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 10:
                    // Print all placed customers
                    Console.WriteLine(shop.ListAllCustomers());

                    Console.WriteLine("");
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 11:
                    // Populate some sample products and customers
                    Console.WriteLine(shop.PopulateShop());

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
                   "8. Visa alla order\n" +
                   "9. Visa alla produkter\n" +
                   "10. Visa alla kunder\n" +
                   "11. Läs in exempeldata\n" +
                   "0. Avsluta\n";
        }
    }
}
