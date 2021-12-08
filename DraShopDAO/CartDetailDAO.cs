using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DraShopOBJ;
using System.Data.SqlClient;
using System.Globalization;

namespace DraShopDAO
{
    public class CartDetailDAO: ICartDetailDAO
    {
        DataHelper dh = new DataHelper();

        public List<CartDetail> GetCartDetails(string cart_id)
        {
            string sqlQuery = cart_id != "" ?
                "select * from dra_cart_detail where cart_id = '" + cart_id + "'" :
                "select * from dra_cart_detail";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return toList(dt);
        }

        public void CreateCartDetail(CartDetail cartDetail)
        {
            dh.StoreReader("createCartDetail", cartDetail._id,
                cartDetail.cart_id, cartDetail.product_id, cartDetail.quantity,
                cartDetail.price, cartDetail.status,
                cartDetail.size, cartDetail.color, cartDetail.image, cartDetail.name); 
        }

        public void UpdateCartDetail(CartDetail cartDetail)
        {
            dh.StoreReader("updateCartDetail", 
                cartDetail._id, 
                cartDetail.quantity,
                cartDetail.status,
                cartDetail.size,
                cartDetail.color,
                cartDetail.image);
        }

        public void DeleteCartDetail(string _id)
        {
            string sqlQuery = "DELETE FROM dra_cart_detail WHERE _id='" + _id +"'";
            dh.ExcuteNonQuery(sqlQuery);
        }

        public List<CartDetail> toList(DataTable dt)
        {
            List<CartDetail> list = new List<CartDetail>();
            foreach (DataRow row in dt.Rows)
            {
                int quantity = int.Parse(row[3].ToString());
                double price = double.Parse(row[4].ToString());
                int status = row[6].ToString() == "False" ? 0 : 1;
                CartDetail prod = new CartDetail(
                    row[0].ToString(), row[1].ToString(),
                    row[2].ToString(), quantity,
                    price, row[5].ToString(),
                    status, row[7].ToString(),
                    row[8].ToString(), row[9].ToString(),
                    row[10].ToString()
                );
                list.Add(prod);
            }
            return list;
        }
    }
}
