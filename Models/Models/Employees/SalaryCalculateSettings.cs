namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    /// <summary>
    /// Класс с настройками для расчета зарплаты
    /// </summary>
    public class SalaryCalculateSettings
    {
        internal double NumberWorkingHoursPerMonth { get; } = 160;
        internal double NumberWorkingDaysPerMonth { get; } = 20;
        internal double NumberWoringHoursPerDay { get; } = 8;
        internal decimal BonusDirector { get; } = 20_000;
        internal double BonusCoefficient { get; } = 2;
    }
}
