using System;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository
{
    public interface ICountTimeRepository
    {
        void AddWorkingTime(DateTime date, double time);
        decimal GetSalary(DateTime beginDate, DateTime lastDate);
    }
}
