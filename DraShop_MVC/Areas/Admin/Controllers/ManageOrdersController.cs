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
    public class ManageOrdersController : Controller
    {
        IManageOrderBUS manageOrderBUS = new ManageOrderBUS();

        // GET: Admin/ManageOrders
        public ActionResult ManageOrdersView()
        {
            return View();
        }

        public ActionResult ManageOrderDetailsView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetOrders()
        {
            List<Order> orders = manageOrderBUS.GetOrders("");
            return Json(orders, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetOrderDetails(string order_id)
        {
            List<OrderDetail> orderDetails = manageOrderBUS.GetOrderDetails(order_id);
            return Json(orderDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateOrder(Order order)
        {
            manageOrderBUS.UpdateOrder(order);
            
        }

        [HttpPost]
        public void UpdateOrderDetail(OrderDetail detail)
        {
            manageOrderBUS.UpdateOrderDetail(detail);
        }
    }
}