using System;
using System.Collections.Generic;
using System.Linq;

namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    /// <summary>
    /// Реализация модели фрилансера
    /// </summary>
    public class FreeLancerEmployee : BaseEmployee
    {
        //Конструкторы
        public FreeLancerEmployee(string name, string surname, Departments dep, decimal baseSalary) 
                           : base(name, surname, dep, baseSalary)
        {
            Position = Positions.Freelance;
        }
        public FreeLancerEmployee(Guid id, string name, string surname, Departments dep, decimal baseSalary) 
                           : base(id, name, surname, dep,  baseSalary)
        {
            Position = Positions.Freelance;
        }

        /// <summary>
        /// Переопределенный метод подсчета заработной платы
        /// </summary>
        /// <param name="tasksLog">Список логов</param>
        /// <returns>Заработанная плата</returns>
        public override decimal CalculateSamary(IEnumerable<CompletedTaskLog> tasksLog)
        {
            //Получаем общее рабочее время
            var totalTimePerDay = tasksLog.Sum(t => t.Time);
            //Возвращаем результат
            //Результат = Рабочее время * базовую ставку в час
            return (decimal)totalTimePerDay * BaseSalary;
        }
    }
}
