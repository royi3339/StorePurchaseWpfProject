using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BE;

namespace PL.VM
{
    public class converter
    {
        public void convertToObservableCollection(List<Product> products, ObservableCollection<ProductVM> ObserPList, Action generalAction, int amount)
        {            
            ObserPList.Clear();
            List<Product> temp = products.ToList();
            foreach (Product p in temp)
            {
                p.amount = amount;
                ProductVM tempProd = new ProductVM(p);
                tempProd.amountChanged += generalAction;
                ObserPList.Add(tempProd);
            }
            generalAction();
        }
    }
}
