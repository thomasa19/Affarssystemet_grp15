using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affarssystemet
{    
    class Product
    {
        public int productNumber { get; private set; }
        public string productName { get; private set; }
        public decimal productPrice { get; set; }
        public int productsInStorage { get; set; }

       

        public Product(int prodNo, string prodName, decimal prodPrice, int prodInStorage)
        {
            productNumber = prodNo;
            productName = prodName;
            productPrice = prodPrice;
            productsInStorage = prodInStorage;
        }

        /* This method updates the number of products in storage.
         * If number of products is decreased a negative integer is passed as argument
         * and if number of products is increased a positive integer is passed as argument.
         */
        public void updateProdInStorage(int updateProdInStorage)
        {
            productsInStorage += updateProdInStorage;                
            
        }

        public override string ToString()
        {
            return "Artikelnummer: " + productNumber + "\n" +
                   "Benämning:     " + productName + "\n" +
                   "Pris:          " + productPrice.ToString("C") + "\n" +
                   "Antal i lager: " + productsInStorage + "\n";
        }
    }
    
}
