using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DraShopBUS;
using DraShopOBJ;

namespace DraShop_MVC.Controllers
{
    public class CartController : Controller
    {
        IClientBUS clientBUS = new ClientBUS();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCart(string customer_id)
        {
            Cart cart = clientBUS.GetCart(customer_id);
            return Json(new { cart }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddToCart(Cart cart, CartDetail cartDetail)
        {
            clientBUS.CreateCart(cart, cartDetail);
        }

        [HttpPost]
        public void UpdateCartDetail(CartDetail cartDetail)
        {
            clientBUS.UpdateCartDetail(cartDetail);
        }

        [HttpPost]
        public void DeleteCartDetail(string _id)
        {
            clientBUS.DeleteCartDetail(_id);
        }
    }
}