using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services
{
    public class ReportService
    {
        private readonly List<CompletedTask> _completedTasks;
        private readonly Positions _position;
        private readonly decimal _baseSalary;
        private readonly int _month;

        public ReportService(Person person, IEnumerable<CompletedTask> completedTasks)
        {
            _position = person.Positions;
            _baseSalary = person.BaseSalary;
            _completedTasks = completedTasks.ToList();
            _month = GetFullMonth(completedTasks);
        }

        public (double, decimal) GetPersonReport()
        {
            double time = default;
            decimal salary = default;
            if (_position.Equals(Positions.Freelance))
            {
                time = GetSumTime();
                salary = GetSalaryFreelance(time);
            }
            else
            {
                if (_month == 0)
                {
                    time = GetSumTime();
                    salary = GetSalary(time);
                }
            }
            return (time, salary);

        }


        #region Реализация

        private int GetFullMonth(IEnumerable<CompletedTask> tasks)
        {
            var list = tasks.ToList();
            var firstmonth = list[0].Date.Month;
            var lasttmonth = list[list.Count - 1].Date.Month;
            return lasttmonth - firstmonth;
        }

        private double GetSumTime()
        {
            double sumTime = 0;

            foreach (var item in _completedTasks)
            {
                sumTime += item.Time;
            }

            return sumTime;
        }

        private decimal GetSalary(double time)
        {
            decimal _time = (decimal)time, salary = default;

            if (time <= 160)
            {
                salary = _baseSalary * _time / 160;
            }
            else
            {
                switch (_position)
                {
                    case Positions.Developer:
                        salary = _baseSalary * (1 + 2 * (_time - 160) / 160);
                        break;
                    case Positions.Director:
                        salary = _baseSalary + 20000 * (_time - 160) / 160;
                        break;
                }

            }
            return salary;
        }

        private decimal GetSalaryFreelance(double time)
        {
            decimal _time = (decimal)time;

            return _baseSalary * _time;
        }

        #endregion




    }
}
