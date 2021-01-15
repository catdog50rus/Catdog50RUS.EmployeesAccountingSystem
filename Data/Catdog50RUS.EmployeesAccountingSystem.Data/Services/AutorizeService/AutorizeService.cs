using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services.EmployeeService;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services.AutorizeService
{
    public class AutorizeService : IAutorize
    {
        private readonly IEmployeeRepository _employeesRepository;

        public AutorizeService(IEmployeeRepository employeeRepository)
        {
            _employeesRepository = employeeRepository;
        }

        public async Task<BaseEmployee> Autentificate(string name)
        {
            if (string.IsNullOrEmpty(name)||string.IsNullOrWhiteSpace(name))
                return null;
            var service = new EmployeeService.EmployeeService(_employeesRepository);
            var employee = await service.GetEmployeeByName(name);
            if (employee == null)
                return null;
         
            return employee;
        }

        public Autorize GetAuthorization(BaseEmployee employee)
        {
            if (employee == null)
                return null;
            var position = employee.Positions;
            var role = Role.None;
            switch (position)
            {
                case Positions.Developer:
                    role = Role.User;
                    break;
                case Positions.Director:
                    role = Role.Director;
                    break;
                case Positions.Freelance:
                    role = Role.User;
                    break;
                default:
                    break;
            }
            
            return new Autorize
            {
                IsAutentificated = true,
                AutorizeRole = role
            };
        }
    }
}
