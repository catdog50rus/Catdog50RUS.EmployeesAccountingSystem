using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services
{
    /// <summary>
    /// Реализация бизнес логики
    /// Получение списка задач
    /// </summary>
    public class CompletedTasksService
    {
        /// <summary>
        /// Внедрение зависимости через интерфейс
        /// </summary>
        private ICompletedTaskRepository TasksRepository { get; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public CompletedTasksService()
        {
            TasksRepository = new FileCompletedTaskRepository();
        }

        #region Interface

        /// <summary>
        /// Добавить новую задачу
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> AddNewTask(CompletedTask task)
        {
            //Проверяем входные параметры на null
            if (task != null)
            {
                //Пытаемся добавить задачу в хранилище, 
                //если результат не null возвращаем true, иначе false
                var result = await TasksRepository.AddCompletedTask(task);
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
             return await TasksRepository.GetPersonsTaskListAsync(person, firstDate, lastDate);
        }
        #endregion
        
    }
}
