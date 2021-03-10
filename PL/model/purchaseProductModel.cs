using BE;
using System.Collections.Generic;
using BL;

namespace PL.model
{
    public class purchaseProductModel
    {
        public List<Product> products;
        
        public purchaseProductModel()
        {
            BLimp myBL = new BLimp();
            products = new List<Product>();            
        }
    }
}
