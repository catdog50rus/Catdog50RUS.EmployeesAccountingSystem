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

        /// <summary>
        /// Внедрение бизнес логики
        /// </summary>
        private readonly IPersons _personsService;
        private readonly bool _isFirstRun;

        public Authorization()
        {
            _personsService = new PersonsService();
            _isFirstRun = _personsService.IsFirstRun;
        }

        public bool IsFirstRun() => _isFirstRun;

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<Person> AutorezationUser()
        {
            var person = await GetPersonByName();
            if (person != null)
            {
                ShowOnConsole.ShowMessage($"Пользователь {person} успешно авторизован!");
                ShowOnConsole.ShowContinue();
                return person;
                
            }
            else
            {
                return null;
            }

        }

        public async Task<Person> GetPerson()
        {
            return await GetPersonByName();
        }

        private async Task<Person> GetPersonByName()
        {
            Console.Clear();
            //Получаем имя сотрудника
            string name = InputParameters.InputStringParameter("Введите имя пользователя");
            //Получаем из хранилища сотрудника по имени и проверяем, если ли сотрудник с таким именем
            var person = await _personsService.GetPersonByName(name);
            if (person == null)
            {
                ShowOnConsole.ShowMessage($"Пользователь с именем {name} не найден!");
                ShowOnConsole.ShowContinue();
                return null;

            }
            else
            {
                return person;    
            }
        }


    }
}
