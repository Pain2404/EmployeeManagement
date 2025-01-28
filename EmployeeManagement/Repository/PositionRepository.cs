using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.ObjectModel;

namespace EmployeeManagement.Repository
{
    public class PositionRepository
    {
        private readonly ApplicationContext _context;
        public PositionRepository(ApplicationContext context) { _context = context; }

        public List<Position> GetAllPositions()
        {
            return _context.Positions.ToList();
        }
        public void AddPosition(Position position)
        {
            if (position != null)
            {
                _context.Positions.Add(position);
                _context.SaveChanges();
            }
        }
        public List<Employee> GetEmployeesByPosition(int positionId)
        {
            if (positionId <= 0)
            {
                Console.WriteLine("Id cannot be <= 0");
            }
            return _context.Employees.Where(x => x.PositionId == positionId).ToList();
            
        }
        public Position GetPositionById(int positionId)
        {
            return _context.Positions.FirstOrDefault(x=>x.Id == positionId);
        }
        public bool DeletePositionById(int Id)
        {
            var position = GetPositionById(Id);
            if (position != null)
            {
                var emloyees = GetEmployeesByPosition(Id);
                foreach (var employee in emloyees)
                {
                    employee.Position = null;
                    employee.PositionId = 0;
                    employee.Status = EmployeeStatus.Inactive;
                }
                _context.Remove(position);
                _context.SaveChanges();
                return true;
            }
            Console.WriteLine("Position was not found");
            return false;
        }
        public bool UpdatePosition(int Id , Position position)
        {
            var positionToUpdate = GetPositionById(Id);
            if (positionToUpdate != null)
            {
                if (!string.IsNullOrEmpty(position.Name)) positionToUpdate.Name = position.Name;
                if (!string.IsNullOrEmpty(position.PositionStatus.ToString())) positionToUpdate.PositionStatus = position.PositionStatus;
                if (position.DepartmentId != null)
                {
                    positionToUpdate.DepartmentId = position.DepartmentId;
                    positionToUpdate.Department = _context.Departments.FirstOrDefault(x => x.Id == position.DepartmentId);
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
