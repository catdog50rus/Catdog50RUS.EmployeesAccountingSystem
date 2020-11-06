using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Components
{
    public class Authorization
    {
        //Поля
        //TODO реализовать внедрение через интерфейс

        /// <summary>
        /// Внедрение бизнес логики
        /// </summary>
        private PersonsService PersonsService { get; }

        public Authorization()
        {
            PersonsService = new PersonsService();
        }


        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<Person> AutorezationUser()
        {
            Console.Clear();
            //Получаем имя сотрудника
            string name = InputParameters.InputStringParameter("Введите имя пользователя");
            //Получаем из хранилища сотрудника по имени и проверяем, если ли сотрудник с таким именем
            var person = await PersonsService.GetPersonByName(name);
            if (person != null)
            {
                ShowOnConsole.ShowError($"Пользователь {person} успешно авторизован!");
                ShowOnConsole.ShowContinue();
                return person;
                
            }
            else
            {
                ShowOnConsole.ShowError($"Пользователь с именем {name} не найден!");
                ShowOnConsole.ShowContinue();
                return null;
            }

        }
    }
}
