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
        /// <summary>
        /// Adding a new Product to the Xml Product List.
        /// </summary>
        /// <param name="p"> The new Product that we want to add. </param>
        void addProduct(Product p);

        /// <summary>
        /// A method that converting int id of Product to a Product type.
        /// </summary>
        /// <param name="lst"> The List of id that we want to convert. </param>
        /// <returns> The converted Product List. </returns>
        ObservableCollection<Product> convertIdToProduct(ObservableCollection<int> lst);

        /// <summary>
        /// Get the QRScanner.
        /// </summary>
        /// <returns> QRScanner. </returns>
        QRScanner getQRScanner();
    }
}
