using static PracticeDictionary.Student;

namespace PracticeDictionary
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }

        public Student(int studentID, string studentName)
        {
            StudentID = studentID;
            StudentName = studentName;
        }

        public override bool Equals(object obj)
        {
            Student other = obj as Student;

            if (other == null)
                return false;

            return this.StudentID == other.StudentID && 
                this.StudentName == other.StudentName;
        }

        public override string ToString()
        {
            return $"ID: {StudentID}, Name: {StudentName}";
        }

        public override int GetHashCode()
        {
            return StudentID.GetHashCode();
        }
    }

        public class StudentMarks
        {
            Dictionary<Student, int> StudentRecords = new Dictionary<Student, int>();

            public void AddStudentRecord(Student student, int marks)
            {
                if (StudentRecords.ContainsKey(student))
                {
                    if (marks > StudentRecords[student])
                    {
                        StudentRecords[student] = marks;
                    }
                }
                else
                {
                    StudentRecords.Add(student, marks);
                }
            }

            public void DisplayRecords()
            {
                foreach (var record in StudentRecords)
                {
                    Console.WriteLine($"ID: {record.Key.StudentID} Name: {record.Key.StudentName} Marks: {record.Value}");
                }
            }
        }
   

    internal class Program
    {
        static void Main(string[] args)
        {
            StudentMarks sm = new StudentMarks();

            sm.AddStudentRecord(new Student(1, "Alice"), 85);
            sm.AddStudentRecord(new Student(2, "Bob"), 90);
            sm.AddStudentRecord(new Student(1, "Alice"), 95); // improvement
            sm.AddStudentRecord(new Student(2, "Bob"), 80);   // lower marks ignored

            sm.DisplayRecords();
        }
    }

}