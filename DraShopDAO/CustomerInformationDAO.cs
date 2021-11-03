using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;
using System.Data;

namespace DraShopDAO
{
    public class CustomerInformationDAO: ICustomerInformationDAO
    {
        DataHelper dh = new DataHelper();

        public CustomerInformation GetCustomerInformation(string customer_id)
        {
            customer_id = customer_id.Trim();
            string querySql =
                customer_id == "" ? "" : "select * from dra_customer_information " +
                "where customer_id='" + customer_id + "'";
            DataTable dt = dh.GetDataTable(querySql);
            return dt.Rows.Count > 0 ? toList(dt)[0] : null;
        }

        public List<CustomerInformation> toList(DataTable dt)
        {
            List<CustomerInformation> list = new List<CustomerInformation>();
            foreach (DataRow row in dt.Rows)
            {
                CustomerInformation info = new CustomerInformation(
                    row[0].ToString(), row[1].ToString(),
                    row[2].ToString(), row[3].ToString(), 
                    row[4].ToString());
                list.Add(info);
            }
            return list;
        }
    }
}
