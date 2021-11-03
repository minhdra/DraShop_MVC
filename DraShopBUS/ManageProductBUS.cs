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

        public List<Product> GetProduct(string id)
        {
            return proDAO.GetProduct(id);
        }

        public ProductList ProductsList(string category_id, int pageIndex, int pageSize, string productName)
        {
            return proDAO.ProductsList(category_id, pageIndex, pageSize, productName);
        }
    }
}
