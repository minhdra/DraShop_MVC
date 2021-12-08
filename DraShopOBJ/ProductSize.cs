using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class ProductSize
    {
        public string _id { get; set; }
        public string product_color_id { get; set; }
        public string size { get; set; }
        public int quantity { get; set; }

        public ProductSize() { }

        public ProductSize(string _id, string product_color_id, string size, int quantity)
        {
            this._id = _id;
            this.product_color_id = product_color_id;
            this.size = size;
            this.quantity = quantity;
        }
    }
}
