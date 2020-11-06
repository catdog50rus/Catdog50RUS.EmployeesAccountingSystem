using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Components
{
    /// <summary>
    /// Компонент UI
    /// Получаем нового сотрудника
    /// </summary>
    class CreatePerson
    { 
        public static Person CreateNewPerson()
        {
            Console.Clear();

            Console.WriteLine("Добавление нового пользователя");
            Console.WriteLine();
            //Получаем данные от пользователя используя компоненты UI
            string name = InputParameters.InputStringParameter("Введите имя сотрудника");
            string surname = InputParameters.InputStringParameter("Введите фамилию сотрудника");
            Departments dep = InputParameters.InputDepartment();
            Positions pos = InputParameters.InputPosition();
            decimal baseSalary = InputParameters.InputDecimlParameter("Введите базовую ставку сотрудника");
            //Возвращаем нового сотрудника
            return new Person(name, surname, dep, pos, baseSalary);

        } 
    }
}
