using StudentManagementApp.Repository;
using StudentManagementApp.Services;
using StudentManagementApp.UI;

namespace StudentManagementApp
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Choose Storage:");
            Console.WriteLine("1. In-Memory");
            Console.WriteLine("2. JSON");

            string choice = Console.ReadLine();

            IStudentRepository repository;

            if (choice == "1")
                repository = new ListStudentRepository();
            else
                repository = new JsonStudentRepository();

            var service = new StudentService(repository);
            var ui = new ConsoleUI(service);

            ui.Run();
        }
    }
}
