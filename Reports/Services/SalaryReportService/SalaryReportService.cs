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
        public SalaryReportService(ICompletedTaskLogsService taskLogsService, IEmployeeService employeeService)
        {
            _taskLogsService = taskLogsService ?? throw new ArgumentNullException(nameof(taskLogsService));
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

        /// <summary>
        /// Получить расширенный отчет по всем сотрудникам
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public async Task<ExtendedSalaryReportAllEmployees> GetAllEmployeesSalaryReport((DateTime firstDate, DateTime lastDate) period)
        {
            //Получаем логи времени и проверяем на null
            var timesLogs = await _taskLogsService.GetCompletedTaskLogs(period.firstDate, period.lastDate);
            if (timesLogs == null)
                return null;

            //Группируем логи по сотруднику
            var employeesTimeLogs = timesLogs.GroupBy(e => e.IdEmployee);

            //Создаем список отчетов по сотруднику
            List<SalaryReport> employeeSalaryReport = new List<SalaryReport>();
            foreach (var log in employeesTimeLogs)
            {
                //Получаем сотрудника по id, проверяем на null
                var employeeId = log.Key;
                var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
                if (employee == null)
                    break;

                //Получаем список логов сотрудника
                var employeeTimeLogs = log.ToList();

                //Создаем отчет по сотруднику и добавляем его в список
                var salaryReport = new SalaryReport(period.firstDate, period.lastDate, employee, employeeTimeLogs)
                {
                    Header = $"Cотрудника: {employee}\n ",
                    TotalSalary = employee.CalculateSamary(employeeTimeLogs)
                };
                employeeSalaryReport.Add(salaryReport);
            }
            //Проверяем, что список отчетов по сотрудникам не пустой
            if (employeeSalaryReport.Count == 0)
                return null;

            //Создаем расширенный отчет по сотрудникам и возвращаем результат
            var result = new ExtendedSalaryReportAllEmployees(employeeSalaryReport)
            {
                Header = $"Отчет по Заработной плате сотрудников \nВ период с {period.firstDate:dd.MM.yyyy} по {period.lastDate:dd.MM.yyyy}\n"
            };

            return result;
        }

        /// <summary>
        /// Получить расширенный отчет по отделам
        /// </summary>
        /// <param name="department"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public async Task<ExtendedSalaryReportAllDepatments> GetAllDepatmentsSalaryReport((DateTime firstDate, DateTime lastDate) period)
        {

            var allEmployeeReport = await GetAllEmployeesSalaryReport(period);
            if (allEmployeeReport == null)
                return null;

            var depatmentsReport = allEmployeeReport.EmployeeSalaryReports.GroupBy(x => x.Employee.Department);


            List<ExtendedSalaryReportAllEmployees> depatmentSalaryReport = new List<ExtendedSalaryReportAllEmployees>();
            foreach (var item in depatmentsReport)
            {
                //Получаем сотрудника по id, проверяем на null и фильтруем по принадлежности к необходимому отделу
                var depatment = item.Key;

                //Получаем список отчетов по сотрудникам отдела
                var employeeSalaryReport = item.ToList();


                //Создаем расширенный отчет по сотрудникам и возвращаем результат
                var depatmentEmployeesReport = new ExtendedSalaryReportAllEmployees(employeeSalaryReport)
                {
                    Header = $"По отделу {depatment}:\n"
                };



                depatmentSalaryReport.Add(depatmentEmployeesReport);
            }
            //Проверяем, что список отчетов по сотрудникам не пустой
            if (depatmentSalaryReport.Count == 0)
                return null;

            //Создаем расширенный отчет по отделу и возвращаем результат
            var result = new ExtendedSalaryReportAllDepatments(depatmentSalaryReport)
            {
                Header = $"Отчет по Заработной плате сотрудников \nВ период с {period.firstDate:dd.MM.yyyy} по {period.lastDate:dd.MM.yyyy} :\n "
            };

            return result;
        }

        ///// <summary>
        ///// Получить расширенный отчет по отделу
        ///// </summary>
        ///// <param name="department"></param>
        ///// <param name="period"></param>
        ///// <returns></returns>
        //public async Task<ExtendedSalaryReportAllEmployees> GetDepartmensSalaryReport(Departments department, (DateTime firstDate, DateTime lastDate) period)
        //{
        //    //Получаем логи времени и проверяем на null
        //    var timesLogs = await _taskLogsService.GetCompletedTaskLogs(period.firstDate, period.lastDate);
        //    if (timesLogs == null)
        //        return null;

        //    //Группируем логи по сотруднику
        //    var employeesTimeLogs = timesLogs.GroupBy(d => d.IdEmployee);

        //    //Создаем список отчетов по сотруднику
        //    List<SalaryReport> depatmentSalaryReport = new List<SalaryReport>();
        //    foreach (var log in employeesTimeLogs)
        //    {
        //        //Получаем сотрудника по id, проверяем на null и фильтруем по принадлежности к необходимому отделу
        //        var employeeId = log.Key;
        //        var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
        //        if (employee == null || !employee.Department.Equals(department))
        //            break;

        //        //Получаем список логов сотрудника нужного отдела
        //        var employeeTimeLogs = log.ToList();

        //        //Создаем отчет по сотруднику и добавляем его в список
        //        var salaryReport = new SalaryReport(period.firstDate, period.lastDate, employee, employeeTimeLogs)
        //        {
        //            Header = $"Cотрудника: {employee}\n ",
        //            TotalSalary = employee.CalculateSamary(employeeTimeLogs)
        //        };
        //        depatmentSalaryReport.Add(salaryReport);
        //    }
        //    //Проверяем, что список отчетов по сотрудникам не пустой
        //    if (depatmentSalaryReport.Count == 0)
        //        return null;

        //    //Создаем расширенный отчет по отделу и возвращаем результат
        //    var result = new ExtendedSalaryReportAllEmployees(depatmentSalaryReport)
        //    {
        //        Header = $"Отчет по Заработной плате сотрудников отдела {department} \nВ период с {period.firstDate:dd.MM.yyyy} по {period.lastDate:dd.MM.yyyy} :\n "
        //    };

        //    return result;
        //}

        #endregion

    }
}
