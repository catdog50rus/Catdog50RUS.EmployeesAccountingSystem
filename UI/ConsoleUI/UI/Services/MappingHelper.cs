using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services
{
    /// <summary>
    /// Вспомогательный класс преобразование моделей в DTO и обратно
    /// </summary>
    static class MappingHelper
    {
        /// <summary>
        /// Преобразование бизнес модели сотрудника в DTO
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Employee ToEmployeeModel(this BaseEmployee e)
        {
            return new Employee
            {
                Id = e.Id,
                NamePerson = e.NamePerson,
                SurnamePerson = e.SurnamePerson,
                Department = e.Department,
                Positions = e.Positions,
                BaseSalary = e.BaseSalary
            };
        }
        /// <summary>
        /// Преобразование DTO сотрудника в бизнес модель
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static BaseEmployee ToBaseEmployee(this Employee e)
        {
            var position = e.Positions;
            BaseEmployee employee = default;

            switch (position)
            {
                case Positions.None:
                    break;
                case Positions.Director:
                    employee = new DirectorEmployee(e.Id, e.NamePerson, e.SurnamePerson, e.Department, e.BaseSalary);
                    break;
                case Positions.Developer:
                    employee = new StaffEmployee(e.Id, e.NamePerson, e.SurnamePerson, e.Department, e.BaseSalary);
                    break;
                case Positions.Freelance:
                    employee = new FreeLancerEmployee(e.Id, e.NamePerson, e.SurnamePerson, e.Department, e.BaseSalary);
                    break;
                default:
                    break;
            }
            return employee;
        }
        /// <summary>
        /// Преобразование DTO лога в бизнес модель
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static CompletedTaskLog ToCompletedTaskLog(this TaskLog log)
        {
            return new CompletedTaskLog(log.IdEmployee, log.Date, log.Time, log.TaskName);
        }
        /// <summary>
        /// Преобразование бизнес модель лога в DTO 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static TaskLog ToTaskLogModel(this CompletedTaskLog log)
        {
            return new TaskLog
            {
                Date = log.Date,
                IdEmployee = log.IdEmployee,
                TaskName = log.TaskName,
                Time = log.Time
            };
        }
    }
}
