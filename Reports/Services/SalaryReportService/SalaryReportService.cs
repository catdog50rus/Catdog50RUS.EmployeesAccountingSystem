using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Reports.Services.SalaryReportService
{
    public class SalaryReportService : ISalaryReportService
    {
        /// <summary>
        /// Инжекция сервиса получения логов
        /// </summary>
        private readonly ICompletedTaskLogsService _taskLogsService;
        private readonly IEmployeeService _employeeService;


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="taskLogsService"></param>
        public SalaryReportService(ICompletedTaskLogsService taskLogsService)
        {
            _taskLogsService = taskLogsService ?? throw new ArgumentNullException(nameof(taskLogsService));
        }

        public SalaryReportService(ICompletedTaskLogsService taskLogsService, IEmployeeService employeeService) : this(taskLogsService)
        {
            _employeeService = employeeService;
        }

        

        #region Интерфейс

        /// <summary>
        /// Получить отчет по зарплате сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <param name="period">Период</param>
        /// <returns>Возвращает отчет</returns>
        public async Task<SalaryReport> GetEmployeeSalaryReport(BaseEmployee employee, (DateTime firstDate, DateTime lastDate) period)
        {
            //Получаем список логов
            var employeeTasksLogList = await _taskLogsService.GetEmployeeTaskLogs(employee.Id, period.firstDate, period.lastDate);
            if (employeeTasksLogList == null)
                return null;

            //Result
            var salaryReport = new SalaryReport(period.firstDate, period.lastDate, employee, employeeTasksLogList)
            {
                Header = $"Отчет по Заработной плате cотрудника: {employee}\nВ период с {period.firstDate:dd.MM.yyyy} по {period.lastDate:dd.MM.yyyy}\n",
                TotalSalary = employee.CalculateSamary(employeeTasksLogList)
            };
            return salaryReport;
        }

        public async Task<SalaryReportPerAllEmployees> GetAllEmployeesSalaryReport((DateTime firstDate, DateTime lastDate) period)
        {
            var timesLogs = await _taskLogsService.GetCompletedTaskLogs(period.firstDate, period.lastDate);
            if (timesLogs == null)
                return null;

            var employeesTimeLogs = timesLogs.GroupBy(e => e.IdEmployee);

            
            List<SalaryReport> employeeSalaryReport = new List<SalaryReport>();
            foreach (var log in employeesTimeLogs)
            {
                var employeeId = log.Key;
                var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
                if (employee == null)
                    break;
                var employeeTimeLogs = log.ToList();
                var salaryReport = new SalaryReport(period.firstDate, period.lastDate, employee, employeeTimeLogs)
                {
                    Header = $"Cотрудника: {employee}\n ",
                    TotalSalary = employee.CalculateSamary(employeeTimeLogs)
                };
                employeeSalaryReport.Add(salaryReport);
            }
            if (employeeSalaryReport.Count == 0)
                return null;

            var result = new SalaryReportPerAllEmployees(employeeSalaryReport)
            {
                Header = $"Отчет по Заработной плате сотрудников \nВ период с {period.firstDate:dd.MM.yyyy} по {period.lastDate:dd.MM.yyyy}\n"
            };

            return result;
        }

        public IEnumerable<SalaryReport> GetDepartmensSalaryReport(Departments department, (DateTime firstDate, DateTime lastDate) period)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
