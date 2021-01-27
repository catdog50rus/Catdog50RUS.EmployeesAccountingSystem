using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository.NUnitTests
{
    [TestFixture]
    class SalaryReportSettingsRepositoryTests
    {
        private ISalaryCalculateSettingsRepository _repository;
        private ReportSettings _settings;
        
        [SetUp]
        public void SetupTests()
        {
            _repository = new FileCSVSalaryCalculateSettingsRepository();

            _settings = new ReportSettings(160, 20, 8, 20_000, 2);
            
        }


        //Получение настроек из файла
        [Test, Order(0)]
        public void InsertSalaryCalculateSettings_ShouldReturBoolResult()
        {
            var result = _repository.SaveSettings(_settings).Result;

            Assert.IsTrue(result);

        }

        //Получение настроек из файла
        [Test, Order(1)]
        public void GetSalaryCalculateSettings_ShouldReturSettings()
        {
            var result = _repository.GetSettings().Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(_settings, result);

        }
    }
}
