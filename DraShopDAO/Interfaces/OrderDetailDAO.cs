using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public interface IOrderDetailDAO
    {
        List<OrderDetail> GetOrderDetails(string order_id);
        void AddOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(string _id);
        void DeleteOrderDetailByOrderId(string order_id);
    }
}
