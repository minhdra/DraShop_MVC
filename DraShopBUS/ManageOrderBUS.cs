using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;
using DraShopDAO;

namespace DraShopBUS
{
    public class ManageOrderBUS: IManageOrderBUS
    {
        IOrderDAO orderDAO = new OrderDAO();
        IOrderDetailDAO orderDetailDAO = new OrderDetailDAO();

        public List<Order> GetOrders(string customer_id)
        {
            return orderDAO.GetOrders(customer_id);
        }

        public Order GetOrder(string customer_id)
        {
            return orderDAO.GetOrder(customer_id);
        }

        public List<OrderDetail> GetOrderDetails(string order_id)
        {
            return orderDetailDAO.GetOrderDetails(order_id);
        }

        // Create order
        public Order AddOrder(Order order, List<OrderDetail> orderDetails)
        {
            string _id = GenerateOrderId();
            order._id = _id;
            order.orderDetails = null;
            orderDAO.AddOrder(order);
            for (int i = 0; i < orderDetails.Count; i++)
            {
                orderDetails[i].order_id = _id;
                AddOrderDetail(orderDetails[i]);
            }

            //if (!CheckOrder(order.customer_id))
            //{
            //    string _id = GenerateOrderId();
            //    order._id = _id;
            //    order.orderDetails = null;
            //    orderDAO.AddOrder(order);
            //    for(int i = 0; i < orderDetails.Count; i++)
            //    {
            //        orderDetails[i].order_id = _id;
            //        AddOrderDetail(orderDetails[i]);
            //    }
            //}
            //else
            //{
            //    Order o = GetOrder(order.customer_id);
            //    for(int i = 0; i < orderDetails.Count; i++)
            //    {
            //        OrderDetail od = CheckOrderDetail(orderDetails[i].product_id, o._id);
            //        orderDetails[i].order_id = o._id;
            //        if (od == null)
            //        {
            //            AddOrderDetail(orderDetails[i]);
            //        }
            //        //else
            //        //{
            //        //    orderDetails[i]._id = od._id;
            //        //    UpdateOrderDetail(orderDetails[i]);
            //        //}

            //    }
            //}

            return GetOrder(order.customer_id);
        }
        // Update order
        public void UpdateOrder(Order order)
        {
            orderDAO.UpdateOrder(order);
        }
        // Delete order
        public void DeleteOrder(string _id)
        {
            orderDAO.DeleteOrder(_id);
        }

        // Create cart detail
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            if (orderDetail == null) return;
            orderDetail._id = GenerateOrderDetailId();
            orderDetailDAO.AddOrderDetail(orderDetail);
        }
        // Update Cart Detail
        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            orderDetailDAO.UpdateOrderDetail(orderDetail);
        }
        // Delete Cart Detail
        public void DeleteOrderDetail(string _id)
        {
            orderDetailDAO.DeleteOrderDetail(_id);
        }

        public string GenerateOrderId()
        {
            Random ran = new Random();
            string id = "" + ran.Next(1, 1000);
            List<Order> list = GetOrders("");
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i]._id.Trim() == id)
                    {
                        id = "" + ran.Next(1, 1000);
                        i = -1;
                    }
                }
            }
            return id;
        }

        public string GenerateOrderDetailId()
        {
            Random ran = new Random();
            string id = "" + ran.Next(1, 1000);
            List<OrderDetail> list = GetOrderDetails("");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i]._id.Trim() == id)
                    {
                        id = "" + ran.Next(1, 1000);
                        i = -1;
                    }
                }
            }
            return id;
        }

        private bool CheckOrder(string customer_id)
        {
            Order order = GetOrder(customer_id);
            if (order == null) return false;
            return true;
        }
        private OrderDetail CheckOrderDetail(string product_id, string order_id)
        {
            List<OrderDetail> list = GetOrderDetails(order_id);
            foreach (var item in list)
            {
                if (item.product_id == product_id) return item;
            }
            return null;
        }
    }
}
