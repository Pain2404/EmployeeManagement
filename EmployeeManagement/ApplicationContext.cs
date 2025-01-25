using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EmployeeManagement
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                   @"Server=DESKTOP-3LBG17T;Database=EmployeeManagement;Trusted_Connection=True;Encrypt=False;");

        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<Employee>()
                    .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);
            foreach (var entry in entries)
            {
                var employee = entry.Entity;
                if (employee.Salary > 10000)
                {
                    throw new ArgumentException("Salary cannot exceed 10.000.");
                }
                if (employee.HireDate > DateTime.Now)
                {
                    throw new ArgumentException("HireDate cannot be in the future");
                }
            }
            return base.SaveChanges();
        }
    }
}
