using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    /// <summary>
    /// Реализация сервиса авторизации
    /// </summary>
    public class AutorizeService : IAutorize
    {
        /// <summary>
        /// Внедрение репозитория сотрудников
        /// </summary>
        private readonly IEmployeeRepository _employeesRepository;
        /// <summary>
        /// Флаг первого запуска приложения
        /// </summary>
        public bool IsFirstRun { get; }

        public AutorizeService(IEmployeeRepository employeeRepository)
        {
            _employeesRepository = employeeRepository;
            IsFirstRun = employeeRepository.IsFirstRun();
        }

        #region Interface

        /// <summary>
        /// Аутентифицировать сотрудника
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<BaseEmployee> AutentificatedUser(string name)
        {
            //Проверяем входные параметры
            if (string.IsNullOrWhiteSpace(name))
                return null;
            //Получаем сотрудника и проверяем его на Null
            var employee = await _employeesRepository.GetEmployeeByNameAsync(name);
            if (employee == null)
                return null;

            return employee;
        }
        /// <summary>
        /// Дать авторизацию сотруднику
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public AutorizeToken GetAuthorization(BaseEmployee employee)
        {
            //Проверяем входные параметры
            if (employee == null)
                return null;
            //Получаем должность сотрудника
            var position = employee.Position;
            //Присваиваем роль в соответствии с должностью
            var role = Role.None;
            switch (position)
            {
                case Positions.Developer:
                    role = Role.Developer;
                    break;
                case Positions.Director:
                    role = Role.Director;
                    break;
                case Positions.Freelance:
                    role = Role.Freelancer;
                    break;
                default:
                    break;
            }

            return new AutorizeToken(role, employee.Id);
        }
       
        #endregion

    }
}
