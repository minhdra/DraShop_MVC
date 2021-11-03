using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class ProductColor
    {
        public string _id { get; set; }
        public string product_id { get; set; }
        public string color { get; set; }
        public string image { get; set; }
        public string hex { get; set; }
        public List<ProductSize> ListSize { get; set; }

        public ProductColor (string _id, string product_id, 
            string image, string color, string hex,
            List<ProductSize> ListSize)
        {
            this._id = _id;
            this.product_id = product_id;
            this.image = image;
            this.color = color;
            this.hex = hex;
            this.ListSize = ListSize;
        }
    }
}
