using System.Linq;
using System.Web.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.Data;
using System;
using System.Globalization;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeDbContext db = new EmployeeDbContext();

        // GET: Employee
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employee/GetEmployees
        public JsonResult GetEmployees()
        {
            var employees = db.Employees.ToList();
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        // POST: Employee/AddEmployee
        [HttpPost]
        public JsonResult AddEmployee(Employee employee)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Empleado agregado exitosamente." });
                }

                // Uso exclusivo para la depuración!!!
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Datos incorrectos.", errors });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Se produjo un error al agregar el empleado.", details = ex.Message });
            }
        }

        // POST: Employee/UpdateEmployee
        [HttpPost]
        public JsonResult UpdateEmployee(Employee employee)
        {
            var existingEmployee = db.Employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existingEmployee != null && ModelState.IsValid)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Position = employee.Position;
                existingEmployee.Office = employee.Office;
                existingEmployee.Salary = employee.Salary;
                db.SaveChanges();
                return Json(new { success = true, message = "Empleado actualizado exitosamente." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Datos inválidos.", errors });
        }

        // POST: Employee/DeleteEmployee
        [HttpPost]
        public JsonResult DeleteEmployee(int id)
        {
            var employee = db.Employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();

                // Verifica si no quedan empleados en la base de datos
                if (!db.Employees.Any())
                {
                    // Reinicia el ID de autoincremento
                    var resetSql = "DBCC CHECKIDENT ('Employees', RESEED, 0)";
                    db.Database.ExecuteSqlCommand(resetSql);
                }

                return Json(new { success = true, message = "Empleado eliminado exitosamente." });
            }
            return Json(new { success = false, message = "Empleado no encontrado." });
        }
    }
}
