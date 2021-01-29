using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Components
{
    public class Authorization
    {
        //Поля

        private readonly IAutorize _autorizeService;

        private readonly bool _isFirstRun;

        public Authorization()
        {
            _autorizeService = new AutorizeService(new FileCSVEmployeeRepository());
            _isFirstRun = _autorizeService.IsFirstRun;
        }

        public bool IsFirstRun() => _isFirstRun;

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<(Autorize, BaseEmployee)> AutorezationUser()
        {

            //Аутентифицируем пользователя
            var employee = await GetEmployee();

            if (employee == null)
                return (null, null);

            //Получаем авторизацию
            var autorize = _autorizeService.GetAuthorization(employee);

            if (autorize == null)
                return (null, null);

            ShowOnConsole.ShowMessage($"Пользователь {employee} успешно авторизован!");
            ShowOnConsole.ShowContinue();
            return (autorize, employee);
        }

        public async Task<BaseEmployee> GetEmployee()
        {
            string name = GetEmployeeName();
            var employee = await _autorizeService.AutentificatedUser(name);
            return employee;
        }

        private string GetEmployeeName()
        {
            Console.Clear();
            //Получаем имя сотрудника
            string name = InputParameters.InputStringParameter("Введите имя пользователя");

            return name;

        }






    }
}
