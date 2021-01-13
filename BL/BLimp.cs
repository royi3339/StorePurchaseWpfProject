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
    public class BLimp : IBL
    {
        IDal myDAL = oneDAL.GetDal();
        public QRScanner myQRScanner = new QRScanner();

        public void addProduct(Product unit)
        {
            myDAL.addProduct(unit);
        }

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

        public QRScanner getQRScanner()
        {
            return myQRScanner;
        }
    }
}
