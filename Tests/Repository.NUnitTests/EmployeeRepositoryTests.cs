using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.NUnitTests
{
    [TestFixture]
    class EmployeeRepositoryTests
    {
        private List<BaseEmployee> _employeeList;
        private IEmployeeRepository _repository;

        [SetUp]
        public void TestSetup()
        {
            _employeeList = new List<BaseEmployee> { new DirectorEmployee("Александр","Александров",Departments.Managment,200_000),
                                                     new StaffEmployee("Алексей","Алексеев",Departments.IT,100_000),
                                                     new FreeLancerEmployee("Николай","Николаев",Departments.IT, 1_000),
                                                     new FreeLancerEmployee("Сергей","Сергеев",Departments.IT, 1_000),
                                                     new StaffEmployee("Иван","Иванов",Departments.IT,100_000),
                                                   };
            _repository = new FileCSVEmployeeRepository();
            
        }

        #region InsertEmployee

        //Добавление сотрудника 
        [Test]
        public void A__InsertNewEmployeeInRepository_ShouldReturnEmployee()
        {
            var testEmployee = new StaffEmployee("TestName", "TestSurName", Departments.IT, 100_000);
            var result = _repository.InsertEmployeeAsync(testEmployee).Result;

            Assert.AreEqual(testEmployee, result);

            _repository.DeleteEmployeeByNameAsync(testEmployee.NamePerson).Wait();
        }

        //Добавление сотрудника, проверка на null
        [Test]
        public void B__InsertNewEmployeeInRepository_ShouldReturnNull()
        {

            var result = _repository.InsertEmployeeAsync(null).Result;

            Assert.IsNull(result);
        }

        #endregion


        #region DeleteEmployee

        //Удаление сотрудника по имени
        [Test]
        public void C__DeleteEmployeeByNameFromRepository_ShouldReturnEmployee()
        {
            var testEmployee = new StaffEmployee("TestName", "TestSurName", Departments.IT, 100_000);
            _repository.InsertEmployeeAsync(testEmployee);

            var result = _repository.DeleteEmployeeByNameAsync(testEmployee.NamePerson).Result;

            Assert.AreEqual(testEmployee, result);
            Assert.AreEqual(5, _repository.GetEmployeesListAsync().Result.ToList().Count());
        }

        //Удаление сотрудника по id
        [Test]
        public void D__DeleteEmployeeByIDFromRepository_ShouldReturnEmployee()
        {
            var testEmployee = new StaffEmployee("TestName", "TestSurName", Departments.IT, 100_000);
            _repository.InsertEmployeeAsync(testEmployee);

            var result = _repository.DeleteEmployeeByIdAsync(testEmployee.Id).Result;

            Assert.AreEqual(testEmployee, result);
            Assert.AreEqual(5, _repository.GetEmployeesListAsync().Result.ToList().Count());
        }

        //Удаление сотрудника по имени, проверка на null
        [TestCase("ghfgh")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void E__DeleteEmployeeByNameFromRepository_ShouldReturnNull(string name)
        {
            var result = _repository.DeleteEmployeeByNameAsync(name).Result;

            Assert.IsNull(result);

        }

        //Удаление сотрудника по id, проверка на null
        [TestCase("00000000-0000-0000-0000-000000000001")]
        public void F__DeleteEmployeeByIDFromRepository_ShouldReturnNull(string strId)
        {

            var id = Guid.Parse(strId);

            var result = _repository.DeleteEmployeeByIdAsync(id).Result;

            Assert.IsNull(result);
        }

        #endregion


        //Получение списка сотрудников
        [Test, Order(0)]
        public void G__GetEmployeesList_ShouldReturnEmployeesList()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), FileCSVSettings.EMPLOYEES_LIST_FILENAME);
            List<BaseEmployee> result = null;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.Create(path).Close();
            
            foreach (var e in _employeeList)
            {
                _repository.InsertEmployeeAsync(e).Wait();
            }

            result = _repository.GetEmployeesListAsync().Result.ToList();

            Assert.AreEqual(_employeeList.Count, result.Count);
            Assert.AreEqual(_employeeList[0], result[0]);
            Assert.AreEqual(_employeeList[1], result[1]);
            Assert.AreEqual(_employeeList[2], result[2]);
            Assert.AreEqual(_employeeList[3], result[3]);
            Assert.AreEqual(_employeeList[4], result[4]);
        }

        #region GetEmployee

        //Получение сотрудника по имени
        [Test]
        public void H__GetEmployeeByNameFromRepository_ShouldReturnEmployee()
        {
            var testEmployee = new StaffEmployee("TestName", "TestSurName", Departments.IT, 100_000);
            _repository.InsertEmployeeAsync(testEmployee);

            var result = _repository.GetEmployeeByNameAsync(testEmployee.NamePerson).Result;

            Assert.AreEqual(testEmployee, result);
            _repository.DeleteEmployeeByNameAsync(testEmployee.NamePerson).Wait();
        }

        //Получение сотрудника по id
        [Test]
        public void I__GetEmployeeByIDFromRepository_ShouldReturnEmployee()
        {
            var testEmployee = new StaffEmployee("TestName", "TestSurName", Departments.IT, 100_000);
            _repository.InsertEmployeeAsync(testEmployee);

            var result = _repository.GetEmployeeByIdAsync(testEmployee.Id).Result;

            Assert.AreEqual(testEmployee, result);
            _repository.DeleteEmployeeByNameAsync(testEmployee.NamePerson).Wait();
        }

        //Получение сотрудника по имени, проверка на null
        [TestCase("ghfgh")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [Order(10)]
        public void J__GetEmployeeByNameFromRepository_ShouldReturnNull(string name)
        {

            var result = _repository.GetEmployeeByNameAsync(name).Result;

            Assert.IsNull(result);

        }

        //Получение сотрудника по id, проверка на null
        [TestCase("00000000-0000-0000-0000-000000000001")]
        public void K__GetEmployeeByIDFromRepository_ShouldReturnNull(string strId)
        {
            var id = Guid.Parse(strId);
            var result = _repository.GetEmployeeByIdAsync(id).Result;

            Assert.IsNull(result);
        }

        #endregion

    }
}
