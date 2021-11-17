using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DraShopOBJ;

namespace DraShopDAO
{
    public class ProductColorDAO: IProductColorDAO
    {
        DataHelper dh = new DataHelper();
        IProductSizeDAO sizeDAO = new ProductSizeDAO();

        public List<ProductColor> GetProductColorsByProduct(string product_id)
        {
            string sqlQuery = product_id != "" ?
                "select * from dra_product_color where product_id = '" + product_id + "'" :
                "select * from dra_product_color";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return toList(dt);
        }

        public List<ProductColor> GetProductColors()
        {
            string sqlQuery = "Select * from dra_product_color";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return toList(dt);
        }

        public void AddProductColor(ProductColor color)
        {
            if(color != null)
            {
                dh.StoreReader("AddProductColor", color._id, color.product_id, color.image, color.color, color.hex);
            }
        }

        public void UpdateProductColor(ProductColor color)
        {
            if (color != null)
            {
                dh.StoreReader("UpdateProductColor", color._id, color.product_id, color.image, color.color, color.hex);
            }
        }

        public void DeleteProductColor(string _id)
        {
            string sqlQuery = "DELETE FROM dra_product_color WHERE _id='" + _id + "'";
            dh.ExcuteNonQuery(sqlQuery);
        }

        public List<ProductColor> toList(DataTable dt)
        {
            List<ProductColor> list = new List<ProductColor>();
            foreach (DataRow row in dt.Rows)
            {
                List<ProductSize> ListSize = sizeDAO.GetProductSizesByProductColor(row[0].ToString().Trim());
                ProductColor prod = new ProductColor(row[0].ToString(), row[1].ToString(),
                    row[2].ToString(), row[3].ToString(), row[4].ToString(), ListSize);
                list.Add(prod);
            }
            return list;
        }
    }
}
