using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC2019.Controllers
{
    [RoutePrefix("Customer")]
    public class CustomerController : Controller
    {
        private DemoEntities demoEntities;

        [Route("GetCustomerDetails")]
        public ActionResult CustomerInfo()
        {
            demoEntities = new DemoEntities();
            List<Customer> records = demoEntities.Customers.ToList();
            var detaisl = from m in demoEntities.Customers
                          join ord in demoEntities.Orders
                          on m.CustomerID equals ord.CustomerID
                          orderby m.CustomerID ascending
                          select m;
            ViewBag.Customer = detaisl;
            // ViewBag.Customer = records;
            return View(ViewBag.Customer);
            // return View(records);
        }
        [Route("{id}")]
        public ActionResult CustomerInfoByID(int id)
        {
            demoEntities = new DemoEntities();
            List<Customer> records = demoEntities.Customers.ToList();
            var detaisl = from m in demoEntities.Customers
                          join ord in demoEntities.Orders
                          on m.CustomerID equals ord.CustomerID
                          where m.CustomerID == id
                          select m;
            ViewBag.Customer = detaisl;
            return View(ViewBag.Customer);

        }
    }

}