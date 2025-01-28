using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.ObjectModel;

namespace EmployeeManagement.Repository
{
    public class DepartmentRepository
    {
        private readonly ApplicationContext _context;
        public DepartmentRepository(ApplicationContext context)
        {
            _context = context;
        }
        public List<Department> GetAllDepartments()
        {
            return _context.Departments.ToList();
        }
        public void AddDepartment(Department department)
        {
            if (department != null)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
            }
        }
        public List<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }
        public Department GetDepartmentById(int id)
        {
            return _context.Departments.FirstOrDefault(x=>x.Id == id);
        }
        public bool DeleteDepartment(int Id)
        { 
            var department = GetDepartmentById(Id);
            if (department != null)
            {
                var positions = GetPositionsByDepartment(Id);
                foreach (var position in positions)
                {
                    position.Department = null;
                    position.DepartmentId = 0;
                    position.PositionStatus = PositionStatus.Inactive;
                }
                _context.Remove(department);
                _context.SaveChanges();
                return true;
            }
            Console.WriteLine("Department was not found");
            return false;
        }
        public List<Position> GetPositionsByDepartment(int Id)
        {
            if (Id <= 0)
            {
                Console.WriteLine("Id cannot be <= 0");
            }
            return _context.Positions.Where(x => x.DepartmentId == Id).ToList();
        }
        public bool UpdateDepartment(int Id, Department department)
        {
            var departmentToUpdate = GetDepartmentById(Id);
            if (departmentToUpdate != null)
            {
                if (!string.IsNullOrEmpty(department.Name)) departmentToUpdate.Name = department.Name;
                if(!string.IsNullOrEmpty(department.Description)) departmentToUpdate.Description = department.Description;
                if (!string.IsNullOrEmpty(department.Status.ToString())) department.Status = department.Status;

                UpdatePositionAndEmployeeStatus(_context.Positions.Where(x => x.DepartmentId == department.Id).ToList() , department.Status);

                if (department.ManagerId != null)
                {
                    departmentToUpdate.ManagerId = department.ManagerId;
                    departmentToUpdate.Manager = _context.Employees.FirstOrDefault(x => x.Id == department.ManagerId);
                }

                _context.SaveChanges();
                return true;
            }
            return false;
        }
        private void UpdatePositionAndEmployeeStatus(List<Position> positions , DepartmentStatus departmentStatus)
        {
            foreach (var position in positions)
            {
                if (departmentStatus == DepartmentStatus.Active)
                {
                    position.PositionStatus = PositionStatus.Active;
                }
                else
                {
                    position.PositionStatus = PositionStatus.Inactive;
                    var employees = _context.Employees.Where(x => x.PositionId == position.Id).ToList();
                    foreach (var employee in employees)
                    {
                        employee.Status = EmployeeStatus.Inactive;
                    }
                }
            }
        }
    }
}
