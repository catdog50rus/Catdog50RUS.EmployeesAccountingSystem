using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Moq;
using NUnit.Framework;
using System;

namespace Services.NUnitTest
{
    class TasksLogsServiceTests
    {
        private readonly Mock<ICompletedTasksLogRepository> _tasksLogRepository;
        private readonly ICompletedTaskLogs _service;
        private readonly BaseEmployee _staffEmployee;
        private readonly BaseEmployee _directorEmployee;
        private readonly BaseEmployee _freelancerEmployee;

        public TasksLogsServiceTests()
        {
            _tasksLogRepository = new Mock<ICompletedTasksLogRepository>();
            _service = new CompletedTasksLogsService(_tasksLogRepository.Object);
            _staffEmployee = new StaffEmployee("Василий", "Васичкин", Departments.IT, 100_000);
            _directorEmployee = new DirectorEmployee("Петр","Петров" ,Departments.Managment,200_000);
            _freelancerEmployee = new FreeLancerEmployee("Иван", "Иванов", Departments.IT, 1_000);
        }

        [Test]
        public void A_AddNewTaskLog_ShouldReturnTrue() 
        {
            //arrange
            var task = new CompletedTask(_staffEmployee, DateTime.Now, 5, "Фича");


            _tasksLogRepository
                .Setup(x => x.InsertCompletedTaskAsync(task))
                .ReturnsAsync(true)
                .Verifiable();

            

            //action
            var result = _service.AddNewTaskLog(task).Result;

            //assert
            _tasksLogRepository.Verify(method => method.InsertCompletedTaskAsync(task), Times.Once);
            Assert.IsTrue(result);
        }

        public void B_GetEmployeeTaskLogs_ShouldReturnCompletedTasks(Guid employeeID, DateTime startday, DateTime stopday) 
        {
            //arrange
            //action
            //assert
        }

        public void C_GetCompletedTaskLogs_ShouldReturnCompletedTasks(DateTime startday, DateTime stopday) 
        {
            //arrange
            //action
            //assert
        }

    }
}
