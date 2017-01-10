using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affarssystemet
{
    /* Represents the customers in the store.
     */
    class Customer
    {
        public int customerNumber { get; private set; }
        public string customerName { get; private set; }
        public string customerAddress { get; private set; }

        public Customer(int custNo, string custName, string custAddress)
        {
            customerNumber = custNo;
            customerName = custName;
            customerAddress = custAddress;
        }

        public override string ToString()
        {
            return "Kundnummer: " + customerNumber + "\n" +
                   "Namn:       " + customerName + "\n" +
                   "Adress:     " + customerAddress + "\n";
        }
    }
    
}
