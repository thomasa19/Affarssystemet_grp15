using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affarssystemet
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // Creates the shop
            Shop shop = new Shop();

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
            Order ord1 = new Order(322, cust1, rows);
            shop.OrderAddProduct(ord1);

            // Prints the order
            Console.WriteLine(ord1);

            Console.ReadLine();
            
        }
    }
}
