namespace StudentEnrollmentSystem
{
    // ---------------- CUSTOM EXCEPTIONS ----------------
    class InvalidStudentAgeException : Exception
    {
        public InvalidStudentAgeException(string message) : base(message) { }
    }

    class InvalidStudentNameException : Exception
    {
        public InvalidStudentNameException(string message) : base(message) { }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // divide two numbers
            try
            {
                Console.WriteLine("Enter first number: ");
                int a = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter second number: ");
                int b = int.Parse(Console.ReadLine());

                int result = a / b;
                Console.WriteLine("result: " + result);
            }
            catch(DivideByZeroException)
            {
                Console.WriteLine("Number can't be divide by 0");
            }
            finally
            {
                Console.WriteLine("In finally block of DivideByZeroException");
            }

            // Convert input into integer

            try
            {
                Console.WriteLine("Enter a number: ");
                int num = int.Parse(Console.ReadLine());
                Console.WriteLine("you entered: " + num);
            }
            catch(FormatException)
            {
                Console.WriteLine("ERROR: Invalid number formate");
            }
            finally
            {
                Console.WriteLine("In finally block of FormateException");
            }

            // Array Index Access 
            try
            {
                int[] arr = { 10, 20, 30 };
                Console.Write("Enter array index (0-2): ");
                int index = int.Parse(Console.ReadLine());
                Console.WriteLine("Value: " + arr[index]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Error: Index out of range.");
            }
            finally
            {
                Console.WriteLine("Operation Completed\n");
            }

            // ---------------- PART 2 & 3: CUSTOM + INNER EXCEPTION ----------------

            try
            {
                string studentName = GetStudentName();
                int studentAge = GetStudentAge();

                Console.WriteLine($"\nStudent Enrolled Successfully!");
                Console.WriteLine($"Name: {studentName}, Age: {studentAge}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n--- Exception Details ---");
                Console.WriteLine("Message: " + ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }

                Console.WriteLine("\nStackTrace:");
                Console.WriteLine(ex.StackTrace);
            }
        }

        // ---------------- HELPER METHODS ----------------
        static int GetStudentAge()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Student Age: ");
                    int age = int.Parse(Console.ReadLine());

                    if (age < 18 || age > 60)
                        throw new InvalidStudentAgeException("Age must be between 18 and 60.");

                    return age;
                }
                catch (InvalidStudentAgeException ex)
                {
                    throw new Exception("Student Age Validation Failed", ex);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
        }

        static string GetStudentName()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Student Name: ");
                    string name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                        throw new InvalidStudentNameException("Student name cannot be empty.");

                    return name;
                }
                catch (InvalidStudentNameException ex)
                {
                    throw new Exception("Student Name Validation Failed", ex);
                }
            }
        }

    }
}