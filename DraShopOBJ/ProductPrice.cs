using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class ProductPrice
    {
        public string _id { get; set; }
        public string product_id { get; set; }
        public double price_current { get; set; }
        public string date_effect { get; set; }
        public string date_expired { get; set; }


    }
}
