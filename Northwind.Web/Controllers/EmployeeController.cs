using Microsoft.AspNetCore.Mvc;
using  Northwind.Web.Models;
using Northwind.Web.Repository;
using System;
using System.Collections.Generic;
namespace Northwind.Web.Controllers
{
    public class EmployeeController : Controller
    {

        //menggunakan dapedency injection
        private readonly IEmployee _IEmployee;

        public EmployeeController(IEmployee iEmployee)
        {
            _IEmployee = iEmployee;
        }

        public IActionResult ListEmployees()
        {
            //Mengisi list dengan cara ini adallah salah, karena bisa merusak mvc
           /* var listOfEmployee = new List<Employee>()
            {
                new Employee{Id=1001,Name="Yuli Ayu",BirthDate=new DateTime(1998,07,24)},
                new Employee{Id=1002,Name="Thalia Indah",BirthDate=new DateTime(1998,09,14)},
                new Employee{Id=1003,Name="Hevanders Silaban",BirthDate=new DateTime(1997,12,19)}
            };*/
            return View("ListEmployees", _IEmployee.GetAll());
        }
    }
}
