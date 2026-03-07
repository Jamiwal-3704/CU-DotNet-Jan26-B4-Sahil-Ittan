namespace EmployeeBonusSolution
{
    public class EmployeeBonus
    {
        public decimal BaseSalary { get; set; }
        public int PerformanceRating { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal DepartmentMultiplier { get; set; }
        public double AttendancePercentage { get; set; }

        public decimal NetAnnualBonus
        {
            get
            {
                if (BaseSalary <= 0)
                    return 0;

                decimal bonusPercentage;

                switch (PerformanceRating)
                {
                    case 5:
                        bonusPercentage = 0.25m;
                        break;

                    case 4:
                        bonusPercentage = 0.18m;
                        break;

                    case 3:
                        bonusPercentage = 0.12m;
                        break;

                    case 2:
                        bonusPercentage = 0.05m;
                        break;

                    case 1:
                        bonusPercentage = 0.00m;
                        break;

                    default:
                        throw new InvalidOperationException("Invalid rating");
                }

                decimal bonus = BaseSalary * bonusPercentage;

                if (YearsOfExperience > 10)
                    bonus += BaseSalary * 0.05m;
                else if (YearsOfExperience > 5)
                    bonus += BaseSalary * 0.03m;

                if (AttendancePercentage < 85)
                    bonus *= 0.80m;

                bonus *= DepartmentMultiplier;

                decimal maxBonus = BaseSalary * 0.40m;
                if (bonus > maxBonus)
                    bonus = maxBonus;

                decimal taxRate =
                    bonus <= 150000 ? 0.10m :
                    bonus <= 300000 ? 0.20m : 0.30m;

                decimal netBonus = bonus * (1 - taxRate);

                return Math.Round(netBonus, 2);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 500000,
                PerformanceRating = 5,
                YearsOfExperience = 6,
                DepartmentMultiplier = 1.1m,
                AttendancePercentage = 95
            };

            Console.WriteLine($"Net Annual Bonus: {emp.NetAnnualBonus}");


        }
    }
}
