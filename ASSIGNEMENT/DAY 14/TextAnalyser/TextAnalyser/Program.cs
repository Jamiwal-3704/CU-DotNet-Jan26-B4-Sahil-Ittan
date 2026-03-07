// you are developing a text anaylysis utility used in educational platform to ealuate basic string processing skills.

// method contract (Mandatory)
// int CountVowels(string input)

// BASICALLY WE ONLY HAVEE TO COUNT THE VOWELS IN A STRING


using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters;

namespace TextAnalyser
{
    internal class Program
    {
        public static int CountVowels(string input)
        {
            int count = 0;
            //if(input == null || input == "")
            //{
            //    return 0;
            //}
            input = input.ToLower();

            foreach(char ch in input)
            {
                if(ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u')
                {
                    count++;
                }
                else if(string.IsNullOrEmpty(input))
                {
                    return 0;
                }
            }

            return count;
        }

        static void Main(string[] args)
        {
            Console.Write("Enter a string to analyze: ");
            string input = Console.ReadLine();
            int VowelCount = CountVowels(input);
            Console.WriteLine($"number of Vowels in given string are: {VowelCount}");
        }
    }
}
