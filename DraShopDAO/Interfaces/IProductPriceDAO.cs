using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public interface IProductPriceDAO
    {
        ProductPrice GetProductPrice(string product_id);
    }
}
