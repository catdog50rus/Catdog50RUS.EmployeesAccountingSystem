using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Reports.Services.SalaryReportService;
using System;

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


        public void GetEmployeeReport(BaseEmployee employee, (DateTime, DateTime) period)
        {
            //await InitialReport();

            //if (Report == null) return;

            //Получаем отчет и проверяем его на null и валидность числовых параметров
            //Выводим результат
            var employeeReport = _salaryReportService.GetEmployeeSalaryReport(employee, period);
            if (employeeReport != null)
            {
                ShowOnConsole.ShowPersonTasks(period, employeeReport);
            }
            else
            {
                ShowError();
            }
            ShowOnConsole.ShowContinue();
        }

        //public static async Task GetAllPersonsReport((DateTime, DateTime) period)
        //{
        //    await InitialReport();
        //    if (Report == null) return;
        //    //Получаем отчет и проверяем его на null
        //    //Выводим результат
        //    var report = await Report.GetAllPersonsReport(period);
        //    if(report != null)
        //    {
        //        ShowOnConsole.ShowAllPersonsTasks(period, report); 
        //    }
        //    else
        //    {
        //        ShowError();
        //    }
        //    ShowOnConsole.ShowContinue();
        //}

        //public static async Task GetDepartmentsReport((DateTime, DateTime) period)
        //{
        //    await InitialReport();
        //    if (Report == null) return;
        //    //Получаем отчет и проверяем его на null
        //    //Выводим результат
        //    var report = await Report.GetDepartmentsReport(period);
        //    if(report != null)
        //        ShowOnConsole.ShowDepartmetsTasks(period, report);
        //    else
        //    {
        //        ShowError();
        //    }
                
        //    ShowOnConsole.ShowContinue();
        //}

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
