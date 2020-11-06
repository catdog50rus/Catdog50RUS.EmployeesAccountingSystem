using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Reports.SalaryReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ReportsUnitTest
{
    [TestClass]
    public class SalaryReportTests
    {
        SalaryReport reportDirector, reportDeveloper, reportFreelancer;
        Person testPerson1, testPerson2, testPerson3;
        double time1 = 187, time2 = 167.5, time3 = 31.5;

        (DateTime, DateTime) period = (DateTime.Parse("02.11.2020"), DateTime.Parse("07.11.2020"));


        [TestInitialize]
        public void Init()
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

            reportDirector = new SalaryReport(testPerson1, period);
            reportDeveloper = new SalaryReport(testPerson2, period);
            reportFreelancer = new SalaryReport(testPerson3, period);
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
            if (time1 > 160)
            {
                testSalary = testPerson1.BaseSalary + 20000 * (decimal)(time1 - 160) / 160;
            }
            else
                testSalary = testPerson1.BaseSalary + 20000 * (decimal)(time1 - 160) / 160;


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
            if(time2 > 160)
            {
                testSalary = testPerson2.BaseSalary + 2 * testPerson2.BaseSalary * (decimal)(time2 - 160) / 160;
            }
            else
                 testSalary = testPerson2.BaseSalary * (decimal)(time2) / 160;

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

    }
}
