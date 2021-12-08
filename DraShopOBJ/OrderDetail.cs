using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class OrderDetail
    {
        public string _id { get; set; }
        public string order_id { get; set; }
        public string product_id { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public string image { get; set; }
        public Product product { get; set; }

        public OrderDetail()
        {

        }

        public OrderDetail(string _id, string order_id,
            string product_id, int quantity,
            double price, string image,
            Product product)
        {
            this._id = _id;
            this.order_id = order_id;
            this.product_id = product_id;
            this.quantity = quantity;
            this.price = price;
            this.image = image;
            this.product = product;
        }
    }
}
