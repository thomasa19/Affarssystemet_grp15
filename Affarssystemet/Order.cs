using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affarssystemet
{
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

        public override string ToString()
        {
            string returnStr = "";
            returnStr += "Ordernummer: " + orderNumber + "\n" +
                         "Kund:        " + customer.customerNameTA + "\n" +
                         "Beställda produkter:\n";

            foreach (var item in orderRows)
            {
                returnStr += item.ToString();
            }
            // Lägga till för ordersumma...
            return returnStr;
        }
    }

    class OrderRow
    {
        public Product product { get; private set; }
        public int numberOf { get; private set; }

        public OrderRow(Product prod, int number)
        {
            product = prod;
            numberOf = number;
        }

        public override string ToString()
        {
            return product.productNameTA + ", " + numberOf.ToString() + " st\n";
        }
    }
}
