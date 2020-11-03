using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services
{
    public class CompletedTasksService
    {
        private readonly ICompletedTaskRepository _counterRepository;

        public CompletedTasksService()
        {
            _counterRepository = new FileCompletedTaskRepository();
        }

        #region Interface

        /// <summary>
        /// Добавить новую задачу
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> AddNewTask(CompletedTask task)
        {
            if (task != null)
            {
                var result = await _counterRepository.AddCompletedTask(task);
                if (result != null)
                    return true;
                else
                    return false;
            }
            else return false;
        }
        /// <summary>
        /// Получить выполненные задачи пользователя
        /// За определенный период
        /// </summary>
        /// <param name="person"></param>
        /// <param name="firstDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CompletedTask>> GetPersonTask(Person person, DateTime firstDate, DateTime lastDate)
        {
            var result = await _counterRepository.GetPersonsTaskListAsync(person, firstDate, lastDate);
            if(result != null)
                return result;
            else
                return null;
        }
        #endregion



        
    }
}
