using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DraShopOBJ;
using DraShopBUS;

namespace DraShop_MVC.Areas.Admin.Controllers
{
    public class ManageCategoriesController : Controller
    {
        IManageCategoriesBUS categoriesBUS = new ManageCategoriesBUS();

        // GET: Admin/ManageCategories
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCategories()
        {
            List<Category> categories = categoriesBUS.GetCategories();
            return Json(new { categories = categories }, JsonRequestBehavior.AllowGet);
        }
    }
}