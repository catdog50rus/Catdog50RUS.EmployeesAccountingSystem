﻿using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
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
        private IEmployeeService _serviceEmployee;
        private ICompletedTaskLogs _serviceCompletedTaskLogs;
        private Mock<IEmployeeRepository> _repositoryEmployee;
        private Mock<ICompletedTasksLogRepository> _repositoryCompletedTaskLog;
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
            _repositoryEmployee = new Mock<IEmployeeRepository>();
            _repositoryEmployee
                .Setup(method => method.GetEmployeesListAsync())
                .ReturnsAsync(() => new List<BaseEmployee> {new StaffEmployee("Алексей","Алексеев",Departments.IT,100_000),
                                                            new FreeLancerEmployee("Николай","Николаев",Departments.IT, 1_000),
                                                            new DirectorEmployee("Александр","Александров",Departments.Managment,200_000)
                                                            })
                .Verifiable();

            _repositoryCompletedTaskLog = new Mock<ICompletedTasksLogRepository>();
            _repositoryCompletedTaskLog
                .Setup(method => method.GetCompletedTasksListAsync())
                .ReturnsAsync(() => new List<CompletedTask>{ new CompletedTask(Guid.NewGuid(),_freelancer.Id, DateTime.Now, 4, "task1") });

            _serviceEmployee = new EmployeeService(_repositoryEmployee.Object, _autorize);
            _serviceCompletedTaskLogs = new CompletedTasksLogsService(_repositoryCompletedTaskLog.Object, _autorize);
        }

        //Добавление нового сотрудника
        [Test]
        public void A_InsertNewEmployee_ShouldReturnFalse()
        {
            _repositoryEmployee
                .Setup(method => method.InsertEmployeeAsync(_freelancer))
                .ReturnsAsync(_freelancer)
                .Verifiable();

            var resultFalseAutorize = _serviceEmployee.InsertEmployeeAsync(_freelancer).Result;

            _repositoryEmployee.Verify(method => method.InsertEmployeeAsync(_freelancer), Times.Never);

            Assert.IsFalse(resultFalseAutorize);

        }

        //Удаление существующего сотрудника
        [Test]
        public void B_DeleteEmployeeByName_ShouldReturnFalse()
        {
            _repositoryEmployee
                .Setup(method => method.DeleteEmployeeByNameAsync("Алексей"))
                .ReturnsAsync(new FreeLancerEmployee("Алексей", "Алексеев", Departments.IT, 100_000));

            var resultFalse = _serviceEmployee.DeleteEmployeeByNameAsync("Алексей").Result;

            _repositoryEmployee.Verify(method => method.DeleteEmployeeByNameAsync("Алексей"), Times.Never);

            Assert.IsFalse(resultFalse);

        }

        //Получение списка сотрудников
        [Test]
        public void C_GetAllEmployeesList_ShouldReturnNull()
        {
            var result = _serviceEmployee.GetAllEmployeeAsync().Result;

            _repositoryEmployee.Verify(method => method.GetEmployeesListAsync(), Times.Never);

            Assert.IsNull(result);
        }

        //Получение сотрудника по имени
        [Test]
        public void D_GetEmployeeByName_ShouldReturnNewEmployee()
        {
            string name = "Алексей";
            _repositoryEmployee
                .Setup(method => method.GetEmployeeByNameAsync(name))
                .ReturnsAsync(() => new FreeLancerEmployee("Алексей", "Алексеев", Departments.IT, 100_000))
                .Verifiable();

            var result = _serviceEmployee.GetEmployeeByNameAsync(name).Result;

            _repositoryEmployee.Verify(method => method.GetEmployeeByNameAsync(name), Times.Never);

            Assert.IsNull(result);
        }

        //Создание нового лога выполненной задачи
        [Test]
        public void E_CreateCompliteTask_ShouldReturnComplitedTask()
        {
            var result1 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date.AddDays(-2), _freelancer, "Task1", 8);
            var result2 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date.AddDays(-1), _freelancer, "Task2", 3);
            var result3 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date.AddDays(-1), _freelancer, "Task3", 5);
            var result4 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date, _freelancer, "Task4", 1);
            var result5 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date, _freelancer, "Task5", 5);

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);
            Assert.IsNotNull(result4);
            Assert.IsNotNull(result5);

            var resultNull1 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date.AddDays(-3), _freelancer, "Task1", 8);
            var resultNull2 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date.AddDays(-1), _freelancer, "", 3);
            var resultNull3 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date.AddDays(-1), _freelancer, "Task3", 25);
            var resultNull4 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date, _freelancer, "Task4", -1);
            var resultNull5 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date, new FreeLancerEmployee(Guid.NewGuid(),"testFrelanser", "testFrelanser", Departments.IT, 1_000), "Task5", 5);
            var resultNull6 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date, null, "Task5", 5);


            Assert.IsNull(resultNull1);
            Assert.IsNull(resultNull2);
            Assert.IsNull(resultNull3);
            Assert.IsNull(resultNull4);
            Assert.IsNull(resultNull5);
            Assert.IsNull(resultNull6);

        }

        //Добавление выполненной задачи в хранилище
        [Test]
        public void F_AddCompliteTask_ShouldReturnBoolResult()
        {
            var task1 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date.AddDays(-2), _freelancer, "Task1", 8);
            _repositoryCompletedTaskLog
                .Setup(method => method.InsertCompletedTaskAsync(task1))
                .ReturnsAsync(true)
                .Verifiable();

            var result = _serviceCompletedTaskLogs.AddNewTaskLog(task1).Result;
            var resultFalse = _serviceCompletedTaskLogs.AddNewTaskLog(null).Result;



            _repositoryCompletedTaskLog.Verify(meth => meth.InsertCompletedTaskAsync(task1), Times.Once);
            _repositoryCompletedTaskLog.Verify(meth => meth.InsertCompletedTaskAsync(null), Times.Never);

            Assert.IsTrue(result);
            Assert.IsFalse(resultFalse);


        }
    }
}
