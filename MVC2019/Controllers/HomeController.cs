using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinessLayer;
namespace MVC2019.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private  UserRegistrationBussinessService userRegistrationBussinessService = null;
        Dictionary<string, object> userRecords = null;
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
            return RedirectToAction("Index", "Home");
            
        }
       // [ActionName("GetEditDetails")]
        [Route("EditDetails/{userRegistrationId}")]
        [HttpGet]
        public ActionResult EditDetails(int userRegistrationId)
        {
            ViewBag.Message = "Edit User Registration Details";
            IEnumerable<UserRegistration> data = userRegistrationBussinessService.UserRegistationListWherCondition(userRegistrationId);
            UserRegistration userRegistration = new UserRegistration();
            userRegistration = data.FirstOrDefault();
            return View(userRegistration);

        }

       [ActionName("EditDetails")]
       [HttpPost]
        //[Route("EditDetails")]
        public ActionResult EditDetails(UserRegistration registration)
        {
            if (ModelState.IsValid)
            {
                userRecords = new Dictionary<string, object>();
                UserRegistration userRegistration = new UserRegistration();

                UpdateModel<UserRegistration>(registration);

                userRecords.Add("@Name", registration.Name);
                userRecords.Add("@Address", registration.Address);
                userRecords.Add("@Country", registration.Country);
                userRecords.Add("@PinCode", registration.PinCode);
                userRecords.Add("@Age", registration.Age);
                userRecords.Add("@Phone", registration.Phone);
                userRecords.Add("@Gender", registration.Gender);
                userRecords.Add("@@UserRegistrationId", registration.RegistrationID);
                userRegistrationBussinessService.AddUserRegistration("spUpdateEmployee", true, userRecords);
               
            }
            return RedirectToAction("Index", "Home");

        }
    }
}