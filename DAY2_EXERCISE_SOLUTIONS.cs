using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLearning.Exercise
{
    internal class DAY2_EXERCISE_SOLUTIONS
    {
        public static void Main(string[] args)
        {
            Exercise1.attendenceCalculator();
            Exercise2.ResultProcessor();
            Exercise3.LibraryFineCalculator();
            Exercise4.BankingInterestCalculator();
            Exercise5.EcommercePricingEngine();
            Exercise6.WeatherMonitor();
            Exercise7.UniversityGrdingEngine();
            Exercise8.MobileDataUsageTracker();
            Exercise9.WarehouseInventoryManager();
            Exercise10.PayrollSystem();
        }

        class Exercise1
        {
            public static void attendenceCalculator()
            {
                int totalLectures = 100;
                int attendedLectures = 86;

                double attendancePercentage = (attendedLectures / (double)totalLectures) * 100;
                Console.WriteLine("Attendance Percentage: " + attendancePercentage + "%");

                // Now convert for display in grade by truncating decimal values
                int truncatedPercentage = (int)attendancePercentage;

                // now convert for display in grade by rounding off decimal values
                int roundedPercentage = (int)Math.Round(attendancePercentage);
            }
        }

        class Exercise2
        {
            public static void ResultProcessor()
            {
                int maths = 92;
                int physics = 96;
                int chemistry = 90;
                int english = 88;

                int subjectCount = 4;

                double average = (maths + physics + chemistry + english) / (double)subjectCount;
                Console.WriteLine($"Average Marks (2 decimals): {average:F2}");

                // converting average to integer for schooarship eligibility
                int truncatedAverage = (int)average;
                int rounded = (int)Math.Round(average);
                Console.WriteLine($"Truncated Average Marks: {truncatedAverage}");
                Console.WriteLine($"Rounded Average Marks: {rounded}");

            }
        }

        class Exercise3
        {
            public static void LibraryFineCalculator()
            {
                int daysOverdue = 5;
                double finePerDay = 0.50;
                double totalFine = daysOverdue * finePerDay;
                Console.WriteLine("Total Library Fine: $" + totalFine);

                double analyticsFine = (double)totalFine;

                Console.WriteLine($"Days Overdue : {daysOverdue}");
                Console.WriteLine($"Fine Per Day : {finePerDay:C}");
                Console.WriteLine($"Total Fine  : {totalFine:C}");
                Console.WriteLine($"Analytics Value (double): {analyticsFine}");
            }
        }

        class Exercise4
        {
            public static void BankingInterestCalculator()
            {
                decimal balance = 10000m;
                float interestRate = 5.5f; // from API

                // Convert float → decimal explicitly
                decimal rate = (decimal)interestRate / 100;

                decimal monthlyInterest = balance * rate / 12;
                balance += monthlyInterest;

                Console.WriteLine($"Updated Balance: {balance:C}");
            }
        }


        class Exercise5
        {
            public static void EcommercePricingEngine()
            {
                double cartTotal = 999.99 + 499.50 + 250.25;

                // Convert to decimal BEFORE financial rules
                decimal subtotal = (decimal)cartTotal;

                decimal taxRate = 0.18m;
                decimal discountRate = 0.10m;

                decimal tax = subtotal * taxRate;
                decimal discount = subtotal * discountRate;

                decimal finalAmount = subtotal + tax - discount;

                Console.WriteLine($"Final Payable Amount: {finalAmount:C}");
            }
        }

        class Exercise6
        {
            public static void WeatherMonitor()
            {
                short rawSensorValue = 325; // example sensor reading

                // Convert to Celsius (example formula)
                double temperatureCelsius = rawSensorValue / 10.0;

                // Daily average conversion
                int displayedAverage = (int)Math.Round(temperatureCelsius);

                Console.WriteLine($"Temperature (C): {temperatureCelsius}");
                Console.WriteLine($"Dashboard Avg : {displayedAverage}");
            }
        }

        class Exercise7
        {
            public static void UniversityGrdingEngine()
            {
                double finalScore = 86.75;
                byte grade;

                if (finalScore >= 90) grade = 10;
                else if (finalScore >= 80) grade = 9;
                else if (finalScore >= 70) grade = 8;
                else if (finalScore >= 60) grade = 7;
                else grade = 0;

                Console.WriteLine($"Final Score: {finalScore}");
                Console.WriteLine($"Grade: {grade}");
            }
        }

        class Exercise8
        {
            public static void MobileDataUsageTracker()
            {
                long bytesUsed = 5_368_709_120; // 5 GB

                double mb = bytesUsed / (1024.0 * 1024);
                double gb = bytesUsed / (1024.0 * 1024 * 1024);

                int roundedGB = (int)Math.Round(gb);

                Console.WriteLine($"Usage: {mb:F2} MB");
                Console.WriteLine($"Usage: {gb:F2} GB");
                Console.WriteLine($"Monthly Summary: {roundedGB} GB");
            }
        }

        class Exercise9
        {
            public static void WarehouseInventoryManager()
            {
                int currentItems = 42000;
                ushort maxCapacity = 50000;

                if (currentItems <= maxCapacity)
                {
                    Console.WriteLine("Within capacity");
                }
                else
                {
                    Console.WriteLine("Capacity exceeded");
                }
            }
        }

        class Exercise10
        {
            public static void PayrollSystem()
            {
                int basicSalary = 40000;
                double allowance = 5000.75;
                double deduction = 2200.50;

                decimal netSalary =
                    basicSalary +
                    (decimal)allowance -
                    (decimal)deduction;

                Console.WriteLine($"Net Salary: {netSalary:C}");
            }
        }

    }
}
