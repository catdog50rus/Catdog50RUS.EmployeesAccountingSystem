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
        private readonly static ISettingsRepository Settings = new ReportSettingsService();
        /// <summary>
        /// Внедрение сервиса отчетов
        /// </summary>
        private static SalaryReport Report;


        public static async Task GetPersonReport(Person person, (DateTime, DateTime) period)
        {
            await InitialReport();

            if (Report == null) return;
            //Получаем отчет и проверяем его на null и валидность числовых параметров
            //Выводим результат
            var personReport = await Report.GetPersonReport(person, period);
            if (personReport != null)
            {
                ShowOnConsole.ShowPersonTasks(period, personReport);
            }
            else
            {
                ShowError();
            }
            ShowOnConsole.ShowContinue();
        }

        public static async Task GetAllPersonsReport((DateTime, DateTime) period)
        {
            await InitialReport();
            if (Report == null) return;
            //Получаем отчет и проверяем его на null
            //Выводим результат
            var report = await Report.GetAllPersonsReport(period);
            if(report != null)
            {
                ShowOnConsole.ShowAllPersonsTasks(period, report); 
            }
            else
            {
                ShowError();
            }
            ShowOnConsole.ShowContinue();
        }

        public static async Task GetDepartmentsReport((DateTime, DateTime) period)
        {
            await InitialReport();
            if (Report == null) return;
            //Получаем отчет и проверяем его на null
            //Выводим результат
            var report = await Report.GetDepartmentsReport(period);
            if(report != null)
                ShowOnConsole.ShowDepartmetsTasks(period, report);
            else
            {
                ShowError();
            }
                
            ShowOnConsole.ShowContinue();
        }

        private static async Task InitialReport()
        {
            //Получаем настройки отчета
            var settings = await Settings.GetSettings();
            if (settings == null)
            {
                ShowOnConsole.ShowMessage("Настройки для отчета не найдены. Для получения отчета необходимо установить данные для расчета");
                ShowOnConsole.ShowContinue();
                return;
            }
            //Передаем в конструктор сервиса Сотрудника и период
            Report = new SalaryReport(settings);
        }

        private static void ShowError()
        {
            ShowOnConsole.ShowMessage("Ошибка получения отчета!");
            ShowOnConsole.ShowContinue();
            ShowOnConsole.ShowMessage("Возможно в заданном периоде нет выполненных задач!");
        }

    }
}
