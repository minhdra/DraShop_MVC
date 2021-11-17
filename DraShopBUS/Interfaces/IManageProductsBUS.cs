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
        Product GetProduct(string id);
        ProductList ProductsList(string category_id, int pageIndex, int pageSize, string productName);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(string product_id);
        // Color
        List<ProductColor> GetProductColors();
        void AddProductColor(ProductColor color);
        void UpdateProductColor(ProductColor color);
        void DeleteProductColor(string _id);
        // Size
        void AddProductSize(ProductSize size);
        void UpdateProductSize(ProductSize size);
        void DeleteProductSize(string _id);
        // Price
        void AddProductPrice(ProductPrice price);
        void UpdateProductPrice(ProductPrice price);
        void DeleteProductPrice(string _id);

        string GenerateProductId();
    }
}
