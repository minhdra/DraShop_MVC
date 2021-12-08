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

        public ProductList GetProductsByCategoryAndGender(string category_id, int gender, int pageIndex, int pageSize, string name)
        {
            return proDAO.GetProductsByCategoryAndGender(category_id, gender, pageIndex, pageSize, name);
        }

        public List<Product> GetProductsHotMen()
        {
            return proDAO.GetProductsHotMen();
        }

        public List<Product> GetProductsHotWomen()
        {
            return proDAO.GetProductsHotWomen();
        }

        public List<Product> GetProductsBestSelling(int length)
        {
            return proDAO.GetProductsBestSelling(length);
        }

        public List<Product> GetProductsNew(int length)
        {
            return proDAO.GetProductsNew(length);
        }

        public List<Product> GetProductsBestDiscount(int length)
        {
            return proDAO.GetProductsBestDiscount(length);
        }


        public Product GetProduct(string id)
        {
            return proDAO.GetProduct(id);
        }

        public Product AddProduct(Product product)
        {
            product._id = GenerateProductId();
            proDAO.AddProduct(product);
            return product;
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
        public ProductColor AddProductColor(ProductColor color)
        {
            color._id = GenerateColorId();
            colorDAO.AddProductColor(color);
            return color;
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
        public List<ProductSize> GetProductSizes()
        {
            return sizeDAO.GetProductSizes();
        }
        public ProductSize AddProductSize(ProductSize size)
        {
            size._id = GenerateSizeId();
            sizeDAO.AddProductSize(size);
            return size;
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
        public List<ProductPrice> GetProductPrices()
        {
            return priceDAO.GetProductPrices();
        }
        public ProductPrice AddProductPrice(ProductPrice price)
        {
            price._id = GeneratePriceId();
            priceDAO.AddProductPrice(price);
            return price;
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
            string id = "" + ran.Next(1, 1000);
            List<Product> list = GetProducts();
            if(list.Count > 0)
            {
                for(int i = 0; i < list.Count; i++)
                {
                    if(list[i]._id.Trim() == id)
                    {
                        id = "" + ran.Next(1, 1000);
                        i = -1;
                    }
                }
            }
            return id;
        }

        public string GenerateColorId()
        {
            Random ran = new Random();
            string id = ran.Next(1, 1000) + "";
            List<ProductColor> list = GetProductColors();
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i]._id.Trim() == id)
                    {
                        id = "" + ran.Next(1, 1000);
                        i = -1;
                    }
                }
            }
            return id;
        }

        public string GenerateSizeId()
        {
            Random ran = new Random();
            string id = ran.Next(1, 1000) + "";
            List<ProductSize> list = GetProductSizes();
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i]._id.Trim() == id)
                    {
                        id = "" + ran.Next(1, 1000);
                        i = -1;
                    }
                }
            }
            return id;
        }

        public string GeneratePriceId()
        {
            Random ran = new Random();
            string id = ran.Next(1, 1000) + "";
            List<ProductPrice> list = GetProductPrices();
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i]._id.Trim() == id)
                    {
                        id = "" + ran.Next(1, 1000);
                        i = -1;
                    }
                }
            }
            return id;
        }
    }
}
