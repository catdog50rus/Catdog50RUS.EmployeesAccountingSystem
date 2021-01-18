using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services.EmployeeService;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Employees.NUnitTest
{
    class FreelancerEmployeeServiceTests
    {
        private readonly BaseEmployee _freelancer;
        private IEmployeeService _service;
        private Mock<IEmployeeRepository> _repository;
        private Autorize _autorize;

        public FreelancerEmployeeServiceTests()
        {
            var id = Guid.NewGuid();
            _freelancer = new FreeLancerEmployee(id, "Петр", "Петров", Departments.IT, 1_000);
            _autorize = new Autorize(Role.Freelancer, id);
        }

        [SetUp]
        public void TestsSetup()
        {
            _repository = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_repository.Object, _autorize);
            _repository
                .Setup(method => method.GetEmployeesListAsync())
                .ReturnsAsync(() => new List<BaseEmployee> {new StaffEmployee("Алексей","Алексеев",Departments.IT,100_000),
                                                            new FreeLancerEmployee("Николай","Николаев",Departments.IT, 1_000),
                                                            new DirectorEmployee("Александр","Александров",Departments.Managment,200_000)
                                                            })
                .Verifiable();
        }

        //Добавление нового сотрудника
        [Test]
        public void A_InsertNewEmployee_ShouldReturnFalse()
        {
            _repository
                .Setup(method => method.InsertEmployeeAsync(_freelancer))
                .ReturnsAsync(_freelancer)
                .Verifiable();

            var resultFalseAutorize = _service.InsertEmployeeAsync(_freelancer).Result;

            _repository.Verify(method => method.InsertEmployeeAsync(_freelancer), Times.Never);

            Assert.IsFalse(resultFalseAutorize);

        }

        //Удаление существующего сотрудника
        [Test]
        public void B_DeleteEmployeeByName_ShouldReturnFalse()
        {
            _repository
                .Setup(method => method.DeleteEmployeeByNameAsync("Алексей"))
                .ReturnsAsync(new FreeLancerEmployee("Алексей", "Алексеев", Departments.IT, 100_000));

            var resultFalse = _service.DeleteEmployeeByNameAsync("Алексей").Result;

            _repository.Verify(method => method.DeleteEmployeeByNameAsync("Алексей"), Times.Never);

            Assert.IsFalse(resultFalse);

        }

        //Получение списка сотрудников
        [Test]
        public void C_GetAllEmployeesList_ShouldReturnNull()
        {
            var result = _service.GetAllEmployeeAsync().Result;

            _repository.Verify(method => method.GetEmployeesListAsync(), Times.Never);

            Assert.IsNull(result);
        }

        //Получение сотрудника по имени
        [Test]
        public void D_GetEmployeeByName_ShouldReturnNewEmployee()
        {
            string name = "Алексей";
            _repository
                .Setup(method => method.GetEmployeeByNameAsync(name))
                .ReturnsAsync(() => new FreeLancerEmployee("Алексей", "Алексеев", Departments.IT, 100_000))
                .Verifiable();

            var result = _service.GetEmployeeByNameAsync(name).Result;

            _repository.Verify(method => method.GetEmployeeByNameAsync(name), Times.Never);

            Assert.IsNull(result);
        }


        public void E_CreateCompliteTask_ShouldReturnComplitedTask()
        {

        }
    }
}
