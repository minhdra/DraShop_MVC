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
        public void AddProduct(Product product)
        {
            proBUS.AddProduct(product);
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
        public void AddProductColor(ProductColor color)
        {
            proBUS.AddProductColor(color);
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
        [HttpPost]
        public void AddProductPrice(ProductPrice price)
        {
            proBUS.AddProductPrice(price);
        }

        [HttpPost]
        public void UpdateProductPrice(ProductPrice price)
        {
            proBUS.UpdateProductPrice(price);
        }

        [HttpPost]
        public void DeleteProductPrice(string _id)
        {
            proBUS.DeleteProductPrice(_id);
        }

        // Size
        [HttpPost]
        public void AddSize(ProductSize size)
        {
            proBUS.AddProductSize(size);
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