using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DraShopBUS;
using DraShopOBJ;

namespace DraShop_MVC.Controllers
{
    public class DetailController : Controller
    {
        IManageProductsBUS prodBUS = new ManageProductBUS();


        public ActionResult Index(string id)
        {
            
            return View();
        }
        // GET: Detail
        public ActionResult ProductDetail()
        {
            return View();
        }

        public JsonResult GetProduct(string id)
        {
            Product product = prodBUS.GetProduct(id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }
    }
}