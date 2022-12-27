using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using webApi201.BusinessLogic;
using webApi201.Controllers;
using webApi201.Entity;
using webApi201.Model;
using webApi201.Repository;
using webApi201UnitTest.Moqrepo;

namespace webApi201UnitTest
{
    [TestClass]
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeBusiness> _business;
        private readonly MapperConfiguration _config;
        private readonly IMoqRepo _repo = new MoqRepo();
        private readonly Mapper _mapper;
        private readonly Mock<IMapper> _mapperMoq;
        private readonly Mock<ILogger<EmployeesController>> _loggerMoq;

        public EmployeeControllerTests()
        {
            _business = new Mock<IEmployeeBusiness>();
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<EmployeeModel, Employee>().ReverseMap());
            _mapper = new Mapper(config);
            _mapperMoq = new Mock<IMapper>();
            _loggerMoq = new Mock<ILogger<EmployeesController>>();
        }

        [TestMethod]
        public void GetEmployeeReturnsListOfEmployees()
        {
            var employee = _repo.GetSampleEmployee();
            _business.Setup(x => x.GetEmployees())
            .Returns(_repo.GetSampleEmployee);

            var controller = new EmployeesController(_business.Object,_loggerMoq.Object);
            var result = controller.Get().Result as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsInstanceOfType(result.Value, typeof(List<Employee>));
        }

        [TestMethod]
        public void GetEmployeeByIdReturnsOneEmployee()
        {
            var newId = Guid.Parse("a5f31665-a3c6-4a12-8248-cd45ff493bb8");
            var employee = _repo.GetSampleEmployee();
            _business.Setup(x => x.GetEmployeeById(newId))
            .Returns(_repo.GetEmployee(newId));

            var controller = new EmployeesController(_business.Object,_loggerMoq.Object);

            var actionResult = controller.Get(newId);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as Employee;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(_repo.GetEmployee(newId).Result.Id, newId);
        }

        [TestMethod]
        public void GetEmployeeByIdReturnsNotFound()
        {
            var newId = Guid.Parse("a5f31665-a3c6-4a12-8248-cd45ff476bb8");
            var employee = _repo.GetSampleEmployee();

            var controller = new EmployeesController(_business.Object,_loggerMoq.Object);

            var actionResult = controller.Get(newId);
            var result = actionResult.Result as StatusCodeResult;

            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual(_repo.GetEmployee(newId).Result, null);
        }

        [TestMethod]
        public void DeleteEmployeeByIdReturnsOk()
        {
            var newId = Guid.Parse("feda54a0-8e67-4993-9ca2-ea39e3923e4d");
            var initCount = _repo.GetCount();
            Employee employee = _repo.GetEmployee(newId).Result;
            _business.Setup(x => x.GetEmployeeById(newId))
                .Returns(_repo.GetEmployee(newId));
            _business.Setup(x => x.DeleteEmployee(employee))
                .Returns(_repo.DeleteEmployee(employee));

            var controller = new EmployeesController(_business.Object,_loggerMoq.Object);

            var actionResult = controller.Get(newId);
            var result = actionResult.Result;


            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(_repo.GetCount(), initCount - 1);
        }

        [TestMethod]
        public void DeleteEmployeeByIdReturnsNotFound()
        {
            var newId = Guid.Parse("feda54a0-8e67-4993-9ca2-ea39e3923e4c");
            var initCount = _repo.GetCount();
            Employee employee = _repo.GetEmployee(newId).Result;
            _business.Setup(x => x.GetEmployeeById(newId))
                .Returns(_repo.GetEmployee(newId));
            _business.Setup(x => x.DeleteEmployee(employee))
                .Returns(_repo.DeleteEmployee(employee));
            var controller = new EmployeesController(_business.Object,_loggerMoq.Object);

            var actionResult = controller.Get(newId);
            var result = actionResult.Result;


            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.AreEqual(_repo.GetCount(), initCount);
        }

        [TestMethod]
        public void PostEmployeeReturnsCreated()
        {
            EmployeeModel employeeModel = new EmployeeModel
            {
                Name = "Doom",
                Designation = "Manager",
                DOB = DateTime.Now,
                DOJ = DateTime.Now,
                Address = "Karnataka"
            };

            var initCount = _repo.GetCount();
            var employee = _mapper.Map<Employee>(employeeModel);
            employee.Id = new Guid();

            _business.Setup(x => x.AddEmployee(employeeModel))
                .Returns(_repo.AddEmployee(employee));

            var controller = new EmployeesController(_business.Object,_loggerMoq.Object);
            var actionResult = controller.Post(employeeModel);
            var result = actionResult.Result as ObjectResult;

            Assert.IsInstanceOfType(result, typeof(CreatedAtRouteResult));
            Assert.AreEqual(_repo.GetCount(), initCount + 1);
        }

        [TestMethod]
        public void PostEmployeeReturnsBadRequest()
        {
            EmployeeModel employeeModel = new EmployeeModel
            {
                Name = null,
                Designation = null,
                DOB = DateTime.Now,
                DOJ = DateTime.Now,
                Address = null
            };

            var initCount = _repo.GetCount();
            var employee = _mapper.Map<Employee>(employeeModel);
            employee.Id = new Guid();

            var controller = new EmployeesController(_business.Object,_loggerMoq.Object);
            var actionResult = controller.Post(null);
            var result = actionResult.Result as BadRequestResult;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(_repo.GetCount(), initCount);
        }



    }

}
