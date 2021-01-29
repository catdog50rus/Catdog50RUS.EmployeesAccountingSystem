using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Reports.Services.SalaryReportService
{
    public interface ISalaryReportService
    {
        /// <summary>
        /// Получить отчет по сотруднику
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        Task<EmployeeSalaryReport> GetEmployeeSalaryReport(BaseEmployee employee, (DateTime firstDate, DateTime lastDate) period);
        /// <summary>
        /// Получить отчет по всем сотрудникам
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        Task<ExtendedSalaryReportAllEmployees> GetAllEmployeesSalaryReport((DateTime firstDate, DateTime lastDate) period);
        /// <summary>
        /// Получить отчет по всем отделам
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        Task<ExtendedSalaryReportAllDepatments> GetAllDepatmentsSalaryReport((DateTime firstDate, DateTime lastDate) period);

        //Task<ExtendedSalaryReportAllEmployees> GetDepartmensSalaryReport(Departments department, (DateTime firstDate, DateTime lastDate) period);
    }
}
