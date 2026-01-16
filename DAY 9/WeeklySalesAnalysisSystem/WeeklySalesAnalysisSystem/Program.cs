using System.Diagnostics;

namespace WeeklySalesAnalysisSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int DAYS = 7;
            decimal[] dailySales = new decimal[DAYS];
            string[] salesCategory = new string[DAYS];

            // -----------------------------
            // 1. Data Capture with Validation
            // -----------------------------

            for (int i = 0; i < DAYS; i++)
            {
                while (true)
                {
                    Console.Write($"Enter sales for Day {i + 1}: ");

                    bool isValid = decimal.TryParse(Console.ReadLine(), out decimal sale);

                    if (isValid && sale >= 0)
                    {
                        dailySales[i] = sale;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Sales must be a non-negative number.");
                    }
                }
            }

                // -----------------------------
                // 2. Weekly Sales Analysis
                // -----------------------------

                decimal averagSales;
                int dayAboveAverage = 0;
                decimal totalSales = 0;
                decimal highestSale = dailySales[0];
                decimal lowestSale = dailySales[0];
                decimal averageSales = 0;
                decimal highestDays = 1;
                decimal lowestDays = 1;

                for(int i = 0; i < DAYS;i++)
                {
                    totalSales += dailySales[i];

                    if (dailySales[i] > highestSale)
                    {
                        highestSale = dailySales[i];
                        highestDays = i + 1;
                    }

                    if (dailySales[i] < lowestSale)
                    {
                        lowestSale = dailySales[i];
                        lowestDays = i + 1;
                    }
                }

                averageSales = totalSales / DAYS;

                for(int i = 0; i < DAYS; i++)
                {
                    if (dailySales[i] > averageSales)
                    {
                        dayAboveAverage++;
                    }
                }

            // -----------------------------
            // 3. Sales Categorization (Parallel Array)
            // -----------------------------

            //string[] salesCategory = new string[DAYS];

            for (int i = 0; i < DAYS; i++)
            {
                if (dailySales[i] < 5000)
                {
                    salesCategory[i] = "Low";
                }
                else if (dailySales[i] >= 5001 && dailySales[i] < 1500)
                {
                    salesCategory[i] = "Medium";
                }
                else
                {
                    salesCategory[i] = "High";
                }
            }

            // -----------------------------
            // 4. Output Report
            // -----------------------------

            Console.WriteLine("Weekly Sales Report");
            Console.WriteLine("-------------------");

            // formate me right side spacing kese dete the ?
            string total_Sales = "total Sales";
            //Console.WriteLine($"{total_Sales.PadRight(25)}:{totalSales,10:f2}");
            Console.WriteLine($"Total Sales        :{totalSales:f2}");
            Console.WriteLine($"Average Daily Sales: {averageSales:f2}");
            Console.WriteLine($"Highest Sales      : {highestSale:f2}");
            Console.WriteLine($"Lowesr Sales       : {lowestSale:f2}\n");
            Console.WriteLine($"Days Above Average : {dayAboveAverage}");
            Console.WriteLine("Day-wise Sales Category:");

            for (int i = 0; i < DAYS; i++)
            {
                Console.WriteLine($"Day {i + 1} : {salesCategory[i]}");
            }

            Console.ReadLine();

        }
    }
}
