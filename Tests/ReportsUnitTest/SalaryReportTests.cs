using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Reports.SalaryReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Settings;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ReportsUnitTest
{
    [TestClass]
    public class SalaryReportTests
    {
        SalaryReport reportDirector, reportDeveloper, reportFreelancer;
        Person testPerson1, testPerson2, testPerson3;
        CompletedTask task1, task2, task3, task4, task5, task6, task7, task8, task9;
        ReportSettings settings;

        double time1, time2, time3;
        readonly PersonsService personsService = new PersonsService();
        readonly CompletedTasksService completedTasksService = new CompletedTasksService();


        (DateTime, DateTime) period = (DateTime.Parse("02.11.2020"), DateTime.Parse("07.11.2020"));


        [TestInitialize]
        public async Task Init()
        {
            testPerson1 = new Person()
            {
                IdPerson = Guid.Parse("8cfca3cc-e79b-43be-8811-91d3a7ddf27f"),
                NamePerson = "TestPerson1",
                SurnamePerson = "Test",
                Department = Departments.Managment,
                Positions = Positions.Director,
                BaseSalary = 200000
            };

            testPerson2 = new Person()
            {
                IdPerson = Guid.Parse("8cfca3cc-e79b-43be-8812-91d3a7ddf27f"),
                NamePerson = "TestPerson2",
                SurnamePerson = "Test",
                Department = Departments.IT,
                Positions = Positions.Developer,
                BaseSalary = 160000
            };

            testPerson3 = new Person()
            {
                IdPerson = Guid.Parse("8cfca3cc-e79b-43be-8813-91d3a7ddf27f"),
                NamePerson = "TestPerson3",
                SurnamePerson = "Test",
                Department = Departments.IT,
                Positions = Positions.Freelance,
                BaseSalary = 1000
            };

            await personsService.InsertPersonAsync(testPerson1);
            await personsService.InsertPersonAsync(testPerson2);
            await personsService.InsertPersonAsync(testPerson3);

            task1 = new CompletedTask()
            {
                IdTask = Guid.Parse("91a1d966-e063-4623-b380-cfc698cb9f5e"),
                Date = DateTime.Parse("03.11.2020"),
                Person = testPerson1,
                Time = 8.5,
                TaskName = "Тестовое задание"
            };
            task2 = new CompletedTask()
            {
                IdTask = Guid.Parse("91a1d966-e063-4623-b381-cfc698cb9f5e"),
                Date = DateTime.Parse("04.11.2020"),
                Person = testPerson1,
                Time = 10.5,
                TaskName = "Тестовое задание2"
            };
            task3 = new CompletedTask()
            {
                IdTask = Guid.Parse("91a1d966-e063-4623-b384-cfc698cb9f5e"),
                Date = DateTime.Parse("05.11.2020"),
                Person = testPerson1,
                Time = 158.5,
                TaskName = "Тестовое задание3"
            };
            task4 = new CompletedTask()
            {
                IdTask = Guid.Parse("91a1d966-e063-4462-b380-cfc698cb9f5e"),
                Date = DateTime.Parse("03.11.2020"),
                Person = testPerson2,
                Time = 9.5,
                TaskName = "Тестовое задание4"
            };
            task5 = new CompletedTask()
            {
                IdTask = Guid.Parse("91a1d966-e063-8662-b387-cfc698cb9f5e"),
                Date = DateTime.Parse("05.11.2020"),
                Person = testPerson2,
                Time = 7,
                TaskName = "Тестовое задание5"
            };
            task6 = new CompletedTask()
            {
                IdTask = Guid.Parse("91a1d966-e063-4612-b389-cfc698cb9f5e"),
                Date = DateTime.Parse("06.11.2020"),
                Person = testPerson2,
                Time = 150,
                TaskName = "Тестовое задание6"
            };
            task7 = new CompletedTask()
            {
                IdTask = Guid.Parse("91a1d966-e463-4632-b389-cfc698cb9f5e"),
                Date = DateTime.Parse("03.11.2020"),
                Person = testPerson3,
                Time = 5,
                TaskName = "Тестовое задание7"
            };
            task8 = new CompletedTask()
            {
                IdTask = Guid.Parse("91a1d966-e463-4626-b389-cfc698cb9f5e"),
                Date = DateTime.Parse("05.11.2020"),
                Person = testPerson3,
                Time = 10,
                TaskName = "Тестовое задание8"
            };
            task9 = new CompletedTask()
            {
                IdTask = Guid.Parse("91a1d966-e463-4665-b389-cfc698cb9f5e"),
                Date = DateTime.Parse("06.11.2020"),
                Person = testPerson3,
                Time = 12,
                TaskName = "Тестовое задание9"
            };

            await completedTasksService.AddNewTask(task1);
            await completedTasksService.AddNewTask(task2);
            await completedTasksService.AddNewTask(task3);
            await completedTasksService.AddNewTask(task4);
            await completedTasksService.AddNewTask(task5);
            await completedTasksService.AddNewTask(task6);
            await completedTasksService.AddNewTask(task7);
            await completedTasksService.AddNewTask(task8);
            await completedTasksService.AddNewTask(task9);

            settings = new ReportSettings(160, 20000, 2);

            reportDirector = new SalaryReport(testPerson1, period, settings);
            reportDeveloper = new SalaryReport(testPerson2, period, settings);
            reportFreelancer = new SalaryReport(testPerson3, period, settings);

            time1 = task1.Time + task2.Time + task3.Time;
            time2 = task4.Time + task5.Time + task6.Time;
            time3 = task7.Time + task8.Time + task9.Time;
        }

        [TestMethod]
        public async Task A_ReturnDirectorTimeSummTest()
        {
            var res = await reportDirector.GetPersonReport();

            Assert.AreEqual(time1, res.Item1);
        }

        [TestMethod]
        public async Task B_ReturnDirectorSalarySummTest()
        {
            decimal testSalary;
            if (time1 > settings.NormTimeInMonth)
            {
                testSalary = testPerson1.BaseSalary + settings.BonusDirector * (decimal)(time1 - settings.NormTimeInMonth) / settings.NormTimeInMonth;
            }
            else
                testSalary = testPerson1.BaseSalary + settings.BonusDirector * (decimal)(time1 - settings.NormTimeInMonth) / settings.NormTimeInMonth;


            var res = await reportDirector.GetPersonReport();

            Assert.AreEqual(testSalary, res.Item2);
        }

        [TestMethod]
        public async Task C_ReturnDeveloperTimeSummTest()
        {
            var res = await reportDeveloper.GetPersonReport();

            Assert.AreEqual(time2, res.Item1);
        }

        [TestMethod]
        public async Task D_ReturnDeveloperSalarySummTest()
        {
            decimal testSalary;
            if(time2 > settings.NormTimeInMonth)
            {
                testSalary = testPerson2.BaseSalary + settings.BonusCoefficient * testPerson2.BaseSalary * (decimal)(time2 - settings.NormTimeInMonth) / settings.NormTimeInMonth;
            }
            else
                 testSalary = testPerson2.BaseSalary * (decimal)(time2) / settings.NormTimeInMonth;

            var res = await reportDeveloper.GetPersonReport();

            Assert.AreEqual(testSalary, res.Item2);
        }

        [TestMethod]
        public async Task E_ReturnFreelancerTimeSummTest()
        {
            var res = await reportFreelancer.GetPersonReport();

            Assert.AreEqual(time3, res.Item1);
        }

        [TestMethod]
        public async Task F_ReturnFreeLancerSalarySummTest()
        {
            var testSalary = testPerson3.BaseSalary * (decimal)time3;

            var res = await reportFreelancer.GetPersonReport();

            Assert.AreEqual(testSalary, res.Item2);
        }

        [TestCleanup]
        public void CleanUp()
        {
            string personsfile = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName, "persons.txt");
            string tasksfile = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName, "completedtasks.txt");

            new FileInfo(personsfile).Delete();
            new FileInfo(tasksfile).Delete();
        }

    }
}
