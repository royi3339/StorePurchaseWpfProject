using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DAL
{
    public interface IDal
    {
        void addProduct(Product unit);
        IEnumerable<Product> getAllProducts();
        void removeProduct(int id);
        void updateProduct(Product unit);
    }
}


