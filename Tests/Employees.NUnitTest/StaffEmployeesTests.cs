using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services.EmployeeService;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using NUnit.Framework;

namespace Employees.NUnitTest
{
    class StaffEmployeesTests
    {
        [TestCase("Вася", "Иванов", Departments.Managment, Positions.Developer, 200000)]
        public void A_InsertStaffEmployee_ShouldReturnTrue(string name, 
                                                         string surname, 
                                                         Departments department, 
                                                         Positions position, 
                                                         decimal baseSalary)
        {
            //arrange
            var staffEmployee = new StaffEmployee(name, surname, department, position, baseSalary);
            var employeeRepository = new FileCSVEmployeeRepository();

            var service = new EmployeeService(employeeRepository);


            //act

            var result = service.InsertEmployeeAsync(staffEmployee);


            //assert

            Assert.IsTrue(result.Result);
        }

        [TestCase("Вася", "Иванов", Departments.Managment, Positions.Developer, 200000)]
        public void B_InsertStaffEmployee_ShouldReturnFalse(string name,
                                                         string surname,
                                                         Departments department,
                                                         Positions position,
                                                         decimal baseSalary)
        {
            //arrange
            var staffEmployee = new StaffEmployee(name, surname, department, position, baseSalary);
            var employeeRepository = new FileCSVEmployeeRepository();

            var service = new EmployeeService(employeeRepository);


            //act
            var result = service.InsertEmployeeAsync(staffEmployee);


            //assert

            Assert.IsFalse(result.Result);
        }


        [TestCase("Вася")]
        public void C_GetStaffEmployeeByName_ShoulsReturnStaffEmployee(string name)
        {
            //arrange
            
            var employeeRepository = new FileCSVEmployeeRepository();
            var service = new EmployeeService(employeeRepository);


            //act
           var result = service.GetPersonByName(name);

            //assert

            Assert.AreEqual(name, result.Result.NamePerson);
        }

    }
}
