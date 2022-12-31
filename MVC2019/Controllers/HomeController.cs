using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinessLayer;
namespace MVC2019.Controllers
{
    public class HomeController : Controller
    {
        private  UserRegistrationBussinessService userRegistrationBussinessService = null;
        public HomeController()
        {
            userRegistrationBussinessService = new UserRegistrationBussinessService();
        }
        public ActionResult Index()
        {
         IEnumerable<UserRegistration> data =  userRegistrationBussinessService.UserRegistationList();
   
           //var data = userRegistrationBussinessService.UserRegistationList();
            ViewBag.Data = data;
            return View(ViewBag.Data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Dictionary<string, object> userRecords = new Dictionary<string, object>();
            if (ModelState.IsValid)
            {
                //foreach (string key in formCollection.AllKeys)
                //{
                //    Response.Write("Key=" + key + " ");
                //    Response.Write("Value=" + formCollection[key]);
                //    Response.Write("<br/>");
                //}
                UserRegistration employee = new UserRegistration();
                employee.Name = formCollection["Name"];
                employee.Address = formCollection["Address"];
                employee.Country = formCollection["Country"];
                employee.PinCode = formCollection["Country"];
                employee.Age = Convert.ToInt32(formCollection["Age"]);
                employee.Phone = formCollection["Phone"];
                employee.Gender = formCollection["Gender"];

                userRecords.Add("@Name", employee.Name);
                userRecords.Add("@Address", employee.Address);
                userRecords.Add("@Country", employee.Country);
                userRecords.Add("@PinCode", employee.PinCode);
                userRecords.Add("@Age", employee.Age);
                userRecords.Add("@Phone", employee.Phone);
                userRecords.Add("@Gender", employee.Gender);

                //DemoEntities demoEntities = new DemoEntities();
                //demoEntities.UserRegistrations.Add(employee);
                ////_dbContext.Employees.Add(employee);
                //demoEntities.SaveChanges();
              
                //RedirectToAction("Index");
            }
            userRegistrationBussinessService.AddUserRegistration("spEmployeee", true, userRecords);
            RedirectToAction("Index", "Home");
            return View();
        }
    }
}