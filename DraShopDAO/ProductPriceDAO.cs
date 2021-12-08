using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;
using System.Data;

namespace DraShopDAO
{
    public class ProductPriceDAO : IProductPriceDAO
    {
        DataHelper dh = new DataHelper();

        public ProductPrice GetProductPrice(string product_id)
        {
            string sqlQuery = "select * from dra_product_price where product_id='" + product_id + "'";
            DataTable dt = dh.GetDataTable(sqlQuery);
            ProductPrice prod = new ProductPrice();
            foreach(DataRow row in dt.Rows)
            {
                prod._id = row[0].ToString();
                prod.product_id = row[1].ToString();
                prod.price_current = double.Parse(row[2].ToString());
                prod.date_effect = row[3].ToString();
                prod.date_expired = row[4].ToString();
                prod.price_origin = double.Parse(row[5].ToString());
            }

            return prod;
        }

        public List<ProductPrice> GetProductPrices()
        {
            string sqlQuery = "select * from dra_product_price";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return toList(dt);
        }

        public void AddProductPrice(ProductPrice price)
        {
            if (price != null)
            {
                dh.StoreReader("AddProductPrice", price._id, price.product_id, price.price_current, 
                    DateTime.Parse(price.date_effect), DateTime.Parse(price.date_expired));
            }
        }

        public void UpdateProductPrice(ProductPrice price)
        {
            if (price != null)
            {
                dh.StoreReader("UpdateProductPrice", price._id, price.product_id, price.price_current,
                    DateTime.Parse(price.date_effect), DateTime.Parse(price.date_expired));
            }
        }

        public void DeleteProductPrice(string _id)
        {
            string sqlQuery = "DELETE FROM dra_product_price WHERE _id='" + _id + "'";
            dh.ExcuteNonQuery(sqlQuery);
        }

        public List<ProductPrice> toList(DataTable dt)
        {
            List<ProductPrice> list = new List<ProductPrice>();
            foreach (DataRow row in dt.Rows)
            {
                double priceCurrent = double.Parse(row[2].ToString());
                double priceOrigin = double.Parse(row[5].ToString());
                ProductPrice prod = new ProductPrice(
                    row[0].ToString(), row[1].ToString(),
                    priceCurrent, row[3].ToString(), 
                    row[4].ToString(), priceOrigin);
                list.Add(prod);

            }
            return list;
        }
    }
}
