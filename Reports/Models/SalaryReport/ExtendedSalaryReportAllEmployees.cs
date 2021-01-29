using System.Collections.Generic;
using System.Linq;

namespace Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport
{
    /// <summary>
    /// Реализация модели отчета по всем сотрудникам
    /// </summary>
    public class ExtendedSalaryReportAllEmployees
    {
        public string Header { get; set; }
        public IEnumerable<EmployeeSalaryReport> EmployeeSalaryReports { get; }
        public double TotalTime { get; }
        public decimal TotalSalary { get;}

        public ExtendedSalaryReportAllEmployees(IEnumerable<EmployeeSalaryReport> employeeSalaryReports)
        {
            EmployeeSalaryReports = employeeSalaryReports;
            TotalTime = employeeSalaryReports.Sum(x => x.TotalTime);
            TotalSalary = employeeSalaryReports.Sum(x => x.TotalSalary);
        }
    }
}
