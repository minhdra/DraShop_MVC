using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;
using DraShopDAO;

namespace DraShopBUS
{
    public class ClientBUS: IClientBUS
    {
        ICustomerDAO customerDAO = new CustomerDAO();

        public bool Register(Customer customer)
        {
            if (CheckUniqueUsername(customer.username)) return false;
            string id = GenerateId();
            customer._id = id;
            customer.status = 0;
            customer.information = null;
            customerDAO.Register(customer);
            return true;
        }

        public Customer Login(string username, string password)
        {
            return customerDAO.Login(username, password);
        }

        public List<Customer> GetCustomers()
        {
            return customerDAO.GetCustomers();
        }

        private string GenerateId()
        {
            Random ran = new Random();
            int number = ran.Next(1, 100);
            string id = "C" + number;
            List<Customer> list = GetCustomers();
            if (list == null) return id;
            for(int i = 0; i < list.Count; i++)
            {
                if (list[0]._id.Trim() == id)
                {
                    number = ran.Next(1, 100);
                    id = "C" + number;
                    i--;
                }
            }
            return id;
        }

        private bool CheckUniqueUsername(string username)
        {
            List<Customer> list = GetCustomers();
            if (list == null) return false;
            foreach(var item in list)
            {
                if (item.username.Trim() == username) return true;
            }
            return false;
        }
    }
}
