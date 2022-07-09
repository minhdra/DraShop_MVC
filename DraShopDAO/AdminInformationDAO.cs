using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public class AdminInformationDAO: IAdminInformationDAO
    {
        DataHelper dh = new DataHelper();

        public AdminInformation GetAdminInformation(string admin_id)
        {
            string sqlQuery =
                "select * from dra_admin_information where admin_id = '" + admin_id + "'";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return dt.Rows.Count > 0 ? toList(dt)[0] : null;
        }

        public List<AdminInformation> toList(DataTable dt)
        {
            List<AdminInformation> list = new List<AdminInformation>();
            foreach (DataRow row in dt.Rows)
            {
                AdminInformation prod = new AdminInformation(
                    row[0].ToString(), row[1].ToString(),
                    row[2].ToString(), row[3].ToString(),
                    row[4].ToString(), row[5].ToString(),
                    row[6].ToString(), row[7].ToString()
                );
                list.Add(prod);
            }
            return list;
        }

    }
}
