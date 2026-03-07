using System.Security.Cryptography.X509Certificates;

namespace StreamBuzzPlatform
{
    public class CreatorStats
    {
        public string CreatorName { get; set; }
        public double[] WeeklyLikes { get; set; }

        public static List<CreatorStats> EngagementBoard = new List<CreatorStats>();
    }

    internal class Program
    {
        public void RegisterCreator(CreatorStats record)
        {
            CreatorStats.EngagementBoard.Add(record);
        }

        public Dictionary<string, int> GetTopPostCounts(List<CreatorStats> records, double likeThreshold)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (var creator in records)
            {
                int count = 0;

                foreach (var like in creator.WeeklyLikes)
                {
                    if (like >= likeThreshold)
                    {
                        count++;
                    }
                }

                if (count > 0)
                {
                    result.Add(creator.CreatorName, count);
                }
            }

            return result;
        }

        public double CalculateAverageLikes()
        {
            double sum = 0;
            int count = 0;

            foreach (var creator in CreatorStats.EngagementBoard)
            {
                foreach (var like in creator.WeeklyLikes)
                {
                    sum += like;
                    count++;
                }
            }

            if (count == 0)
                return 0;

            return sum / count;
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            int choice;

            do
            {
                Console.WriteLine("1. Register Creator");
                Console.WriteLine("2. Show Top Posts");
                Console.WriteLine("3. Calculate Average Likes");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice:");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:

                        CreatorStats creator = new CreatorStats();

                        Console.WriteLine("Enter Creator Name:");
                        creator.CreatorName = Console.ReadLine();

                        Console.WriteLine("Enter weekly likes (Week 1 to 4):");

                        creator.WeeklyLikes = new double[4];

                        for (int i = 0; i < 4; i++)
                        {
                            creator.WeeklyLikes[i] = Convert.ToDouble(Console.ReadLine());
                        }

                        program.RegisterCreator(creator);

                        Console.WriteLine("Creator registered successfully");
                        Console.WriteLine();
                        break;

                    case 2:

                        Console.WriteLine("Enter like threshold:");
                        double threshold = Convert.ToDouble(Console.ReadLine());

                        var result = program.GetTopPostCounts(CreatorStats.EngagementBoard, threshold);

                        if (result.Count == 0)
                        {
                            Console.WriteLine("No top-performing posts this week");
                        }
                        else
                        {
                            foreach (var item in result)
                            {
                                Console.WriteLine(item.Key + " - " + item.Value);
                            }
                        }

                        Console.WriteLine();
                        break;

                    case 3:

                        double avg = program.CalculateAverageLikes();
                        Console.WriteLine("Overall average weekly likes: " + avg);
                        Console.WriteLine();
                        break;

                    case 4:

                        Console.WriteLine("Logging off - Keep Creating with StreamBuzz!");
                        break;

                    default:

                        Console.WriteLine("Invalid choice");
                        break;
                }

            } while (choice != 4);

        }
    }
}
