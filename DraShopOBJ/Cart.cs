using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class Cart
    {
        public string _id { get; set; }
        public string customer_id { get; set; }
        public List<CartDetail> ListCartDetail { get; set; }

        public Cart() { }
        public Cart(string id, string customer_id,
            List<CartDetail> list)
        {
            this._id = id;
            this.customer_id = customer_id;
            this.ListCartDetail = list;
        }

    }
}
