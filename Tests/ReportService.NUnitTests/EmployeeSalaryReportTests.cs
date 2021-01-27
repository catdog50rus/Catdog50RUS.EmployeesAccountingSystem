using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Reports.Services.SalaryReportService;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ReportService.NUnitTests
{
    class EmployeeSalaryReportTests
    {

        //Получение директором отчета по зарплате сотрудника
        [TestCase("345f97a8-284c-4533-b976-b13d3c75188f", "Петр", "Петров", Departments.Managment, 200_000, Positions.Director, 24_750)]
        [TestCase("345f97a8-287c-4533-b976-b13d3c75188f", "Иван", "Иванов", Departments.IT, 160_000, Positions.Developer, 21_000)]
        [TestCase("345f97a8-288c-4533-b976-b13d3c75188f", "Сидор", "Сидоров", Departments.IT, 1_000, Positions.Freelance, 20_000)]
        public void A_GetEmployeeReportByDircetor_ReturnReport(string _id, string name, string surname, 
                                                               Departments department, decimal baseSalary, Positions position, 
                                                               decimal expactedTotalSalary)
        {
            var directorID = Guid.Parse("345f97a8-284c-4533-b976-b13d3c75188f");
            var id = Guid.Parse(_id);
            var _autorize = new Autorize(Role.Director, directorID);
           

            double expactedTotalTime = 20;

            #region TestSetup

            //Настройка MOCK депозитария
            ICompletedTaskLogsService _serviceCompletedTaskLogs;
            ISalaryReportService _salaryReportService;
            Mock<ICompletedTasksLogRepository> _repositoryCompletedTaskLog;
            _repositoryCompletedTaskLog = new Mock<ICompletedTasksLogRepository>();
            _repositoryCompletedTaskLog
                .Setup(method => method.GetCompletedTasksListByEmployeeAsync(id,
                                        DateTime.Now.Date.AddDays(-5), DateTime.Now.Date))
                .ReturnsAsync(() => new List<CompletedTaskLog> { new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-5), 5, "TestTask4"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-5), 3, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-3), 9, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),})
                .Verifiable();

            //Настройка сервисов
            _serviceCompletedTaskLogs = new CompletedTasksLogsService(_repositoryCompletedTaskLog.Object, _autorize);
            _salaryReportService = new SalaryReportService(_serviceCompletedTaskLogs);

            //Настройка сотрудника
            BaseEmployee employee = null;
            switch (position)
            {
                case Positions.None:
                    break;
                case Positions.Director:
                    employee = new DirectorEmployee(id, name, surname, department, baseSalary);
                    break;
                case Positions.Developer:
                    employee = new StaffEmployee(id, name, surname, department, baseSalary);
                    break;
                case Positions.Freelance:
                    employee = new FreeLancerEmployee(id, name, surname, department, baseSalary);
                    break;
                default:
                    break;
            }


            #endregion


            DateTime firstDay = DateTime.Now.Date.AddDays(-5);
            DateTime lastDay = DateTime.Now.Date;


            var result = _salaryReportService.GetEmployeeSalaryReport(employee, (firstDay, lastDay));

            Assert.IsNotNull(result);
            Assert.AreEqual(expactedTotalSalary, result.TotalSalary);
            Assert.AreEqual(expactedTotalTime, result.TotalTime);

        }

        //Получение разработчиком отчета по своей зарплате
        [TestCase("345f97a8-287c-4533-b976-b13d3c75188f", "Иван", "Иванов", Departments.IT, 160_000, Positions.Developer, 21_000)]
        public void B_GetEmployeeReportByStaffEmployee_ReturnReport(string _id, string name, string surname,
                                                                    Departments department, decimal baseSalary, Positions position, 
                                                                    decimal expactedTotalSalary)
        {
            var staffID = Guid.Parse("345f97a8-287c-4533-b976-b13d3c75188f");
            var id = Guid.Parse(_id);
            var _autorize = new Autorize(Role.Developer, staffID);


            double expactedTotalTime = 20;

            #region TestSetup
            //Настройка MOCK депозитария
            ICompletedTaskLogsService _serviceCompletedTaskLogs;
            ISalaryReportService _salaryReportService;
            Mock<ICompletedTasksLogRepository> _repositoryCompletedTaskLog;
            _repositoryCompletedTaskLog = new Mock<ICompletedTasksLogRepository>();
            _repositoryCompletedTaskLog
                .Setup(method => method.GetCompletedTasksListByEmployeeAsync(id,
                                        DateTime.Now.Date.AddDays(-5), DateTime.Now.Date))
                .ReturnsAsync(() => new List<CompletedTaskLog> { new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-5), 5, "TestTask4"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-5), 3, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-3), 9, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),})
                .Verifiable();

            //Настройка сервисов
            _serviceCompletedTaskLogs = new CompletedTasksLogsService(_repositoryCompletedTaskLog.Object, _autorize);
            _salaryReportService = new SalaryReportService(_serviceCompletedTaskLogs);

            //Настройка сотрудника
            BaseEmployee employee = null;
            switch (position)
            {
                case Positions.None:
                    break;
                case Positions.Director:
                    employee = new DirectorEmployee(id, name, surname, department, baseSalary);
                    break;
                case Positions.Developer:
                    employee = new StaffEmployee(id, name, surname, department, baseSalary);
                    break;
                case Positions.Freelance:
                    employee = new FreeLancerEmployee(id, name, surname, department, baseSalary);
                    break;
                default:
                    break;
            }


            #endregion


            DateTime firstDay = DateTime.Now.Date.AddDays(-5);
            DateTime lastDay = DateTime.Now.Date;


            var result = _salaryReportService.GetEmployeeSalaryReport(employee, (firstDay, lastDay));

            Assert.IsNotNull(result);
            Assert.AreEqual(expactedTotalSalary, result.TotalSalary);
            Assert.AreEqual(expactedTotalTime, result.TotalTime);

        }

        //Получение разработчиком отчета по зарплате сотрудника
        [TestCase("345f97a8-284c-4533-b976-b13d3c75188f", "Петр", "Петров", Departments.Managment, 200_000, Positions.Director, 24_750)]
        [TestCase("345f97a8-288c-4533-b976-b13d3c75188f", "Сидор", "Сидоров", Departments.IT, 1_000, Positions.Freelance, 20_000)]
        public void C_GetEmployeeReportByStaffEmployee_ReturnNull(string _id, string name, string surname,
                                                                    Departments department, decimal baseSalary, Positions position,
                                                                    decimal expactedTotalSalary)
        {
            var staffID = Guid.Parse("345f97a8-287c-4533-b976-b13d3c75188f");
            var id = Guid.Parse(_id);
            var _autorize = new Autorize(Role.Developer, staffID);

            #region TestSetup
            //Настройка MOCK депозитария
            ICompletedTaskLogsService _serviceCompletedTaskLogs;
            ISalaryReportService _salaryReportService;
            Mock<ICompletedTasksLogRepository> _repositoryCompletedTaskLog;
            _repositoryCompletedTaskLog = new Mock<ICompletedTasksLogRepository>();
            _repositoryCompletedTaskLog
                .Setup(method => method.GetCompletedTasksListByEmployeeAsync(id,
                                        DateTime.Now.Date.AddDays(-5), DateTime.Now.Date))
                .ReturnsAsync(() => new List<CompletedTaskLog> { new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-5), 5, "TestTask4"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-5), 3, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-3), 9, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),})
                .Verifiable();

            //Настройка сервисов
            _serviceCompletedTaskLogs = new CompletedTasksLogsService(_repositoryCompletedTaskLog.Object, _autorize);
            _salaryReportService = new SalaryReportService(_serviceCompletedTaskLogs);

            //Настройка сотрудника
            BaseEmployee employee = null;
            switch (position)
            {
                case Positions.None:
                    break;
                case Positions.Director:
                    employee = new DirectorEmployee(id, name, surname, department, baseSalary);
                    break;
                case Positions.Developer:
                    employee = new StaffEmployee(id, name, surname, department, baseSalary);
                    break;
                case Positions.Freelance:
                    employee = new FreeLancerEmployee(id, name, surname, department, baseSalary);
                    break;
                default:
                    break;
            }


            #endregion


            DateTime firstDay = DateTime.Now.Date.AddDays(-5);
            DateTime lastDay = DateTime.Now.Date;


            var result = _salaryReportService.GetEmployeeSalaryReport(employee, (firstDay, lastDay));

            Assert.IsNull(result);

        }

        //Получение фрилансером отчета по своей зарплате
        [TestCase("345f97a8-288c-4533-b976-b13d3c75188f", "Сидор", "Сидоров", Departments.IT, 1_000, Positions.Freelance, 20_000)]
        public void D_GetEmployeeReportByFreelancerEmployee_ReturnReport(string _id, string name, string surname,
                                                                    Departments department, decimal baseSalary, Positions position,
                                                                    decimal expactedTotalSalary)
        {
            var freelancerID = Guid.Parse("345f97a8-288c-4533-b976-b13d3c75188f");
            var id = Guid.Parse(_id);
            var _autorize = new Autorize(Role.Freelancer, freelancerID);


            double expactedTotalTime = 20;

            #region TestSetup
            //Настройка MOCK депозитария
            ICompletedTaskLogsService _serviceCompletedTaskLogs;
            ISalaryReportService _salaryReportService;
            Mock<ICompletedTasksLogRepository> _repositoryCompletedTaskLog;
            _repositoryCompletedTaskLog = new Mock<ICompletedTasksLogRepository>();
            _repositoryCompletedTaskLog
                .Setup(method => method.GetCompletedTasksListByEmployeeAsync(id,
                                        DateTime.Now.Date.AddDays(-5), DateTime.Now.Date))
                .ReturnsAsync(() => new List<CompletedTaskLog> { new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-5), 5, "TestTask4"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-5), 3, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-3), 9, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),})
                .Verifiable();

            //Настройка сервисов
            _serviceCompletedTaskLogs = new CompletedTasksLogsService(_repositoryCompletedTaskLog.Object, _autorize);
            _salaryReportService = new SalaryReportService(_serviceCompletedTaskLogs);

            //Настройка сотрудника
            BaseEmployee employee = null;
            switch (position)
            {
                case Positions.None:
                    break;
                case Positions.Director:
                    employee = new DirectorEmployee(id, name, surname, department, baseSalary);
                    break;
                case Positions.Developer:
                    employee = new StaffEmployee(id, name, surname, department, baseSalary);
                    break;
                case Positions.Freelance:
                    employee = new FreeLancerEmployee(id, name, surname, department, baseSalary);
                    break;
                default:
                    break;
            }


            #endregion


            DateTime firstDay = DateTime.Now.Date.AddDays(-5);
            DateTime lastDay = DateTime.Now.Date;


            var result = _salaryReportService.GetEmployeeSalaryReport(employee, (firstDay, lastDay));

            Assert.IsNotNull(result);
            Assert.AreEqual(expactedTotalSalary, result.TotalSalary);
            Assert.AreEqual(expactedTotalTime, result.TotalTime);

        }

        //Получение фрилансером отчета по зарплате других сотрудников
        [TestCase("345f97a8-284c-4533-b976-b13d3c75188f", "Петр", "Петров", Departments.Managment, 200_000, Positions.Director, 24_750)]
        [TestCase("345f97a8-287c-4533-b976-b13d3c75188f", "Иван", "Иванов", Departments.IT, 160_000, Positions.Developer, 21_000)]
        public void E_GetEmployeeReportByStaffEmployee_ReturnNull(string _id, string name, string surname,
                                                                    Departments department, decimal baseSalary, Positions position,
                                                                    decimal expactedTotalSalary)
        {
            var freelancerID = Guid.Parse("345f97a8-288c-4533-b976-b13d3c75188f");
            var id = Guid.Parse(_id);
            var _autorize = new Autorize(Role.Freelancer, freelancerID);

            #region TestSetup
            //Настройка MOCK депозитария
            ICompletedTaskLogsService _serviceCompletedTaskLogs;
            ISalaryReportService _salaryReportService;
            Mock<ICompletedTasksLogRepository> _repositoryCompletedTaskLog;
            _repositoryCompletedTaskLog = new Mock<ICompletedTasksLogRepository>();
            _repositoryCompletedTaskLog
                .Setup(method => method.GetCompletedTasksListByEmployeeAsync(id,
                                        DateTime.Now.Date.AddDays(-5), DateTime.Now.Date))
                .ReturnsAsync(() => new List<CompletedTaskLog> { new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-5), 5, "TestTask4"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-5), 3, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-3), 9, "TestTask5"),
                                                                 new CompletedTaskLog(Guid.NewGuid(), id,
                                                                                DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),})
                .Verifiable();

            //Настройка сервисов
            _serviceCompletedTaskLogs = new CompletedTasksLogsService(_repositoryCompletedTaskLog.Object, _autorize);
            _salaryReportService = new SalaryReportService(_serviceCompletedTaskLogs);

            //Настройка сотрудника
            BaseEmployee employee = null;
            switch (position)
            {
                case Positions.None:
                    break;
                case Positions.Director:
                    employee = new DirectorEmployee(id, name, surname, department, baseSalary);
                    break;
                case Positions.Developer:
                    employee = new StaffEmployee(id, name, surname, department, baseSalary);
                    break;
                case Positions.Freelance:
                    employee = new FreeLancerEmployee(id, name, surname, department, baseSalary);
                    break;
                default:
                    break;
            }


            #endregion


            DateTime firstDay = DateTime.Now.Date.AddDays(-5);
            DateTime lastDay = DateTime.Now.Date;


            var result = _salaryReportService.GetEmployeeSalaryReport(employee, (firstDay, lastDay));

            Assert.IsNull(result);

        }

    }
}
