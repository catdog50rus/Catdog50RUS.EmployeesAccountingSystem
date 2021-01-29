﻿using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public interface IAutorize
    { 
        public bool IsFirstRun { get; }

        Task<BaseEmployee> AutentificatedUser(string name);
        Autorize GetAuthorization(BaseEmployee employee);   
    }

}
