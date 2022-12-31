using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC2019.Models;
namespace MVC2019.Controllers
{
    public class EmployeeController : Controller
    {
        private  Employee Employee = null;
        private DemoEntities demoEntities;
        public ActionResult GetEmployeeDetails()
        {
            ViewBag.Message = "Retrieve the employee details";
            Employee = new Employee() { EmpID = 22, EmpName = "Robin", EmpDesignations = "Tester", Salary = 3000, EmployeeDepartment = new EmployeeDepartment() { Address = "Chandigarh", Department = "Testing Proactice" } };
            
            return View(Employee);
        }

        public ActionResult GetCustomerDetails()
        {
            demoEntities = new DemoEntities();
            var records = demoEntities.Customers.AsEnumerable();
            ViewBag.Customer = records;
            return View(ViewBag.Customer);
        }
    }
}