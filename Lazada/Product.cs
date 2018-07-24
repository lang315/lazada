using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazada
{
    class Product
    {

        //public string ConfigSKU { get; set; }
        //public string SimpleSKU { get; set; }
        //public string ConfigID { get; set; }
        //public string FormToken { get; set; }
        public string ItemID { get; set; }
        public string SKUID { get; set; }


        public Product(string linkProduct)
        {
            int countSplit = linkProduct.Split('-').Count();
            ItemID = linkProduct.Split('-')[countSplit - 2].Remove(0,1);
            SKUID = linkProduct.Split('-').Last().Replace(".html", "").Remove(0, 1);
        }

    }
}
