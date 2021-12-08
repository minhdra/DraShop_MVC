using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public interface IOrderDAO
    {
        Order GetOrder(string customer_id);
        List<Order> GetOrders(string customer_id);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(string _id);
    }
}
