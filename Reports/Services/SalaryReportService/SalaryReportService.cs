using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.Reports.Services.SalaryReportService
{
    public class SalaryReportService : ISalaryReportService
    {
        /// <summary>
        /// Инжекция сервиса получения логов
        /// </summary>
        private readonly ICompletedTaskLogsService _taskLogsService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="taskLogsService"></param>
        public SalaryReportService(ICompletedTaskLogsService taskLogsService)
        {
            _taskLogsService = taskLogsService ?? throw new ArgumentNullException(nameof(taskLogsService));
        }

        #region Интерфейс

        /// <summary>
        /// Получить отчет по зарплате сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <param name="period">Период</param>
        /// <returns>Возвращает отчет</returns>
        public SalaryReport GetEmployeeSalaryReport(BaseEmployee employee, (DateTime firstDate, DateTime lastDate) period)
        {
            //Получаем список логов
            var employeeTasksLogList = _taskLogsService.GetEmployeeTaskLogs(employee.Id, period.firstDate, period.lastDate).Result;
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
        
        #endregion

    }
}
