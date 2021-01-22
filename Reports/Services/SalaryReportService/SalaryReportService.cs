using Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;
using System.Threading.Tasks;

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
                Header = "",
                TotalSalary = 0
            };

            return salaryReport;

        }




        
    }
}
