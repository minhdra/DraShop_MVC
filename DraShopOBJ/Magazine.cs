using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraShopOBJ
{
    public class Magazine
    {
        public string magazine_id { get; set; }
        public string title {get;set;}
        public string content {get;set;}
        public DateTime date_create {get;set;}
        public string  user_id_create {get;set;}

        public Magazine () { }

        public Magazine(string magazine_id,
            string title, string content,
            DateTime date_create, string user_id_create)
        {
            this.magazine_id = magazine_id;
            this.title = title;
            this.content = content;
            this.date_create = date_create;
            this.user_id_create = user_id_create;
        }
       
    }
}
