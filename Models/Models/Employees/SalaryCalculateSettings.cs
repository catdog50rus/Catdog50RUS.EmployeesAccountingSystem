namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    public class SalaryCalculateSettings
    {
        internal double NumberWorkingHoursPerMonth { get; } = 160;
        internal double NumberWorkingDaysPerMonth { get; } = 20;
        internal double NumberWoringHoursPerDay { get; } = 8;
        internal decimal BonusDirector { get; } = 20_000;
        internal double BonusCoefficient { get; } = 2;

               
        public SalaryCalculateSettings()
        {
                
        }

        public SalaryCalculateSettings(SalaryCalculateSettings settings)
        {
          
            NumberWorkingHoursPerMonth = settings.NumberWorkingHoursPerMonth;
            NumberWorkingDaysPerMonth = settings.NumberWorkingDaysPerMonth;
            NumberWoringHoursPerDay = settings.NumberWoringHoursPerDay;
            BonusDirector = settings.BonusDirector;
            BonusCoefficient = settings.BonusCoefficient;
        }


    }
}
