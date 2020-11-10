using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
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
        //Настройки
        /// <summary>
        /// Месячная норма часов
        /// </summary>
        private readonly int _normTimeInMonth;
        /// <summary>
        /// Бонус директора
        /// </summary>
        private readonly decimal _bonusDirector;
        /// <summary>
        /// Коэффициент переработки сотрудника на зарплате
        /// </summary>
        private readonly decimal _bonusCoefficient;

        //Поля
        /// <summary>
        /// Внедрение сервиса работы с задачами
        /// </summary>
        private ICompletedTask CompletedTasksService { get; } = new CompletedTasksService();

        /// <summary>
        /// Должность сотрудника
        /// </summary>
        private Positions Position { get; set; }
        /// <summary>
        /// Базовая ставка сотрудника
        /// </summary>
        private decimal BaseSalary { get; set; }

        public SalaryReport(ReportSettings settings)
        {
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
        public async Task<(double, decimal, IEnumerable<CompletedTask>)> GetPersonReport(Person person, (DateTime, DateTime) period)
        {
            Position = person.Positions;
            BaseSalary = person.BaseSalary;
            return await GetPersonTasksList(person.IdPerson, period);
        }


        /// <summary>
        /// Получить отчет по всем сотрудникам
        /// Возвращает список кортежей с сотрудниками, количеством рабочих часов, заработком и списком выполненных задач
        /// </summary>
        /// <returns></returns>
        public async Task<List<(Person, double, decimal, List<CompletedTask>)>> GetAllPersonsReport((DateTime, DateTime) period)
        {
            return await GetCompletedTasksList(period);
        }

        #endregion


        #region Реализация

        /// <summary>
        /// Получить отчет по выполненным задачам сотрудника
        /// </summary>
        /// <returns></returns>
        private async Task<(double, decimal, IEnumerable<CompletedTask>)> GetPersonTasksList(Guid personID, (DateTime, DateTime) period)
        {
            //Получаем список выполненных задач сотрудником
            var result = await CompletedTasksService.GetPersonTask(personID, period.Item1, period.Item2);
            if (result != null)
            {
                //Суммарное время
                double time = result.Sum(t => t.Time);
                //Получаем сумму оплаты
                decimal salary = GetSalary(time);

                return (time, salary, result);
            }
            return (0, 0, null);
        }

        /// <summary>
        /// Получить отчет по выполненным задачам всех сотрудников
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        private async Task<List<(Person, double, decimal, List<CompletedTask>)>> GetCompletedTasksList((DateTime, DateTime) period)
        {
            var result = new List<(Person, double, decimal, List<CompletedTask>)>();
            //Получаем список выполненных задач за месяц и проверяем его на null
            var list = await CompletedTasksService.GetCompletedTask(period.Item1, period.Item2);
            if (list != null)
            {
                //Группируем полученный список по сотрудникам и отбираем ID сотрудника и и время
                var query = list.GroupBy(p=>p.Person.IdPerson).Select(l => new { ID = l.Key, Times = l.Sum(t => t.Time) }) ;

                foreach (var item in query)
                {
                    //Получаем сотрудника по ID
                    var person = list.Select(p => p.Person).FirstOrDefault(p => p.IdPerson == item.ID);
                    //Получаем список задач по Сотруднику
                    var taskslist = list.Where(p => p.Person.IdPerson == item.ID).ToList();
                    //Получаем базовую ставку сотрудника
                    BaseSalary = person.BaseSalary;
                    //Получаем должность сотрудника
                    Position = person.Positions;
                    //Получаем оплату
                    decimal salary = GetSalary(item.Times);
                    //Добавляем полученные данные в итоговую коллекцию
                    result.Add((person, item.Times, salary, taskslist));
                }
                return result;
            }
            else
                return null;           
        }

        /// <summary>
        /// Получаем сумму оплаты
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private decimal GetSalary(double time)
        {
            decimal salary = default;
            //В зависимости от должности рассчитываем оплату
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
            return salary;
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
