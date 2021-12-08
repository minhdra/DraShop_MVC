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
        public double price_origin { get; set; }

        public ProductPrice() { }

        public ProductPrice(string _id,
            string product_id, double price_current,
            string date_effect, string date_expired,
            double price_origin)
        {
            this._id = _id;
            this.product_id = product_id;
            this.price_current = price_current;
            this.date_effect = date_effect;
            this.date_expired = date_expired;
            this.price_origin = price_origin;
        }
    }
}
