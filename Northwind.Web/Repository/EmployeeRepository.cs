using Northwind.Web.Models;
using System;
using System.Collections.Generic;

namespace Northwind.Web.Repository
{
    public class EmployeeRepository : IEmployee
    {
        public List<Employee> GetAll()
        {
            var listOfEmployee = new List<Employee>()
            {
                new Employee{Id=1001,Name="Yuli Ayu",BirthDate=new DateTime(1998,07,24)},
                new Employee{Id=1002,Name="Thalia Indah",BirthDate=new DateTime(1998,09,14)},
                new Employee{Id=1003,Name="Hevanders Silaban",BirthDate=new DateTime(1997,12,19)},
                new Employee{Id=1004,Name="Victor Purba",BirthDate=new DateTime(1994,12,19)}
            };

            return listOfEmployee;
            //throw new System.NotImplementedException();
        }
    }
}
