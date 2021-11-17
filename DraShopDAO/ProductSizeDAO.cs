using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DraShopOBJ;

namespace DraShopDAO
{
    public class ProductSizeDAO: IProductSizeDAO
    {
        DataHelper dh = new DataHelper();

        public List<ProductSize> GetProductSizesByProductColor(string product_color_id)
        {
            string sqlQuery = product_color_id != "" ?
                "select * from dra_product_size where product_color_id = '" + product_color_id + "'" :
                "select * from dra_product_size";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return toList(dt);
        }

        public void AddProductSize(ProductSize size)
        {
            if (size != null)
            {
                dh.StoreReader("AddProductSize", size._id, size.product_color_id, 
                    size.size, size.quantity);
            }
        }

        public void UpdateProductSize(ProductSize size)
        {
            if (size != null)
            {
                dh.StoreReader("UpdateProductSize", size._id, size.product_color_id,
                    size.size, size.quantity);
            }
        }

        public void DeleteProductSize(string _id)
        {
            string sqlQuery = "DELETE FROM dra_product_size WHERE _id='" + _id + "'";
            dh.ExcuteNonQuery(sqlQuery);
        }

        public List<ProductSize> toList(DataTable dt)
        {
            List<ProductSize> list = new List<ProductSize>();
            foreach (DataRow row in dt.Rows)
            {
                int quantity = int.Parse(row[3].ToString().Trim());
                ProductSize prod = new ProductSize(row[0].ToString(), row[1].ToString(),
                    row[2].ToString(), quantity);
                list.Add(prod);

            }
            return list;
        }
    }
}
