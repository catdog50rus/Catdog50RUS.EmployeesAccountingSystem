using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services
{
    /// <summary>
    /// Реализация бизнес логики
    /// Получение списка задач
    /// </summary>
    public class CompletedTasksLogsService : ICompletedTaskLogsService
    {
        /// <summary>
        /// Внедрение зависимости через интерфейс
        /// </summary>
        private readonly ICompletedTasksLogRepository _tasksRepository;
        private readonly Autorize _autorize;

        /// <summary>
        /// Конструктор
        /// </summary>
        public CompletedTasksLogsService(ICompletedTasksLogRepository repository, Autorize autorize)
        {
            _tasksRepository = repository;
            if (autorize != null)
                _autorize = autorize;
        }

        #region Interface

        /// <summary>
        /// Создание выполненной задачи
        /// </summary>
        /// <param name="date"></param>
        /// <param name="employee"></param>
        /// <param name="taskname"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public CompletedTaskLog CreateNewTask(DateTime date, BaseEmployee employee, string taskname, double time)
        {
            //Первичная валидация данных
            if (employee == null ||
                string.IsNullOrWhiteSpace(taskname) ||
                time <= 0 || time > 24 ||
                date > DateTime.Now.AddDays(1))
            {
                return null;
            }

            //Внедрение ограничений по ролям пользователей
            bool isValid = default;
            switch (_autorize.UserRole)
            {
                case Role.Freelancer: //Может создавать логи для себя, при этом дата лога не может быть старше чем за 2 дня
                    isValid = _autorize.UserId.Equals(employee.Id) && 
                              (date.Date >= DateTime.Now.Date.AddDays(-2));
                    break;
                case Role.Developer: //Может создавать логи только за себя
                    isValid = _autorize.UserId.Equals(employee.Id);
                    break;
                case Role.Director: //Может создавать логи за всех
                    isValid = true;
                    break;
                default:
                    break;
                    
            }
            if (!isValid)
                return null;

            return new CompletedTaskLog(Guid.NewGuid(), employee.Id, date,time,taskname);
        }

        /// <summary>
        /// Добавить новую задачу
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> AddNewTaskLog(CompletedTaskLog task)
        {
            //Проверяем входные параметры на null
            if (task == null)
                return false;

            //Пытаемся добавить задачу в хранилище, 
            //если результат не null возвращаем true, иначе false
            var result = await _tasksRepository.InsertCompletedTaskAsync(task);
            if (result != null)
                return true;
            else
                return false;

        }

        /// <summary>
        /// Получить выполненные задачи пользователя
        /// за определенный период
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startday"></param>
        /// <param name="stopday"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CompletedTaskLog>> GetEmployeeTaskLogs(Guid id, DateTime startday, DateTime stopday)
        {
            //Первичная валидация данных
            if (!ValidateDate(startday, stopday) || id == Guid.Empty)
                return null;
            //Валидация на основе прав доступа
            bool isValid = default;
            switch (_autorize.UserRole)
            {
                case Role.None:
                    break;
                case Role.Admin:
                    break;
                case Role.Director: //Полные права
                    isValid = true;
                    break;
                case Role.Developer: //Может получать только свои логи
                    isValid = _autorize.UserId.Equals(id);
                    break;
                case Role.Freelancer: //Может получать только свои логи
                    isValid = _autorize.UserId.Equals(id);
                    break;
                default:
                    break;
            }
            //Получение результата на основе валидации прав доступа
            if (isValid)
            {
                var result = await _tasksRepository.GetCompletedTasksListByEmployeeAsync(id, startday, stopday);
                if (result == null || result.ToList().Count == 0)
                    return null;
                else
                    return result;
            }
            else
                return null;

        }
        
        /// <summary>
        /// Получение логов всех пользователей
        /// </summary>
        /// <param name="startday"></param>
        /// <param name="stopday"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CompletedTaskLog>> GetCompletedTaskLogs(DateTime startday, DateTime stopday)
        {
            //Первичная валидация данных
            if (!ValidateDate(startday, stopday))
                return null;
            //Валидация прав доступа
            bool isValid = default;
            switch (_autorize.UserRole)
            {
                case Role.None:
                    break;
                case Role.Admin:
                    break;
                case Role.Director: //Есть доступ
                    isValid = true;
                    break;
                case Role.Developer:
                    break;
                case Role.Freelancer:
                    break;
                default:
                    break;
            }

            if (isValid)
            {
                var result = await _tasksRepository.GetCompletedTasksListInPeriodAsync(startday, stopday);
                if (result == null || result.ToList().Count == 0)
                    return null;
                else
                    return result;
            }
            else
                return null;
        }
        
        #endregion
        
        /// <summary>
        /// Валидация данных
        /// </summary>
        /// <param name="startday"></param>
        /// <param name="stopday"></param>
        /// <returns></returns>
        private bool ValidateDate(DateTime startday, DateTime stopday)
        {
            //Проверяем, чтобы начальная дата была не больше конечной даты
            if (startday > stopday)
                return false;
            //Проверяем, чтобы конечная дата была не больше, чем текущая плюс 1 год
            if (stopday > DateTime.Now.AddYears(1))
                return false;
            
            return true;
        }


    }
}
