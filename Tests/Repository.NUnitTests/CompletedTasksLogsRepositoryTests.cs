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
        [SetUp]
        public void TestsSetup()
        {
            _repository = new FileCSVCompletedTasksLogRepository();
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

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
        }

        #region Insert

        //Добавление выполненной задачи
        [Test]
        public void A__InsertTimeLog_ShouldReturnTaskLogsList()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), FileCSVSettings.TASKSLOGS_FILENAME);
            if (File.Exists(path))
                File.Delete(path);

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




        #endregion



    }
}
