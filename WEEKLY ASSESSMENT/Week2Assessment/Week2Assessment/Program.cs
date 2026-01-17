using System;

namespace Week2Assessment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] policyHolderNames = new string[5];
            decimal[] annualPremiums = new decimal[5];

            for (int i = 0; i < policyHolderNames.Length; i++)
            {
                // Name validation
                while (true)
                {
                    Console.Write($"Enter the name of person {i + 1}: ");
                    string nameInput = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(nameInput))
                    {
                        policyHolderNames[i] = nameInput.ToUpper();
                        break;
                    }
                    Console.WriteLine("Name cannot be empty. Try again.");
                }

                // Premium validation
                while (true)
                {
                    Console.Write($"Enter the annual premium of person {i + 1}: ");
                    string premiumInput = Console.ReadLine();

                    if (decimal.TryParse(premiumInput, out decimal premium) && premium > 0)
                    {
                        annualPremiums[i] = premium;
                        break;
                    }
                    Console.WriteLine("Premium must be greater than 0. Try again.");
                }
            }

            
            decimal totalPremium = 0;
            decimal highestPremium = annualPremiums[0];
            decimal lowestPremium = annualPremiums[0];

            for (int i = 0; i < annualPremiums.Length; i++)
            {
                totalPremium += annualPremiums[i];

                if (annualPremiums[i] > highestPremium)
                    highestPremium = annualPremiums[i];

                if (annualPremiums[i] < lowestPremium)
                    lowestPremium = annualPremiums[i];
            }

            decimal averagePremium = totalPremium / annualPremiums.Length;

            
            Console.WriteLine("\nINSURANCE PREMIUM SUMMARY");
            Console.WriteLine(new string('-', 27));
            Console.WriteLine($"{"NAME",-12}{"PREMIUM",-12}{"CATEGORY",-12}");
            Console.WriteLine(new string('-', 45));

            for (int i = 0; i < policyHolderNames.Length; i++)
            {
                // Category calculated on the fly
                string category;

                if (annualPremiums[i] < 10000)
                    category = "LOW";
                else if (annualPremiums[i] <= 25000)
                    category = "MEDIUM";
                else
                    category = "HIGH";

                Console.WriteLine($"{policyHolderNames[i],-12}{annualPremiums[i],-12:0,000.00}{category,-12}");
            }

            Console.WriteLine(new string('-', 45));
            Console.WriteLine($"{"Total Premium",-20}: {totalPremium:0,000.00}");
            Console.WriteLine($"{"Average Premium",-20}: {averagePremium:0,000.00}");
            Console.WriteLine($"{"Highest Premium",-20}: {highestPremium:0,000.00}");
            Console.WriteLine($"{"Lowest Premium",-20}: {lowestPremium:0,000.00}");
        }
    }
}
