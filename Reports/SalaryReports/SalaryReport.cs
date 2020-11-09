﻿using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Reports.SalaryReports
{
    /// <summary>
    /// Реализация бизнес логики
    /// Формирование отчетов
    /// </summary>
    public class SalaryReport
    {
        //TODO константы вынести в файл с настройками
        /// <summary>
        /// Месячная норма часов
        /// </summary>
        private readonly int _normTimeInMonth = 160;
        /// <summary>
        /// Бонус директора
        /// </summary>
        private readonly decimal _bonusDirector = 20000;
        /// <summary>
        /// Коэффициент переработки сотрудника на зарплате
        /// </summary>
        private readonly decimal _bonusCoefficient = 2;

        //Поля
        /// <summary>
        /// Внедрение сервиса работы с задачами
        /// </summary>
        private ICompletedTask CompletedTasksService { get; } = new CompletedTasksService();

        /// <summary>
        /// Должность сотрудника
        /// </summary>
        private Positions Position { get; }
        /// <summary>
        /// Базовая ставка сотрудника
        /// </summary>
        private decimal BaseSalary { get; }
        /// <summary>
        /// Начальная дата отчета
        /// </summary>
        private DateTime FirstDate { get; }
        /// <summary>
        /// Конечная дата отчета
        /// </summary>
        private DateTime LastDate { get; }
        /// <summary>
        /// Сотрудника
        /// </summary>
        private Person Person { get; }

        /// <summary>
        /// Конструктор для отчета Сотрудника
        /// </summary>
        /// <param name="person">СОтрудник</param>
        /// <param name="completedTasks">Список задач</param>
        public SalaryReport(Person person, (DateTime, DateTime) period, ReportSettings settings)
        {
            //Инициализация полей
            Person = person;
            Position = person.Positions;
            BaseSalary = person.BaseSalary;
            FirstDate = period.Item1;
            LastDate = period.Item2;
            //Настройки
            _normTimeInMonth = settings.NormTimeInMonth;
            _bonusDirector = settings.BonusDirector;
            _bonusCoefficient = settings.BonusCoefficient;
        }

        #region Interface

        /// <summary>
        /// Получить отчет по сотруднику
        /// Возвращает кортеж с количеством рабочих часов и заработком
        /// </summary>
        /// <returns></returns>
        public async Task<(double, decimal, List<CompletedTask>)> GetPersonReport()
        {
            var personTasks = await GetPersonTasksList();
            if (personTasks != null)
            {
                double time = GetSumTime(personTasks);
                decimal salary = default;

                //Если сотрудник фрилансер просто возвращаем сумму часов и сумму оплаты
                switch (Position)
                {
                    case Positions.Freelance:
                        salary = GetSalaryFreelance(time);
                        break;
                    case Positions.Director:
                        salary = GetSalaryDirector(time);
                        break;
                    case Positions.Developer:
                        salary = GetSalaryPerson(time);
                        break;
                }

                return (time, salary, personTasks);
            }
            return (0, 0, null);
        }

        #endregion


        #region Реализация

        /// <summary>
        /// Получить список выполненных задач
        /// </summary>
        /// <returns></returns>
        private async Task<List<CompletedTask>> GetPersonTasksList()
        {
            var result = await CompletedTasksService.GetPersonTask(Person, FirstDate, LastDate);
            if (result != null)
                return result.ToList();
            else
                return null;
        }

        /// <summary>
        /// Возвращаем общее количество отработанных часов
        /// </summary>
        /// <returns></returns>
        private double GetSumTime(List<CompletedTask> tasks)
        {
            return tasks.Sum(t => t.Time);
        }

        //TODO Методы рассчитывает переработку за количество часов сверх месячной нормы. Учитывая наличие месячной нормы считаю этот принцип единственно возможным
        /// <summary>
        /// Возвращаем заработную плату сотрудника
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private decimal GetSalaryPerson(double time)
        {
            decimal _time = (decimal)time, salary;

            if (time <= _normTimeInMonth)
            {
                salary = BaseSalary * _time / _normTimeInMonth;
            }
            else
            {
                salary = BaseSalary * (1 + _bonusCoefficient * (_time - _normTimeInMonth) / _normTimeInMonth);
            }
            return salary;
        }

        /// <summary>
        /// Возвращаем заработную плату руководителя
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private decimal GetSalaryDirector(double time)
        {
            decimal _time = (decimal)time, salary;

            if (time <= _normTimeInMonth)
            {
                salary = BaseSalary * _time / _normTimeInMonth;
            }
            else
            {
                salary = BaseSalary + _bonusDirector * (_time - _normTimeInMonth) / _normTimeInMonth;
            }
            return salary;
        }

        /// <summary>
        /// Возвращаем зарплату фрилансера
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private decimal GetSalaryFreelance(double time)
        {
            return BaseSalary * (decimal)time;
        }

        #endregion
    }
}
