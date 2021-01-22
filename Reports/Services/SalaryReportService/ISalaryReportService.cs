using Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.Reports.Services.SalaryReportService
{
    public interface ISalaryReportService
    {
        SalaryReport GetEmployeeSalaryReport(BaseEmployee employee, (DateTime firstDate, DateTime lastDate) period);

    }
}
