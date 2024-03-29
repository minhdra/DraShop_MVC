﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DraShopOBJ;
using System.Data;
using System.Data.SqlClient;

namespace DraShopDAO
{
    public class ProductDAO : IProductDAO
    {
        DataHelper dh = new DataHelper();
        public List<Product> GetProducts()
        {
            DataTable dt = dh.GetDataTable("Select * from dra_product");
            return toList(dt);
        }

        public List<Product> SearchProducts(string category_id, string keyword)
        {
            string sqlQuery;
            if (category_id.Trim() == "") sqlQuery = "select * from dra_product where name like '%" + keyword + "%'";
            else sqlQuery = "Select * from dra_product where category_id='" + category_id + "'" +
                " and name like '%" + keyword + "%'";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return toList(dt);
        }

        public List<Product> GetProductsByCategory(string category_id)
        {
            string sqlQuery;
            sqlQuery = category_id != "" ?
                "select * from dra_product where category_id = '" + category_id.Trim() + "'" :
                "select * from dra_product";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return toList(dt);
        }

        public List<Product> GetProductsBestSelling(int length)
        {
            List<Product> l = new List<Product>();
            IProductColorDAO productColorDAO = new ProductColorDAO();
            IProductPriceDAO productPrice = new ProductPriceDAO();
            SqlDataReader dr = dh.StoreReader("GetProductsBestSelling", length);
            while (dr.Read()) {
                List<ProductColor> colors = productColorDAO.GetProductColorsByProduct(dr[0].ToString());
                ProductPrice price = productPrice.GetProductPrice(dr[0].ToString());
                Product product = new Product(dr[0].ToString(),
                    dr[1].ToString(), dr[2].ToString(),
                    dr[3].ToString(), dr[4].ToString(),
                    dr[5].ToString(), dr[6].ToString(),
                    dr[7].ToString(), dr[8].ToString(),
                    dr[9].ToString(),
                    colors, price);
                l.Add(product);
            }

            return l;
        }

        public List<Product> GetProductsNew(int length)
        {
            List<Product> l = new List<Product>();
            IProductColorDAO productColorDAO = new ProductColorDAO();
            IProductPriceDAO productPrice = new ProductPriceDAO();
            SqlDataReader dr = dh.StoreReader("GetProductsNew", length);
            while (dr.Read())
            {
                List<ProductColor> colors = productColorDAO.GetProductColorsByProduct(dr[0].ToString());
                ProductPrice price = productPrice.GetProductPrice(dr[0].ToString());
                Product product = new Product(dr[0].ToString(),
                    dr[1].ToString(), dr[2].ToString(),
                    dr[3].ToString(), dr[4].ToString(),
                    dr[5].ToString(), dr[6].ToString(),
                    dr[7].ToString(), dr[8].ToString(),
                    dr[9].ToString(),
                    colors, price);
                l.Add(product);
            }

            return l;
        }

        public List<Product> GetProductsBestDiscount(int length)
        {
            List<Product> l = new List<Product>();
            IProductColorDAO productColorDAO = new ProductColorDAO();
            IProductPriceDAO productPrice = new ProductPriceDAO();
            SqlDataReader dr = dh.StoreReader("GetProductsBestDiscount", length);
            while (dr.Read())
            {
                List<ProductColor> colors = productColorDAO.GetProductColorsByProduct(dr[0].ToString());
                ProductPrice price = productPrice.GetProductPrice(dr[0].ToString());
                Product product = new Product(dr[0].ToString(),
                    dr[1].ToString(), dr[2].ToString(),
                    dr[3].ToString(), dr[4].ToString(),
                    dr[5].ToString(), dr[6].ToString(),
                    dr[7].ToString(), dr[8].ToString(),
                    dr[9].ToString(),
                    colors, price);
                l.Add(product);
            }

            return l;
        }

        public ProductList ProductsList(string category_id, int pageIndex, int pageSize, string productName)
        {
            ProductList list = new ProductList();
            List<Product> l = new List<Product>();
            IProductColorDAO colorDAO = new ProductColorDAO();
            IProductPriceDAO priceDAO = new ProductPriceDAO();
            SqlDataReader dr = dh.StoreReader("GetProductsByPage", category_id, pageIndex, pageSize, productName);
            while (dr.Read())
            {
                List<ProductColor> color = colorDAO.GetProductColorsByProduct(dr[0].ToString());
                ProductPrice price = priceDAO.GetProductPrice(dr[0].ToString());
                if (price != null)
                {
                    price.date_effect = price.date_effect.ToString();
                    price.date_expired = price.date_expired.ToString();
                }
                Product product = new Product(dr[0].ToString(),
                    dr[1].ToString(), dr[2].ToString(),
                    dr[3].ToString(), dr[4].ToString(),
                    dr[5].ToString(), dr[6].ToString(),
                    dr[7].ToString(), dr[8].ToString(),
                    dr[9].ToString(),
                    color, price);
                l.Add(product);
            }
            list.listProducts = l;
            dr.NextResult();
            while (dr.Read())
            {
                list.totalCount = dr["totalCount"].ToString();
            }
            return list;
        }

        public ProductList GetProductsByCategoryAndGender(string category_id, int gender, int pageIndex, int pageSize, string name)
        {
            ProductList list = new ProductList();
            List<Product> l = new List<Product>();
            IProductColorDAO colorDAO = new ProductColorDAO();
            IProductPriceDAO priceDAO = new ProductPriceDAO();
            SqlDataReader dr = dh.StoreReader("GetProductsByCategoryAndGender", category_id, gender, pageIndex, pageSize, name);
            while (dr.Read())
            {
                List<ProductColor> color = colorDAO.GetProductColorsByProduct(dr[0].ToString());
                ProductPrice price = priceDAO.GetProductPrice(dr[0].ToString());
                if (price != null)
                {
                    price.date_effect = price.date_effect.ToString();
                    price.date_expired = price.date_expired.ToString();
                }
                Product product = new Product(dr[0].ToString(),
                    dr[1].ToString(), dr[2].ToString(),
                    dr[3].ToString(), dr[4].ToString(),
                    dr[5].ToString(), dr[6].ToString(),
                    dr[7].ToString(), dr[8].ToString(),
                    dr[9].ToString(),
                    color, price);
                l.Add(product);
            }
            list.listProducts = l;
            dr.NextResult();
            while (dr.Read())
            {
                list.totalCount = dr["totalCount"].ToString();
            }

            return list;
        }

        public ProductList GetProductsByCategory(string category_id, int pageIndex, int pageSize, string name)
        {
            ProductList list = new ProductList();
            List<Product> l = new List<Product>();
            IProductColorDAO colorDAO = new ProductColorDAO();
            IProductPriceDAO priceDAO = new ProductPriceDAO();
            SqlDataReader dr = dh.StoreReader("GetProductsByCategoryAndGender", category_id, 2, pageIndex, pageSize, name);
            while (dr.Read())
            {
                List<ProductColor> color = colorDAO.GetProductColorsByProduct(dr[0].ToString());
                ProductPrice price = priceDAO.GetProductPrice(dr[0].ToString());
                Product product = new Product(dr[0].ToString(),
                    dr[1].ToString(), dr[2].ToString(),
                    dr[3].ToString(), dr[4].ToString(),
                    dr[5].ToString(), dr[6].ToString(),
                    dr[7].ToString(), dr[8].ToString(),
                    dr[9].ToString(),
                    color, price);
                l.Add(product);
            }
            list.listProducts = l;
            dr.NextResult();
            while (dr.Read())
            {
                list.totalCount = dr["totalCount"].ToString();
            }

            return list;
        }

        public List<Product> GetProductsHotMen()
        {
            DataTable dt = dh.GetDataTable("exec proc_getProductsHotMen");
            return toList(dt);
        }

        public List<Product> GetProductsHotWomen()
        {
            DataTable dt = dh.GetDataTable("exec proc_getProductsHotWomen");
            return toList(dt);
        }

        public Product GetProduct(string id)
        {
            DataTable dt = dh.GetDataTable("select * from dra_product where _id='" + id + "'");
            return toList(dt).Count > 0 ? toList(dt)[0] : null;
        }

        public void AddProduct(Product product)
        {
            DateTime date = DateTime.Now;
            dh.StoreReader("AddProduct", product._id, product.name, product.description,
                product.category_id, product.made_in, product.gender, product.brand,
                product.image_avatar, product.summary, date);
        }

        public void UpdateProduct(Product product)
        {
            dh.StoreReader("UpdateProduct", product._id, product.name, product.description,
               product.category_id, product.made_in, product.gender, product.brand,
               product.image_avatar, product.summary);
        }

        public void DeleteProduct(string product_id)
        {
            string sqlQuery = "DELETE FROM dra_product WHERE _id='" + product_id + "'";
            dh.ExcuteNonQuery(sqlQuery);
        }

        

        public List<Product> toList(DataTable dt)
        {
            IProductColorDAO color = new ProductColorDAO();
            IProductPriceDAO price = new ProductPriceDAO();
            List<Product> list = new List<Product>();
            foreach(DataRow row in dt.Rows)
            {
                List<ProductColor> ListColor = color.GetProductColorsByProduct(row[0].ToString().Trim());
                ProductPrice prodPrice = price.GetProductPrice(row[0].ToString().Trim());
                if(prodPrice != null)
                {
                    prodPrice.date_effect = prodPrice.date_effect.ToString();
                    prodPrice.date_expired = prodPrice.date_expired.ToString();
                }
                Product prod = new Product(
                    row[0].ToString(), row[1].ToString(),
                    row[2].ToString(), row[3].ToString(),
                    row[4].ToString(), row[5].ToString(),
                    row[6].ToString(), row[7].ToString(),
                    row[8].ToString(), row[9].ToString(),
                    ListColor,
                    prodPrice);
                list.Add(prod);

            }
            return list;
        }
    }
}