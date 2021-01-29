using System;
using System.Collections.Generic;
using System.Linq;

namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    /// <summary>
    /// Реализация модели штатного сотрудника
    /// </summary>
    public class StaffEmployee : BaseEmployee
    {
        //Конструкторы
        public StaffEmployee(string name, string surname, Departments department, decimal baseSalary)
                            : base(name, surname, department, baseSalary)
        {
            Position = Positions.Developer;
        }
        public StaffEmployee(Guid id, string name, string surname, Departments department,  decimal baseSalary) 
                            : base(id, name, surname, department, baseSalary)
        {
            Position = Positions.Developer;
        }
        public StaffEmployee(string name, string surname, Departments department, Positions pos, decimal baseSalary)
                            : base(name, surname, department, pos, baseSalary)
        {
            
        }
        public StaffEmployee(Guid id, string name, string surname, Departments department, Positions pos, decimal baseSalary)
                            : base(id, name, surname, department, pos, baseSalary)
        {
            
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
            var salaryInHour = BaseSalary / (decimal)NumberWorkingHoursPerMonth;
            //Получаем список логов сгруппированный по дням 
            var tasksLogGroupByDays = tasksLog.GroupBy(d => d.Date.ToShortDateString());
            //Запускаем цикл подсчета общей зарплаты
            foreach (var log in tasksLogGroupByDays)
            {
                //Суммируем общее рабочее время в день
                var totalTimePerDay = log.Sum(t => t.Time);
                //Считаем была ли переработка
                var overtime = totalTimePerDay - NumberWoringHoursPerDay;
                //Если переработка была, считаем результат как (стандартный рабочий день + удовоенное время переработки)* часовую ставку
                //Иначе просто перемножаем рабочее время на часовую ставку
                if (overtime > 0)
                    totalSalary += (decimal)(NumberWoringHoursPerDay + overtime * BonusCoefficient) * salaryInHour;
                else
                    totalSalary += (decimal)(totalTimePerDay) * salaryInHour;
            }
            //Возвращаем результат
            return totalSalary;
        }
    }
}
