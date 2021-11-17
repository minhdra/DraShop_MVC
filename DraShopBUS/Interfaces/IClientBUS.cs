using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopBUS
{
    public interface IClientBUS
    {
        bool Register(Customer customer);
        Customer Login(string username, string password);
        List<Customer> GetCustomers();
        Cart GetCart(string customer_id);
        List<Cart> GetCarts();
        void CreateCart(Cart cart, CartDetail cartDetail);
        void UpdateCartDetail(CartDetail cartDetail);
        void DeleteCartDetail(string _id);
    }
}
