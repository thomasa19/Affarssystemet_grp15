using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affarssystemet
{
  //abstract class Product
  //  {
  //  }

  //  class  Computer : Product
  //  {
        
  //  }

  //  class  Printer : Product
  //  {
        
  //  }

  //  class ComputerDisplay : Product
  //  {
        
  //  }

  //  class Telephone : Product
  //  {
        
  //  }

    
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

        /* This method updates the number of products in storage.
         * If number of products is decrease a negative integer is passed as argument
         * and if number of products is increased a positive integer is passed as argument.
         */
        public void updateProdInStorage(int updateProdInStorage)
        {
            try
            {
                productsInStorageTA += updateProdInStorage;                
            }
            catch (InvalidOperationException)
            {
                // do something?
            }
        }




        public override string ToString()
        {
            return "Artikelnummer: " + productNumberTA + "\n" +
                   "Benämning:     " + productNameTA + "\n" +
                   "Pris:          " + productPriceTA.ToString("C") + "\n" +
                   "Antal i lager: " + productsInStorageTA + "\n";
        }
    }
    
}
