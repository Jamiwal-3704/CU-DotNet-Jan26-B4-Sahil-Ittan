//using GreetingLibrary;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GreetingApp
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            string name;
//            Console.WriteLine("Please, enter your name sir/mam: ");
//            name = Console.ReadLine();
//            Console.WriteLine(GreetingHelper.GetGreeting(name));

//        }
//    }
//}


using System;
using GreetingLibrary;

namespace GreetingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();

            string greeting = GreetingHelper.GetGreeting(name);

            Console.WriteLine(greeting);

            Console.ReadLine(); // keeps console open
        }
    }
}
