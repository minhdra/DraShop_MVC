using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DraShopBUS;
using DraShopOBJ;

namespace DraShop_MVC.Areas.Admin.Controllers
{
    public class ManageProductsController : Controller
    {
        IManageProductsBUS proBUS = new ManageProductBUS();

        // GET: Admin/ManageProducts
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult ProductDetails()
        {
            return View("ProductDetails");
        }

        [HttpGet]
        public JsonResult Upload(string category_id)
        {
            List<string> l = new List<string>();
            string path = Server.MapPath("~/images/product-detail/" + category_id + "/");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            foreach(string key in Request.Files)
            {
                HttpPostedFileBase pf = Request.Files[key];
                pf.SaveAs(path + pf.FileName);
                l.Add(pf.FileName);
            }
            return Json(l, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddProduct(Product product)
        {
            Product p = proBUS.AddProduct(product);
            return Json(p, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateProduct(Product product)
        {
            proBUS.UpdateProduct(product);
        }

        [HttpPost]
        public void DeleteProduct(string _id)
        {
            proBUS.DeleteProduct(_id);
        }

        // Color
        [HttpGet]
        public JsonResult GetProductColors()
        {
            List<ProductColor> list = proBUS.GetProductColors();
            return Json(new { colors = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddProductColor(ProductColor color)
        {
            ProductColor c = proBUS.AddProductColor(color);
            return Json(c, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateProductColor(ProductColor color)
        {
            proBUS.UpdateProductColor(color);
        }

        [HttpPost]
        public void DeleteProductColor(string _id)
        {
            proBUS.DeleteProductColor(_id);
        }

        // Price
        [HttpGet]
        public JsonResult GetProductPrices()
        {
            List<ProductPrice> list = proBUS.GetProductPrices();
            return Json(new { listPrices = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddPrice(ProductPrice price)
        {
            ProductPrice p = proBUS.AddProductPrice(price);
            return Json(p, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdatePrice(ProductPrice price)
        {
            proBUS.UpdateProductPrice(price);
        }

        [HttpPost]
        public void DeletePrice(string _id)
        {
            proBUS.DeleteProductPrice(_id);
        }

        // Size
        [HttpGet]
        public JsonResult GetProductSizes()
        {
            List<ProductSize> list = proBUS.GetProductSizes();
            return Json(new { listSizes = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddSize(ProductSize size)
        {
            ProductSize s = proBUS.AddProductSize(size);
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateSize(ProductSize size)
        {
            proBUS.UpdateProductSize(size);
        }

        [HttpPost]
        public void DeleteSize(string _id)
        {
            proBUS.DeleteProductSize(_id);
        }
    }
}