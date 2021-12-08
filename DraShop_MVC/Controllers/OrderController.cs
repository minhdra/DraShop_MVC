using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DraShopOBJ;
using DraShopBUS;

namespace DraShop_MVC.Controllers
{
    public class OrderController : Controller
    {
        IManageOrderBUS manageOrderBUS = new ManageOrderBUS();

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetOrder(string customer_id)
        {
            List<Order> orders = manageOrderBUS.GetOrders(customer_id.Trim());
            return Json(orders, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddOrder(Order order, List<OrderDetail> orderDetails)
        {
            Order o = manageOrderBUS.AddOrder(order, orderDetails);
            return Json(new { order = o }, JsonRequestBehavior.AllowGet);
        }


    }
}