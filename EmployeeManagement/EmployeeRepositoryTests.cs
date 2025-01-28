using EmployeeManagement.ObjectModel;
using EmployeeManagement.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmployeeManagement.Tests
{
    public class EmployeeRepositoryTests
    {
        private readonly EmployeeRepository _repository;
        private readonly ApplicationContext _context;
        public EmployeeRepositoryTests() 
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
     .UseInMemoryDatabase("TestDatabase")
     .Options;

             _context = new ApplicationContext(options);
            _repository = new EmployeeRepository(_context);
        }
        [Fact]
        public void AddEmployee_ShouldAddEmployeeToDataBase_WhenEmployeeIsValid()
        {
            //var employee = new Employee() { Name = "Yaroslav", new Position{ Name = "Developer", new Department{ } }, Salary = 1000 };
            //_repository.AddEmployee(employee);
            //Assert.Single(_context.Employees);
            //var addedEmployee = _context.Employees.First();
            //Assert.Equal("Yaroslav", addedEmployee.Name);
        }
        [Fact]
        public void AddEmployee_ShouldNotAddEmployeeToDataBase_WhenEmployeeIsNotValid()
        {
            var employee = new Employee() {Name = null,Position = null , Salary = -100};
            Assert.Throws<DbUpdateException>(() => _repository.AddEmployee(employee));
        }
        [Fact]
        public void UpdateEmployee_ShouldUpdateEmployee_WhenEmployeeExists()
        {
            //_repository.AddEmployee(new Employee() { Id = 1, Name = "Yaroslav", Position = "Dev" });
            var result = _repository.UpdateEmployee(1,new Employee() { Name = "YaroslavUpdated"});
            Assert.True(result);
            Assert.Equal("YaroslavUpdated", _repository.GetById(1).Name);
        }
        [Fact]
        public void DeleteEmployee_ShouldDeleteEmployeeFromDataBase_WhenEmployeeExists()
        {
            //_repository.AddEmployee(new Employee() {Id = 1, Name = "Yaroslav" , Position = "Dev"});
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
            //_repository.AddEmployee(new Employee() {Id = 1 ,Name = "Yaroslav" , Position = "Dev"} );
            var result = _repository.SearchByName("Yaroslav");
            Assert.NotEmpty(result);            
        }
        [Fact]
        public void SearchEmployeesByParams_ShouldReturnEmployeesFromDataBase_WhenEmployeesExist()
        {
            //_repository.AddEmployee(new Employee() { Id = 1, Name = "Yaroslav", Position = "Dev" });
            //_repository.AddEmployee(new Employee() { Id = 2, Name = "Yaroslav", Position = "User" });
            //_repository.AddEmployee(new Employee() { Id = 3, Name = "Yaroslav", Position = "Manager" });
            var result = _repository.SearchEmployees("slav","Us");
            Assert.Single(result);
        }
    }
}