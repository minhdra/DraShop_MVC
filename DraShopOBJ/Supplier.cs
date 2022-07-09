using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class Supplier
    {
        public string supplier_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }

        public Supplier() { }

        public Supplier(string supplier_id,
            string name, string address,
            string email, string phone_number)
        {
            this.supplier_id = supplier_id;
            this.name = name;
            this.address = address;
            this.email = email;
            this.phone_number = phone_number;
        }

    }
}
