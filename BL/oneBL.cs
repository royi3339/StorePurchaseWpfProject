using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class oneBL
    {
        static IBL instance = null;
        public static IBL GetBL()
        {
            if (instance == null)
                instance = new BLimp();
            return instance;
        }
    }
}