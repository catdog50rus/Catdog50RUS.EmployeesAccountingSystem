using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services.AutorizeService;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services.EmployeeService;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Employees.NUnitTest
{
    class StaffEmployeesTests
    {
        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="department"></param>
        /// <param name="position"></param>
        /// <param name="baseSalary"></param>
        [TestCase("Вася", "Иванов", Departments.IT, Positions.Developer, 200000)]
        [TestCase("Петя", "Петров", Departments.IT, Positions.Developer, 180000)]
        public void A_InsertStaffEmployee_ShouldReturnTrue(string name, 
                                                         string surname, 
                                                         Departments department, 
                                                         Positions position, 
                                                         decimal baseSalary)
        {
            //arrange

            var staffEmployee = new StaffEmployee(name, surname, department, position, baseSalary);
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock
                .Setup(x => x.InsertEmployeeAsync(staffEmployee))
                .ReturnsAsync(() => new StaffEmployee(Guid.NewGuid(), name, surname, department, position, baseSalary));

            var service = new EmployeeService(employeeRepositoryMock.Object);


            //act

            var result = service.InsertEmployeeAsync(staffEmployee);


            //assert

            Assert.IsTrue(result.Result);
        }
        /// <summary>
        /// Добавление существующего сотрудника
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="department"></param>
        /// <param name="position"></param>
        /// <param name="baseSalary"></param>
        [TestCase("Вася", "Иванов", Departments.IT, Positions.Developer, 200000)]
        public void B_InsertStaffEmployee_ShouldReturnFalse(string name,
                                                         string surname,
                                                         Departments department,
                                                         Positions position,
                                                         decimal baseSalary)
        {
            var staffEmployee = new StaffEmployee(name, surname, department, position, baseSalary);
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock
                .Setup(x => x.InsertEmployeeAsync(staffEmployee))
                .ReturnsAsync(() => new StaffEmployee(Guid.NewGuid(), name, surname, department, position, baseSalary));
            employeeRepositoryMock
                .Setup(x => x.GetEmployeesListAsync())
                .ReturnsAsync(() => new List<EmployeesBase> { new StaffEmployee(Guid.NewGuid(), name, surname, department, position, baseSalary) });

            var service = new EmployeeService(employeeRepositoryMock.Object);


            //act
            //var res = service.InsertEmployeeAsync(staffEmployee);
            var result = service.InsertEmployeeAsync(staffEmployee);


            //assert

            Assert.AreEqual(false, result.Result);
        }

        /// <summary>
        /// Получение сотрудника по имени
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="department"></param>
        /// <param name="position"></param>
        /// <param name="baseSalary"></param>
        [TestCase("Вася","Иванов", Departments.IT, Positions.Developer, 200000)]
        public void C_GetStaffEmployeeByName_ShoulsReturnStaffEmployee(string name,
                                                         string surname,
                                                         Departments department,
                                                         Positions position,
                                                         decimal baseSalary)
        {
            //arrange
           
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock
                .Setup(x => x.GetEmployeeByNameAsync(name))
                .ReturnsAsync(() => new StaffEmployee(Guid.NewGuid(), name, surname, department, position, baseSalary))
                .Verifiable();

            var service = new EmployeeService(employeeRepositoryMock.Object);


            //act
            var result = service.GetEmployeeByName(name);

            //assert
            employeeRepositoryMock.VerifyAll();
            Assert.AreEqual(name, result.Result.NamePerson);
        }

        [TestCase("Вася", "Иванов", Departments.IT, Positions.Developer, 200000)]
        public void D_AutorizeStaffEmployee_ShouldReturnAutorize(string name,
                                                         string surname,
                                                         Departments department,
                                                         Positions position,
                                                         decimal baseSalary)
        {
            //arrange

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();

            employeeRepositoryMock
                .Setup(x => x.GetEmployeeByNameAsync(name))
                .ReturnsAsync(() => new StaffEmployee(Guid.NewGuid(), name, surname, department, position, baseSalary));
            employeeRepositoryMock
                .Setup(x => x.GetEmployeesListAsync())
                .ReturnsAsync(() => new List<EmployeesBase> { new StaffEmployee(Guid.NewGuid(), name, surname, department, position, baseSalary) });

            var service = new AutorizeService(employeeRepositoryMock.Object);
            
            

            //act
            var autenteficatedUser = service.Autentificate(name).Result;
            var autorize = service.GetAuthorization(autenteficatedUser);


            //assert

            Assert.AreEqual(name, autenteficatedUser.NamePerson);
            Assert.AreEqual(true, autorize.IsAutentificated);
            Assert.AreEqual(Role.User, autorize.AutorizeRole);

        }


    }
}
