using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public interface ICartDAO
    {
        List<Cart> GetCarts();
        Cart GetCart(string customer_id);
        void CreateCart(Cart cart);
    }
}
