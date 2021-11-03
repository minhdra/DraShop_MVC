using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DraShopOBJ
{
    public class Category
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string name_en { get; set; }

        public Category()
        {

        }

        public Category(string _id, string name, string name_en)
        {
            this._id = _id;
            this.name = name;
            this.name_en = name_en;
        }
    }
}