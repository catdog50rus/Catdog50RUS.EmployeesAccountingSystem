using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Reports.SalaryReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsUnitTest
{
    [TestClass]
    public class SalaryReportTests
    {
        SalaryReport report;
        Person testPerson1, testPerson2, testPerson3;
        CompletedTask task1, task2, task3, task4, task5, task6, task7, task8, task9;
        ReportSettings settings;

        double time1, time2, time3;
        decimal sum1, sum2, sum3;
        readonly PersonsService personsService = new PersonsService();
        readonly CompletedTasksService completedTasksService = new CompletedTasksService();


        (DateTime, DateTime) period = (DateTime.Parse("02.11.2020"), DateTime.Parse("07.11.2020"));

        (DateTime, DateTime) month = (DateTime.Parse("01.11.2020"), DateTime.Parse("01.12.2020"));


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

            report = new SalaryReport(settings);

            time1 = task1.Time + task2.Time + task3.Time;
            time2 = task4.Time + task5.Time + task6.Time;
            time3 = task7.Time + task8.Time + task9.Time;


            if (time1 > settings.NormTimeInMonth)
            {
                sum1 = testPerson1.BaseSalary + settings.BonusDirector * (decimal)(time1 - settings.NormTimeInMonth) / settings.NormTimeInMonth;
            }
            else
                sum1 = testPerson1.BaseSalary + settings.BonusDirector * (decimal)(time1 - settings.NormTimeInMonth) / settings.NormTimeInMonth;

            if (time2 > settings.NormTimeInMonth)
            {
                sum2 = testPerson2.BaseSalary + settings.BonusCoefficient * testPerson2.BaseSalary * (decimal)(time2 - settings.NormTimeInMonth) / settings.NormTimeInMonth;
            }
            else
                sum2 = testPerson2.BaseSalary * (decimal)(time2) / settings.NormTimeInMonth;

            sum3 = testPerson3.BaseSalary * (decimal)time3;

        }

        [TestMethod]
        public async Task A_ReturnDirectorTimeSummTest()
        {
            var res = await report.GetPersonReport(testPerson1, period);

            Assert.AreEqual(time1, res.Time);
        }

        [TestMethod]
        public async Task B_ReturnDirectorSalarySummTest()
        {
            decimal testSalary = sum1;
            

            var res = await report.GetPersonReport(testPerson1, period);

            Assert.AreEqual(testSalary, res.Salary);
        }

        [TestMethod]
        public async Task C_ReturnDeveloperTimeSummTest()
        {
            var res = await report.GetPersonReport(testPerson2, period);

            Assert.AreEqual(time2, res.Time);
        }

        [TestMethod]
        public async Task D_ReturnDeveloperSalarySummTest()
        {
            decimal testSalary = sum2;

            var res = await report.GetPersonReport(testPerson2, period);

            Assert.AreEqual(testSalary, res.Salary);
        }

        [TestMethod]
        public async Task E_ReturnFreelancerTimeSummTest()
        {
            var res = await report.GetPersonReport(testPerson3, period);

            Assert.AreEqual(time3, res.Time);
        }

        [TestMethod]
        public async Task F_ReturnFreeLancerSalarySummTest()
        {
            var testSalary = sum3;

            var res = await report.GetPersonReport(testPerson3, period);

            Assert.AreEqual(testSalary, res.Salary);
        }


        [TestMethod]
        public async Task G_ReturnAllPersonsSalaryTimeTest()
        {
           
            var testTime = time1 + time2 + time3;

            var res = await report.GetAllPersonsReport(month);

            var allTime = res.Sum(s => s.Time);


            Assert.AreEqual(testTime, allTime);
        }

        [TestMethod]
        public async Task H_ReturnAllPersonsSalarySummTest()
        {
            var testSalary = sum1 + sum2 + sum3;

            var res = await report.GetAllPersonsReport(month);

            var allSum = res.Sum(s => s.Salary);

            
            Assert.AreEqual(testSalary, allSum);
        }


        [TestMethod]
        public async Task I_ReturnDepartmentsSalaryTimeTest()
        {

            var testTime = time1 + time2 + time3;

            var res = await report.GetDepartmentsReport(month);

            var allTime = res.Sum(s => s.Item2.Sum(x=>x.Time));


            Assert.AreEqual(testTime, allTime);
        }

        [TestMethod]
        public async Task J_ReturnDepartmensSalarySummTest()
        {
            var testSalary = sum1 + sum2 + sum3;

            var res = await report.GetDepartmentsReport(month);

            var allSum = res.Sum(s => s.Item2.Sum(x => x.Salary));


            Assert.AreEqual(testSalary, allSum);
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
