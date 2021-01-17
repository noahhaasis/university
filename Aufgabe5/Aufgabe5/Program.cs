using System;

namespace Aufgabe5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pascalsches Dreieck:");
            int[] f=null;
            for (int i = 0; i < 8; i++)
            {
                f = PascalDreieck(f);Ausgabe(f);
            } 
            Console.WriteLine(); 
            Console.WriteLine("Primzahlen bis 20");  
            Primzahlen(20, true);
            const int Primzahlgrenze = 750000;
            Console.WriteLine($"Anzahl der Primzahlen bis {Primzahlgrenze}: {Primzahlen(Primzahlgrenze)}");
        }

        static int[] PascalDreieck(int[] f)
        {
            // The new array is one element longer than the old one
            int[] res = new int[(f?.Length ?? 0) + 1];

            for (int i = 0; i < res.Length; i++)
            {
                // The Last and the First number are always one
                if (i == 0 || i == res.Length - 1)
                {
                    res[i] = 1;
                }
                else
                {
                    // Sum of the two elements above it in the old array
                    res[i] = f[i - 1] + f[i];
                }
            }
            
            return res;
        }

        static void Ausgabe(int[] f)
        {
            for (int i = 0; i < f.Length; i++)
            {
                Console.Write(f[i]);
                if (i < f.Length - 1)
                {
                    Console.Write(" "); // Separate the individual numbers by a single space
                }
            }

            Console.WriteLine();
        }

        static int Primzahlen(int n, bool ausgabe = false)
        {
            // The array is initialized to `false` by default
            // So values marked as "not prime" are `true`
            bool[] primzahlen = new bool[n + 1];
            primzahlen[0] = true; // Mark 0 as not prime
            primzahlen[1] = true; // Mark 1 as not prime

            // Find the next prime and remove all multiples of it from the field.
            // Repeat this process until no new prime is found.
            int numberOfPrimes = 0;
            int currentPrime = 0;
            bool foundPrime = true;
            while (foundPrime)
            {
                currentPrime = FindNextPrime(currentPrime, primzahlen);
                foundPrime = currentPrime != -1;
                
                if (foundPrime) // Found a new prime
                {
                    RemoveMultiples(primzahlen, currentPrime);
                    numberOfPrimes += 1;
                    if (ausgabe)
                    {
                        Console.Write($"      {currentPrime}");
                    }
                }
            }
            
            if (ausgabe) Console.WriteLine();
            
            return numberOfPrimes;
        }

        // Mark multiples of `n` as not prime in the given field
        static void RemoveMultiples(bool[] primzahlen, int n)
        {
            for (int i = n; i < primzahlen.Length; i += n)
            {
                primzahlen[i] = true;
            }
        }

        // Find the next prime number in the given field.
        // Return -1 if no new prime number is found.
        static int FindNextPrime(int start, bool[] primzahlen)
        {
            for (int i = start; i < primzahlen.Length; i++)
            {
                if (!primzahlen[i]) // Primes are marked as `false`
                {
                    return i;
                }
            }

            return -1; // No prime found
        }
    }
}