using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class oneDAL
    {
        static IDal instance = null;
        public static IDal GetDal()
        {
            if (instance == null)
                instance = new DalXMLimp();
            return instance;
        }
    }
}