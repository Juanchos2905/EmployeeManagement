using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Position { get; set; }

        [Required]
        [StringLength(20)]
        public string Office { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El salario debe ser un número positivo.")]
        public decimal Salary { get; set; }
    }
}