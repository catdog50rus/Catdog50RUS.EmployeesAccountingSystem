using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
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
        private readonly BaseEmployee _staff;
        private readonly BaseEmployee _freelancer;
        private IEmployeeService _serviceEmployee;
        private ICompletedTaskLogsService _serviceCompletedTaskLogs;
        private Mock<IEmployeeRepository> _repositoryEmployee;
        private Mock<ICompletedTasksLogRepository> _repositoryCompletedTaskLog;
        private Autorize _autorize;

        public DirectorEmployeeTests()
        {
            var id = Guid.NewGuid();
            _director = new DirectorEmployee(id, "Петр", "Петров", Departments.Managment, 200_000);
            _autorize = new Autorize(Role.Director, id);

            _staff = new StaffEmployee(id, "Иван", "Иванов", Departments.IT, 100_000);
            _freelancer = new FreeLancerEmployee(id, "Василий", "Васин", Departments.IT, 1_000);
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
                .Setup(method => method.GetCompletedTasksListByEmployeeAsync(_director.Id,
                                        DateTime.Now.Date.AddDays(-10), DateTime.Now.Date))
                .ReturnsAsync(() => new List<CompletedTaskLog> { new CompletedTaskLog(Guid.NewGuid(), _director.Id, 
                                                                                DateTime.Now.Date.AddDays(-5), 5, "TestTask4") })
                .Verifiable();

            _serviceEmployee = new EmployeeService(_repositoryEmployee.Object, _autorize);
            _serviceCompletedTaskLogs = new CompletedTasksLogsService(_repositoryCompletedTaskLog.Object, _autorize);
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
            _repositoryEmployee
                .Setup(method => method.InsertEmployeeAsync(_director))
                .ReturnsAsync(_director)
                .Verifiable();

            var resultTrue = _serviceEmployee.InsertEmployeeAsync(_director).Result;
            var resultFalse = _serviceEmployee.InsertEmployeeAsync(null).Result;

            _repositoryEmployee.Verify(method => method.GetEmployeesListAsync(), Times.Once);
            _repositoryEmployee.Verify(method => method.InsertEmployeeAsync(_director), Times.Once);
            _repositoryEmployee.Verify(method => method.InsertEmployeeAsync(null), Times.Never);

            Assert.IsTrue(resultTrue);
            Assert.IsFalse(resultFalse);

        }

        [Test]
        //Добавление существующего сотрудника
        public void B_InsertMoreThenOneEmployee_ShouldReturnFalse()
        {
            _repositoryEmployee
                .Setup(method => method.GetEmployeesListAsync())
                .ReturnsAsync(() => new List<BaseEmployee> { _director });
            _repositoryEmployee
                .Setup(method => method.InsertEmployeeAsync(_director))
                .ReturnsAsync(_director)
                .Verifiable();

            var result = _serviceEmployee.InsertEmployeeAsync(_director).Result;

            _repositoryEmployee.Verify(method => method.GetEmployeesListAsync(), Times.Once);
            _repositoryEmployee.Verify(method => method.InsertEmployeeAsync(_director), Times.Never);

            Assert.IsFalse(result);

        }

        [Test]
        //Удаление существующего сотрудника
        public void C_DeleteEmployeeByName_ShouldReturnBoolResults()
        {
            _repositoryEmployee
                .Setup(method => method.DeleteEmployeeByNameAsync("Алексей"))
                .ReturnsAsync(new StaffEmployee("Алексей", "Алексеев", Departments.IT, 100_000));

            var resultTrue = _serviceEmployee.DeleteEmployeeByNameAsync("Алексей").Result;

            var resultFalseEmpty = _serviceEmployee.DeleteEmployeeByNameAsync("").Result;

            var resultFalseNull = _serviceEmployee.DeleteEmployeeByNameAsync(null).Result;

            _repositoryEmployee.Verify(method => method.DeleteEmployeeByNameAsync("Алексей"), Times.Once);
            _repositoryEmployee.Verify(method => method.DeleteEmployeeByNameAsync(""), Times.Never);
            _repositoryEmployee.Verify(method => method.DeleteEmployeeByNameAsync(" "), Times.Never);
            _repositoryEmployee.Verify(method => method.DeleteEmployeeByNameAsync(null), Times.Never);

            Assert.IsTrue(resultTrue);
            Assert.IsFalse(resultFalseEmpty);
            Assert.IsFalse(resultFalseNull);

        }

        [Test]
        //Удаление несуществующего сотрудника
        public void D_DeleteEmployeeByName_ShouldReturnFalse()
        {
            _repositoryEmployee
                .Setup(method => method.DeleteEmployeeByNameAsync("Евгений"))
                .ReturnsAsync(() => null);

            var resultFalse = _serviceEmployee.DeleteEmployeeByNameAsync("Евгений").Result;

            _repositoryEmployee.Verify(method => method.DeleteEmployeeByNameAsync("Евгений"), Times.Once);
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

            var result = _serviceEmployee.GetAllEmployeeAsync().Result.ToList();

            _repositoryEmployee.Verify(method => method.GetEmployeesListAsync(), Times.Once);

            Assert.AreEqual(expactedListCount, result.Count());
            Assert.AreEqual(expactedStaffEmployee, result[0]);
            Assert.AreEqual(expactedFreelancerEmployee, result[1]);
            Assert.AreEqual(expactedDirectorEmployee, result[2]);
        }

        //Получение сотрудника по имени
        [Test]
        public void F_GetEmployeeByName_ShouldReturnNewEmployee()
        {
            string name = "Алексей";
            _repositoryEmployee
                .Setup(method => method.GetEmployeeByNameAsync(name))
                .ReturnsAsync(() => new StaffEmployee("Алексей", "Алексеев", Departments.IT, 100_000))
                .Verifiable();

            var result = _serviceEmployee.GetEmployeeByNameAsync(name).Result;
            var resultFalseNull = _serviceEmployee.GetEmployeeByNameAsync(null).Result;
            var resultFalseEmpty = _serviceEmployee.GetEmployeeByNameAsync("").Result;

            _repositoryEmployee.Verify(method => method.GetEmployeeByNameAsync(name), Times.Once);
            _repositoryEmployee.Verify(method => method.GetEmployeeByNameAsync(null), Times.Never);
            _repositoryEmployee.Verify(method => method.GetEmployeeByNameAsync(" "), Times.Never);

            Assert.AreEqual(name, result.NamePerson);
            Assert.IsNull(resultFalseNull);
            Assert.IsNull(resultFalseEmpty);
        }

        //Получение не существующего сотрудника по имени
        [Test]
        public void G_GetEmployeeByName_ShouldReturnNull()
        {
            string name = "Евгений";
            _repositoryEmployee
                .Setup(method => method.GetEmployeeByNameAsync(name))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = _serviceEmployee.GetEmployeeByNameAsync(name).Result;

            _repositoryEmployee.Verify(method => method.GetEmployeeByNameAsync(name), Times.Once);

            Assert.IsNull(result);
        }

        //Создание нового лога выполненной задачи
        [Test]
        public void H_CreateCompliteTask_ShouldReturnComplitedTask()
        {
            var result1 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date.AddDays(-5), _director, "Task1", 8);
            var result2 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date.AddDays(-1), _staff, "Task2", 3);
            var result3 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date.AddDays(-10), _freelancer, "Task3", 5);

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);

            var resultNull2 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date.AddDays(-1), _staff, "", 3);
            var resultNull3 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date.AddDays(-1), _staff, "Task3", 25);
            var resultNull4 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date, _staff, "Task4", -1);
            var resultNull6 = _serviceCompletedTaskLogs.CreateNewTask(DateTime.Now.Date, null, "Task5", 5);



            Assert.IsNull(resultNull2);
            Assert.IsNull(resultNull3);
            Assert.IsNull(resultNull4);

            Assert.IsNull(resultNull6);

        }

        //Добавление выполненной задачи в хранилище
        [Test]
        public void I_AddCompliteTask_ShouldReturnBoolResult()
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

        //Получение логов выполненных задач
        [Test]
        public void J_GetCompletedTaskLogs_ShouldReturnCompletedTaskLoagsList()
        {

            var result = _serviceCompletedTaskLogs.GetEmployeeTaskLogs(_director.Id,
                                        DateTime.Now.Date.AddDays(-10), DateTime.Now.Date).Result;
            var result2 = _serviceCompletedTaskLogs.GetEmployeeTaskLogs(_director.Id,
                                        DateTime.Now.Date.AddDays(-1), DateTime.Now.Date).Result;

            _repositoryCompletedTaskLog.Verify(meth => meth.GetCompletedTasksListByEmployeeAsync(_director.Id,
                                        DateTime.Now.Date.AddDays(-10), DateTime.Now.Date), Times.Once);
            _repositoryCompletedTaskLog.Verify(meth => meth.GetCompletedTasksListByEmployeeAsync(_director.Id,
                                        DateTime.Now.Date.AddDays(-1), DateTime.Now.Date), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ToList().Count);

            Assert.IsNull(result2);


            //Тестирование первичной валидации
            var result3 = _serviceCompletedTaskLogs.GetEmployeeTaskLogs(_director.Id,
                                        DateTime.Now.Date.AddDays(-1), DateTime.Now.Date.AddDays(40).AddYears(2)).Result;
            _repositoryCompletedTaskLog.Verify(meth => meth.GetCompletedTasksListByEmployeeAsync(_director.Id,
                                        DateTime.Now.Date.AddDays(-1), DateTime.Now.Date.AddDays(40).AddYears(2)), Times.Never);
            Assert.IsNull(result3);

            var result4 = _serviceCompletedTaskLogs.GetEmployeeTaskLogs(_director.Id,
                                        DateTime.Now.Date.AddDays(1), DateTime.Now.Date).Result;
            _repositoryCompletedTaskLog.Verify(meth => meth.GetCompletedTasksListByEmployeeAsync(_director.Id,
                                        DateTime.Now.Date.AddDays(1), DateTime.Now.Date), Times.Never);
            Assert.IsNull(result4);

            var result5 = _serviceCompletedTaskLogs.GetEmployeeTaskLogs(Guid.Empty,
                                        DateTime.Now.Date.AddDays(-10), DateTime.Now.Date).Result;
            _repositoryCompletedTaskLog.Verify(meth => meth.GetCompletedTasksListByEmployeeAsync(Guid.Empty,
                                        DateTime.Now.Date.AddDays(-10), DateTime.Now.Date), Times.Never);
            Assert.IsNull(result5);

            //Тестирование валидации прав
            var id = Guid.NewGuid();
            _repositoryCompletedTaskLog
                .Setup(method => method.GetCompletedTasksListByEmployeeAsync(id,
                                        DateTime.Now.Date.AddDays(-10), DateTime.Now.Date));

            var result6 = _serviceCompletedTaskLogs.GetEmployeeTaskLogs(id,
                                        DateTime.Now.Date.AddDays(-10), DateTime.Now.Date).Result;

            _repositoryCompletedTaskLog.Verify(meth => meth.GetCompletedTasksListByEmployeeAsync(id,
                                        DateTime.Now.Date.AddDays(-10), DateTime.Now.Date), Times.Once);

            Assert.IsNull(result6);
        }

        //Получение логов всех выполненных задач
        [Test]
        public void K_GetAllCompletedTaskLogs_ShouldReturnCompletedTaskLoagsList()
        {
            _repositoryCompletedTaskLog
                .Setup(meth => meth.GetCompletedTasksListInPeriodAsync(DateTime.Now.Date.AddDays(-10), DateTime.Now.Date))
                .ReturnsAsync(() => new List<CompletedTaskLog>{new CompletedTaskLog(Guid.NewGuid(), _director.Id,
                                                                                DateTime.Now.Date.AddDays(-5), 5, "TestTask4"),
                                                           new CompletedTaskLog(Guid.NewGuid(), _staff.Id,
                                                                                DateTime.Now.Date.AddDays(-2), 7, "TestTask3"),
                                                           new CompletedTaskLog(Guid.NewGuid(), _freelancer.Id,
                                                                                DateTime.Now.Date.AddDays(-4), 8, "TestTask7") });


            var result = _serviceCompletedTaskLogs.GetCompletedTaskLogs(DateTime.Now.Date.AddDays(-10), 
                                                                        DateTime.Now.Date).Result;

            _repositoryCompletedTaskLog
               .Setup(meth => meth.GetCompletedTasksListInPeriodAsync(DateTime.Now.Date.AddDays(-4), DateTime.Now.Date))
               .ReturnsAsync(() => new List<CompletedTaskLog>{new CompletedTaskLog(Guid.NewGuid(), _staff.Id,
                                                                                DateTime.Now.Date.AddDays(-2), 7, "TestTask3"),
                                                           new CompletedTaskLog(Guid.NewGuid(), _freelancer.Id,
                                                                                DateTime.Now.Date.AddDays(-4), 8, "TestTask7") });

            var result2 = _serviceCompletedTaskLogs.GetCompletedTaskLogs(DateTime.Now.Date.AddDays(-4),
                                                                        DateTime.Now.Date).Result;

            _repositoryCompletedTaskLog.Verify(meth => meth.GetCompletedTasksListInPeriodAsync(
                                                                        DateTime.Now.Date.AddDays(-10),
                                                                        DateTime.Now.Date), Times.Once);
            _repositoryCompletedTaskLog.Verify(meth => meth.GetCompletedTasksListInPeriodAsync(
                                                                        DateTime.Now.Date.AddDays(-4),
                                                                        DateTime.Now.Date), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.ToList().Count);

            Assert.IsNotNull(result2);
            Assert.AreEqual(2, result2.ToList().Count);


            //Тестирование первичной валидации
            var result3 = _serviceCompletedTaskLogs.GetCompletedTaskLogs(DateTime.Now.Date.AddDays(10),
                                                                        DateTime.Now.Date).Result;
            _repositoryCompletedTaskLog.Verify(meth => meth.GetCompletedTasksListInPeriodAsync(
                                                                        DateTime.Now.Date.AddDays(10),
                                                                        DateTime.Now.Date), Times.Never);
            Assert.IsNull(result3);

            var result4 = _serviceCompletedTaskLogs.GetCompletedTaskLogs(DateTime.Now.Date.AddDays(-10),
                                                                        DateTime.Now.Date.AddYears(2)).Result;
            _repositoryCompletedTaskLog.Verify(meth => meth.GetCompletedTasksListInPeriodAsync(
                                                                        DateTime.Now.Date.AddDays(-10),
                                                                        DateTime.Now.Date.AddYears(2)), Times.Never);
            Assert.IsNull(result4);

        }

    }
}
