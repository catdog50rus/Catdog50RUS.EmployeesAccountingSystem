using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Components
{
    /// <summary>
    /// Компонент UI
    /// Получаем новую задачу
    /// </summary>
    class CreateTask
    {
        public static async Task<CompletedTaskLog> CreatNewTask(Person person)
        {
            Console.Clear();
            
            //Проверяем, если пользователь - Директор, то он может загрузить данные для любого сотрудника
            if (person.Positions.Equals(Positions.Director))
            {

                ShowSelectUserMenu();
                var newperson = await SelectPerson();
                if (newperson != null)
                    person = newperson;
                Console.Clear();

            }
            //Получаем данные от пользователя используя компоненты UI

            //Проверяем, чтобы введенная дата не была будущей 
            DateTime date = DateTime.Today.AddDays(1);
            while(date > DateTime.Today)
            {
                //Если пользователь фрилансер, проверяем, чтобы дата была не позднее,
                //чем за два дня до сегодняшней
                if (person.Positions.Equals(Positions.Freelance))
                {
                    while (date < DateTime.Today.AddDays(-2))
                    {
                        date = InputParameters.InputDateParameter("Введите дату выполнения задачи");
                    }
                }
                else
                    date = InputParameters.InputDateParameter("Введите дату выполнения задачи");
            }

            string taskName = InputParameters.InputStringParameter("Введите наименование задачи");
            double time = InputParameters.InputDoubleParameter("Введите затраченное время в часах (например: 3,5)");
            //Возвращаем задачу
            return new CompletedTaskLog(person.IdPerson, date, time, taskName);
        }

        private static async Task<Person> SelectPerson()
        {
            Console.WriteLine();
            Person person = default;
            var key = Console.ReadKey().KeyChar;
            switch (key)
            {
                case '1':
                    person = await new Authorization().GetPerson();
                    break;
                default:
                    break;
            }
            return person;


        }

        private static void ShowSelectUserMenu()
        {
            Console.WriteLine("Выберете сотрудника для ввода выполненной задачи:");
            Console.WriteLine();
            Console.WriteLine("1 - Выбрать другого сотрудника");
            Console.WriteLine("Любая клавиши - продолжить");
        }
    }
}
