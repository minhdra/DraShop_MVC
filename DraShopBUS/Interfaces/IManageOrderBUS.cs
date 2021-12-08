using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopBUS
{
    public interface IManageOrderBUS
    {
        Order GetOrder(string customer_id);
        List<Order> GetOrders(string customer_id);
        List<OrderDetail> GetOrderDetails(string order_id);
        Order AddOrder(Order order, List<OrderDetail> orderDetails);
        void UpdateOrder(Order order);
        void DeleteOrder(string _id);
        void AddOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(string _id);
    }
}
