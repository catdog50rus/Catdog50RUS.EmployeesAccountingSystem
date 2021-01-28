using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Reports.Services.SalaryReportService
{
    public interface ISalaryReportService
    {
        
        Task<SalaryReport> GetEmployeeSalaryReport(Guid id, (DateTime firstDate, DateTime lastDate) period);
        Task<ExtendedSalaryReportAllEmployees> GetAllEmployeesSalaryReport((DateTime firstDate, DateTime lastDate) period);
        //Task<ExtendedSalaryReportAllEmployees> GetDepartmensSalaryReport(Departments department, (DateTime firstDate, DateTime lastDate) period);
        Task<ExtendedSalaryReportAllDepatments> GetAllDepatmentsSalaryReport((DateTime firstDate, DateTime lastDate) period);
    }
}
