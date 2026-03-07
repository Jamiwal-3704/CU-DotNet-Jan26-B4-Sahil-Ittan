using System.Net.Mail;

namespace StudentManagementSystem
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Marks { get; set; }

        public override string ToString()
        {
            return $"Id - {Id} Name - {Name} Marks - {Marks}";
        }

    }

    // Manager Class
    class StudentManager
    {
        Dictionary<int, Student> StudentData = new Dictionary<int, Student>();

        public bool AddStudent(Student student)
        {
            int id = student.Id;
            if (!StudentData.ContainsKey(id))
            {
                StudentData.Add(student.Id, student);
                return true;
            }

            return false;
        }

        public void DisplayAllStudents()
        {
            foreach (var student in StudentData)
            {
                Console.WriteLine(student.Value);
            }
        }

        public Student SearchStudent(int Id)
        {
            Student student = null;
            bool found = StudentData.TryGetValue(Id, out student);

            return student;
        }

        public bool UpdateStudent(int Id, int marks)
        {
            Student foundStudent = SearchStudent(Id);

            if (foundStudent != null)
            {
                foundStudent.Marks = marks;
                return true;
            }
            return false;
        }

        public bool DeleteStudent(int Id)
        {
            // optimized method
            return StudentData.Remove(Id);

            // brute force method
            //Student foundStudent = SearchStudent(Id);
            //if (foundStudent != null)
            //{
            //    StudentData.Remove(Id);
            //    return true;
            //}
            //return false;
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            StudentManager manager = new StudentManager();
            int choice;

            do
            {
                Console.WriteLine("\n====== Student Management System ======");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Display All Students");
                Console.WriteLine("3. Search Student");
                Console.WriteLine("4. Update Student Marks");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Student ID: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("Enter Student Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter Student Marks: ");
                        int marks = int.Parse(Console.ReadLine());

                        bool added = manager.AddStudent(new Student
                        {
                            Id = id,
                            Name = name,
                            Marks = marks
                        });

                        Console.WriteLine(added
                            ? "Student added successfully."
                            : "Student with this ID already exists.");
                        break;

                    case 2:
                        manager.DisplayAllStudents();
                        break;

                    case 3:
                        Console.Write("Enter Student ID to search: ");
                        int searchId = int.Parse(Console.ReadLine());

                        Student foundStudent = manager.SearchStudent(searchId);
                        if (foundStudent == null)
                            Console.WriteLine("Student not found.");
                        else
                            Console.WriteLine(foundStudent);
                        break;

                    case 4:
                        Console.Write("Enter Student ID to update: ");
                        int updateId = int.Parse(Console.ReadLine());

                        Console.Write("Enter new marks: ");
                        int newMarks = int.Parse(Console.ReadLine());

                        bool updated = manager.UpdateStudent(updateId, newMarks);
                        Console.WriteLine(updated
                            ? "Student updated successfully."
                            : "Student not found.");
                        break;

                    case 5:
                        Console.Write("Enter Student ID to delete: ");
                        int deleteId = int.Parse(Console.ReadLine());

                        bool deleted = manager.DeleteStudent(deleteId);
                        Console.WriteLine(deleted
                            ? "Student deleted successfully."
                            : "Student not found.");
                        break;

                    case 6:
                        Console.WriteLine("Exiting application...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

            } while (choice != 0);
        }
    }
}
