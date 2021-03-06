using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DraShopOBJ;
using DraShopBUS;
using Newtonsoft.Json;

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

        public ActionResult OrderCustomer()
        {
            return View();
        }

        public ActionResult ProfileCustomer()
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
        public JsonResult GetCustomer(string username, string password, bool remember)
        {
            Customer customer = client.Login(username, password);
            if (customer != null && !remember) customer.password = "";
            if (customer == null)
            {
                Session["login"] = "0";
                Session["customer"] = "";
            }
            else
            {
                Session["login"] = "1";
                Session["customer"] = JsonConvert.SerializeObject(customer);
                Session.Timeout = 360;
            }
            return Json(new { login = Session["login"], customer = customer }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Logout ()
        {
            Session.Remove("login");
            Session.Remove("customer");
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDeliveryAddresses(string customer_id)
        {
            List<DeliveryAddress> deliveryAddresses = client.GetDeliveryAddresses(customer_id);
            return Json(deliveryAddresses, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            DeliveryAddress item = client.CreateDeliveryAddress(deliveryAddress);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            client.UpdateDeliveryAddress(deliveryAddress);
        }

        [HttpPost]
        public void DeleteDeliveryAddress(string _id)
        {
            client.DeleteDeliveryAddress(_id);
        }
    }
}