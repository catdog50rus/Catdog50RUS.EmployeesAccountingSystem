namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public class ReportSettings
    {
        public double NumberWorkingHoursPerMonth { get; }
        public double NumberWorkingDaysPerMonth { get; }
        public double NumberWoringHoursPerDay { get; }
        public decimal BonusDirector { get; }
        public decimal BonusCoefficient { get; }

        public ReportSettings(double numberWorkingHoursPerMonth, double numberWorkingDaysPerMonth, 
                              double numberWoringHoursPerDay, decimal bonusDirector, decimal bonusCoefficient)
        {
            NumberWorkingHoursPerMonth = numberWorkingHoursPerMonth;
            NumberWorkingDaysPerMonth = numberWorkingDaysPerMonth;
            NumberWoringHoursPerDay = numberWoringHoursPerDay;
            BonusDirector = bonusDirector;
            BonusCoefficient = bonusCoefficient;
        }

        public string ToFile(char dataSeparator)
        {
            return $"{NumberWorkingHoursPerMonth}{dataSeparator} " +
                   $"{NumberWorkingDaysPerMonth}{dataSeparator} " +
                   $"{NumberWoringHoursPerDay}{dataSeparator} " +
                   $"{BonusDirector}{dataSeparator} " +
                   $"{BonusCoefficient}{dataSeparator}";
        }

        public override string ToString() => $"{NumberWorkingHoursPerMonth}, " +
                                             $"{NumberWorkingDaysPerMonth}, " +
                                             $"{NumberWoringHoursPerDay}, " +
                                             $"{BonusCoefficient}, " +
                                             $"{BonusDirector}";

        public override bool Equals(object obj) => ToString().Equals(obj.ToString());

        public override int GetHashCode() =>base.GetHashCode();
    }
}
