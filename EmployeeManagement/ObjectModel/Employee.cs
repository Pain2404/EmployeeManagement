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
        public Position? Position { get; set; }

        public int PositionId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;

        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
    }
    public enum EmployeeStatus
    {
        Active = 1,
        Inactive = 0
    }
}
