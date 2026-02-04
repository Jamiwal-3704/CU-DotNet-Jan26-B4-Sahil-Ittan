//namespace TheSecureTerminal
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            string AccessCode = "1729";
//            int validityChance = 3;
//            Console.Write("Enter the secreate code: ");
//            string displayCode = "";
//            //Console.WriteLine(ch.KeyChar);
//            int i = AccessCode.Length;
//            while(AccessCode != displayCode && i != 0)
//            {

//                ConsoleKeyInfo ch = Console.ReadKey(true);
//                if(char.IsDigit(ch.KeyChar))
//                {
//                    displayCode += ch.KeyChar;
//                    Console.Write("*");
//                    i--;
//                }
//                else
//                {
//                    Console.WriteLine("Invalide Character Printed");
//                }

//            }
//            while(validityChance != 0)
//            {
//                if (AccessCode == displayCode)
//                {
//                    Console.WriteLine("The code is correct");
//                    Console.WriteLine(displayCode);
//                    Console.WriteLine("Secure Terminal Access Granted!");
//                    Console.WriteLine("Write your secure message here: ");
//                    string SecreteSentence = Console.ReadLine();
//                    Console.WriteLine("your secrete code is : " + SecreteSentence);
//                }
//                else
//                {
//                    Console.WriteLine();
//                    Console.WriteLine("Invalid Secrete code entered");
//                    validityChance--;
//                }
//            }
//        }
//    }
//}


namespace TheSecureTerminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string AccessCode = "1729";
            int validityChance = 3;

            while(validityChance > 0)
            {
                Console.WriteLine("Enter the secret code:");
                string displayCode = ""; // reset ever attempt
                int i = AccessCode.Length; // reset digit counter

                while(i > 0)
                {
                    ConsoleKeyInfo ch = Console.ReadKey(true);

                    if(char.IsDigit(ch.KeyChar))
                    {
                        displayCode += ch.KeyChar;
                        Console.Write("*");
                        i--;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid character! Enter digits only.");
                    }
                }
                if (AccessCode == displayCode)
                {
                    Console.WriteLine("\nCode correct!");
                    Console.WriteLine("Secure Terminal Access Granted!");

                    Console.Write("Write your secure message here: ");
                    string secretSentence = Console.ReadLine();
                    Console.WriteLine("Your secret message: " + secretSentence);
                    return; // stop program after success
                }
                else
                {
                    validityChance--;
                    Console.WriteLine("Invalid secret Code!");

                    if(validityChance > 0)
                    {
                        Console.WriteLine($"Attempts left: {validityChance}");
                    }
                }

                Console.WriteLine("System Locked for 24 hours!");


            }

        }
    }
}