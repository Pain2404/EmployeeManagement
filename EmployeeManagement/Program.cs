using EmployeeManagement;
using EmployeeManagement.ObjectModel;
using EmployeeManagement.Repository;

var repository = new EmployeeRepository(new ApplicationContext());
//repository.AddEmployee(new Employee { Name = "Test",Position = "Test", Salary = 10002, HireDate= DateTime.Now});

var employees = repository.SearchEmployees("Yar",null,1100);
foreach (var employee in employees)
{
    Console.WriteLine(employee.Name + " " + employee.Position + " " + employee.Salary + " " + employee.HireDate);
}


