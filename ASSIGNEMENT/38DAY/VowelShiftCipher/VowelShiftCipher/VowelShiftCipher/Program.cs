/*
TEST CASES
dfhj   	fgjk
hello	jimmu
aeiou	eioua
apple   eqqmi
crypt   dszqv
*/
using System;

namespace VowelShiftCipher
{
    public class VowelShiftCipherApp
    {
        public string EncryptUsingNextLetter(string input)
        {
            string result = "";

            foreach (char ch in input)
            {
                // Vowel logic
                if (ch == 'a')
                    result += 'e';
                else if (ch == 'e')
                    result += 'i';
                else if (ch == 'i')
                    result += 'o';
                else if (ch == 'o')
                    result += 'u';
                else if (ch == 'u')
                    result += 'a';

                // Consonant logic
                else
                {
                    char nextChar = (char)(ch + 1);

                    // Skip vowels
                    if (nextChar == 'a' || nextChar == 'e' ||
                        nextChar == 'i' || nextChar == 'o' ||
                        nextChar == 'u')
                    {
                        nextChar = (char)(nextChar + 1);
                    }

                    result += nextChar;
                }
            }

            return result;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            VowelShiftCipherApp obj = new VowelShiftCipherApp();
            Console.WriteLine("dfhj --> " + obj.EncryptUsingNextLetter("dfhj"));   // fgjk
            Console.WriteLine("dfhj --> " + obj.EncryptUsingNextLetter("hello"));  // jimmu
            Console.WriteLine("dfhj --> " + obj.EncryptUsingNextLetter("aeiou"));  // eioua
            Console.WriteLine("dfhj --> " + obj.EncryptUsingNextLetter("apple"));  // eqqmi
            Console.WriteLine("dfhj --> " + obj.EncryptUsingNextLetter("crypt"));  // dszqv

            // user input 
            string input = Console.ReadLine();
            Console.WriteLine($"{input} --> " + obj.EncryptUsingNextLetter(input));
        }
    }
}
