using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DraShopOBJ;

namespace DraShopDAO
{
    public class CustomerDAO: ICustomerDAO
    {
        DataHelper dh = new DataHelper();

        public List<Customer> GetCustomers()
        {
            string querySql = "select * from dra_customer";
            DataTable dt = dh.GetDataTable(querySql);
            return dt.Rows.Count > 0 ? toList(dt) : null;
        }

        public void Register(Customer customer)
        {
            if(customer != null)
            {
                string sqlQuery = "exec proc_register '" +
                    "" + customer._id + "', '" +
                    "" + customer.username + "', '" +
                    "" + customer.email + "', '" +
                    "" + customer.password + "', '" +
                    "" + customer.status + "'";

                dh.ExcuteNonQuery(sqlQuery);
            }
        }

        public Customer Login(string username, string password)
        {
            string sqlQuery = "select * from dra_customer " +
                "where username='" + username + "' and password='" + password + "'";
            DataTable dt = dh.GetDataTable(sqlQuery);
            return dt.Rows.Count > 0 ? toList(dt)[0] : null;
        }

        public List<Customer> toList(DataTable dt)
        {
            List<Customer> list = new List<Customer>();
            CustomerInformationDAO infoDAO = new CustomerInformationDAO();
            foreach (DataRow row in dt.Rows)
            {
                CustomerInformation info = infoDAO.GetCustomerInformation(row[0].ToString());
                int status = row[4].ToString() == "false" ? 0 : 1;
                Customer customer = new Customer(
                    row[0].ToString(), row[1].ToString(),
                    row[2].ToString(), row[3].ToString(),
                    status,
                    info);
                list.Add(customer);
            }
            return list;
        }
    }
}
