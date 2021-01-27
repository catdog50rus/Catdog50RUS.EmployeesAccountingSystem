namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    public class SalaryCalculateSettings
    {
        internal readonly decimal NUMBER_WORKING_HOURS_PER_MONTH = 160M;
        internal readonly int NUMBER_WORKING_DAYS_PER_MONTH = 20;
        internal readonly double NUMBER_WORKING_HOURS_PER_DAY = 8;
        internal readonly double BONUS_COEFFICIENT = 2.0;
        internal readonly decimal BONUS_DIRECTOR = 20_000;

        public SalaryCalculateSettings()
        {

        }
    }
}
