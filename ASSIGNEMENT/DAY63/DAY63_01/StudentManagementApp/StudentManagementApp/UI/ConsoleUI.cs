using StudentManagementApp.Models;
using StudentManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementApp.UI
{
    public class ConsoleUI
    {
        private readonly StudentService _service;

        public ConsoleUI(StudentService service)
        {
            _service = service;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n1. Add Student");
                Console.WriteLine("2. View All");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Delete");
                Console.WriteLine("5. Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Grade: ");
                        double grade = double.Parse(Console.ReadLine());

                        _service.AddStudent(new Student { Id = id, Name = name, Grade = grade });
                        break;

                    case "2":
                        var students = _service.GetAllStudents();
                        foreach (var s in students)
                            Console.WriteLine($"{s.Id} - {s.Name} - {s.Grade}");
                        break;

                    case "3":
                        Console.Write("Id: ");
                        int uid = int.Parse(Console.ReadLine());

                        Console.Write("Name: ");
                        string uname = Console.ReadLine();

                        Console.Write("Grade: ");
                        double ugrade = double.Parse(Console.ReadLine());

                        _service.UpdateStudent(new Student { Id = uid, Name = uname, Grade = ugrade });
                        break;

                    case "4":
                        Console.Write("Id: ");
                        int did = int.Parse(Console.ReadLine());

                        _service.DeleteStudent(did);
                        break;

                    case "5":
                        return;
                }
            }
        }
    }
}
