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
        public string password { get; set; }
        public int status { get; set; }
        public string email { get; set; }
        public CustomerInformation information { get; set; }
        public Cart cart { get; set; }

        public Customer() { }
        public Customer(
            string _id, string username,
            string password, int status, 
            string email, 
            CustomerInformation information,
            Cart cart)
        {
            this._id = _id;
            this.username = username;
            this.password = password;
            this.status = status;
            this.email = email;
            this.information = information;
            this.cart = cart;
        }
    }
}
