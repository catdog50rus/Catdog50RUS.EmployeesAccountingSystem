using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services.AutorizeService
{
    public interface IAutorize
    {
        Autorize GetAuthorization(EmployeesBase employee);
        Task<EmployeesBase> Autentificate(string name);
    }

}
