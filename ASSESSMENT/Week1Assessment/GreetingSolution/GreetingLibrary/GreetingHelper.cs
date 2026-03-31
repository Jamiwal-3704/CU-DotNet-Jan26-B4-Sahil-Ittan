using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GreetingLibrary
{
    public class GreetingHelper
    {
        public static string GetGreeting(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Hello, Guest!";
            }

            return $"Hello, {name}!";
        }
    }
}