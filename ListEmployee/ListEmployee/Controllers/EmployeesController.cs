using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ListEmployee.Models;
using ListEmployee.Loader;

namespace ListEmployee.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationContext dt = new ApplicationContext();


        public ActionResult Index(int? team)
        {
            IQueryable<Employee> players = dt.Employees.Include(p => p.Unit);
            if (team != null && team != 0)
            {
                players = players.Where(p => p.UnitId == team);
            }

            List<Unit> teams = dt.Units.ToList();

            teams.Insert(0, new Unit { UnitName = "All", UnitId = 0 });

            EmployeesListViewModel plvm = new EmployeesListViewModel
            {
                Employees = players.ToList(),
                Units = new SelectList(teams, "UnitId", "UnitName")
            };
            return View(plvm);
        }

        [HttpPost]
        public ActionResult Clear(HttpPostedFileBase upload)
        {
            dt.Employees.RemoveRange(dt.Employees);
            dt.Units.RemoveRange(dt.Units);
            dt.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            LoadXmlFromFile loader = new LoadXmlFromFile(upload, dt);
            try
            {
                loader.Load();
            }
            catch
            {
                TempData["AlertMessage"] = "Ошибка чтения XML-файла. Пожалуйста, убедитесь, что файл был выбран и формат данных в нем совпадает с требованниями";
            }


            IQueryable<Employee> players = dt.Employees.Include(p => p.Unit);

            List<Unit> teams = dt.Units.ToList();

            teams.Insert(0, new Unit { UnitName = "All", UnitId = 0 });
            EmployeesListViewModel plvm = new EmployeesListViewModel
            {
                Employees = players.ToList(),
                Units = new SelectList(teams, "UnitId", "UnitName")

            };
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = dt.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dt.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
