using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public class OrderDAO: IOrderDAO
    {
        DataHelper dh = new DataHelper();
        public Order GetOrder(string customer_id)
        {
            string sqlQuery = "select * from dra_order where customer_id='" + customer_id + "'";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return ToList(dt).Count > 0 ? ToList(dt)[0] : null;
        }

        public List<Order> GetOrders(string customer_id)
        {
            string sqlQuery = customer_id != "" ? 
                "select * from dra_order where customer_id='" + customer_id + "'" : 
                "select * from dra_order";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return ToList(dt).Count > 0 ? ToList(dt) : null;
        }

        public void AddOrder(Order order)
        {
            dh.StoreReader("AddOrder", order._id, DateTime.Parse(order.date_create), order.address, order.total,
                order.status, order.customer_id);
        }

        public void UpdateOrder(Order order)
        {
            dh.StoreReader("UpdateOrder", order._id, DateTime.Parse(order.date_create), order.address, order.total,
                order.status, order.customer_id);
        }

        public void DeleteOrder(string _id)
        {
            IOrderDetailDAO orderDetailDAO = new OrderDetailDAO();
            orderDetailDAO.DeleteOrderDetailByOrderId(_id);
            string sqlQuery = "delete from dra_order where _id='" + _id + "'";
            dh.ExcuteNonQuery(sqlQuery);
        }

        public List<Order> ToList(DataTable dt)
        {
            List<Order> list = new List<Order>();
            IOrderDetailDAO orderDetailDAO = new OrderDetailDAO();
            foreach(DataRow row in dt.Rows)
            {
                List<OrderDetail> orderDetails = orderDetailDAO.GetOrderDetails(row[0].ToString());
                Order order = new Order(row[0].ToString(), row[1].ToString(),
                    row[2].ToString(), double.Parse(row[3].ToString()),
                    int.Parse(row[4].ToString()), row[5].ToString(),
                    orderDetails);
                list.Add(order);
            }

            return list;
        }
    }
}
