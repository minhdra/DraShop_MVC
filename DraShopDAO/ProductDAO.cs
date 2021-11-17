using System;
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

        public List<Product> GetProductsByCategory(string category_id)
        {
            string sqlQuery;
            sqlQuery = category_id != "" ?
                "select * from dra_product where category_id = '" + category_id.Trim() + "'" :
                "select * from dra_product";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return toList(dt);
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
            dh.StoreReader("AddProduct", product._id, product.name, product.description,
                product.category_id, product.made_in, product.gender, product.brand,
                product.style_id, product.status, product.image_avatar, product.summary);
        }

        public void UpdateProduct(Product product)
        {
            dh.StoreReader("UpdateProduct", product._id, product.name, product.description,
               product.category_id, product.made_in, product.gender, product.brand,
               product.style_id, product.status, product.image_avatar, product.summary);
        }

        public void DeleteProduct(string product_id)
        {
            string sqlQuery = "DELETE FROM dra_product WHERE _id='" + product_id + "'";
            dh.ExcuteNonQuery(sqlQuery);
        }

        public ProductList ProductsList(string category_id, int pageIndex, int pageSize, string productName)
        {
            ProductList list = new ProductList();
            List<Product> l = new List<Product>();
            IProductColorDAO colorDAO = new ProductColorDAO();
            IProductPriceDAO priceDAO = new ProductPriceDAO();
            SqlDataReader dr = dh.StoreReader("GetProductsByPage", category_id, pageIndex, pageSize, productName);
            while(dr.Read())
            {
                List<ProductColor> color = colorDAO.GetProductColorsByProduct(dr[0].ToString());
                ProductPrice price = priceDAO.GetProductPrice(dr[0].ToString());
                Product product = new Product(dr[0].ToString(),
                    dr[1].ToString(), dr[2].ToString(),
                    dr[3].ToString(), dr[4].ToString(),
                    dr[5].ToString(), dr[6].ToString(),
                    dr[7].ToString(), dr[8].ToString(),
                    dr[9].ToString(), dr[10].ToString(),
                    color, price);
                l.Add(product);
            }
            list.listProducts = l;
            dr.NextResult();
            while(dr.Read())
            {
                list.totalCount = dr["totalCount"].ToString();
            }
            return list;
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
                prodPrice.date_effect = prodPrice.date_effect.ToString();
                prodPrice.date_expired = prodPrice.date_expired.ToString();
                Product prod = new Product(
                    row[0].ToString(), row[1].ToString(),
                    row[2].ToString(), row[3].ToString(),
                    row[4].ToString(), row[5].ToString(),
                    row[6].ToString(), row[7].ToString(),
                    row[8].ToString(), row[9].ToString(),
                    row[10].ToString(), ListColor,
                    prodPrice);
                list.Add(prod);

            }
            return list;
        }
    }
}