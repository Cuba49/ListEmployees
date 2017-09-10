using ListEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ListEmployee.Loader
{
    public class LoadXmlFromFile
    {
        HttpPostedFileBase upload;
        ApplicationContext db;


        public void Load()
        {
            foreach (XElement level1Element in XElement.Load(upload.InputStream).Elements("Unit"))
            {
                Unit unit = new Unit();
                unit.UnitName = level1Element.Attribute("Title").Value;
                unit.Employees = new List<Employee>();
                foreach (XElement level2Element in level1Element.Elements("Employee"))
                {
                    Employee employee = new Employee();
                    employee.EmployeeName = level2Element.Attribute("Name").Value;
                    employee.EmployeePosition = level2Element.Attribute("Position").Value;
                    employee.EmployeeHireDate = Convert.ToDateTime(level2Element.Attribute("HireDate").Value);
                    employee.Unit = unit;
                    unit.Employees.Add(employee);
                    db.Employees.Add(employee);

                }
                db.Units.Add(unit);
                db.SaveChanges();

            }
        }

        public LoadXmlFromFile(HttpPostedFileBase upload, ApplicationContext db)
        {
            this.upload = upload;
            this.db = db;
        }
    }
}