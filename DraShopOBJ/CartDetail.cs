using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class CartDetail
    {
        public string _id { get; set; }
        public string cart_id { get; set; }
        public string product_id { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public string date_create { get; set; }
        public int status { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public string image { get; set; }
        public string name { get; set; }

        public CartDetail() { }
        public CartDetail(string id, string cart_id, string product_id,
            int quantity, double price, string date_create, int status,
            string size, string color, string image,
            string name)
        {
            this._id = id;
            this.cart_id = cart_id;
            this.product_id = product_id;
            this.quantity = quantity;
            this.price = price;
            this.date_create = date_create;
            this.status = status;
            this.size = size;
            this.color = color;
            this.image = image;
            this.name = name;
        }
    }
}
