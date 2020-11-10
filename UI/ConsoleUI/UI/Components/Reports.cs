using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Reports.SalaryReports;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    static class Reports
    {
        private static ISettingsRepository Settings { get; } = new ReportSettingsService();
        /// <summary>
        /// Внедрение сервиса отчетов
        /// </summary>
        private static SalaryReport Report;


        public static async Task GetPersonReport(Person person, (DateTime, DateTime) period)
        {
            await InitialReport();
            //Получаем отчет и проверяем его на null и валидность числовых параметров
            //Выводим результат
            var personReport = await Report.GetPersonReport(person, period);
            if (personReport.Item3 != null)
            {
                if (personReport.Item1 >= 0 && personReport.Item2 >= 0)
                    ShowOnConsole.ShowPersonTasks(person, period, personReport);
            }
            else
            {
                ShowOnConsole.ShowMessage("Ошибка получения отчета!");
            }
            ShowOnConsole.ShowContinue();
        }

        public static async Task GetAllPersonsReport((DateTime, DateTime) period)
        {
            await InitialReport();
            //Получаем отчет и проверяем его на null и валидность числовых параметров
            //Выводим результат
            var report = await Report.GetAllPersonsReport(period);
            if(report != null)
            {
                ShowOnConsole.ShowAllPersonsTasks(period, report); 
            }
            else
            {
                ShowOnConsole.ShowMessage("Ошибка получения отчета!");
            }
            ShowOnConsole.ShowContinue();
        }

        private static async Task InitialReport()
        {
            //Получаем настройки отчета
            var settings = await Settings.GetSettings();
            if (settings == null)
            {
                ShowOnConsole.ShowMessage("Настройки для отчета не найдены");
                ShowOnConsole.ShowMessage("Для получения отчета необходимо установить данные для расчета");
                return;
            }
            //Передаем в конструктор сервиса Сотрудника и период
            Report = new SalaryReport(settings);
        }

    }
}
