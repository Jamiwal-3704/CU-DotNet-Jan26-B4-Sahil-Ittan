using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HangManGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hangman!");

            // Random word selection
            string[] Words = { "om", "shivam", "sahil", "het" };
            Random random = new Random();
            string selectedWord = Words[random.Next(Words.Length)].ToLower();

            int lifeLine = 6;

            // Display word setup: _ _ _
            char[] displayWord = new char[selectedWord.Length];
            for (int i = 0; i < displayWord.Length; i++)
                displayWord[i] = '_';

            // Store guessed letters
            HashSet<char> guessedLetters = new HashSet<char>();

            while (lifeLine > 0 && new string(displayWord) != selectedWord)
            {
                Console.WriteLine("\nWord: " + string.Join(" ", displayWord));
                Console.WriteLine("Lives left: " + lifeLine);
                Console.WriteLine("Guessed letters: " + string.Join(", ", guessedLetters));

                Console.Write("Guess a letter: ");
                string tempWord = Console.ReadLine().ToLower();

                // Validation → only single alphabet allowed
                bool isLetter = Regex.IsMatch(tempWord, @"^[a-z]{1}$");
                if (!isLetter)
                {
                    Console.WriteLine("Enter a valid single letter!");
                    continue;
                }

                char guess = tempWord[0];

                // Repeated letter check
                if (guessedLetters.Contains(guess))
                {
                    Console.WriteLine($"You already guessed '{guess}'. Try again.");
                    continue;
                }

                // Add letter to guessed list
                guessedLetters.Add(guess);

                // Check if letter exists in word
                bool found = false;
                for (int i = 0; i < selectedWord.Length; i++)
                {
                    if (selectedWord[i] == guess)
                    {
                        displayWord[i] = guess;
                        found = true;
                    }
                }

                if (found)
                    Console.WriteLine("Correct guess!");
                else
                {
                    Console.WriteLine("Wrong guess!");
                    lifeLine--;
                }
            }

            Console.WriteLine("\nFinal Word: " + string.Join(" ", displayWord));

            if (new string(displayWord) == selectedWord)
                Console.WriteLine($"You WON! The word was '{selectedWord}'");
            else
                Console.WriteLine($"Game Over! The word was '{selectedWord}'");

            Console.ReadKey();
        }
    }
}
