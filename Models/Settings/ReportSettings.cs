namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public class ReportSettings
    {
        public int NormTimeInMonth { get; }
        public decimal BonusDirector { get; }
        public decimal BonusCoefficient { get; }

        public ReportSettings(int normTimeInMonth, decimal bonusDirector, decimal bonusCoefficient)
        {
            NormTimeInMonth = normTimeInMonth;
            BonusDirector = bonusDirector;
            BonusCoefficient = bonusCoefficient;
        }
        

        public string ToFile(char dataSeparator)
        {
            return $"{NormTimeInMonth}{dataSeparator} " +
                   $"{BonusDirector}{dataSeparator} " +
                   $"{BonusCoefficient}{dataSeparator}";
        }
    }
}
