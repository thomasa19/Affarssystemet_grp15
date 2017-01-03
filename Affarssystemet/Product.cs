using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affarssystemet
{
    class Product
    {

    }

    /* 
    class Product
    {
        public int productNumberTA { get; private set; }
        public string productNameTA { get; private set; }
        public decimal productPriceTA { get; private set; }
        public int productsInStorageTA { get; private set; }

        public Product(int prodNo, string prodName, decimal prodPrice, int prodInStorage)
        {
            productNumberTA = prodNo;
            productNameTA = prodName;
            productPriceTA = prodPrice;
            productsInStorageTA = prodInStorage;
        }

        public override string ToString()
        {
            return "Artikelnummer: " + productNumberTA + "\n" +
                   "Benämning:     " + productNameTA + "\n" +
                   "Pris:          " + productPriceTA.ToString("C") + "\n" +
                   "Antal i lager: " + productsInStorageTA + "\n";
        }
    }
    */
}
