using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DraShopBUS;
using DraShopOBJ;

namespace DraShop_MVC.Areas.Admin.Controllers
{
    [Authorize]
    public class ManageCustomersController : Controller
    {
        IClientBUS clientBUS = new ClientBUS();

        // GET: Admin/ManageCustomer
        public ActionResult ManageCustomersView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCustomers()
        {
            List<Customer> customers = clientBUS.GetCustomers();
            return Json(customers, JsonRequestBehavior.AllowGet);
        }
    }
}