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
        public void _CreateEmployee_ShouldReturnNewEmployee()
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
        public void A_InsertNewEmployee_ShouldReturnBoolResult()
        {
            _repository
                .Setup(method => method.InsertEmployeeAsync(_director))
                .ReturnsAsync(_director)
                .Verifiable();

            var resultTrue = _service.InsertEmployeeAsync(_director).Result;
            var resultFalse = _service.InsertEmployeeAsync(null).Result;

            _repository.Verify(method => method.GetEmployeesListAsync(), Times.Once);
            _repository.Verify(method => method.InsertEmployeeAsync(_director), Times.Once);
            _repository.Verify(method => method.InsertEmployeeAsync(null), Times.Never);

            Assert.IsTrue(resultTrue);
            Assert.IsFalse(resultFalse);

        }

        [Test]
        //Добавление существующего сотрудника
        public void B_InsertMoreThenOneEmployee_ShouldReturnFalse()
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

        [Test]
        //Удаление существующего сотрудника
        public void C_DeleteEmployeeByName_ShouldReturnBoolResults()
        {
            _repository
                .Setup(method => method.DeleteEmployeeByNameAsync("Алексей"))
                .ReturnsAsync(new StaffEmployee("Алексей", "Алексеев", Departments.IT, 100_000));

            var resultTrue = _service.DeleteEmployeeByNameAsync("Алексей").Result;

            var resultFalseEmpty = _service.DeleteEmployeeByNameAsync("").Result;

            var resultFalseNull = _service.DeleteEmployeeByNameAsync(null).Result;

            _repository.Verify(method => method.DeleteEmployeeByNameAsync("Алексей"), Times.Once);
            _repository.Verify(method => method.DeleteEmployeeByNameAsync(""), Times.Never);
            _repository.Verify(method => method.DeleteEmployeeByNameAsync(" "), Times.Never);
            _repository.Verify(method => method.DeleteEmployeeByNameAsync(null), Times.Never);

            Assert.IsTrue(resultTrue);
            Assert.IsFalse(resultFalseEmpty);
            Assert.IsFalse(resultFalseNull);

        }

        [Test]
        //Удаление несуществующего сотрудника
        public void D_DeleteEmployeeByName_ShouldReturnFalse()
        {
            _repository
                .Setup(method => method.DeleteEmployeeByNameAsync("Евгений"))
                .ReturnsAsync(() => null);

            var resultFalse = _service.DeleteEmployeeByNameAsync("Евгений").Result;

            _repository.Verify(method => method.DeleteEmployeeByNameAsync("Евгений"), Times.Once);
            Assert.IsFalse(resultFalse);
        }

        
        //Получение списка сотрудников
        [Test]
        public void E_GetAllEmployeesList_ShouldReturnEmployeesList()
        {
            var expactedListCount = 3;

            var expactedDirectorEmployee = new DirectorEmployee("Александр", "Александров", Departments.Managment, 200_000);
            var expactedStaffEmployee = new StaffEmployee("Алексей", "Алексеев", Departments.IT, 100_000);
            var expactedFreelancerEmployee = new FreeLancerEmployee("Николай", "Николаев", Departments.IT, 1_000);

            var result = _service.GetAllEmployeeAsync().Result.ToList();

            _repository.Verify(method => method.GetEmployeesListAsync(), Times.Once);

            Assert.AreEqual(expactedListCount, result.Count());
            Assert.AreEqual(expactedStaffEmployee, result[0]);
            Assert.AreEqual(expactedFreelancerEmployee, result[1]);
            Assert.AreEqual(expactedDirectorEmployee, result[2]);


        }

    }
}
