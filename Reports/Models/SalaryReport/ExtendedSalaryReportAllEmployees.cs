using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport
{
    public class ExtendedSalaryReportAllEmployees
    {
        public string Header { get; set; }
        public IEnumerable<SalaryReport> EmployeeSalaryReports { get; }
        public double TotalTime { get; }
        public decimal TotalSalary { get;}


        public ExtendedSalaryReportAllEmployees(IEnumerable<SalaryReport> employeeSalaryReports)
        {
            EmployeeSalaryReports = employeeSalaryReports;
            TotalTime = employeeSalaryReports.Sum(x => x.TotalTime);
            TotalSalary = employeeSalaryReports.Sum(x => x.TotalSalary);
        }


    }
}
