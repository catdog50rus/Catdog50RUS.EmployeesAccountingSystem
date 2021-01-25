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
    class CompletedTasksLogsRepositoryTests
    {
        private ICompletedTasksLogRepository _repository;
        private List<CompletedTaskLog> _completedTaskLogs;
        Guid id1 = Guid.NewGuid();
        Guid id2 = Guid.NewGuid();
        Guid id3 = Guid.NewGuid();

        [SetUp]
        public void TestsSetup()
        {
            _repository = new FileCSVCompletedTasksLogRepository();

            _completedTaskLogs = new List<CompletedTaskLog>
            {
                new CompletedTaskLog(Guid.NewGuid(), id1, DateTime.Now.Date.AddDays(-5), 5, "TestTask4"),
                new CompletedTaskLog(Guid.NewGuid(), id1, DateTime.Now.Date.AddDays(-5), 3, "TestTask5"),
                new CompletedTaskLog(Guid.NewGuid(), id2, DateTime.Now.Date.AddDays(-3), 9, "TestTask5"),
                new CompletedTaskLog(Guid.NewGuid(), id3, DateTime.Now.Date.AddDays(-2), 3, "TestTask6"),
                new CompletedTaskLog(Guid.NewGuid(), id1, DateTime.Now.Date.AddDays(-5), 5, "TestTask4"),
                new CompletedTaskLog(Guid.NewGuid(), id2, DateTime.Now.Date.AddDays(-5), 3, "TestTask5"),
                new CompletedTaskLog(Guid.NewGuid(), id3, DateTime.Now.Date.AddDays(-3), 9, "TestTask5"),
                new CompletedTaskLog(Guid.NewGuid(), id3, DateTime.Now.Date.AddDays(-2), 3, "TestTask6")
            };

            var path = Path.Combine(Directory.GetCurrentDirectory(), FileCSVSettings.TASKSLOGS_FILENAME);
            if (File.Exists(path))
                File.Delete(path);
        }

        #region Insert

        //Добавление выполненной задачи
        [Test]
        public void A__InsertTimeLog_ShouldReturnTaskLogsList()
        {
            var id = Guid.NewGuid();
            var log = new CompletedTaskLog(Guid.NewGuid(), id, DateTime.Now.Date.AddDays(-2), 3, "TestTask6");

            var result = _repository.InsertCompletedTaskAsync(log).Result;

            var newlist = _repository.GetCompletedTasksListInPeriodAsync(DateTime.Now.AddYears(-2), DateTime.Now).Result.ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual((1), newlist.Count);
        }

        //Добавление выполненной задачи, результат null
        [Test]
        public void B__InsertTimeLog_ShouldReturnNull()
        {

            var result = _repository.InsertCompletedTaskAsync(null).Result;

            Assert.IsNull(result);

        }

        #endregion

        #region GetAll

        //Получение логов выполненных задач в заданный период
        [Test]
        public void C_GetAllCompletedTaskLogsInPeriod_ShoulReturnTaskLoagsList()
        {
            foreach (var l in _completedTaskLogs)
            {
                _repository.InsertCompletedTaskAsync(l);

            }

            var startDate = DateTime.Now.AddDays(-10);
            var stopDate = DateTime.Now;

            var result = _repository.GetCompletedTasksListInPeriodAsync(startDate, stopDate).Result.ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(_completedTaskLogs.Count, result.Count);
            Assert.AreEqual(40d, result.Sum(t => t.Time));
        }

        //Получение логов выполненных задач в заданный период, return null
        [TestCase("2021.02.25","2021.01.25")]
        [TestCase("2021.01.01", "2021.01.05")]
        [TestCase("2021.01.20", "2021.01.05")]
        public void D_GetAllCompletedTaskLogsInPeriod_ShoulReturnNull(string strFirstDay, string strLastDay)
        {
            foreach (var l in _completedTaskLogs)
            {
                _repository.InsertCompletedTaskAsync(l);

            }

            var startDate = DateTime.Parse(strFirstDay);
            var stopDate = DateTime.Parse(strLastDay);

            var result = _repository.GetCompletedTasksListInPeriodAsync(startDate, stopDate).Result;

            Assert.IsNull(result);
        }

        #endregion

        #region GetByEmployee

        //Получение логов выполненных задач в заданный период
        [Test]
        public void E_GetAllCompletedTaskLogsByEmployeeInPeriod_ShoulReturnTaskLoagsList()
        {
            foreach (var l in _completedTaskLogs)
            {
                _repository.InsertCompletedTaskAsync(l);

            }

            var startDate = DateTime.Now.AddDays(-10);
            var stopDate = DateTime.Now;

            var result = _repository.GetCompletedTasksListByEmployeeAsync(id1, startDate, stopDate).Result.ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(13d, result.Sum(t => t.Time));
        }

        //Получение логов выполненных задач в заданный период, return null
        [TestCase("2021.02.25", "2021.01.25")]
        [TestCase("2021.01.01", "2021.01.05")]
        [TestCase("2021.01.20", "2021.01.05")]
        [TestCase("2021.01.20", "2021.01.25")] //Вариант корректный по времени, но неверный id
        public void F_GetAllCompletedTaskLogsInPeriod_ShoulReturnNull(string strFirstDay, string strLastDay)
        {
            foreach (var l in _completedTaskLogs)
            {
                _repository.InsertCompletedTaskAsync(l);

            }

            var id = Guid.NewGuid();
            var startDate = DateTime.Parse(strFirstDay);
            var stopDate = DateTime.Parse(strLastDay);

            var result = _repository.GetCompletedTasksListByEmployeeAsync(id, startDate, stopDate).Result;

            Assert.IsNull(result);
        }

        #endregion
    }
}
