namespace EmployeeCompensationSystem
{
    internal class Program
    {
        class Employee
        {
            public int EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public decimal BasicSalary { get; set; }
            public int ExperienceInYears { get; set; }

            // Parameterized Constructor
            public Employee(int id, string name, decimal basicSalary, int experience)
            {
                EmployeeId = id;
                EmployeeName = name;
                BasicSalary = basicSalary;
                ExperienceInYears = experience;
            }

            // Base Method (NOT virtual)
            public decimal CalculateAnnualSalary()
            {
                return BasicSalary * 12;
            }

            public void DisplayEmployeeDetails()
            {
                Console.WriteLine($"ID: {EmployeeId}");
                Console.WriteLine($"Name: {EmployeeName}");
                Console.WriteLine($"Annual Salary: {CalculateAnnualSalary()}");
                Console.WriteLine("-----------------------------------");
            }
        }

        // 2. Permanent Employee
        class PermanentEmployee : Employee
        {
            public PermanentEmployee(int id, string name, decimal basicSalary, int experience)
                : base(id, name, basicSalary, experience)
            {
            }

            // Method Hiding
            public new decimal CalculateAnnualSalary()
            {
                decimal hra = BasicSalary * 0.20m;
                decimal specialAllowance = BasicSalary * 0.10m;
                decimal loyaltyBonus = ExperienceInYears >= 5 ? 50000 : 0;

                return (BasicSalary + hra + specialAllowance) * 12 + loyaltyBonus;
            }
        }

        // 3. Contract Employee
        class ContractEmployee : Employee
        {
            public int ContractDurationInMonths { get; set; }

            public ContractEmployee(int id, string name, decimal basicSalary, int experience, int duration)
                : base(id, name, basicSalary, experience)
            {
                ContractDurationInMonths = duration;
            }

            // Method Hiding
            public new decimal CalculateAnnualSalary()
            {
                decimal bonus = ContractDurationInMonths >= 12 ? 30000 : 0;
                return (BasicSalary * 12) + bonus;
            }
        }

        // 4. Intern Employee
        class InternEmployee : Employee
        {
            public InternEmployee(int id, string name, decimal stipend, int experience)
                : base(id, name, stipend, experience)
            {
            }

            // Method Hiding
            public new decimal CalculateAnnualSalary()
            {
                return BasicSalary * 12;
            }
        }

        static void Main(string[] args)
        {
            // Base class object
            Employee emp = new Employee(1, "Amit", 30000, 3);

            // Base reference → Derived object
            Employee permEmpBaseRef = new PermanentEmployee(2, "Rohit", 50000, 6);

            // Derived reference
            PermanentEmployee permEmp = new PermanentEmployee(3, "Neha", 50000, 6);

            ContractEmployee contractEmp = new ContractEmployee(4, "Suresh", 40000, 4, 14);
            InternEmployee internEmp = new InternEmployee(5, "Pooja", 15000, 0);

            Console.WriteLine("Using Base Class Reference:");
            Console.WriteLine(permEmpBaseRef.CalculateAnnualSalary()); // Base method

            Console.WriteLine("\nUsing Derived Class Reference:");
            Console.WriteLine(permEmp.CalculateAnnualSalary()); // Derived method

            Console.WriteLine("\nOther Employees:");
            emp.DisplayEmployeeDetails();
            permEmp.DisplayEmployeeDetails();
            contractEmp.DisplayEmployeeDetails();
            internEmp.DisplayEmployeeDetails();
        }
    }
}
