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

        //Добавление сотрудника 
        [Test]
        public void InsertNewEmployeeInRepository_ShouldReturnEmployee()
        {
            var testEmployee = new StaffEmployee("TestName", "TestSurName", Departments.IT, 100_000);
            var result = _repository.InsertEmployeeAsync(testEmployee).Result;

            Assert.AreEqual(testEmployee, result);
        }

        //Добавление сотрудника, проверка на null
        [Test]
        public void InsertNewEmployeeInRepository_ShouldReturnNull()
        {
            
            var result = _repository.InsertEmployeeAsync(null).Result;

            Assert.IsNull(result);
        }

        //Удаление сотрудника по имени
        [Test]
        public void DeleteEmployeeByNameFromRepository_ShouldReturnEmployee()
        {
            var testEmployee = new StaffEmployee("TestName", "TestSurName", Departments.IT, 100_000);
            _repository.InsertEmployeeAsync(testEmployee).Wait();

            var result = _repository.DeleteEmployeeByNameAsync(testEmployee.NamePerson).Result;

            Assert.AreEqual(testEmployee, result);
        }

        //Удаление сотрудника по id
        [Test]
        public void DeleteEmployeeByIDFromRepository_ShouldReturnEmployee()
        {
            var testEmployee = new StaffEmployee("TestName", "TestSurName", Departments.IT, 100_000);
            _repository.InsertEmployeeAsync(testEmployee).Wait();

            var result = _repository.DeleteEmployeeByIdAsync(testEmployee.Id).Result;

            Assert.AreEqual(testEmployee, result);
        }

        //Удаление сотрудника по имени, проверка на null
        [TestCase("ghfgh")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void DeleteEmployeeByNameFromRepository_ShouldReturnNull(string name)
        {

            var testEmployee = new StaffEmployee("TestName", "TestSurName", Departments.IT, 100_000);
            _repository.InsertEmployeeAsync(testEmployee).Wait();

            var result = _repository.DeleteEmployeeByNameAsync(name).Result;

            Assert.IsNull(result);

        }

        //Удаление сотрудника по id, проверка на null
        [TestCase("ghfgh")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void DeleteEmployeeByIDFromRepository_ShouldReturnNull(string name)
        {

            var testEmployee = new StaffEmployee("TestName", "TestSurName", Departments.IT, 100_000);
            _repository.InsertEmployeeAsync(testEmployee).Wait();

            var result = _repository.DeleteEmployeeByNameAsync(name).Result;

            Assert.IsNull(result);
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
