using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.ObjectModel;

namespace EmployeeManagement.Repository
{
    public class EmployeeRepository
    {
        private readonly ApplicationContext _context;
        public EmployeeRepository(ApplicationContext context) 
        {
            _context = context;
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }
        public Employee GetById(int id)
        {
            return _context.Employees.FirstOrDefault(x=>x.Id == id);
        }
        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }
        public bool DeleteEmployee(int id)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                _context.Remove(employee);
                _context.SaveChanges();
                return true;
            }
            Console.WriteLine("Employee not found");
            return false;
        }
        public IEnumerable<Employee> SearchEmployees(string name = null , string position = null , decimal? minSalary = null , decimal? maxSalary = null)
        {
            IQueryable<Employee> query = _context.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            if (!string.IsNullOrEmpty(position))
            {
                query = query.Where(x => x.Position.ToLower().Contains(position.ToLower()));
            }
            if (minSalary.HasValue)
            {
                query = query.Where(x => x.Salary > minSalary);
            }
            if (maxSalary.HasValue)
            {
                query = query.Where(x => x.Salary < maxSalary);
            }
            return query.ToList();
        }
        public bool UpdateEmployee(int id , Employee employee)
        {
            var foundEmployee = GetById(id);
            if (foundEmployee != null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(foundEmployee.Name)) employee.Name = foundEmployee.Name;
            if (foundEmployee.Salary > 0) employee.Salary = foundEmployee.Salary;
            if (!string.IsNullOrEmpty(foundEmployee.Position)) employee.Position = foundEmployee.Position;
            if (foundEmployee.HireDate != default) employee.HireDate = foundEmployee.HireDate;
            _context.SaveChanges();
            return true;

        }
        public IEnumerable<Employee> SearchByName(string name)
        {
            return _context.Employees.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
        }
        public IEnumerable<Employee> SearchBySalary(decimal minSalary, decimal maxSalary)
        {
            return _context.Employees.Where(x => x.Salary >= minSalary && x.Salary <= maxSalary).ToList();
        }
        public IEnumerable<Employee> SearchByHireDate(DateTime? startDate , DateTime? endDate)
        {
            return _context.Employees.Where(x => (x.HireDate >= startDate || !startDate.HasValue) &&
                    (x.HireDate <= endDate || !endDate.HasValue)).ToList();
        }
        public IEnumerable<Employee> GetSortedEmployees(string sortBy , bool ascending = true)
        {
            IQueryable<Employee> employees = _context.Employees;
            switch (sortBy.ToLower())
            {
                case "name": 
                    employees = ascending ? employees.OrderBy(x => x.Name) : employees.OrderByDescending(x => x.Name);
                    break;
                case "salary":
                    employees = ascending ? employees.OrderBy(x=>x.Salary) : employees.OrderByDescending (x => x.Salary);
                    break;
                case "hiredate":
                    employees = ascending ? employees.OrderBy(x => x.HireDate) : employees.OrderByDescending(x => x.HireDate);
                    break;
                case "position":
                    employees = ascending ? employees.OrderBy(x => x.Position) : employees.OrderByDescending(x => x.Position);
                    break;
                default: 
                    employees = ascending ? employees.OrderBy(x => x.Name) : employees.OrderByDescending(x => x.Name);
                    break;
            }
            return employees.ToList();
        }
        public IEnumerable<Employee> GetPagedEmployees(int pageNumber , int size)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            return _context.Employees.Skip((pageNumber - 1) * size).Take(size).ToList();
        }
    }
}
