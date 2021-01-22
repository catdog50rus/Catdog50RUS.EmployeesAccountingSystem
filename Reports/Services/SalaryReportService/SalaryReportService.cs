using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport;
using System;
using System.Linq;

namespace Catdog50RUS.EmployeesAccountingSystem.Reports.Services.SalaryReportService
{
    public class SalaryReportService : ISalaryReportService
    {
        private readonly ICompletedTaskLogs _taskLogsService;

        public SalaryReportService(ICompletedTaskLogs taskLogsservice)
        {
            _taskLogsService = taskLogsservice ?? throw new ArgumentNullException(nameof(taskLogsservice));
        }
        


        public SalaryReport GetEmployeeSalaryReport(BaseEmployee employee, (DateTime firstDate, DateTime lastDate) period)
        {
            IQueryable<CompletedTaskLog> employeeTasksLogList = _taskLogsService.GetEmployeeTaskLogs(employee.Id, 
                                                                                              period.firstDate, 
                                                                                              period.lastDate)
                                                                         .Result
                                                                         //.ToList()
                                                                         .Where(e=>e.IdEmployee==employee.Id)
                                                                         .AsQueryable();

            //Result
            var salaryReport = new SalaryReport(period.firstDate, period.lastDate, employee, employeeTasksLogList)
            {
                Header = $"Отчет по Заработной плате cотрудника: {employee}\nВ период с {period.firstDate:dd.MM.yyyy} по {period.lastDate:dd.MM.yyyy}\n",
                TotalSalary = employee.CalculateSamary(employeeTasksLogList)
            };

            return salaryReport;

        }




        
    }
}
