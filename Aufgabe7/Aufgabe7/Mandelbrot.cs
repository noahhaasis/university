using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace Mandelbrot
{
    class Mandelbrot
    {
        const string zeichenvorrat = " Programmieren!";
        // ... auch hübsch:
        //const string zeichenvorrat = "\u263b                  ";

        const int breite = 95;
        const int hoehe = 30;
        const double minX = -2;
        const double maxX = 0.8;
        const double minY = -1.5;
        const double maxY = 1.5;
        const double xSchritt = (maxX - minX) / breite;
        const double ySchritt = (maxY - minY) / hoehe;



        /// <summary>
        /// Berechnung der Folge z = z*z + c
        /// </summary>
        /// <param name="zeile">Zeile</param>
        /// <param name="spalte">Spalte</param>
        /// <returns>0, falls c Element der Mandelbrot-Menge; sonst: Anzahl der Iterationen, bis |z| > 2</returns>
        static int Mandel(int zeile, int spalte)
        {
            // Die entsprechende komplexe Zahl c:
            double x = minX + spalte * xSchritt;
            double y = minY + zeile * ySchritt;
            System.Numerics.Complex c = new System.Numerics.Complex(x, y);

            // Folge berechnen:
            System.Numerics.Complex z = c;
            for (int i = 0; i < zeichenvorrat.Length; i++)
            {
                if (z.Magnitude > 2)
                {
                    // Die Folge ist nicht beschränkt
                    // => c ist nicht Element der Mandelbrot-Menge
                    return i;
                }
                z = z * z + c;
            }
            // Die Folge ist beschränkt (|z| bleibt <= 2)
            // => c ist Element der Mandelbrot-Menge
            return 0;
        }

       
        static void Main(string[] args)
        {
            var abbildung = new char[hoehe, breite];
            for (int i = 0; i < hoehe; i++)
            {
                for (int j = 0; j < breite; j++)
                {
                    abbildung[i, j] = zeichenvorrat[Mandel(i, j)];
                }
            }
            
            FeldAusgeben(abbildung);
           
            // gespiegelt
            Spiegeln(abbildung);
            FeldAusgeben(abbildung);
            
            // 10 mal scrollen
            for (int i = 0; i < 10; i++) Scrollen(abbildung);
            FeldAusgeben(abbildung);
        }

        private static void Spiegeln(char[,] abbildung)
        {
            var zeilenKopie = new char[abbildung.GetLength(1)];
            for (int zeile = 0; zeile < abbildung.GetLength(0); zeile++)
            {
                // Kopiere die Zeile
                for (int i = 0; i < zeilenKopie.Length; i++)
                {
                    zeilenKopie[i] = abbildung[zeile, i];
                }
                
                // einzelne Zeile spiegeln
                for (int spalte = 0; spalte < abbildung.GetLength(1); spalte++)
                {
                    // [0, 1, 2, 3, 4]
                    // i = length - (i + 1)
                    abbildung[zeile, spalte] = zeilenKopie[abbildung.GetLength(1) - (spalte + 1)];
                }
                
            }
        }

        static void FeldAusgeben(char [,]feld)
        {
            for (int i = 0; i < feld.GetLength(0); i++)
            {
                for (int j = 0; j < feld.GetLength(1); j++)
                {
                    Console.Write(feld[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void Scrollen(char[,] feld)
        {
            // Kopiere die erste Zeile in ein Array
            var zeile = new char[feld.GetLength(1)];
            for (int i = 0; i < zeile.Length; i++)
            {
                zeile[i] = feld[0, i];
            }
           
            // Verschiebe all Zeilen um eins nach unten
            for (int i = 1; i < feld.GetLength(0); i++)
            {
                // Kopiere Zeile i in Zeile i-1
                for (int j = 0; j < feld.GetLength(1); j++)
                {
                    feld[i - 1, j] = feld[i, j];
                }
            }
            
            // Kopiere die ehemalige erste Zeile in die letzte Zeile des Felds
            for (int i = 0; i < zeile.Length; i++)
            {
                feld[feld.GetLength(0) - 1, i] = zeile[i];
            }
        }
    }
}