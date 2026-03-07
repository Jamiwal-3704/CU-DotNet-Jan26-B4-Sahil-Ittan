namespace DailyLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file = @"..\..\..\journal.txt";
            //FileStream fs = new FileStream(file, FileMode.Append, FileAccess.ReadWrite);
            using StreamWriter fs = new StreamWriter(file, true);
            Console.WriteLine("journal.txt is open. Please enter your journal entry:");
            string? input = Console.ReadLine();
            fs.WriteLine(input);
        }
    }
}
