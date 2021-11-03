using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DraShop_MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult getMenu()
        {
            return PartialView("_Menu");
        } 
    }
}