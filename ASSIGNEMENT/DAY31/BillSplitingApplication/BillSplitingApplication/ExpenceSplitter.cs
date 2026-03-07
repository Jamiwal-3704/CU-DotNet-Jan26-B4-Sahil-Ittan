using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillSplitingApplication
{
    internal class ExpenceSplitter
    {
        static List<string> SettleExpenceShare(Dictionary<string,double> expenses)
        {
            List<string> settlement = new List<string>();


            // Creditors and Debtors
            Queue<KeyValuePair<string,double>> receivers = new Queue<KeyValuePair<string, double>>();
            Queue<KeyValuePair<string,double>> payers = new Queue<KeyValuePair<string, double>>();

            // total expense
            var totalExpense = expenses.Values.Sum();
            var persons = expenses.Count;
            var share = totalExpense / persons;


            // after checking add persons to the particular creditor or debitor queue
            // populate payers and receivers queue
            foreach (var person in expenses)
            {
                if(person.Value > share)
                {
                    receivers.Enqueue(
                        new KeyValuePair<string, double>(person.Key, person.Value - share)
                        );
                }

                else if(person.Value < share)
                {
                    payers.Enqueue(
                        new KeyValuePair<string, double>(person.Key, Math.Abs(person.Value - share))
                        );
                }
            }

            // settlement 
            while( payers.Count > 0 && receivers.Count > 0)
            {

                var payer = payers.Dequeue();
                var receiver = receivers.Dequeue();

                var amount = Math.Min(payer.Value, receiver.Value);

                settlement.Add($"{payer.Key},{receiver.Key},{amount}");

                if(payer.Value > amount){
                    payers.Enqueue(new KeyValuePair<string, double>(payer.Key, Math.Abs(amount - payer.Value)));
                }

                if(receiver.Value > amount){
                    receivers.Enqueue(new KeyValuePair<string, double>(receiver.Key, Math.Abs(amount - receiver.Value)));
                }
            }
            return settlement;
        }

        static void Main(string[] arg)
        {
            Dictionary<string, double> expenses = new Dictionary<string, double>()
            {
                //new KeyValuePair<string,double>()
                {"Aman", 900},
                {"Sunil", 0},
                {"Kartik", 1290}
            };

            List<string> settlement =  SettleExpenceShare(expenses);
            // settlement item - from, to, amount

            foreach(var payment in settlement)
            {
                Console.WriteLine(payment);
            }
        }
    }
}
