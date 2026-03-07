using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace LoanPortfolioManager
{
    class Loan
    {

        public string ClientName { get; set; }
        public double Principal { get; set; }
        public double interestRate { get; set; }

        string status;

        public string Status(int interestRate)
        {
            if(interestRate < 5)
            {
                status = "Low Risk";
            }
            else if(interestRate > 5 && interestRate < 10)
            {
                status = "Medium Risk";
            }
            else
            {
                status = "High Risk";
            }
            return status;
        }
    }

    internal class Program
    {
        public static Loan AddList(string client)
        {
            string[] arr = client.Split(',');

            Loan loan =  new Loan()
            {
                ClientName = arr[0],
                Principal = double.Parse(arr[1]),
                interestRate = double.Parse(arr[2])
            };
            return loan;
        }

        public static void ReadCsv(string csvFile)
        {
            using StreamReader sr = new StreamReader(csvFile);
            string? temp = sr.ReadLine();
            while(temp != null)
            {
                Console.WriteLine(temp);
                temp = sr.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            List<Loan> LoanList = new List<Loan>();
            string csvFile = @"..\..\..\loanDetails.csv";
            using StreamWriter loanWriter = new StreamWriter(csvFile,true);
            
            string input = string.Empty;

            while(input != "stop")
            {
                input = Console.ReadLine();
                if(input != null && input != "stop")
                {
                    Loan tempLoan = AddList(input);
                    LoanList.Add(tempLoan);
                    loanWriter.WriteLine(input);
                }
                else
                {
                    break;
                }
            }
            loanWriter.Close();
            ReadCsv(csvFile);
        }
    }
}
