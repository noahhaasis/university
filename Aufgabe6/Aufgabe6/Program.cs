using System;
using System.Collections.Generic;

namespace Aufgabe6
{
    class Program
    {

        static void Main(string[] args)
        {
            int maxNumberOfErrors = 10;
            int numberOfErrors = 0;
            List<char> guessedLetters = new List<char>(); // List of all valid guesses the player made
            
            Console.WriteLine("Hangman");
            Console.WriteLine("=======");
           
            // Generate a random word and print the censored version
            string word = RandomWord();
            string normalizedWord = word.ToLower();
            Console.WriteLine($"Gesucht ist ein Wort mit {word.Length} Buchstaben.");
            Console.WriteLine($"Das geheime Wort ist {RenderWord(word, guessedLetters)}");

            // Let the user guess until he guessed the whole word or he has to many wrong guesses
            while (numberOfErrors < maxNumberOfErrors)
            {
                char guess = GetGuess();
                bool isCorrect = normalizedWord.Contains(guess);
                
                if (isCorrect) // The guessed letter is in the word. Add it to the list of valid guesses
                {
                    guessedLetters.Add(guess);
                }
                string censoredWord = RenderWord(word, guessedLetters);
                
                Console.WriteLine($"Das geheime Wort ist: {censoredWord}  --> {guess} ist {(isCorrect ? "richtig" : "falsch")}!");
                
                if (!isCorrect) // The guessed letter is false
                {
                    numberOfErrors += 1;
                    Console.WriteLine($"{numberOfErrors} von {maxNumberOfErrors} Fehlern.");
                }
                
                // Check if the player won
                if (!censoredWord.Contains('-'))
                {
                    Console.WriteLine("Du hast gewonnen!");
                    return;
                }

                Console.WriteLine();
            }
            
            Console.WriteLine("Du bist tot!");
        }
        
        static char GetGuess()
        {
            Console.Write("Buchstaben eingeben: ");
            char letter = Char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();
            return letter;
        }

        static string RandomWord()
        {
            string[] words =
            {
                "programmieren",
                "beweisen",
                "isomorphie",
                "endofunctor",
                "catamorphism",
                "homomorphism",
                "method",
                "closure",
                "polymorphism",
                "affine"
            };
            Random random = new Random();
            return words[random.Next(words.Length)];
        }

        /// <summary>
        /// Show each letter in the word if it's part of the guessed letters. Show '-' otherwise.
        /// </summary>
        static string RenderWord(string word, List<char> guessedLetters)
        {
            string res = "";
            foreach (char c in word)
            {
                if (guessedLetters.Contains(Char.ToLower(c)))
                    res += c.ToString();
                else
                    res += "-";
            }

            return res;
        }
    }
}