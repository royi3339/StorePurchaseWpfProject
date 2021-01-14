using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Collections.ObjectModel;

namespace BL
{
    /// <summary>
    /// BLimp class implementation.
    /// </summary>
    public class BLimp : IBL
    {
        IDal myDAL = oneDAL.GetDal();
        public QRScanner myQRScanner = new QRScanner();

        /// <summary>
        /// Adding a new Product to the Xml Product List.
        /// </summary>
        /// <param name="p"> the new Product that we want to add. </param>
        public void addProduct(Product p) { myDAL.addProduct(p); }

        /// <summary>
        /// A method that converting int id of Product to a Product type.
        /// </summary>
        /// <param name="lst"> the List of id that we want to convert. </param>
        /// <returns> ghgghh </returns>
        public ObservableCollection<Product> convertIdToProduct(ObservableCollection<int> lst)
        {
            List<Product> prod = myDAL.getAllProducts().ToList();
            ObservableCollection<Product> newProd = new ObservableCollection<Product>();
            foreach (int a in lst)
            {
                newProd.Add(prod[a - 1]);
            }
            return newProd;

            //return lst.Select(id => prod[id-1] ).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public QRScanner getQRScanner()
        {
            return myQRScanner;
        }
    }
}
