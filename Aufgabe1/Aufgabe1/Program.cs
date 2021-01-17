using System;

namespace Aufgabe1
{
    class Program
    {
        static void Main() {
            double pfefferPreis = 5.10;
            double paprikaPreis = 3.90;
            double curryPreis = 4.50;
           
            Console.WriteLine("Herzlich Willkommen bei Scharfe Küche!");
            Console.Write("Pfeffer   (à {0,4:0.00}€):  ", pfefferPreis);
            int pfefferAnzahl = Int32.Parse(Console.ReadLine());
            
            Console.Write("Paprika   (à {0,4:0.00}€):  ", paprikaPreis);
            int paprikaAnzahl = Int32.Parse(Console.ReadLine());
            
            Console.Write("Curry     (à {0,4:0.00}€):  ", curryPreis);
            int curryAnzahl = Int32.Parse(Console.ReadLine());
            Console.WriteLine();

            double pfefferGesamtPreis = pfefferPreis * pfefferAnzahl;
            double paprikaGesamtPreis = paprikaAnzahl * paprikaPreis;
            double curryGesamtPreis = curryAnzahl * curryPreis;
            double zwischensumme = pfefferGesamtPreis + paprikaGesamtPreis + curryGesamtPreis;
            if (zwischensumme < 10)
            {
                Console.WriteLine("\nIhre Bestelllung erriecht nicht den Mindestbestellwert von 10,00 Euro");
                return;
            }

            if (25 <= zwischensumme && zwischensumme < 30)
            {
                Console.Write("Ihnen fehlen nur noch 4,80 Euro bis zum kostenfreien Versand!\nWollen Sie noch eine Dose Pfeffer mehr bestellen und den Versand sparen (ja|nein) ?");
                string answer = Console.ReadLine();
                Console.WriteLine();
                if (answer == "ja")
                {
                    pfefferAnzahl += 1;
                    pfefferGesamtPreis += pfefferPreis;
                    zwischensumme += pfefferPreis;
                }
            }
            
            Console.WriteLine("Bestellbestätigung durch Scharfe Küche: ");
            Console.WriteLine("Pfeffer                         {0:D} x {1:F2}        {2,5:#0.00} EUR", pfefferAnzahl, pfefferPreis, pfefferGesamtPreis);
            Console.WriteLine("Paprika                         {0:D} x {1:F2}        {2,5:#0.00} EUR", paprikaAnzahl, paprikaPreis, paprikaGesamtPreis);
            Console.WriteLine("Curry                           {0:D} x {1:F2}        {2,5:#0.00} EUR", curryAnzahl, curryPreis, curryGesamtPreis);

            Console.WriteLine("------------------------------------------------------------------");
            double enthalteneMehrwertsteuer = zwischensumme - zwischensumme/1.16;
            double versandpauschale = zwischensumme < 30 ? 3.90 : 0;

            Console.WriteLine("Zwischensumme                                   {zwischensumme,5:#0.00} EUR", zwischensumme);
            Console.WriteLine("Enthaltene MwSt (16%)                           {0,5:#0.00} EUR", enthalteneMehrwertsteuer);
            Console.WriteLine("Versandpauschale                                {0,5:#0.00} EUR", versandpauschale);

            Console.WriteLine("------------------------------------------------------------------");
            double summe = zwischensumme + versandpauschale;
            Console.WriteLine("Summe                                           {0,5:#0.00} EUR",  summe);

        }
    }
}