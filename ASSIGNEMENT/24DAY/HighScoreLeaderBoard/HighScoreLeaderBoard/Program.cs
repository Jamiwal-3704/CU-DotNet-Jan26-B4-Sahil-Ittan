using System.Collections.Generic;

namespace HighScoreLeaderBoard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<double, string> leaderBoard = new SortedDictionary<double, string>()
            {
                {55.42,"SwiftRacer"},
                {52.10,"SpeedDemon"},
                {58.91,"SteadyEddie"},
                {51.05,"TurboTom"}
            };

            // printing all the leader board according to the lap timings
            foreach (var item in leaderBoard)
            {
                Console.WriteLine($"{item.Value} : {item.Key}");
            }

            // find the fastest Racer in the race
            var fastest = leaderBoard.First();
            Console.WriteLine($"Fastest Racer: " + fastest);

            leaderBoard.Remove(58.91);
            leaderBoard.Add(54, "SteadyEddie");

            Console.WriteLine("Updated LeaderBoard");

            foreach(var item in leaderBoard)
            {
                Console.WriteLine($"Name: {item.Value} --> Speed: {item.Key}");
            }
        }
    }
}
