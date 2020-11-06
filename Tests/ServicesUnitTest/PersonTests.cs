using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesUnitTest
{
    [TestClass]
    public class PersonTests
    {
        Person testPerson;
        PersonsService service;
        Guid id = Guid.Parse("8cfca3cc-e79b-43be-881f-91d3a7ddf27f");

        [TestInitialize]
        public void Init()
        {
            testPerson = new Person()
            {
                IdPerson = id,
                NamePerson = "TestPerson",
                SurnamePerson = "Test",
                Department = Departments.Managment,
                Positions = Positions.Director,
                BaseSalary = 200000
            };

            service = new PersonsService();
        }


        [TestMethod]
        public async Task A_InsertPersonTest()
        {
            var res = await service.InsertPersonAsync(testPerson);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public async Task B_GetPersonTest()
        {
            var res = await service.GetPersonByName(testPerson.NamePerson);
            if (res != null)
                Assert.AreEqual(res.ToFile(), testPerson.ToFile());
        }

        [TestMethod]
        public async Task C_GetListPersonsTest()
        {
            var res = await service.GetAllPersonsAsync();
            var person = res.FirstOrDefault(p => p.IdPerson == testPerson.IdPerson);
            Assert.AreEqual(person.ToFile(), testPerson.ToFile());
        }

        [TestMethod]
        public async Task D_DeletePersonTest()
        {
            var res = await service.DeletePersonAsync(testPerson.IdPerson);
            Assert.IsTrue(res);
        }
    }
}
