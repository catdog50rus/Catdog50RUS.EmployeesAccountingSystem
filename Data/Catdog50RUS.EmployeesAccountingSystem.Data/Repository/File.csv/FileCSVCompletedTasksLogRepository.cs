﻿using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv
{
    public class FileCSVCompletedTasksLogRepository : FileCSVBase, ICompletedTasksLogRepository
    {
        /// <summary>
        /// Внедрение имени файла
        /// </summary>
        private static readonly string _filename = FileCSVSettings.TASKSLOGS_FILENAME;

        public FileCSVCompletedTasksLogRepository() : base(_filename) { }

        #region Interface

        /// <summary>
        /// Асинхронное добавление выполненной задачи
        /// </summary>
        /// <returns></returns>
        public async Task<CompletedTaskLog> InsertCompletedTaskAsync(CompletedTaskLog taskLog)
        {
            //Проверяем входные данные на null
            if (taskLog == null)
                return null;
            //Преобразуем задачу в строку используя модель
            string line = taskLog.ToFile(DataSearator);
            //Записываем данные в файл и проверяем результат
            var writingResult = await base.WriteAsync(line);
            if (writingResult)
                return taskLog;
            else
                return null;
        }

        /// <summary>
        /// Получить список всех задач
        /// за определенный период
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CompletedTaskLog>> GetCompletedTasksListInPeriodAsync(DateTime beginDate,
                                                                        DateTime lastDate)
        {
            //Получаем список всех задач
            var tasksList = await GetCompletedTasksListAsync();

            //Проверяем, есть ли в списке задачи, выполненные в начальную дату или позднее
            //Если задач нет выходим из метода, возвращаем null

            if (tasksList.FirstOrDefault(d => d.Date >= beginDate) == null)
                return null;

            //Передаем в результирующий список задач выполненных в заданный период
            var result = tasksList.Where(d => d.Date >= beginDate && d.Date <= lastDate);
            //Если список пустой возвращаем null
            if (!result.Any())
                return null;
            //Возвращаем результат
            return result;

        }

        /// <summary>
        /// Получить список задач
        /// выполненных конкретным сотрудником
        /// за определенный период
        /// </summary>
        /// <param name="person"></param>
        /// <param name="beginDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CompletedTaskLog>> GetCompletedTasksListByEmployeeAsync(Guid employeeID,
                                                                              DateTime beginDate,
                                                                              DateTime lastDate)
        {
            //Получаем список всех задач
            var tasksList = await GetCompletedTasksListInPeriodAsync(beginDate, lastDate);
            if (tasksList == null) 
                return null;

            //Проверяем, есть ли в списке задачи, выполненные заданным сотрудником
            //Если задач нет выходим из метода, возвращаем null
            //Иначе передаем в результирующий список все задачи сотрудника
            var result = tasksList.Where(p => p.IdEmployee == employeeID);
            if (!result.Any())
                return null;

            return result;
        }

        #endregion


        /// <summary>
        /// Получить список всех выполненных задач
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<CompletedTaskLog>> GetCompletedTasksListAsync()
        {
            //Считываем данные из файла
            var dataLines = await ReadAsync();

            //Создаем новый список выполненных задач
            List<CompletedTaskLog> result = new List<CompletedTaskLog>();

            foreach (var line in dataLines)
            {
                var model = line.Split(DataSearator);
                //Получаем компонент модели
                Guid.TryParse(model[0],out Guid id);
                DateTime.TryParse(model[1], out DateTime date);
                Guid.TryParse(model[2], out Guid idEmployee);
                double.TryParse(model[3], out double time);
                string comment = model[4];

                //Заполняем модель
                CompletedTaskLog task = new CompletedTaskLog(id, idEmployee, date, time, comment);
                //Проверяем полученную модель на null и добавляем в результирующий список
                if (task != null)
                    result.Add(task);
                model = default;
            }
            return result.OrderBy(d => d.Date);
        }
    }
}
