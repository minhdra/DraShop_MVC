using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopDAO;
using DraShopOBJ;

namespace DraShopBUS
{
    public class ManageAdminBUS: IManageAdminBUS
    {
        IAdminDAO adminDAO = new AdminDAO();

        public Admin GetAdmin(string username, string password)
        {
            return adminDAO.GetAdmin(username, password);
        } 

        public List<Admin> GetAdmins()
        {
            return adminDAO.GetAdmins();
        }
    }
}
