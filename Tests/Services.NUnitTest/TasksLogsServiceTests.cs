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

        public void A_AddNewTaskLog_ShouldReturnTrue() 
        {
            //arrange
                       

            //action


            //assert
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
