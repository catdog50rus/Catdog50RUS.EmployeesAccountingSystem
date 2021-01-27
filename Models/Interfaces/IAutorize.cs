using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public interface IAutorize
    { 
        Task<BaseEmployee> AutentificatedUser(string name);
        Autorize GetAuthorization(BaseEmployee employee);   
    }

}
