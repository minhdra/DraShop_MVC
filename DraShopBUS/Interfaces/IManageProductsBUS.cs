using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopDAO;
using DraShopOBJ;

namespace DraShopBUS
{
    public interface IManageProductsBUS
    {
        List<Product> GetProducts();
        List<Product> GetProductsByCategory(string category_id);
        List<Product> GetProductsHotMen();
        List<Product> GetProductsHotWomen();
        List<Product> GetProduct(string id);
        ProductList ProductsList(string category_id, int pageIndex, int pageSize, string productName);
    }
}
