using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.ObjectModel
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public List<Position> Positions { get; set; } = new List<Position>();

        public Employee? Manager { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateEstablished { get; set; }
        public DepartmentStatus Status { get; set; } = DepartmentStatus.Active;
    }
    public enum DepartmentStatus
    {
        Active = 1,
        Archived = 0
    }
}
