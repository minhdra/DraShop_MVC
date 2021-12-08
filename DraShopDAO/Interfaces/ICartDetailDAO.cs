using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public interface ICartDetailDAO
    {
        List<CartDetail> GetCartDetails(string cart_id);
        void CreateCartDetail(CartDetail cartDetail);
        void UpdateCartDetail(CartDetail cartDetail);
        void DeleteCartDetail(string _id);
        //void DeleteCartDetailByOrderId(string order_id);
    }
}
