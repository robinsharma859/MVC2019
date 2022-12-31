using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC2019.Controllers
{
    public class ProductController : Controller
    {
       
        private DemoEntities demoEntities;
        // GET: Product
        [Route("Product/ProductDetails/{categoryID}")]
        public ActionResult ProductDetails(int categoryID)
        {
            demoEntities = new DemoEntities();
            List<Product> records = demoEntities.Products.ToList().Where(x=>x.CategoryID == categoryID).ToList();
            ViewBag.ProductData = records;
            return View(ViewBag.ProductData);
        }
    }
}