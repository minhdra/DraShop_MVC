using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class DeliveryAddress
    {
        public string _id { get; set; }
        public string customer_id { get; set; }
        public string customer_name { get; set; }
        public string phone_number { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string communce { get; set; }
        public string specific_address { get; set; }
        public string type_address { get; set; }
        public int status { get; set; }

        public DeliveryAddress() { }
        public DeliveryAddress(string _id, string customer_id,
            string customer_name, string phone_number,
            string province, string district,
            string communce, string specific_address,
            string type_address, int status)
        {
            this._id = _id;
            this.customer_id = customer_id;
            this.customer_name = customer_name;
            this.phone_number = phone_number;
            this.province = province;
            this.district = district;
            this.communce = communce;
            this.specific_address = specific_address;
            this.type_address = type_address;
            this.status = status;
        }
    }
}
