using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.ObjectModel
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Range(0,double.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        public string Position { get; set; }

    }
}
