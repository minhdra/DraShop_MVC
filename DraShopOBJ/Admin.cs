using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class Admin
    {
        public string _id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public int status { get; set; }
        public AdminInformation adminInformation { get; set; }

        public Admin()
        {

        }

        public Admin(string _id, string username, string password,
            string role, int status, AdminInformation adminInformation)
        {
            this._id = _id;
            this.username = username;
            this.password = password;
            this.role = role;
            this.status = status;
            this.adminInformation = adminInformation;
        }
    }
}
