using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.ObjectModel;
using EmployeeManagement.Repository;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Tests
{
    public class DepartmentRepositoryTests
    {
        private readonly DepartmentRepository _repository;
        private readonly ApplicationContext _context;
        public DepartmentRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
     .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
     .Options;

            _context = new ApplicationContext(options);
            _repository = new DepartmentRepository(_context);
        }
        [Fact]
        public void DeleteDepartment_ShouldDeleteDepartmentAndUpdatePositions_WhenDepartmentExists()
        {
            var positionRepository = new PositionRepository(_context);

            _repository.AddDepartment(new Department() { Id = 1, Name = "IT" ,Description = "IT department"});
            positionRepository.AddPosition(new Position() { Id = 1, Name = "Developer", DepartmentId = 1 });

            var positions = _repository.GetPositionsByDepartment(1);
            var result = _repository.DeleteDepartment(1);

            Assert.True(result);
            Assert.All(positions, x => Assert.Equal(PositionStatus.Inactive, x.PositionStatus));
            Assert.All(positions, x => Assert.Equal(0, x.DepartmentId));
        }
    }
}
