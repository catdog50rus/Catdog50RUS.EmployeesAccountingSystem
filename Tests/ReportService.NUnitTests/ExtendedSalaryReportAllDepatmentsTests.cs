using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services.EmployeeService;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Reports.Services.SalaryReportService;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ReportService.NUnitTests
{
    [TestFixture]
    class ExtendedSalaryReportAllDepartmentsTests
    {

        //Получение директором отчета по зарплате сотрудников по отделам
        [TestCase("345f97a8-284c-4533-b976-b13d3c75188f", "Петр", "Петров", Departments.Managment, 200_000)]

        public void A_GetDepatmentsReportByDircetor_ReturnReport(string _id, string name, string surname,
                                                               Departments department, decimal baseSalary)
        {
            var directorID = Guid.Parse("345f97a8-284c-4533-b976-b13d3c75188f");
            var id = Guid.Parse(_id);
            var _autorize = new Autorize(Role.Director, directorID);

            double expactedAllTotalTime = 35;
            decimal expactedAllTotalSalary = 39_750;

            #region TestSetup

            //Настройка MOCK депозитария
            ICompletedTaskLogsService _serviceCompletedTaskLogs;
            ISalaryReportService _salaryReportService;
            IEmployeeService _employeeService;

            var idEmp1 = directorID;
            var idEmp2 = Guid.Parse("345f97a8-287c-4533-b976-b13d3c75188f");
            var idEmp3 = Guid.Parse("345f97a8-288c-4533-b976-b13d3c75188f");


            Mock<ICompletedTasksLogRepository> _repositoryCompletedTaskLog = new Mock<ICompletedTasksLogRepository>();
            _repositoryCompletedTaskLog
                .Setup(method => method.GetCompletedTasksListInPeriodAsync(DateTime.Now.Date.AddDays(-5), DateTime.Now.Date))
                .ReturnsAsync(() => new List<CompletedTaskLog> { new CompletedTaskLog(Guid.NewGuid(), idEmp1,
                                                                                DateTime.Now.Date.AddDays(-5), 5, "TestTask4"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp2,
                                                                                DateTime.Now.Date.AddDays(-5), 3, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp1,
                                                                                DateTime.Now.Date.AddDays(-3), 9, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp2,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp2,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp1,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp3,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp3,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp1,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6")})
                .Verifiable();

            Mock<IEmployeeRepository> _repositoryEmployee;
            _repositoryEmployee = new Mock<IEmployeeRepository>();
            _repositoryEmployee
                .Setup(method => method.GetEmployeeByIdAsync(idEmp1))
                .ReturnsAsync(new DirectorEmployee(idEmp1, name, surname, department, baseSalary));

            _repositoryEmployee
                .Setup(method => method.GetEmployeeByIdAsync(idEmp2))
                .ReturnsAsync(new StaffEmployee(idEmp2, "Витя", "Викторов", Departments.IT, 160_000));

            _repositoryEmployee
                .Setup(method => method.GetEmployeeByIdAsync(idEmp3))
                .ReturnsAsync(new FreeLancerEmployee(idEmp3, "Леня", "Леонидов", Departments.IT, 1_000));


            //Настройка сервисов
            _serviceCompletedTaskLogs = new CompletedTasksLogsService(_repositoryCompletedTaskLog.Object, _autorize);
            _employeeService = new EmployeeService(_repositoryEmployee.Object, _autorize);
            _salaryReportService = new SalaryReportService(_serviceCompletedTaskLogs, _employeeService);



            #endregion


            DateTime firstDay = DateTime.Now.Date.AddDays(-5);
            DateTime lastDay = DateTime.Now.Date;


            var result = _salaryReportService.GetAllDepatmentsSalaryReport((firstDay, lastDay)).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(expactedAllTotalSalary, result.TotalSalary);
            Assert.AreEqual(expactedAllTotalTime, result.TotalTime);

        }

        //Получение директором отчета по зарплате сотрудников по отделам, результат null вне диапазона дат
        [TestCase("345f97a8-284c-4533-b976-b13d3c75188f", "Петр", "Петров", Departments.Managment, 200_000)]

        public void B_GetDepatmentsReportByDircetor_ReturnNull(string _id, string name, string surname,
                                                               Departments department, decimal baseSalary)
        {
            var directorID = Guid.Parse("345f97a8-284c-4533-b976-b13d3c75188f");
            var id = Guid.Parse(_id);
            var _autorize = new Autorize(Role.Director, directorID);


            #region TestSetup

            //Настройка MOCK депозитария
            ICompletedTaskLogsService _serviceCompletedTaskLogs;
            ISalaryReportService _salaryReportService;
            IEmployeeService _employeeService;

            var idEmp1 = directorID;
            var idEmp2 = Guid.Parse("345f97a8-287c-4533-b976-b13d3c75188f");
            var idEmp3 = Guid.Parse("345f97a8-288c-4533-b976-b13d3c75188f");


            Mock<ICompletedTasksLogRepository> _repositoryCompletedTaskLog = new Mock<ICompletedTasksLogRepository>();
            _repositoryCompletedTaskLog
                .Setup(method => method.GetCompletedTasksListInPeriodAsync(DateTime.Now.Date.AddDays(-5), DateTime.Now.Date))
                .ReturnsAsync(() => new List<CompletedTaskLog> { new CompletedTaskLog(Guid.NewGuid(), idEmp1,
                                                                                DateTime.Now.Date.AddDays(-5), 5, "TestTask4"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp2,
                                                                                DateTime.Now.Date.AddDays(-5), 3, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp1,
                                                                                DateTime.Now.Date.AddDays(-3), 9, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp2,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp2,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp1,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp3,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp3,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp1,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6")})
                .Verifiable();

            Mock<IEmployeeRepository> _repositoryEmployee;
            _repositoryEmployee = new Mock<IEmployeeRepository>();
            _repositoryEmployee
                .Setup(method => method.GetEmployeeByIdAsync(idEmp1))
                .ReturnsAsync(new DirectorEmployee(idEmp1, name, surname, department, baseSalary));

            _repositoryEmployee
                .Setup(method => method.GetEmployeeByIdAsync(idEmp2))
                .ReturnsAsync(new StaffEmployee(idEmp2, "Витя", "Викторов", Departments.IT, 160_000));

            _repositoryEmployee
                .Setup(method => method.GetEmployeeByIdAsync(idEmp3))
                .ReturnsAsync(new FreeLancerEmployee(idEmp3, "Леня", "Леонидов", Departments.IT, 1_000));


            //Настройка сервисов
            _serviceCompletedTaskLogs = new CompletedTasksLogsService(_repositoryCompletedTaskLog.Object, _autorize);
            _employeeService = new EmployeeService(_repositoryEmployee.Object, _autorize);
            _salaryReportService = new SalaryReportService(_serviceCompletedTaskLogs, _employeeService);



            #endregion


            DateTime firstDay = DateTime.Now.Date.AddDays(-10);
            DateTime lastDay = DateTime.Now.Date.AddDays(-7);


            var result = _salaryReportService.GetAllDepatmentsSalaryReport((firstDay, lastDay)).Result;

            Assert.IsNull(result);
        }


        //Получение директором отчета по зарплате сотрудников по отделам, данных об одном сотруднике нет
        [TestCase("345f97a8-284c-4533-b976-b13d3c75188f", "Петр", "Петров", Departments.Managment, 200_000)]
        public void C_GetDepatmentsReportByDircetor_ReturnReport(string _id, string name, string surname,
                                                               Departments department, decimal baseSalary)
        {
            var directorID = Guid.Parse("345f97a8-284c-4533-b976-b13d3c75188f");
            var id = Guid.Parse(_id);
            var _autorize = new Autorize(Role.Director, directorID);

            double expactedAllTotalTime = 29;
            decimal expactedAllTotalSalary = 33_750;

            #region TestSetup

            //Настройка MOCK депозитария
            ICompletedTaskLogsService _serviceCompletedTaskLogs;
            ISalaryReportService _salaryReportService;
            IEmployeeService _employeeService;

            var idEmp1 = directorID;
            var idEmp2 = Guid.Parse("345f97a8-287c-4533-b976-b13d3c75188f");
            var idEmp3 = Guid.Parse("345f97a8-288c-4533-b976-b13d3c75188f");


            Mock<ICompletedTasksLogRepository> _repositoryCompletedTaskLog = new Mock<ICompletedTasksLogRepository>();
            _repositoryCompletedTaskLog
                .Setup(method => method.GetCompletedTasksListInPeriodAsync(DateTime.Now.Date.AddDays(-5), DateTime.Now.Date))
                .ReturnsAsync(() => new List<CompletedTaskLog> { new CompletedTaskLog(Guid.NewGuid(), idEmp1,
                                                                                DateTime.Now.Date.AddDays(-5), 5, "TestTask4"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp2,
                                                                                DateTime.Now.Date.AddDays(-5), 3, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp1,
                                                                                DateTime.Now.Date.AddDays(-3), 9, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp2,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp2,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp1,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp3,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp3,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), idEmp1,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6")})
                .Verifiable();

            Mock<IEmployeeRepository> _repositoryEmployee;
            _repositoryEmployee = new Mock<IEmployeeRepository>();
            _repositoryEmployee
                .Setup(method => method.GetEmployeeByIdAsync(idEmp1))
                .ReturnsAsync(new DirectorEmployee(idEmp1, name, surname, department, baseSalary));

            _repositoryEmployee
                .Setup(method => method.GetEmployeeByIdAsync(idEmp2))
                .ReturnsAsync(new StaffEmployee(idEmp2, "Витя", "Викторов", Departments.IT, 160_000));

            _repositoryEmployee
                .Setup(method => method.GetEmployeeByIdAsync(idEmp3))
                .ReturnsAsync(() => null);


            //Настройка сервисов
            _serviceCompletedTaskLogs = new CompletedTasksLogsService(_repositoryCompletedTaskLog.Object, _autorize);
            _employeeService = new EmployeeService(_repositoryEmployee.Object, _autorize);
            _salaryReportService = new SalaryReportService(_serviceCompletedTaskLogs, _employeeService);



            #endregion


            DateTime firstDay = DateTime.Now.Date.AddDays(-5);
            DateTime lastDay = DateTime.Now.Date;


            var result = _salaryReportService.GetAllDepatmentsSalaryReport((firstDay, lastDay)).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(expactedAllTotalSalary, result.TotalSalary);
            Assert.AreEqual(expactedAllTotalTime, result.TotalTime);

        }

    }
}
