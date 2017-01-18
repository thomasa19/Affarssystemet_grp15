using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affarssystemet
{
    /* Represents the order made by a certain customer.
     * Uses class OrderRow to control the various products.
     */
    class Order
    {
        public int orderNumber { get; private set; }
        public Customer customer { get; private set; }
        public List<OrderRow> orderRows { get; private set; }

        public Order(int orderNo, Customer cust, List<OrderRow> ordRows)
        {
            orderNumber = orderNo;
            customer = cust;
            orderRows = ordRows;
        }

        // Updates an order row in an order.
        public string UpdateOrderRows(int prodNo, int newNumberOf)
        {
            bool found = false;
            string returnStr = "";
            
            foreach (OrderRow row in orderRows)
            {
                if (row.product.productNumber == prodNo)
                {
                    row.UpdateRow(newNumberOf);
                    returnStr += "Orderraden är ändrad till:\n";
                    returnStr += row.ToString();
                    found = true;
                }
            }
            if (!found)
            {
                returnStr += "Produkten finns inte i ordern, inget ändrades. Försök igen.";
            }

            return returnStr;
        }

        public bool FindProductRow(Product findProduct)
        {
            bool found = false;

            foreach (OrderRow row in orderRows)
            {
                if (row.product.productNumber == findProduct.productNumber)
                {
                    found = true;
                }
            }

            return found;
        }

        public override string ToString()
        {
            decimal orderSum = 0m;
            string returnStr = "";
            returnStr += "Ordernummer: " + orderNumber + "\n" +
                         " Kund: " + customer.customerNumber + ", " + customer.customerName + "\n" +
                         " Beställda produkter:\n";

            foreach (OrderRow item in orderRows)
            {
                returnStr += item.ToString();
                orderSum += item.product.productPrice * item.numberOf;
            }

            returnStr += "Ordersumma: " + orderSum.ToString("C") + "\n";

            return returnStr;
        }
    }

    /* Represents a row in an order.
     * Used by class Order to control the various products in an order.
     */
    class OrderRow
    {
        public Product product { get; private set; }
        public int numberOf { get; private set; }

        public OrderRow(Product prod, int number)
        {
            product = prod;
            numberOf = number;
        }

        // Changes the bought number of a product.
        public void UpdateRow(int newNumberOf)
        {
            product.productsInStorage += (numberOf - newNumberOf);
            numberOf = newNumberOf;
        }

        public override string ToString()
        {
            return "  " + product.productNumber + ", " + product.productName + ", " + numberOf + " st á " + product.productPrice.ToString("C") + "\n";
        }
    }
}
