using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            // Loading example data into the shop.
            shop.PopulateShop();

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
        }


        /* The functionality for the menu.
         */
        public static void MenuChoice(out bool runAgain, int menuAlternative)
        {
            runAgain = true; // Needs to be set for return, false only for exit alternative (0).
            // Variables used in various routines.
            int customerNo = 0;
            int productNo = 0;
            int orderNo = 0;
            int numberOf = 0;
            int newNumberOf = 0;
            ConsoleKeyInfo cki;

            switch (menuAlternative)
            {
                case 1:
                    string prodName;
                    int prodStorage;
                    decimal prodPrice;
                    string close1; //close if choosing not to proceed
                    string close2;
                   
                    
                  
                    //Add products by name,price, storage.
                    do
                    {
                        Console.Write("\nLägg till produkt namn: ");
                        
                        while (String.IsNullOrWhiteSpace(prodName = Console.ReadLine().ToUpper()) || prodName.Any(c => char.IsDigit(c)) || prodName.Length > 20 || prodName.Length < 3 || prodName.Any(c => char.IsPunctuation(c)))
                        {
                            if (String.IsNullOrWhiteSpace(prodName))
                            Console.Write("Du har inte skrivit något, Vänligen försök igen: ");
                            if (prodName.Any(c => char.IsDigit(c)))
                                Console.Write("Namnet får bara innehålla bokstäver, försök igen: ");
                            if (prodName.Length > 20)
                                Console.Write("Du har skrivit ett namn som är längre än 20 tecken, vänligen försök igen: ");
                            if (prodName.Length < 3 && !String.IsNullOrWhiteSpace(prodName))
                                Console.Write("Du skrivit ett namn med mindre än 3 tecken, vänligen försök igen: ");
                            if (prodName.Any(c => char.IsPunctuation(c)))
                                Console.Write("Ogiltigt tecken användes, försök igen: ");
                            
                        }
                        

                        Console.Write("Lägg till produkt pris: ");
                        while (!decimal.TryParse( close1 =Console.ReadLine(), out prodPrice))
                        {
                            Console.Write("Något blev fel försök igen eller skriv 'q' för att stänga: ");

                            if (close1 == "q")
                                return;
                        }
                       
                        Console.Write("Lägg till antal i lager: ");
                        while (!int.TryParse(close2 =Console.ReadLine(), out prodStorage))                         
                        {
                            Console.Write("Något blev fel försök igen eller skriv 'q' för att stänga: ");
                            if (close2 == "q")
                                return;                          
                        }

                        Console.WriteLine("\nDu kan avbryta med \"n\", tryck enter eller annan bokstav för att spara.");
                        Console.WriteLine();
                        cki = Console.ReadKey(true);
                        if (cki.Key != ConsoleKey.N)
                        {
                            Product product;

                            shop.ProductsAddProduct(product = new Product(shop.ProductGetNextNumber(), prodName, prodPrice, prodStorage));
                            Console.WriteLine("\nProdukten är sparad.\n" + product);
                        }
                        else
                            Console.WriteLine("Produkten registrerades inte.");
                        Console.WriteLine("Vill du lägga till fler produkter? j/n");
                       
                        cki = Console.ReadKey(false);
                    } while (cki.Key == ConsoleKey.J);

                   

                    Console.WriteLine("");
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 2:
                    // Changes a product
                    int choiceProduct = 99, choice = 99;
                    Console.WriteLine("Ändra produkt\n");
                    Console.WriteLine(shop.ProductsShortList());

                    Console.Write("\n\nVilken produkt vill du ändra (Ange artikelnummer) : ");

                    // Controls if input is an integer if not it defaults to 99.
                    if (int.TryParse(Console.ReadLine(), out choiceProduct))
                    {
                        if (shop.ProductGetByNumber(choiceProduct) != null)
                        {
                            Console.WriteLine("Vill du ändra:\n" +
                                              "1. antal\n" +
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
                    // Adds a customer

                    string custName;
                    string custAddress;
                    ConsoleKeyInfo ck;
                    bool doAgain = false;
                    bool addNewCustomer = true;

                    do
                    {
                        Console.WriteLine("\nLägg till en kund");
                        Console.Write("\nAnge kundnamn [Ex: Olle Olsson]: ");
                        while (string.IsNullOrWhiteSpace(custName = Console.ReadLine()) || custName.Any(x => char.IsDigit(x) || custName.Any(c => char.IsPunctuation(c))))
                        {
                            Console.WriteLine("Ett kundnamn måste anges och får endast innehålla bokstäver och blanksteg mellan för och efternamn. Prova igen.");
                            Console.Write("\nAnge kundnamn: ");
                        }
                        
                        Console.Write("\nAnge kundadress [Ex: Marknadsvägen 63, Jukkasjärvi]: ");

                        while (string.IsNullOrWhiteSpace(custAddress = Console.ReadLine()) || custAddress.Count(x => char.IsPunctuation(x))>1)
                        {
                            Console.WriteLine("En kundadress måste anges och får endast innehålla bokstäver, siffror, blanksteg och ett komma. Prova igen.");
                            Console.Write("\nAnge kundadress: ");
                        }
                        Console.WriteLine("\nVill du lägga till kund med nedan uppgifter? (J/N):");
                        Console.WriteLine(custName);
                        Console.WriteLine(custAddress);
                        ck = Console.ReadKey(false);

                        if (ck.Key == ConsoleKey.N)
                        {
                            Console.WriteLine("\n\nVill du lägga till ny kundinformation? (J/N)\n");
                            ck = Console.ReadKey(false);
                            if (ck.Key == ConsoleKey.N)
                            {
                                addNewCustomer = false;
                                doAgain = false;
                            }
                            else if (ck.Key == ConsoleKey.J)
                                doAgain = true;
                            else
                            {
                                doAgain = false;
                                addNewCustomer = false;
                                Console.WriteLine("\nFelaktigt val, gör om för att lägga till ny kund\n");
                            }                                
                        }
                        else if (ck.Key == ConsoleKey.J)
                        {
                            doAgain = false;
                            addNewCustomer = true;
                        }
                        else
                        {
                            doAgain = false;
                            addNewCustomer = false;
                            Console.WriteLine("\nFelaktigt val, gör om för att lägga till ny kund\n");
                        }   
                    } while (doAgain);
                    

                    if(addNewCustomer)
                    {
                        Customer customer = new Customer(shop.CustomerGetNextNumber(), custName, custAddress);
                        shop.CustomersAddCustomer(customer);

                        Console.WriteLine("\nKund tillagd med nedan uppgifter:");
                        Console.WriteLine(customer.ToString());

                    }                    

                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;

                case 4:
                    // Adds an order
                    if (shop.ProductGetNextNumber() == 201)
                        Console.WriteLine("Det finns inga produkter, du måste börja med att registrera några.");
                    else if (shop.CustomerGetNextNumber() == 101)
                        Console.WriteLine("Det finns inga kunder, du måste börja med att registrera några.");
                    else
                    {
                        Console.WriteLine(shop.CustomersShortList());
                        Console.Write("Ange kundnummer: ");
                        while (!int.TryParse(Console.ReadLine(), out customerNo) || shop.CustomerGetByNumber(customerNo) == null)
                            Console.Write("Du angav inte korrekt kundnr, försök igen: ");
                        List<OrderRow> myNewOrder = new List<OrderRow>();

                        do
                        {
                            Console.WriteLine(shop.ProductsShortList());
                            Console.Write("Ange produktnr: ");
                            while (!int.TryParse(Console.ReadLine(), out productNo) || shop.ProductGetByNumber(productNo) == null || myNewOrder.Find(x => x.product.productNumber == productNo) != null)
                                Console.Write("Du angav ett felaktigt produktnr eller har redan lagt till produkten, försök igen: ");

                            Console.Write("Ange antal: ");
                            while (!int.TryParse(Console.ReadLine(), out numberOf) || numberOf < 1)
                                Console.Write("Antal måste vara ett heltal större än 0, försök igen: ");
                            myNewOrder.Add(new OrderRow(shop.ProductGetByNumber(productNo), numberOf));

                            Console.Write("Lägg till en rad till? (j/n)");
                            cki = Console.ReadKey(true);
                        } while (cki.Key == ConsoleKey.J);

                        Console.WriteLine("\n\nDu håller på att skapa följande order:");
                        Console.WriteLine("Ordernr: " + shop.OrderNextNumber);
                        Console.WriteLine("Kund: " + customerNo + ", " + shop.CustomerGetByNumber(customerNo).customerName);
                        Console.WriteLine("Produkter:");
                        foreach (var item in myNewOrder)
                        {
                            Console.WriteLine(" " + item.product.productNumber + ", " + item.product.productName + ", " + item.numberOf + " st" +
                                ((item.product.productsInStorage - item.numberOf < 0) ? "\n OBS att denna produkt restnoteras och leveransen försenas." : ""));
                        }

                        Console.WriteLine("\nDu kan avbryta med \"n\", tryck enter eller annan bokstav för att spara.");
                        Console.WriteLine();
                        cki = Console.ReadKey(true);
                        if (cki.Key != ConsoleKey.N)
                        {
                            int saveOrderNo = shop.OrderNextNumber;
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
                    // Update an order row
                    if (shop.NumberOfPlacedOrders() == 0)
                        Console.WriteLine("Det finns inga ordrar, du måste börja med att registrera några.");
                    else
                    {
                        Console.WriteLine("Vill du se en lista över alla ordrar? (j/n)");
                        cki = Console.ReadKey(true);
                        if (cki.Key == ConsoleKey.J)
                            Console.WriteLine(shop.ListAllOrders());

                        Console.Write("Ange order du vill ändra: ");
                        while (!int.TryParse(Console.ReadLine(), out orderNo) || shop.OrderGetByNumber(orderNo) == null)
                            Console.Write("Du angav ett felaktigt ordernr, försök igen: ");
                        if (shop.OrderGetByNumber(orderNo) == null)
                            Console.WriteLine("Ordern finns inte, försök igen.");
                        else
                        {
                            Console.WriteLine("\n" + shop.OrderGetByNumber(orderNo).ToString());
                            Console.Write("Ange produktnr du vill ändra: ");
                            while (!int.TryParse(Console.ReadLine(), out productNo) || shop.ProductGetByNumber(productNo) == null || !shop.ProductInOrder(productNo, orderNo))
                                Console.Write("Du angav ett felaktigt produktnr, försök igen: ");

                            Console.Write("Ange nytt antal: ");
                            while (!int.TryParse(Console.ReadLine(), out newNumberOf) || newNumberOf < 1)
                                Console.Write("Antal måste vara ett heltal större än 0, försök igen: ");

                            Console.WriteLine(shop.OrderUpdate(orderNo, productNo, newNumberOf));
                        }
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 6:
                    // Remove one order
                    Console.WriteLine("Ta bort order, nedan ordrar finns. \n\n");
                    Console.WriteLine(shop.ListAllOrders());

                    Console.Write("Ange order nr: ");
                    int input;
                    if(!int.TryParse(Console.ReadLine(),out input))
                        Console.WriteLine("Du har angett ett val som antingen inte finns eller har fel format");
                    else if (!shop.DeleteOrder(input))
                        Console.WriteLine("Du har angett ett val som antingen inte finns eller har fel format");


                    Console.WriteLine("Tryck enter för att fortsätta.");
                    Console.ReadLine();
                    break;
                case 7:
                    // Find if a customer has an order
                    bool cont=true;
                    do
                    {
                        Console.WriteLine("\n\nVisa order för ett visst kund(nummer)");                        
                        Console.WriteLine(shop.CustomersShortList());
                        Console.Write("Skriv in ett kundnr: ");
                                                
                        if (!int.TryParse(Console.ReadLine(), out input))
                          Console.WriteLine("Du har angett ett val som har fel format");                      
                        else if (shop.CustomerGetByNumber(input)==null)
                            Console.WriteLine("Du har angett ett kundnummer som inte finns");
                        else if (shop.OrderGetByCustomerNumber(input)==null)
                        {
                            Console.WriteLine("Kunden har ingen order i shoppen");
                            cont = false;
                        }                            
                        else
                        {
                            foreach (var item in shop.OrderGetByCustomerNumber(input))
                            {
                                Console.WriteLine(item);                                
                            }
                            Console.WriteLine("Total kostnad för samtliga ordrar: "+shop.CustomerTotalCost(input) + " kr");
                            cont = false;
                        }          
                    } while (cont);
                    
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
                   "0. Avsluta\n";
        }
    }
}
