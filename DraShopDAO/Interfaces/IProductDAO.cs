using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public interface IProductDAO
    {
        List<Product> GetProducts();
        List<Product> GetProductsByCategory(string category_id);
        List<Product> GetProductsHotMen();
        List<Product> GetProductsHotWomen();
        Product GetProduct(string id);
        ProductList ProductsList(string category_id, int pageIndex, int pageSize, string productName);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(string product_id);
    }
}
