using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public interface IAutorize
    { 
        /// <summary>
        /// Флаг первого запуска приложения
        /// </summary>
        public bool IsFirstRun { get; }
        /// <summary>
        /// Аутентифицировать сотрудника
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseEmployee> AutentificatedUser(string name);
        /// <summary>
        /// Дать авторизацию сотруднику
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        AutorizeToken GetAuthorization(BaseEmployee employee);   
    }

}
