using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
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
    class DirectorEmployeeTests
    {
        private readonly BaseEmployee _director;
        private IEmployeeService _service;
        private Mock<IEmployeeRepository> _repository;

        public DirectorEmployeeTests()
        {
            _director = new DirectorEmployee("Петр", "Петров", Departments.Managment, 200_000);
            
        }

        [SetUp]
        public void TestsSetup()
        {
            _repository = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_repository.Object);

            _repository
                .Setup(method => method.GetEmployeesListAsync())
                .ReturnsAsync(() => new List<BaseEmployee> {new StaffEmployee("Алексей","Алексеев",Departments.IT,100_000),
                                                            new FreeLancerEmployee("Николай","Николаев",Departments.IT, 1_000),
                                                            new DirectorEmployee("Александр","Александров",Departments.Managment,200_000)
                                                            })
                .Verifiable();
        }

        [Test]
        public void CreateEmployee_ShouldReturnNewEmployee()
        {
            var directorNew = new DirectorEmployee("Александр", "Александров", Departments.Managment, 200_000);
            var directorGet = new DirectorEmployee(Guid.NewGuid(), "Александр", "Александров", Departments.Managment, 200_000);
            
            var staffNew = new StaffEmployee("Алексей", "Алексеев", Departments.IT, 100_000);
            var staffGet = new StaffEmployee(Guid.NewGuid(), "Алексей", "Алексеев", Departments.IT, 100_000);
            
            var staffNewNotIT = new StaffEmployee("Алексей", "Алексеев", Departments.IT, Positions.None, 100_000);
            var staffGetNotIT = new StaffEmployee(Guid.NewGuid(), "Алексей", "Алексеев", Departments.IT, Positions.None, 100_000);
            
            var freelancerNew = new FreeLancerEmployee("Николай", "Николаев", Departments.IT, 1_000);
            var freelancerGet = new FreeLancerEmployee(Guid.NewGuid(), "Николай", "Николаев", Departments.IT, 1_000);

            Assert.AreEqual(Positions.Director, directorNew.Positions);
            Assert.AreEqual(Positions.Director, directorGet.Positions);
            Assert.AreEqual(typeof(DirectorEmployee), directorNew.GetType());
            Assert.AreEqual(typeof(DirectorEmployee), directorGet.GetType());

            Assert.AreEqual(Positions.Developer, staffNew.Positions);
            Assert.AreEqual(Positions.Developer, staffGet.Positions);
            Assert.AreEqual(typeof(StaffEmployee), staffGet.GetType());
            Assert.AreEqual(typeof(StaffEmployee), staffNew.GetType());

            Assert.AreEqual(Positions.None, staffNewNotIT.Positions);
            Assert.AreEqual(Positions.None, staffGetNotIT.Positions);
            Assert.AreEqual(typeof(StaffEmployee), staffGetNotIT.GetType());
            Assert.AreEqual(typeof(StaffEmployee), staffNewNotIT.GetType());

            Assert.AreEqual(Positions.Freelance, freelancerNew.Positions);
            Assert.AreEqual(Positions.Freelance, freelancerGet.Positions);
            Assert.AreEqual(typeof(FreeLancerEmployee), freelancerNew.GetType());
            Assert.AreEqual(typeof(FreeLancerEmployee), freelancerGet.GetType());


        }

        [Test]
        //Добавление нового сотрудника
        public void A_InsertNewDirector_ShouldReturnTrue()
        {
            _repository
                .Setup(method => method.InsertEmployeeAsync(_director))
                .ReturnsAsync(_director)
                .Verifiable();

            var result = _service.InsertEmployeeAsync(_director).Result;

            _repository.Verify(method => method.GetEmployeesListAsync(), Times.Once);
            _repository.Verify(method => method.InsertEmployeeAsync(_director), Times.Once);

            Assert.IsTrue(result);

        }

        [Test]
        //Добавление существующего сотрудника
        public void B_InsertMoreThenOneNewDirector_ShouldReturnTrue()
        {
            _repository
                .Setup(method => method.GetEmployeesListAsync())
                .ReturnsAsync(() => new List<BaseEmployee> { _director });
            _repository
                .Setup(method => method.InsertEmployeeAsync(_director))
                .ReturnsAsync(_director)
                .Verifiable();

            var result = _service.InsertEmployeeAsync(_director).Result;

            _repository.Verify(method => method.GetEmployeesListAsync(), Times.Once);
            _repository.Verify(method => method.InsertEmployeeAsync(_director), Times.Never);

            Assert.IsFalse(result);

        }
    }
}
