using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Reports.Services.SalaryReportService;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class Reports
    {
        //private readonly static IReportSettingsRepository Settings = new ReportSettingsService();
        private readonly ISalaryReportService _salaryReportService;

        public Reports(ISalaryReportService salaryReportService)
        {
            _salaryReportService = salaryReportService;
        }


        public async Task GetEmployeeReport(Guid id, (DateTime, DateTime) period)
        {
            //Получаем отчет и проверяем его на null и валидность числовых параметров
            //Выводим результат
            var employeeReport = await _salaryReportService.GetEmployeeSalaryReport(id, period);
            if (employeeReport != null)
            {
                ShowOnConsole.ShowPersonTasks(employeeReport);
            }
            else
            {
                ShowError();
            }
            ShowOnConsole.ShowContinue();
        }

        public async Task GetAllPersonsReport((DateTime, DateTime) period)
        {
            //Получаем отчет и проверяем его на null
            //Выводим результат

            var report = await _salaryReportService.GetAllEmployeesSalaryReport(period);
            if (report != null)
            {
                ShowOnConsole.ShowAllPersonsReport(report);
            }
            else
            {
                ShowError();
            }
            ShowOnConsole.ShowContinue();
        }

        public async Task GetAllDepartmentsReport((DateTime, DateTime) period)
        {
            //Получаем отчет и проверяем его на null
            //Выводим результат
            var report = await _salaryReportService.GetAllDepatmentsSalaryReport(period);
            if(report != null)
                ShowOnConsole.ShowDepartmetsReport(report);
            else
            {
                ShowError();
            }

            ShowOnConsole.ShowContinue();
        }

        //private static async Task InitialReport()
        //{
        //    //Получаем настройки отчета
        //    var settings = await Settings.GetSettings();
        //    if (settings == null)
        //    {
        //        ShowOnConsole.ShowMessage("Настройки для отчета не найдены. Для получения отчета необходимо установить данные для расчета");
        //        ShowOnConsole.ShowContinue();
        //        return;
        //    }
        //    //Передаем в конструктор сервиса Сотрудника и период
        //    Report = new SalaryReportOld(settings);
        //}

        private static void ShowError()
        {
            ShowOnConsole.ShowMessage("Ошибка получения отчета!");
            ShowOnConsole.ShowContinue();
            ShowOnConsole.ShowMessage("Возможно в заданном периоде нет выполненных задач!");
        }

    }
}
