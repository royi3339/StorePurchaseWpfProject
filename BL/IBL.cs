using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        void addProduct(Product unit);
        ObservableCollection<Product> convertIdToProduct(ObservableCollection<int> lst);
        QRScanner getQRScanner();

    }
}
