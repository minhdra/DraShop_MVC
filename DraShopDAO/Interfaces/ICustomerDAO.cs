using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraShopOBJ;

namespace DraShopDAO
{
    public interface ICustomerDAO
    {
        void Register(Customer customer);
        Customer Login(string username, string password);
        List<Customer> GetCustomers();
    }
}
