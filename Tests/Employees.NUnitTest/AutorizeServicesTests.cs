using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services.AutorizeService;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services.EmployeeService;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Employees.NUnitTest
{
    class AutorizeServicesTests
    {

        private IAutorize _service;
        private Mock<IEmployeeRepository> _repository;


        [SetUp]
        public void TestsSetup()
        {
            _repository = new Mock<IEmployeeRepository>();
            _service = new AutorizeService(_repository.Object);
        }

        [Test]
        public void AutorizatedUser_ShoultReturnAutorizeOrNull()
        {
            string name = "Алексей";
            Guid id = Guid.NewGuid();

            _repository
                .Setup(method => method.GetEmployeeByNameAsync(name))
                .ReturnsAsync(new StaffEmployee(id, "Алексей", "Алексеев", Departments.IT, 100_000));

            var user = _service.AutentificatedUser(name).Result;  
            var userNull = _service.AutentificatedUser("").Result;
            var userNullName = _service.AutentificatedUser("Test").Result;

            var result = _service.GetAuthorization(user);
            var resultNull = _service.GetAuthorization(userNull);

            Assert.IsNotNull(user);
            Assert.IsNull(userNull);
            Assert.IsNull(userNullName);

            Assert.IsNull(resultNull);
            Assert.AreEqual(id, result.UserId);
            Assert.AreEqual(Role.Developer, result.UserRole);




        }

    }
}
