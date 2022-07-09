using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DraShopOBJ
{
    public class Product
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string category_id { get; set; }
        public string made_in { get; set; }
        public string gender { get; set; }
        public string brand { get; set; }
        public string image_avatar { get; set; }
        public string summary { get; set; }
        public string date_create { get; set; }
        public List<ProductColor> ListColor { get; set; }
        public ProductPrice price { get; set; }

        public Product()
        {

        }

        public Product(string _id, string name,
           string description, string category_id, 
           string made_in, string gender, 
           string brand, string image_avatar, 
           string summary, string date_create,
           List<ProductColor> ListColor, ProductPrice price
        )
        {
            this._id = _id;
            this.name = name;
            this.description = description;
            this.category_id = category_id;
            this.made_in = made_in;
            this.gender = gender;
            this.brand = brand;
            this.image_avatar = image_avatar;
            this.summary = summary;
            this.date_create = date_create;
            this.ListColor = ListColor;
            this.price = price;
        }
    }
}