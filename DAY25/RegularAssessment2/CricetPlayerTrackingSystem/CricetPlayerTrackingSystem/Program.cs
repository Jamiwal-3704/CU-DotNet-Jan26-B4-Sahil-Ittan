namespace CricetPlayerTrackingSystem
{
    class Player
    {
        public string Name { get; set; }
        public int RunsScored { get; set; }
        public int BallsFaced { get; set; }
        public bool IsOut { get; set; }
        public double StrikeRate { get; set; }
        public double Average { get; set; }

        public Player(string name, int runs, int balls, bool isOut)
        { 
            Name = name;
            RunsScored = runs;
            BallsFaced = balls;
            IsOut = isOut;
        }

        public void CalculateStats()
        {
            if(BallsFaced == 0)
            {
                throw new DivideByZeroException();
            }

            StrikeRate = (double)RunsScored / BallsFaced * 100;
            Average = IsOut ? RunsScored : RunsScored;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Write data to csv
                string file = @"..\..\..\player.csv";
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine("Name,Runs,Balls,IsOut");
                    sw.WriteLine("Virat Kohli,120,80,true");
                    sw.WriteLine("Steve Smith,84,90,true");
                    sw.WriteLine("Rohit Sharma,85,60,false");
                    sw.WriteLine("MS Dhoni,50,30,true");
                    sw.WriteLine("Joe Root,110,120,true");
                    // will be filtered (<10 balls)
                    sw.WriteLine("New Player,5,4,true"); 
                }

                // Read process csv
                List<Player> players = new List<Player>();

                foreach(string line in File.ReadAllLines(file))
                {
                    if(line.StartsWith("Name"))
                    {
                        continue;
                    }
                    string[] data = line.Split(',');

                    string name = data[0].Trim();
                    int runs = int.Parse(data[1].Trim());
                    int balls = int.Parse(data[2].Trim());
                    bool isOut = bool.Parse(data[3].Trim());

                    //Filter condition
                    if(balls < 10)
                    {
                        continue;
                    }

                    Player p = new Player(name, runs, balls, isOut);
                    p.CalculateStats();

                    players.Add(p);
                }

                // 3️⃣ SORT BY STRIKE RATE (DESC)
                players = players
                          .OrderByDescending(p => p.StrikeRate)
                          .ToList();

                // 4️⃣ DISPLAY OUTPUT
                Console.WriteLine();
                Console.WriteLine("Name\t\tRuns\tSR\tAvg");
                Console.WriteLine("---------------------------------------");

                foreach (Player p in players)
                {
                    Console.WriteLine(
                        $"{p.Name,-15}{p.RunsScored,-8}{p.StrikeRate:F2}\t{p.Average:F2}");
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("csv file Exception called");
                Console.WriteLine(ex);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid data format in CSV");
                Console.WriteLine(ex);
            }
            catch(DivideByZeroException ex)
            {
                Console.WriteLine("Balls faced cannot be zero");
                Console.WriteLine(ex);
            }
        }
    }
}
