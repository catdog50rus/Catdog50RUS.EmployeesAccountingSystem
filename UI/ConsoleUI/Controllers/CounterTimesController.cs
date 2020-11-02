using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Controllers
{
    public class CounterTimesController
    {
        private readonly ICountTimeRepository _counterRepository;

        public CounterTimesController()
        {
            _counterRepository = new FileCountTimeRepository();
        }

        public async Task AddNewTask(CounterTimes counter)
        {
            
            if (counter != null)
            {
                await _counterRepository.AddWorkingTime(counter);
            }
        }

        public async Task<IEnumerable<CounterTimes>> GetPersonTask(Person person, DateTime beginDate, DateTime lastDate)
        {
            var result = await _counterRepository.GetPersonsTaskListAsync(person, beginDate, lastDate);
            return result;
        }

        private (double,decimal) GetSumm(IEnumerable<CounterTimes> tasksList)
        {
            List<CounterTimes> list = tasksList.ToList();
            double summTime = 0;
            decimal salary;

            foreach (var item in tasksList)
            {
                summTime += item.Time;
            }

            var person = list[0].Person;
            switch (person.Positions)
            {
                case Positions.Director:
                    break;
                case Positions.Developer:
                    break;
                case Positions.Freelance:
                    break;
                case Positions.None:
                    break;
            }

            
            if (summTime <= 160)
            {
                salary = person.BaseSalary * (decimal)summTime / 160;
            }
            else
            {
                salary = person.BaseSalary * (160 + 2*(decimal)(summTime - 160)/160);
            }
            return (summTime, salary);
        }
    }
}
