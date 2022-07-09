using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DraShopBUS;
using DraShopOBJ;

namespace DraShop_MVC.Controllers
{
    public class ProductController : Controller
    {
        IManageProductsBUS prodBUS = new ManageProductBUS();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchResult()
        {
            return View();
        }

        public JsonResult GetProducts()
        {
            List<Product> list = prodBUS.GetProducts();
            return Json(new { products = list }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchProducts(string category_id, string keyword)
        {
            List<Product> list = prodBUS.SearchProducts(category_id, keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductsBestSelling(int length)
        {
            List<Product> list = prodBUS.GetProductsBestSelling(length);
            return Json(new { products = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductsNew(int length)
        {
            List<Product> list = prodBUS.GetProductsNew(length);
            return Json(new { products = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductsBestDiscount(int length)
        {
            List<Product> list = prodBUS.GetProductsBestDiscount(length);
            return Json(new { products = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductsHotMen()
        {
            List<Product> list = prodBUS.GetProductsHotMen();
            return Json(new { products = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductsHotWomen()
        {
            List<Product> list = prodBUS.GetProductsHotWomen();
            return Json(new { products = list }, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetProductsByCategory(string category_id)
        //{
        //    List<Product> list = prodBUS.GetProductsByCategory(category_id);
        //    return Json(new { products = list }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetProductsByCategory(string category_id, int pageIndex, int pageSize, string name)
        {
            ProductList productList = prodBUS.GetProductsByCategory(category_id, pageIndex, pageSize, name);
            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductsByCategoryAndGender(string category_id, int gender, int pageIndex, int pageSize, string name)
        {
            ProductList productList = prodBUS.GetProductsByCategoryAndGender(category_id, gender, pageIndex, pageSize, name);
            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductsPagination(string category_id, int pageIndex, int pageSize, string name)
        {
            ProductList productList = prodBUS.ProductsList(category_id, pageIndex, pageSize, name);
            return Json(productList, JsonRequestBehavior.AllowGet);
        }
    }
}