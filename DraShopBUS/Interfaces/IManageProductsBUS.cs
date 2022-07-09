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
        Product AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(string product_id);
        // Color
        List<ProductColor> GetProductColors();
        ProductColor AddProductColor(ProductColor color);
        void UpdateProductColor(ProductColor color);
        void DeleteProductColor(string _id);
        // Size
        List<ProductSize> GetProductSizes();
        ProductSize AddProductSize(ProductSize size);
        void UpdateProductSize(ProductSize size);
        void DeleteProductSize(string _id);
        // Price
        List<ProductPrice> GetProductPrices();
        ProductPrice AddProductPrice(ProductPrice price);
        void UpdateProductPrice(ProductPrice price);
        void DeleteProductPrice(string _id);
    }
}
