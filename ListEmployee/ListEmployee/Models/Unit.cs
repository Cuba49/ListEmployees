using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListEmployee.Models
{
    public class Unit
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}