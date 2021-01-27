using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services.ReportSettings;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Moq;
using NUnit.Framework;

namespace ReportService.NUnitTests
{
    [TestFixture]
    class SalaryCalculatingSettingsServiceTests
    {
        private ISalaryCalculateSettingsService _service;
        private Mock<ISalaryCalculateSettingsRepository> _mockRepository;
        private SalaryCalculatingSettings _settings;


        [SetUp]
        public void SetupTests()
        {
            _mockRepository = new Mock<ISalaryCalculateSettingsRepository>();
            _service = new SalaryCalculatingSettingsService(_mockRepository.Object);
            _settings = new SalaryCalculatingSettings(160, 20, 8, 20_000, 2);
        }

        //Получение настроек
        [Test]
        public void A_GetSalaryCalculatingSettings_ShouldReturnSettings()
        {
            _mockRepository
                .Setup(method => method.GetSettings())
                .ReturnsAsync(_settings)
                .Verifiable();

            var result = _service.GetSalaryCalculatingSettings().Result;

            _mockRepository.Verify(method => method.GetSettings(), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(_settings, result);


        }

        //Запись настроек
        [Test]
        public void B_SaveSalaryCalculatingSettings_ShouldReturnBoolResult()
        {
            _mockRepository
                .Setup(method => method.SaveSettings(_settings))
                .ReturnsAsync(true)
                .Verifiable();

            var result = _service.SaveSalaryCalculatingSettings(_settings).Result;
            _mockRepository.Verify(x => x.SaveSettings(_settings), Times.Once);

            Assert.IsTrue(result);

        }

        //Запись настроек результат false
        [Test]
        public void C_SaveSalaryCalculatingSettings_ShouldReturnFalse()
        {
            _mockRepository
                .Setup(method => method.SaveSettings(null))
                .Verifiable();

            var result = _service.SaveSalaryCalculatingSettings(_settings).Result;
            _mockRepository.Verify(x => x.SaveSettings(null), Times.Never);

            Assert.IsFalse(result);

        }


    }
}
