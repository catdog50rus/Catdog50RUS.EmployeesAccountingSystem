using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Models;
using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Components
{
    /// <summary>
    /// Компонент UI
    /// Получаем нового сотрудника
    /// </summary>
    class CreateNewEmployee
    {
        /// <summary>
        /// Создать нового сотрудника
        /// </summary>
        /// <returns></returns>
        public static Employee CreateNewPerson()
        {
            Console.Clear();
            Console.WriteLine("Добавление нового пользователя");
            Console.WriteLine();

            //Получаем данные от пользователя используя компоненты UI
            string name = InputParameters.InputStringParameter("Введите имя сотрудника");
            string surname = InputParameters.InputStringParameter("Введите фамилию сотрудника");
            Departments dep = InputParameters.InputDepartment();
            Positions pos = InputParameters.InputPosition(dep);
            decimal baseSalary = InputParameters.InputDecimlParameter("Введите базовую ставку сотрудника");

            //Возвращаем нового сотрудника DTO
            return new Employee 
            {
                Id = Guid.NewGuid(),
                NamePerson = name,
                SurnamePerson = surname,
                Department = dep,
                Positions = pos,
                BaseSalary = baseSalary
            };
        } 
    }
}
