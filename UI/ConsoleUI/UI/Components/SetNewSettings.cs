using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Models.Settings;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI
{
    class SetNewSettings
    {
        public static ReportSettings CreateNewSettings()
        {
            Console.Clear();
            int normTimeInMonth = InputParameters.InputIntegerParameter("Введите норму часов в месяц (целое число)");
            decimal bonusDirector = InputParameters.InputDecimlParameter("Введите бонус директора");
            decimal bonusCoeff = InputParameters.InputDecimlParameter("Введите коэффициент за переработку сотрудника");

            return new ReportSettings(normTimeInMonth, bonusDirector, bonusCoeff);
        }
    }
}
