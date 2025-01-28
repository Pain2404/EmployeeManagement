using EmployeeManagement;
using EmployeeManagement.ObjectModel;
using EmployeeManagement.Repository;
using Microsoft.EntityFrameworkCore;


var repository = new DepartmentRepository(new ApplicationContext());
repository.AddDepartment(new Department() { Name = "IT" , DateEstablished = DateTime.Now, Description = "IT"});
var deaprtment = repository.GetDepartmentById(4);
Console.WriteLine(deaprtment.Name);
//repository.AddEmployee(new Employee { Name = "Test",Position = "Test", Salary = 10002, HireDate= DateTime.Now});

//var employees = repository.SearchEmployees("Yar",null,1100);
//foreach (var employee in employees)
//{
//    Console.WriteLine(employee.Name + " " + employee.Position + " " + employee.Salary + " " + employee.HireDate);
//}


