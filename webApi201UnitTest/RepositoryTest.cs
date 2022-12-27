using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using webApi201.Entity;
using webApi201.Repository;

namespace webApi201UnitTest
{
    [TestClass]
    public class RepositoryTest
    {
        private readonly EmployeeDbContext _dbContextMock;
        private readonly Mock<DbSet<Employee>> _dbSetMock;

        public RepositoryTest()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString()
                );
            _dbContextMock = new EmployeeDbContext(dbOptions.Options);
            _dbSetMock = new Mock<DbSet<Employee>>();
        }

        [TestMethod]
        public void GetEmployeesReturnsListOfEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee
                    {
                        Id = Guid.Parse("a5f31665-a3c6-4a12-8248-cd45ff493bb8"),
                        Name = "Doe",
                        Designation = "Manager",
                        DOB = DateTime.Now,
                        DOJ = DateTime.Now,
                        Address = "Karnataka"
                    },
                new Employee
                    {
                        Id = Guid.Parse("feda54a0-8e67-4993-9ca2-ea39e3923e4d"),
                        Name = "Doe",
                        Designation = "Manager",
                        DOB = DateTime.Now,
                        DOJ = DateTime.Now,
                        Address = "Karnataka"
                    },
                new Employee
                    {
                        Id = Guid.Parse("ff77d9ce-92a2-440d-8098-9ada6225dd04"),
                        Name = "Doe",
                        Designation = "Manager",
                        DOB = DateTime.Now,
                        DOJ = DateTime.Now,
                        Address = "Karnataka"
                    }
            };
            _dbContextMock.Employees.AddRange(employees);
            _dbContextMock.SaveChanges();

            var repo = new EmployeeRepository(_dbContextMock);

            var employeesReturnd = repo.GetEmployees().Result as List<Employee>;

            Assert.AreEqual(employees.Count, employeesReturnd.Count);

        }
        [TestMethod]
        public void GetEmployeeByIdReturnsEmployee()
        {
            var id = Guid.NewGuid();
            _dbContextMock.Employees.Add(new Employee
            {
                Id = id,
                Name = "Doe",
                Designation = "Manager",
                DOB = DateTime.Now,
                DOJ = DateTime.Now,
                Address = "Karnataka"
            });
            _dbContextMock.SaveChanges();

            var repo = new EmployeeRepository(_dbContextMock);

            var employee = repo.GetEmployeeId(id);

            Assert.IsNotNull(employee);
        }

        [TestMethod]

        public void AddEmployeeAddsNewEmployee()
        {
            var repo = new EmployeeRepository(_dbContextMock);
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = "Doe",
                Designation = "Manager",
                DOB = DateTime.Now,
                DOJ = DateTime.Now,
                Address = "Karnataka"
            };

            var result = repo.AddEmployee(employee).Result;
            var rowsEffected = repo.SaveChanges().Result;

            var employees = _dbContextMock.Employees.ToListAsync().Result;
            Assert.AreEqual(result, employee);
            Assert.AreEqual(1, employees.Count);
            Assert.AreEqual(1, rowsEffected);
        }
        [TestMethod]
        public void DeleteEmployeeDeletesExistingEmployee()
        {
            var repo = new EmployeeRepository(_dbContextMock);
            var id = Guid.NewGuid();
            var employee = new Employee
            {
                Id = id,
                Name = "Doe",
                Designation = "Manager",
                DOB = DateTime.Now,
                DOJ = DateTime.Now,
                Address = "Karnataka"
            };
            _dbContextMock.Employees.Add(employee);
            _dbContextMock.SaveChanges();

            var result = repo.DeleteEmployee(employee);
            var rowsEffected = repo.SaveChanges().Result;

            var employees = _dbContextMock.Employees.ToListAsync().Result;
            Assert.AreEqual(result, employee);
            Assert.AreEqual(0, employees.Count);
            Assert.AreEqual(1, rowsEffected);
        }
    }
}

