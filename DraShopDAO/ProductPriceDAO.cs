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
                prod.date_effect = DateTime.Parse(row[3].ToString());
                prod.date_expired = DateTime.Parse(row[4].ToString());
            }

            return prod;
        }
    }
}
