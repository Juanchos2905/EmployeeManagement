using EmployeeManagement.Models;
using System.Data.Entity;

namespace EmployeeManagement.Data
{
    /*Con esta clase le daremos persistencia a los datos utilizando el ORM de c#*/
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext() : base("EmployeeDbContext")
        {
        }
        //DBset!
        public DbSet<Employee> Employees { get; set; }
    }
}