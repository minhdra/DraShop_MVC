using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public class AdminDAO: IAdminDAO
    {
        DataHelper dh = new DataHelper();

        public List<Admin> GetAdmins()
        {
            string querySql = "select * from dra_admin";
            DataTable dt = dh.GetDataTable(querySql);
            return dt.Rows.Count > 0 ? toList(dt) : null;
        }

        public Admin GetAdmin(string username, string password)
        {
            string sqlQuery = "select * from dra_admin " +
                "where username='" + username + "' and password='" + password + "'";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return dt.Rows.Count > 0 ? toList(dt)[0] : null;
        }

        public List<Admin> toList(DataTable dt)
        {
            List<Admin> list = new List<Admin>();
            IAdminInformationDAO infoDAO = new AdminInformationDAO();
            foreach (DataRow row in dt.Rows)
            {
                AdminInformation info = infoDAO.GetAdminInformation(row[0].ToString());
                int status = row[4].ToString() == "False" ? 0 : 1;
                Admin admin = new Admin(
                    row[0].ToString(), row[1].ToString(),
                    row[2].ToString(), row[3].ToString(),
                    status, info);
                list.Add(admin);
            }
            return list;
        }
    }
}
