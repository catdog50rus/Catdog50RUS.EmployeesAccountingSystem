using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Reports.Services.SalaryReportService;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    /// <summary>
    /// Получить отчеты
    /// </summary>
    class Reports
    {
        #region Field & Constructors

        /// <summary>
        /// Внедрения сервиса отчетов
        /// </summary>
        private readonly ISalaryReportService _salaryReportService;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="salaryReportService"></param>
        public Reports(ISalaryReportService salaryReportService)
        {
            _salaryReportService = salaryReportService;
        }

        #endregion

        /// <summary>
        /// Получить отчет по сотруднику
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public async Task GetEmployeeReport(BaseEmployee employee, (DateTime, DateTime) period)
        {
            //Получаем отчет и проверяем его на null
            var report = await _salaryReportService.GetEmployeeSalaryReport(employee, period);
            if(report == null)
                ShowError();
           
            //Вывод отчета на консоль
            report.ShowEmployeeSalaryReport();
            ShowOnConsole.ShowContinue();
        }
        /// <summary>
        /// Получить отчет по всем сотрудникам
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public async Task GetAllPersonsReport((DateTime, DateTime) period)
        {
            //Получаем отчет и проверяем его на null
            var report = await _salaryReportService.GetAllEmployeesSalaryReport(period);
            if(report==null)
                ShowError();

            //Выводим результат на консоль
            report.ShowAllEmployeeSalaryReport();
            ShowOnConsole.ShowContinue();
        }
        /// <summary>
        /// Получить отчет по отделам
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public async Task GetAllDepartmentsReport((DateTime, DateTime) period)
        {
            //Получаем отчет и проверяем его на null
            var report = await _salaryReportService.GetAllDepatmentsSalaryReport(period);
            if(report == null)
                ShowError();
            
            //Вывести отчет на консоль
            report.ShowDepartmetsSalaryReport();
            ShowOnConsole.ShowContinue();
        }

        /// <summary>
        /// Вывести на консоль сообщение об ошибке
        /// </summary>
        private static void ShowError()
        {
            ShowOnConsole.ShowMessage("Ошибка получения отчета!");
            ShowOnConsole.ShowContinue();
            ShowOnConsole.ShowMessage("Возможно в заданном периоде нет выполненных задач!");
        }

    }
}
