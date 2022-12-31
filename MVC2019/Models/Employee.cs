using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC2019.Models
{
    public class Employee
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmpDesignations { get; set; }

        public int Salary { get; set; }

        public EmployeeDepartment EmployeeDepartment { get; set; }
    }

    public class EmployeeDepartment
    {
        public string Department { get; set; }
        public string Address { get; set; }
    }
}