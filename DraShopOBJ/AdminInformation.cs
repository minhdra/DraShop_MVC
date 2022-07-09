using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class AdminInformation
    {
        public string _id { get; set; }
        public string admin_id { get; set; }
        public string full_name { get; set; }
        public string avatar { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string identity_card { get; set; }
        
        public AdminInformation() { }

        public AdminInformation(string _id,
            string admin_id, string full_name,
            string avatar, string phone_number,
            string address, string email,
            string identity_card)
        {
            this._id = _id;
            this.admin_id = admin_id;
            this.full_name = full_name;
            this.avatar = avatar;
            this.phone_number = phone_number;
            this.address = address;
            this.email = email;
            this.identity_card = identity_card;
        }
    }
}
