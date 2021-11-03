using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class CustomerInformation
    {
        public string _id { get; set; }
        public string customer_id { get; set; }
        public string full_name { get; set; }
        public string address { get; set; }
        public string phone_number { get; set; }

        public CustomerInformation(string _id, string customer_id,
            string full_name, string address,
            string phone_number)
        {
            this._id = _id;
            this.customer_id = customer_id;
            this.full_name = full_name;
            this.address = address;
            this.phone_number = phone_number;
        }
    }
}
