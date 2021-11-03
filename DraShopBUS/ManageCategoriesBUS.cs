using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;
using DraShopDAO;

namespace DraShopBUS
{
    public class ManageCategoriesBUS : IManageCategoriesBUS
    {
        ICategoryDAO cateDAO = new CategoryDAO();
        public List<Category> GetCategories()
        {
            return cateDAO.GetCategories();
        }
    }
}
