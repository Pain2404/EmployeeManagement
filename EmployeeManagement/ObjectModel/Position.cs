using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.ObjectModel
{
    public class Position
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public Department? Department { get; set; }

        public int? DepartmentId { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();

        public PositionStatus PositionStatus { get; set; } = PositionStatus.Active;
        

    }
    public enum PositionStatus
    {
        Active = 1,
        Inactive = 0,
    }
}
