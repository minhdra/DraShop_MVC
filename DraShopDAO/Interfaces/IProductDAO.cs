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
        List<Product> SearchProducts(string category_id, string keyword);
        List<Product> GetProductsByCategory(string category_id);
        ProductList GetProductsByCategoryAndGender(string category_id, int gender, int pageIndex, int pageSize, string name);
        ProductList GetProductsByCategory(string category_id, int pageIndex, int pageSize, string name);
        List<Product> GetProductsHotMen();
        List<Product> GetProductsHotWomen();
        List<Product> GetProductsBestSelling(int length);
        List<Product> GetProductsNew(int length);
        List<Product> GetProductsBestDiscount(int length);
        Product GetProduct(string id);
        ProductList ProductsList(string category_id, int pageIndex, int pageSize, string productName);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(string product_id);
    }
}
