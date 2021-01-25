using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repository.NUnitTests
{
    [TestFixture]
    class EmployeeRepositoryTests
    {
        private List<BaseEmployee> _employeeList;
        private IEmployeeRepository _repository;

        public EmployeeRepositoryTests()
        {
            _employeeList = new List<BaseEmployee> { new DirectorEmployee("Александр","Александров",Departments.Managment,200_000),
                                                     new StaffEmployee("Алексей","Алексеев",Departments.IT,100_000),
                                                     new FreeLancerEmployee("Николай","Николаев",Departments.IT, 1_000),
                                                     new FreeLancerEmployee("Сергей","Сергеев",Departments.IT, 1_000),
                                                     new StaffEmployee("Иван","Иванов",Departments.IT,100_000),
                                                   };
            _repository = new FileCSVEmployeeRepository();
            foreach (var e in _employeeList)
            {
                _repository.InsertEmployeeAsync(e).Wait();
            }

        }

        [Test]
        public void AddEmployeeInRepository_ShouldReturnTrue()
        {
            Console.WriteLine();
        }


        [TearDown]
        public void ClearFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), FileCSVSettings.EMPLOYEES_LIST_FILENAME);
            if(File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
