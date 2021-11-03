using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class Customer
    {
        public string _id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int status { get; set; }
        public CustomerInformation information { get; set; }

        public Customer() { }
        public Customer(string _id, string username,
            string email,
            string password, int status,
            CustomerInformation information)
        {
            this._id = _id;
            this.username = username;
            this.email = email;
            this.password = password;
            this.status = status;
            this.information = information;
        }
    }
}
