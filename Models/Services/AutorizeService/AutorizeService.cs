using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public class AutorizeService : IAutorize
    {
        private readonly IEmployeeRepository _employeesRepository;

        public AutorizeService(IEmployeeRepository employeeRepository)
        {
            _employeesRepository = employeeRepository;
        }


        #region Interface

        public async Task<BaseEmployee> AutentificatedUser(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
            var employee = await _employeesRepository.GetEmployeeByNameAsync(name);
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

            return new Autorize(role, employee.Id);

        }
       
        #endregion

    }
}
