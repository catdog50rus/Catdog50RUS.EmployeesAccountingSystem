using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Components
{
    /// <summary>
    /// Компонент UI
    /// Сервис авторизации
    /// </summary>
    public class Authorization
    {
        #region Fields & Constructors

        /// <summary>
        /// Внедрение сервиса авторизации
        /// </summary>
        private readonly IAutorize _autorizeService;
        /// <summary>
        /// Флаг первого запуска программы
        /// </summary>
        private readonly bool _isFirstRun;
        /// <summary>
        /// Конструктор
        /// </summary>
        public Authorization()
        {
            _autorizeService = new AutorizeService(new FileCSVEmployeeRepository());
            _isFirstRun = _autorizeService.IsFirstRun;
        }

        #endregion

        /// <summary>
        /// Получения флага первого запуска программы
        /// </summary>
        /// <returns></returns>
        public bool IsFirstRun() => _isFirstRun;
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<(Autorize, BaseEmployee)> AutorezationUser()
        {
            //Аутентифицируем пользователя и проверяем результат
            var employee = await GetEmployee();
            if (employee == null)
                return (null, null);

            //Получаем авторизацию и проверяем результат
            var autorize = _autorizeService.GetAuthorization(employee);
            if (autorize == null)
                return (null, null);

            //Выводим на экран текстовые данные
            ShowOnConsole.ShowMessage($"Пользователь {employee} успешно авторизован!");
            ShowOnConsole.ShowContinue();
            return (autorize, employee);
        }
        /// <summary>
        /// Получить аутентифицированного сотрудника
        /// </summary>
        /// <returns></returns>
        public async Task<BaseEmployee> GetEmployee()
        {
            //Получаем имя сотрудника
            string name = GetEmployeeName();
            //Аутентифицируем сотрудника по имени
            var employee = await _autorizeService.AutentificatedUser(name);
            if (employee == null)
                return null;

            return employee;
        }
        /// <summary>
        /// Получить имя
        /// </summary>
        /// <returns></returns>
        private string GetEmployeeName()
        {
            Console.Clear();
            //Получаем имя сотрудника
            string name = InputParameters.InputStringParameter("Введите имя пользователя");

            return name;
        }
    }
}
