using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public class OrderDetailDAO: IOrderDetailDAO
    {
        DataHelper dh = new DataHelper();

        public List<OrderDetail> GetOrderDetails(string order_id)
        {
            string sqlQuery = order_id == "" ? "select * from dra_order_detail" :
                "select * from dra_order_detail where order_id='" + order_id + "'";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return ToList(dt);
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            dh.StoreReader("AddOrderDetail", orderDetail._id, orderDetail.order_id, orderDetail.product_id,
                orderDetail.quantity, orderDetail.price, orderDetail.image, orderDetail.color, orderDetail.size);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            dh.StoreReader("UpdateOrderDetail", orderDetail._id, orderDetail.order_id, orderDetail.product_id,
                orderDetail.quantity, orderDetail.price, orderDetail.image, orderDetail.color, 
                orderDetail.size, orderDetail.flag);
        }

        public void DeleteOrderDetail(string _id)
        {
            string sqlQuery = "delete from dra_order_detail where _id='" + _id + "'";
            dh.ExcuteNonQuery(sqlQuery);
        }

        public void DeleteOrderDetailByOrderId(string order_id)
        {
            string sqlQuery = "delete from dra_order_detail where order_id='" + order_id + "'";
            dh.ExcuteNonQuery(sqlQuery);
        }

        public List<OrderDetail> ToList(DataTable dt)
        {
            List<OrderDetail> list = new List<OrderDetail>();
            IProductDAO productDAO = new ProductDAO();
            foreach(DataRow row in dt.Rows)
            {
                Product product = productDAO.GetProduct(row[2].ToString());
                OrderDetail orderDetail = new OrderDetail(
                    row[0].ToString(), row[1].ToString(), 
                    row[2].ToString(), int.Parse(row[3].ToString()), 
                    double.Parse(row[4].ToString()), row[5].ToString(),
                    row[6].ToString(), row[7].ToString(),
                    int.Parse(row[8].ToString()),
                    product);
                list.Add(orderDetail);
            }

            return list;
        }
    }
}
