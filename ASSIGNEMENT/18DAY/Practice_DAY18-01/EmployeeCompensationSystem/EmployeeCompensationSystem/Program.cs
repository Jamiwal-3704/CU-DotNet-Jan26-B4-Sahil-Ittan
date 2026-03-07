namespace EmployeeCompensationSystem
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal BasicSalary { get; set; }
        public int ExperienceInYears { get; set; }


        public Employee(int employeeId, string employeeName, decimal basicSalary, int experienceInYears)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            BasicSalary = basicSalary;
            ExperienceInYears = experienceInYears;
        }

        // methods
        public decimal CalculateAnnualSalary()
        {
            return BasicSalary * 12;
        }

        public void DisplayEmployeeDetails()
        {
            Console.WriteLine($"{EmployeeId}, {EmployeeName}, {CalculateAnnualSalary()}, {ExperienceInYears}");
        }
    }

    // DERIVED CLASS 
    public class PermanentEmployee : Employee
    {
        // inherit the method and implement the methods hidding using the method 	overloading
        public PermanentEmployee(int id, string name, decimal salary, int exp) : base(id, name, salary, exp) {}

        public new decimal CalculateAnnualSalary()
        {
            decimal annualSalary;
            decimal HouseRentAllowance = BasicSalary * 0.2m;
            decimal specialAllowance = BasicSalary * 0.1m;
            decimal loyaltyBonus = 50000;
            if (ExperienceInYears > 5)
            {
                annualSalary = (BasicSalary * 12 + HouseRentAllowance * 12 + specialAllowance * 12 + loyaltyBonus);
                return annualSalary;
            }
            else
            {
                annualSalary = (BasicSalary * 12 + HouseRentAllowance * 12 + specialAllowance * 12);
                return annualSalary;
            }
        }
    }

    public class ContractEmployee : Employee
    {
        public ContractEmployee(int id, string name, decimal salary, int exp) : base(id, name, salary, exp) { }

        public int ContractDurationMonths { get; set; }
        public new decimal CalculateAnnualSalary(int contractDurationMonths)
        {
            decimal annualSalary;
            decimal bonus = ContractDurationMonths >= 12 ? 30000 : 0;
            return (BasicSalary * 12) + bonus;
        }
    }

    public class InternEmployee : Employee
    {
        public InternEmployee(int id, string name, decimal salary, int exp) : base(id, name, salary, exp) { }

        decimal annualSalary;
        public new decimal CalculateAnnualSalary()
        {
            return BasicSalary * 12;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Employee emp1 = new Employee(14503, "sahil ittan", 150000, 15);
            PermanentEmployee emp2 = new PermanentEmployee(14524, "Om Sathapathy", 170000, 10);
            ContractEmployee emp3 = new ContractEmployee(14529, "Shivam Mourya", 100000, 9);
            InternEmployee emp4 = new InternEmployee(14554, "Lovish Garg", 120000, 13);

            emp1.DisplayEmployeeDetails();
            Console.WriteLine("emp1 : " + emp1.CalculateAnnualSalary());

            emp2.DisplayEmployeeDetails();
            Console.WriteLine("emp2 : " + emp2.CalculateAnnualSalary());

            emp3.DisplayEmployeeDetails();
            Console.WriteLine("emp3 : " + emp3.CalculateAnnualSalary());

            emp4.DisplayEmployeeDetails();
            Console.WriteLine("emp4 : "+emp4.CalculateAnnualSalary());
        }
    }
}
