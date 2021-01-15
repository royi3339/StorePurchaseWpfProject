using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// oneBL.
    /// </summary>
    public class oneBL
    {
        static IBL instance = null;

        /// <summary>
        /// [static:] Return a BL if exist, and if not exist creat one and return it.
        /// </summary>
        /// <returns> IBL. </returns>
        public static IBL GetBL()
        {
            if (instance == null)
                instance = new BLimp();
            return instance;
        }
    }
}