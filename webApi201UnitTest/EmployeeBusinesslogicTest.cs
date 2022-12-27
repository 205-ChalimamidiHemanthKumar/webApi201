using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using webApi201.BusinessLogic;
using webApi201.Entity;
using webApi201.Model;
using webApi201.Repository;
using webApi201UnitTest.Moqrepo;

namespace webApi201UnitTest
{
    [TestClass]
    public class EmployeeBusinesslogicTest
    {
        private readonly MoqRepo _mockRepo;
        private readonly Mock<IEmployeeRepository> _repo;
        private readonly Mapper _mapper;
        private readonly Mock<IMapper> _mapperMoq;

        public EmployeeBusinesslogicTest()
        {
            _mockRepo = new MoqRepo();
            _repo = new Mock<IEmployeeRepository>();
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<EmployeeModel, Employee>().ReverseMap());
            _mapper = new Mapper(config);
            _mapperMoq = new Mock<IMapper>();
        }

        [TestMethod]
        public void GetEmployeesReturnsListOfEmployees()
        {
            var employee = _mockRepo.GetSampleEmployee();
            _repo.Setup(x => x.GetEmployees())
            .Returns(_mockRepo.GetSampleEmployee);

            var business = new EmployeeBusiness(_repo.Object, _mapperMoq.Object);
            var result = business.GetEmployees().Result ;

            Assert.IsInstanceOfType(result, typeof(List<Employee>));
        }

        [TestMethod]
        public void GetEmployeesByIdReturnsEmployee()
        {
            var newId = Guid.Parse("a5f31665-a3c6-4a12-8248-cd45ff493bb8");
            var employee = _mockRepo.GetSampleEmployee();
            _repo.Setup(x => x.GetEmployeeId(newId))
            .Returns(_mockRepo.GetEmployee(newId));

            var business = new EmployeeBusiness(_repo.Object, _mapperMoq.Object);
            var result = business.GetEmployeeById(newId).Result;

            Assert.IsInstanceOfType(result, typeof(Employee));
        }
        [TestMethod]
        public void AddEmployeeReturnsEmployee()
        {
            EmployeeModel employeeModel = new EmployeeModel
            {
                Name = "Doom",
                Designation = "Manager",
                DOB = DateTime.Now,
                DOJ = DateTime.Now,
                Address = "Karnataka"
            };
            var initalCount = _mockRepo.GetCount(); 
            Employee employee = _mapper.Map<Employee>(employeeModel);
            _mapperMoq.Setup(s => s.Map<Employee>(employeeModel))
                .Returns(employee);
            _repo.Setup(s => s.AddEmployee(employee))
                .Returns(_mockRepo.AddEmployee(employee));
            _repo.Setup(s => s.SaveChanges())
                .Returns(_mockRepo.SaveChanges());

            var business = new EmployeeBusiness(_repo.Object, _mapperMoq.Object);
            var result = business.AddEmployee(employeeModel).Result;

            Assert.IsInstanceOfType(result, typeof(Employee));
            Assert.AreEqual(initalCount + 1, _mockRepo.GetCount());

        }
        [TestMethod]
        public void UpdateEmployeeReturnsUpdatedEmployee()
        {
            EmployeeModel employeeModel = new EmployeeModel
            {
                Id = Guid.Parse("a5f31665-a3c6-4a12-8248-cd45ff493bb8"),
                Name = "Max",
                Designation = "Manager",
                DOB = DateTime.Now,
                DOJ = DateTime.Now,
                Address = "Karnataka"
            };
            Employee employee = _mockRepo.GetEmployee(Guid.Parse("a5f31665-a3c6-4a12-8248-cd45ff493bb8")).Result;
            var originalName = employee.Name;
            _mapperMoq.Setup(s => s.Map(employeeModel, employee))
                .Returns(_mapper.Map(employeeModel,employee));
            _repo.Setup(s => s.SaveChanges())
                .Returns(_mockRepo.SaveChanges());

            var business = new EmployeeBusiness(_repo.Object, _mapperMoq.Object);
            var result = business.UpdateEmployee(employeeModel,employee).Result;

            Assert.AreNotEqual(result.Name, originalName);
        }

        [TestMethod]
        public void DeleteEmployeeRemovesEmployee()
        {
            Employee employee = new Employee
            {
                Id = Guid.Parse("a5f31665-a3c6-4a12-8248-cd45ff493bb8"),
                Name = "Doe",
                Designation = "Manager",
                DOB = DateTime.Now,
                DOJ = DateTime.Now,
                Address = "Karnataka"
            };
            var intialCount = _mockRepo.GetCount();
            _repo.Setup(s => s.DeleteEmployee(employee))
                .Returns(_mockRepo.DeleteEmployee(employee).Result);
            _repo.Setup(s => s.SaveChanges())
                .Returns(_mockRepo.SaveChanges());

            var business = new EmployeeBusiness(_repo.Object, _mapperMoq.Object);
            var result = business.DeleteEmployee(employee).Result;

            Assert.IsInstanceOfType(result, typeof(Employee));
            Assert.AreEqual(intialCount - 1, _mockRepo.GetCount());
        }
    }
}
