using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;
using DraShopDAO;

namespace DraShopBUS
{
    public class ManageProductBUS: IManageProductsBUS
    {
        IProductDAO proDAO = new ProductDAO();
        IProductColorDAO colorDAO = new ProductColorDAO();
        IProductPriceDAO priceDAO = new ProductPriceDAO();
        IProductSizeDAO sizeDAO = new ProductSizeDAO();
        //public ManageProductBUS(IManageProductsBUS pro)
        //{
        //    this.proDAO = pro;
        //}
        public List<Product> GetProducts()
        {
            return proDAO.GetProducts();
        }

        public List<Product> GetProductsByCategory(string category_id)
        {
            return proDAO.GetProductsByCategory(category_id);
        }

        public List<Product> GetProductsHotMen()
        {
            return proDAO.GetProductsHotMen();
        }

        public List<Product> GetProductsHotWomen()
        {
            return proDAO.GetProductsHotWomen();
        }

        public Product GetProduct(string id)
        {
            return proDAO.GetProduct(id);
        }

        public void AddProduct(Product product)
        {
            //product._id = GenerateProductId();
            proDAO.AddProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            proDAO.UpdateProduct(product);
        }

        public void DeleteProduct(string product_id)
        {
            proDAO.DeleteProduct(product_id);
        }

        public ProductList ProductsList(string category_id, int pageIndex, int pageSize, string productName)
        {
            return proDAO.ProductsList(category_id, pageIndex, pageSize, productName);
        }

        // Color
        public List<ProductColor> GetProductColors()
        {
            return colorDAO.GetProductColors();
        }
        public void AddProductColor(ProductColor color)
        {
            colorDAO.AddProductColor(color);
        }
        public void UpdateProductColor(ProductColor color)
        {
            colorDAO.UpdateProductColor(color);
        }
        public void DeleteProductColor(string _id)
        {
            colorDAO.DeleteProductColor(_id);
        }


        // Size
        public void AddProductSize(ProductSize size)
        {
            sizeDAO.AddProductSize(size);
        }
        public void UpdateProductSize(ProductSize size)
        {
            sizeDAO.UpdateProductSize(size);
        }
        public void DeleteProductSize(string _id)
        {
            sizeDAO.DeleteProductSize(_id);
        }
        // Price
        public void AddProductPrice(ProductPrice price)
        {
            priceDAO.AddProductPrice(price);
        }
        public void UpdateProductPrice(ProductPrice price)
        {
            priceDAO.UpdateProductPrice(price);
        }
        public void DeleteProductPrice(string _id)
        {
            priceDAO.DeleteProductPrice(_id);
        }

        public string GenerateProductId()
        {
            Random ran = new Random();
            string id = "P" + ran.Next(1, 100);
            List<Product> list = GetProducts();
            if(list.Count > 0)
            {
                for(int i = 0; i < list.Count; i++)
                {
                    if(list[i]._id.Trim() == id)
                    {
                        id = "P" + ran.Next(1, 100);
                        i--;
                    }
                }
            }
            return id;
        }
    }
}
