//using System;

//namespace LoanEMICalculation
//{
//    // Base Class
//    class Loan
//    {
//        public string LoanNumber { get; set; }
//        public string CustomerName { get; set; }
//        public decimal PrincipalAmount { get; set; }
//        public int TenureInYears { get; set; }

//        public Loan(string loanNumber, string customerName, decimal principal, int tenure)
//        {
//            LoanNumber = loanNumber;
//            CustomerName = customerName;
//            PrincipalAmount = principal;
//            TenureInYears = tenure;
//        }

//        // Base EMI Calculation (10% Simple Interest)
//        public decimal CalculateEMI()
//        {
//            decimal interest = PrincipalAmount * 0.10m;
//            decimal totalAmount = PrincipalAmount + interest;
//            int months = TenureInYears * 12;

//            return totalAmount / months;
//        }
//    }

//    // Derived Class: HomeLoan
//    class HomeLoan : Loan
//    {
//        public HomeLoan(string loanNumber, string customerName, decimal principal, int tenure)
//            : base(loanNumber, customerName, principal, tenure)
//        {
//        }

//        // Method Hiding
//        public new decimal CalculateEMI()
//        {
//            decimal interest = PrincipalAmount * 0.08m;
//            decimal processingFee = PrincipalAmount * 0.01m;
//            decimal totalAmount = PrincipalAmount + interest + processingFee;

//            return totalAmount / (TenureInYears * 12);
//        }
//    }

//    // Derived Class: CarLoan
//    class CarLoan : Loan
//    {
//        public CarLoan(string loanNumber, string customerName, decimal principal, int tenure)
//            : base(loanNumber, customerName, principal, tenure)
//        {
//        }

//        // Method Hiding
//        public new decimal CalculateEMI()
//        {
//            decimal updatedPrincipal = PrincipalAmount + 15000;
//            decimal interest = updatedPrincipal * 0.09m;
//            decimal totalAmount = updatedPrincipal + interest;

//            return totalAmount / (TenureInYears * 12);
//        }
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            // Create loans
//            Loan[] loans = new Loan[4];

//            loans[0] = new HomeLoan("HL001", "Amit", 2000000, 20);
//            loans[1] = new HomeLoan("HL002", "Neha", 1500000, 15);
//            loans[2] = new CarLoan("CL001", "Ravi", 600000, 5);
//            loans[3] = new CarLoan("CL002", "Sonal", 800000, 6);

//            Console.WriteLine("EMI Details:\n");

//            foreach (Loan loan in loans)
//            {
//                Console.WriteLine($"Loan No: {loan.LoanNumber}");
//                Console.WriteLine($"Customer: {loan.CustomerName}");
//                Console.WriteLine($"EMI: {loan.CalculateEMI()}");
//                Console.WriteLine("-----------------------------");
//            }
//        }
//    }
//}

using System;

namespace LoanEMICalculation
{
    class Loan
    {
        public string LoanNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal PrincipalAmount { get; set; }
        public int Tenure { get; set; } // in years

        public Loan()
        {
            LoanNumber = string.Empty;
            CustomerName = string.Empty;
            PrincipalAmount = 0;
            Tenure = 0;
        }
        public Loan(string loanNumber, string customerName, decimal principalAmount, int tenure)
        {
            this.LoanNumber = loanNumber;
            this.CustomerName = customerName;
            this.PrincipalAmount = principalAmount;
            this.Tenure = tenure;
        }

        public decimal EmiCalculator()
        {
            float rateOfInterest = 10 / 100;
            decimal Result = (PrincipalAmount + (PrincipalAmount * (decimal)rateOfInterest)) / Tenure;
            return Result;
        }
    }
    class HomeLoan : Loan
    {
        public HomeLoan(string loanNumber, string customerName, decimal principalAmount, int tenure) : base(loanNumber, customerName, principalAmount, tenure)
        { }
        public new decimal EmiCalculator()
        {
            float rateOfInterest = 8 / 100;
            decimal Result = ((PrincipalAmount + (PrincipalAmount * (decimal)rateOfInterest)) / Tenure) + PrincipalAmount * (decimal)0.01;
            return Result;

        }
    }

    class CarLoan : Loan
    {
        public CarLoan(string loanNumber, string customerName, decimal principalAmount, int tenure) : base(loanNumber, customerName, principalAmount, tenure)
        { }
        public new decimal EmiCalculator()
        {
            float rateOfInterest = 9 / 100;
            decimal Result = ((PrincipalAmount + (PrincipalAmount * (decimal)rateOfInterest)) / Tenure) + PrincipalAmount * (decimal)0.005;
            return Result;

        }
    }

    //base class array can be Created instead of creating object array of derived class
    internal class LoanNumberPrint
    {
        public static void Main()
        {
            Loan[] loans = new Loan[2];
            HomeLoan hl = new HomeLoan("150001", "Alice", 500000, 10);
            CarLoan cl = new CarLoan("250001", "Bob", 500000, 10);
            loans[0] = hl;
            loans[1] = cl;
            for (int i = 0; i < loans.Length; i++)
            {
                Console.WriteLine(loans[i].EmiCalculator());
            }
        }
    }

}