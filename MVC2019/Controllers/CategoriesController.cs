using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC2019.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
           var demoEntities = new DemoEntities();
            List<Category> records = demoEntities.Categories.ToList();
            return View(records);
        }
    }
}