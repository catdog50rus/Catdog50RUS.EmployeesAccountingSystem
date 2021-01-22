using System;
using System.Collections.Generic;
using System.Linq;

namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    public class DirectorEmployee : BaseEmployee
    {
        public decimal Bonus { get; }

        public DirectorEmployee(string name, string surname, Departments dep, decimal baseSalary, decimal bonus = 20_000) 
                         : base(name, surname, dep, baseSalary)
        {
            Positions = Positions.Director;
            Bonus = bonus;
        }

        public DirectorEmployee(Guid id, string name, string surname, Departments dep, decimal baseSalary, decimal bonus = 20_000) 
                         : base(id, name, surname, dep, baseSalary)
        {
            Positions = Positions.Director;
            Bonus = bonus;
        }

        /// <summary>
        /// Переопределенный метод подсчета заработной платы
        /// </summary>
        /// <param name="tasksLog">Список логов</param>
        /// <returns>Заработанная плата</returns>
        public override decimal CalculateSamary(IEnumerable<CompletedTaskLog> tasksLog)
        {
            //Всего заработано
            var totalSalary = 0M;
            //Ставка за час работы
            var salaryInHour = BaseSalary / SalaryReportSettings.NUMBER_WORKING_HOURS_PER_MONTH;
            //Бонус за рабочий день
            var bonusPerDay = Bonus / SalaryReportSettings.NUMBER_WORKING_DAYS_PER_MONTH;

            //Получаем список логов сгруппированный по дням 
            var tasksLogGroupByDays = tasksLog.GroupBy(d => d.Date.ToShortDateString());

            //Запускаем цикл подсчета общей зарплаты
            foreach (var log in tasksLogGroupByDays)
            {
                //Суммируем общее рабочее время в день
                var totalTimePerDay = log.Sum(t => t.Time);

                //Считаем была ли переработка
                var overtime = SalaryReportSettings.NUMBER_WORKING_HOURS_PER_DAY - totalTimePerDay;

                //Если переработка была, считаем результат как рабочее время * на часовую ставку + бонус, 
                //рассчитанный как месячный бонус / количество рабочих дней
                //
                //Иначе просто перемножаем рабочее время на часовую ставку
                if (overtime > 0)
                    totalSalary += (decimal)(totalTimePerDay) * salaryInHour + bonusPerDay;
                else
                    totalSalary += (decimal)(totalTimePerDay) * salaryInHour;
            }
            //Возвращаем результат
            return totalSalary;
        }
    }
}
