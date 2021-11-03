using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DraShopOBJ;
using DraShopBUS;

namespace DraShop_MVC.Controllers
{
    public class CategoryController : Controller
    {
        ManageCategoriesBUS cateBUS = new ManageCategoriesBUS();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCategories()
        {
            List<Category> list = cateBUS.GetCategories();
            return Json(new { categories = list }, JsonRequestBehavior.AllowGet);
        }
    }
}