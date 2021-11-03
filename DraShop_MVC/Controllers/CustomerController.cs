using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DraShopOBJ;
using DraShopBUS;

namespace DraShop_MVC.Controllers
{
    public class CustomerController : Controller
    {
        ClientBUS client = new ClientBUS();

        // GET: Customer
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SignUp(string username, string email, string password)
        {
            Customer customer = new Customer();
            customer.username = username;
            customer.email = email;
            customer.password = password;
            bool status = client.Register(customer);
            if(status)
                return Json(new { customer = customer}, JsonRequestBehavior.AllowGet);
            return Json(new { customer = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCustomer(string username, string password)
        {
            Customer customer = client.Login(username, password);
            return Json(new { customer = customer }, JsonRequestBehavior.AllowGet);
        }
    }
}