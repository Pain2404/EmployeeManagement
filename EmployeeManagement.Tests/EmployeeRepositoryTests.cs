using EmployeeManagement.ObjectModel;
using EmployeeManagement.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Tests
{
    public class EmployeeRepositoryTests
    {
        private readonly EmployeeRepository _repository;
        private readonly ApplicationContext _context;
        private Employee _employee = new Employee()
        {
            Name = "Yaroslav", Id = 1,
            Position = new Position()
            {
                Name = "Developer",
                Department = new Department() { Name = "IT", Description = "IT department" }
            }
        };
        public EmployeeRepositoryTests() 
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
     .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
     .Options;

             _context = new ApplicationContext(options);
            _repository = new EmployeeRepository(_context);
        }
        [Fact]
        public void AddEmployee_ShouldAddEmployeeToDataBase_WhenEmployeeIsValid()
        {
            _repository.AddEmployee(_employee);
            Assert.Single(_context.Employees);
            var addedEmployee = _context.Employees.First();
            Assert.Equal("Yaroslav", addedEmployee.Name);
        }
        [Fact]
        public void AddEmployee_ShouldNotAddEmployeeToDataBase_WhenEmployeeIsNotValid()
        {
            var employee = _employee;
            employee.Salary = -100;
            Assert.Throws<ArgumentException>(() => _repository.AddEmployee(employee));
        }
        [Fact]
        public void UpdateEmployee_ShouldUpdateEmployee_WhenEmployeeExists()
        {
            _repository.AddEmployee(_employee);
            var result = _repository.UpdateEmployee(1, new Employee()
            {
                Name = "YaroslavUpdated",
                Position = new Position()
                {
                    Name = "Developer",
                    Department = new Department() { Name = "IT", Description = "IT department" }
                }
            });
            Assert.True(result);
            Assert.Equal("YaroslavUpdated", _repository.GetById(1).Name);
        }
        [Fact]
        public void DeleteEmployee_ShouldDeleteEmployeeFromDataBase_WhenEmployeeExists()
        {
            _repository.AddEmployee(_employee);
            var findEmployee = _repository.GetById(1);
            var result = _repository.DeleteEmployee(1);
            Assert.True(result);
        }
        [Fact]
        public void DeleteEmployee_ShouldNotDeleteEmployee_WhenEmployeeDoesNotExist()
        {
            var result = _repository.DeleteEmployee(1);
            Assert.False(result);
        }
        [Fact]
        public void SearchEmployeeByName_ShouldReturnEmployeeFromDataBase_WhenEmployeeExists()
        {
            _repository.AddEmployee(_employee);
            var result = _repository.SearchByName("Yaroslav");
            Assert.NotEmpty(result);            
        }
        [Fact]
        public void SearchEmployeesByParams_ShouldReturnEmployeesFromDataBase_WhenEmployeesExist()
        {
            _repository.AddEmployee(_employee);
            var employee2 = new Employee()
            {
                Name = "Yaroslav",
                Position = new Position()
                {
                    Name = "HR",
                    Department = new Department() { Name = "HR", Description = "HR department" }
                }
            };
            _repository.AddEmployee(employee2);
            var result = _repository.SearchEmployees("slav","loper");
            Assert.Single(result);
        }
    }
}