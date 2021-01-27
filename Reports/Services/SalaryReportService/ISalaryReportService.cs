using Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Reports.Services.SalaryReportService
{
    public interface ISalaryReportService
    {
        
        SalaryReport GetEmployeeSalaryReport(BaseEmployee employee, (DateTime firstDate, DateTime lastDate) period);
        Task<SalaryReportPerAllEmployees> GetAllEmployeesSalaryReport((DateTime firstDate, DateTime lastDate) period);
        IEnumerable<SalaryReport> GetDepartmensSalaryReport(Departments department, (DateTime firstDate, DateTime lastDate) period);

    }
}
