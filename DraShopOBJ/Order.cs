using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class Order
    {
        public string _id { get; set; }
        public string date_create { get; set; }
        public string address { get; set; }
        public double total { get; set; }
        public int status { get; set; }
        public string customer_id { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
        public Order() { }
        public Order(string _id, string date_create,
            string address, double total,
            int status, string customer_id,
            List<OrderDetail> orderDetails)
        {
            this._id = _id;
            this.date_create = date_create;
            this.address = address;
            this.total = total;
            this.status = status;
            this.customer_id = customer_id;
            this.orderDetails = orderDetails;
        }
    }
}
