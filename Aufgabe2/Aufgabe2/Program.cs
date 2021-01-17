using System;
using System.Runtime.CompilerServices;

namespace Aufgabe2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ask the user for a number in [10; 10000[
            int number;
            while (true)
            {
                Console.Write("Geben Sie eine Zahl größer gleich 10 und kleiner als 10000 ein: ");
                number = Int32.Parse(Console.ReadLine());
                if (10 <= number && number < 10000)
                {
                    break;
                }

                Console.WriteLine("Ungültige Eingabe, bitte wiederholen.\n");
            }


            int divider = number < 1000 ? 50 : 500;
            Console.WriteLine($"{divider:D}-er kleiner als {number:D}");

            // Print all numbers between 0 and `number` which are divisible by divider (excluding 0)
            for (int i = number - 1; i > 0; i--)
            {
                if (i % divider == 0)
                    Console.WriteLine(i);
            }

            Console.WriteLine();

            Console.WriteLine($"Teiler von {number}:");
            // Print out every divisor of `number` and compute their sum
            int sum = 0;
            for (int i = 2; i <= number / 2; i++)
            {
                if (number % i == 0)
                {
                    sum += i;
                    Console.WriteLine(i);
                }
            }

            // Print the sum of the true dividers of `number`
            if (sum == 0)
            {
                Console.WriteLine($"{number} ist eine Primzahl.");
            }
            else
            {
                Console.WriteLine($"Die Summe aller echten Teiler von {number} ist {sum}.");
            }

            Console.WriteLine();

            // Iterate over all digits of `number` by getting the leading digit using (% 10) and reducing the number (/ 10)
            // each iteration. Also keep track of the first and last digit and the number of even and odd digits.
            int evenDigits = 0;
            int oddDigits = 0;
            int firstDigit = 0;
            int lastDigit = 0;

            int temp = number;
            int digit = 0;
            for (int i = 0; temp > 0; i++) // Iterate over all the digits 
            {
                digit = temp % 10;
                temp /= 10;

                if (i == 0)
                {
                    lastDigit = digit;
                }

                if (digit % 2 == 0) // even
                {
                    evenDigits += 1;
                }
                else
                {
                    oddDigits += 1; // odd
                }
            }

            firstDigit = digit;

            // Print the total number of the even and odd digits of the input numbers
            Console.WriteLine($"Die Zahl {number} hat {evenDigits} gerade und {oddDigits} ungerade Ziffern.\n");

            // Print the product of the first and last digit
            Console.WriteLine(
                $"Das Produkt der ersten und letzten Ziffer von {number} ist {firstDigit}*{lastDigit}={firstDigit * lastDigit}.");
        }
    }
}