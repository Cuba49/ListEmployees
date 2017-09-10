using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListEmployee.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePosition { get; set; }
        public DateTime EmployeeHireDate { get; set; }
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }
    }
}