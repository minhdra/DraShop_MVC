using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public interface IProductSizeDAO
    {
        List<ProductSize> GetProductSizesByProductColor(string product_color_id);
        void AddProductSize(ProductSize size);
        void UpdateProductSize(ProductSize size);
        void DeleteProductSize(string size);
    }
}
