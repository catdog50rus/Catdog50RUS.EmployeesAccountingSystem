﻿using System.Collections.Generic;
using System.Linq;

namespace Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport
{
    /// <summary>
    /// Реализация модели отчета по отделам
    /// </summary>
    public class ExtendedSalaryReportAllDepatments
    {
        public string Header { get; set; }
        public IEnumerable<ExtendedSalaryReportAllEmployees> EmployeeSalaryReports { get; }
        public double TotalTime { get; }
        public decimal TotalSalary { get; }

        public ExtendedSalaryReportAllDepatments(IEnumerable<ExtendedSalaryReportAllEmployees> employeeSalaryReports)
        {
            EmployeeSalaryReports = employeeSalaryReports;
            TotalTime = employeeSalaryReports.Sum(x => x.TotalTime);
            TotalSalary = employeeSalaryReports.Sum(x => x.TotalSalary);
        }
    }
}
