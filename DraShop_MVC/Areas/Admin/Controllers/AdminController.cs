using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DraShopOBJ;
using DraShopBUS;
using Newtonsoft.Json;
using System.Web.Security;

namespace DraShop_MVC.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        IManageAdminBUS manageAdminBUS = new ManageAdminBUS();

        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAdmin(string username, string password)
        {
            DraShopOBJ.Admin admin = manageAdminBUS.GetAdmin(username, password);
            
            if(admin != null)
            {
                FormsAuthentication.SetAuthCookie(admin.username, false);
            }
            return Json(admin, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}