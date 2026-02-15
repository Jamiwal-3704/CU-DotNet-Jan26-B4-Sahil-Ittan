using System.Runtime.Serialization;

namespace SkillBalance
{
    class SkillBalance
    {
        public static int SkillBalanceMain(int k, List<List<int>> contests)
        {
            int totalGain = 0;
            List<int> zero = new List<int>();
            List<int> one = new List<int>();

            for (int i = 0; i < contests.Count; i++)
            {
                if (contests[i][1] == 0)
                {
                    zero.Add(contests[i][0]);
                    Console.WriteLine($"zero --> {contests[i][0]}");
                }
                else
                {
                    one.Add(contests[i][0]);
                    Console.WriteLine($"One --> {contests[i][0]}");
                }
            }
            Console.WriteLine("TotalGain from UnImportant contests: ");
            Console.Write("UnImportantSum = ");
            int unImportantSum = 0;
            foreach (var unImportant in zero)
            {
                //Console.WriteLine($"Adding {unImportant} to TotalGain");
                totalGain += unImportant;
                unImportantSum += unImportant;
            }
            Console.WriteLine(unImportantSum);
            Console.WriteLine();

            // sort the oneList and reverse it to take the highest elements till k and add them to the totalGain and otherwise subtract other elements 
            one.Sort();
            one.Reverse();

            Console.WriteLine("TotalGain from Important contests: ");

            int[] selectedElementFromOne = new int[one.Count];
            int ImportantSum = 0;
            for (int i = 0; i < one.Count; i++)
            {
                if (i < k)
                {
                    ImportantSum += one[i];
                    totalGain += one[i];
                    selectedElementFromOne[i] = one[i];
                }
                else
                {
                    totalGain -= one[i];
                }
            }
            Console.Write("ImportantSum = ");
            Console.WriteLine(ImportantSum);
            Console.WriteLine();
            int winContest = 0;
            foreach(int win in one)
            {   if (!selectedElementFromOne.Contains(win))
                {
                    winContest += win;
                }
            }
            Console.WriteLine("WinContests: " + winContest);
            Console.WriteLine($"{ImportantSum} + {unImportantSum} - {winContest}");
            int total = ImportantSum + unImportantSum - winContest;
            return totalGain;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int k = 2;

            List<List<int>> Contests = new List<List<int>>()
            {
                new List<int>{10,1},
                new List<int>{8,1},
                new List<int>{5,1},
                new List<int>{7,0},
                new List<int>{4,0}
            };

            //SkillBalance skill = new SkillBalance();
            int result = SkillBalance.SkillBalanceMain(k, Contests);
            Console.WriteLine("Result : " + result);
        }
    }
}
