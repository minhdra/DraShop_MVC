using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DraShopOBJ;

namespace DraShopDAO
{
    public class CartDAO: ICartDAO
    {
        DataHelper dh = new DataHelper();
        public Cart GetCart(string customer_id)
        {
            if (customer_id == "") return null;
            string sqlQuery = "select * from dra_cart where customer_id='" + customer_id + "'";
            DataTable dt = dh.GetDataTable(sqlQuery);
            if (dt.Rows.Count <= 0) return null;
            ICartDetailDAO cartDetailDAO = new CartDetailDAO();
            Cart cart = new Cart();
            foreach(DataRow row in dt.Rows)
            {
                List<CartDetail> ListCartDetail = cartDetailDAO.GetCartDetails(row[0].ToString().Trim());
                cart._id = row[0].ToString().Trim();
                cart.customer_id = row[1].ToString().Trim();
                cart.ListCartDetail = ListCartDetail;
            }
            return cart;
        }

        public void CreateCart(Cart cart)
        {
            string sqlQuery = "insert into dra_cart " +
                "values ('" + cart._id + "', '" + cart.customer_id + "')";
            dh.ExcuteNonQuery(sqlQuery);
        }

        public List<Cart> GetCarts()
        {
            string sqlQuery = "select * from dra_cart";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return toList(dt);
        }

        public List<Cart> toList(DataTable dt)
        {
            List<Cart> list = new List<Cart>();
            ICartDetailDAO cartDetailDAO = new CartDetailDAO();
            foreach (DataRow row in dt.Rows)
            {
                List<CartDetail> ListCartDetail = cartDetailDAO.GetCartDetails(row[1].ToString().Trim());
                Cart cart = new Cart(row[0].ToString(), row[1].ToString(),
                    ListCartDetail);
                list.Add(cart);
            }
            return list;
        }
    }
}
