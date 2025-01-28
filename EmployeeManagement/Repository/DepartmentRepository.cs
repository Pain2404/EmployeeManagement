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
        //public void UpdateDepartment(int Id , Department department)
        //{
        //    var departmentToUpdate = GetDepartmentById(Id);
        //    if (departmentToUpdate != null)
        //    {

        //    }
        //}
    }
}
