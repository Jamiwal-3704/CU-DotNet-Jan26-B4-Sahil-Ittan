using System;

namespace SimpleUserLoginMessageProcessor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // take input mesaage from the user in particular formate
            Console.WriteLine("Enter your message here" + "formate --> <UserName>|<LoginMessage>");
            string message = Console.ReadLine();

            // split input into userName and loginMessage
            string[] parts = message.Split('|');
            string userName = parts[0];
            string loginMessage = parts[1];

            // clean the message using the trim, toLower methods
            string cleanedMessage = message.Trim().ToLower();
            // SAHIL| LOGIN SUCCESSFULL  --> sahil| login successfull   

            string standardMessage = "login successfull";
            string status;

            if(!loginMessage.Contains("successfull"))
            {
                status = "LOGIN FAIL";
            }
            else if(cleanedMessage.Equals(standardMessage))
            {
                status = "LOGIN SUCCESS";
            }
            else
            {
                status = "LOGIN SUCCESS (CUSTOM MESSAGE)";
            }

            // OUTPUT AFTER WHOLE PROCESS -->
            Console.WriteLine($"user: {userName,5}");
            Console.WriteLine($"Message: {cleanedMessage}");
            Console.WriteLine($"Status: {status}");
        }
    }
}
