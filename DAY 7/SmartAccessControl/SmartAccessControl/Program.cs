namespace SmartAccessControl
{
    internal class Program
    {
        static void Main()
        {
            // Read all input using a single ReadLine
            string input = Console.ReadLine();

            // Basic input check
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            // Split input into parts
            string[] parts = input.Split('|');

            if (parts.Length != 5)
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            // -------------------------
            // GateCode Validation
            // -------------------------
            string gateCode = parts[0];
            if (gateCode.Length != 2 ||
                !char.IsLetter(gateCode[0]) ||
                !char.IsDigit(gateCode[1]))
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            // -------------------------
            // UserInitial Validation
            // -------------------------
            if (parts[1].Length != 1)
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            char userInitial = parts[1][0];
            if (!char.IsUpper(userInitial))
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            // -------------------------
            // AccessLevel Validation
            // -------------------------
            if (!byte.TryParse(parts[2], out byte accessLevel) ||
                accessLevel < 1 || accessLevel > 7)
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            // -------------------------
            // IsActive Validation
            // -------------------------
            if (!bool.TryParse(parts[3], out bool isActive))
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            // -------------------------
            // Attempts Validation
            // -------------------------
            if (!byte.TryParse(parts[4], out byte attempts) ||
                attempts > 200)
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            // -------------------------
            // Business Logic
            // -------------------------
            string status;

            if (!isActive)
            {
                status = "ACCESS DENIED – INACTIVE USER";
            }
            else if (attempts > 100)
            {
                status = "ACCESS DENIED – TOO MANY ATTEMPTS";
            }
            else if (accessLevel >= 5)
            {
                status = "ACCESS GRANTED – HIGH SECURITY";
            }
            else
            {
                status = "ACCESS GRANTED – STANDARD";
            }

            // -------------------------
            // Output Formatting
            // -------------------------
            Console.WriteLine($"{"Gate",-10}: {gateCode}");
            Console.WriteLine($"{"User",-10}: {userInitial}");
            Console.WriteLine($"{"Level",-10}: {accessLevel}");
            Console.WriteLine($"{"Attempts",-10}: {attempts}");
            Console.WriteLine($"{"Status",-10}: {status}");
        }
    }
}
