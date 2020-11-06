using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesUnitTest
{
    [TestClass]
    public class TaskTests
    {
        Person testPerson;
        CompletedTask testTask;
        CompletedTasksService service;
        Guid id = Guid.Parse("8cfca3cc-e79b-43be-881f-91d3a7ddf27f");
        Guid idTask = Guid.Parse("8cfca3cc-e79b-4444-881f-91d3a7ddf27f");
        double time = 180;
        decimal baseSalary = 200000;
        (DateTime, DateTime) period = (DateTime.Today, DateTime.Today.AddDays(1));

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
                BaseSalary = baseSalary
            };

            testTask = new CompletedTask()
            {
                IdTask = idTask,
                Date = DateTime.Now,
                Person = testPerson,
                TaskName = "Тестовое задание",
                Time = time
            };

            service = new CompletedTasksService();
        }

        [TestMethod]
        public async Task A_AddNewTaskTest()
        {
            var res = await service.AddNewTask(testTask);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public async Task B_GetPersonTaskPersonTest()
        {
            var res = await service.GetPersonTask(testPerson, period.Item1, period.Item2);
            var task = res.FirstOrDefault(p => p.Person.IdPerson == id);

            Assert.AreEqual(idTask, task.IdTask);
        }

        [TestMethod]
        public async Task C_GetPersonTaskPeriodTest()
        {
            var res = await service.GetPersonTask(testPerson, period.Item1, period.Item2);
            var task = res.FirstOrDefault(p => p.Date >= DateTime.Today);

            Assert.AreEqual(idTask, task.IdTask);
        }
    }
}
