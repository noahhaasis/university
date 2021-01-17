using System;

namespace Aufgabe4
{
    class Program
    {
        /// <summary>
        /// Main aus der Angabe
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int credit1 = 10;
            int credit2 = 10;
            Play(ref credit1, ref credit2);
 
            Console.WriteLine("\nEndergebnisse:");
            Console.WriteLine($"Spieler 1 hat aus 10 Jetons {credit1} gemacht.");
            Console.WriteLine($"Spieler 2 hat aus 10 Jetons {credit2} gemacht.");
        }

        private static void Play(ref int credit1, ref int credit2)
        {
            while (credit1 > 0 && credit2 > 0)
            {
                int einsatz1 = 0, einsatz2 = 0;
                bool beenden = EinsaetzeSetzen(ref einsatz1, credit1, ref einsatz2, credit2);
                if (beenden) return;
                
                // Spieler 1 zieht
                Console.WriteLine("~~~~ Spieler 1");
                int punkte1 = SpielerKartenZiehen();
                
                // Spieler 2 zieht
                Console.WriteLine("~~~~ Spieler 2");
                int punkte2 = SpielerKartenZiehen();
                
                // Croupier zieht
                int croupierPunkte = CroupierZiehen();

                // Gewinne berechnen und ausgeben
                int gewinn1 = GewinnBerechnen(croupierPunkte, punkte1, einsatz1);
                Console.WriteLine($"Spieler 1 hat {(gewinn1 > 0 ? "gewonnen" : "verloren")}. Jetons jetzt {credit1}");
                credit1 += gewinn1;
                
                int gewinn2 = GewinnBerechnen(croupierPunkte, punkte2, einsatz2);
                Console.WriteLine($"Spieler 2 hat {(gewinn2 > 0 ? "gewonnen" : "verloren")}. Jetons jetzt {credit2}");
                credit2 += gewinn2;
                
                Console.WriteLine("Runde beendet");
            }
        }

        // Berechne den Gewinne eines Spielers. Ein Gewinner kann seinen Einsatz gewinnen oder verlieren.
        // Der Spieler gewinnt, wenn er weniger als 22 Punkte hat, aber mehr als der croupier. Sonst verliert er.
        private static int GewinnBerechnen(int croupierPunkte, int punkte, int einsatz)
        {
            if (croupierPunkte >= 22 || (punkte <= 21 && punkte > croupierPunkte)) // Spieler gewinnt
            {
                return einsatz;
            }

            return -einsatz; // Spieler verliert seinen Einsatz
        }

        // Wandle eine Karte in einen String um.
        private static string KarteZuString(bool istSpieler, int karte, int punkte = 0)
        {
            switch (karte)
            {
                case 11: return "B";// Bube
                case 12: return "D"; // Dame
                case 13: return "K"; // König
                default: return KartenWert(istSpieler, karte, punkte).ToString(); // Zahl oder Ass
            } 
        }

        // Berechne den Wert eine Karte. Wird für Spieler und den Croupier unterschiedlich berechnet und kann
        // von der Punktzahl des Spielers abhängen.
        private static int KartenWert(bool istSpieler, int karte, int spielerPunkte = 0)
        {
            switch (karte)
            {
                case 1: // Ass: 1 oder 11 
                    // Für den Croupier ist eine Ass immer 11 Punkte
                    if (!istSpieler || spielerPunkte + 11 < 22) return 11;
                    else return 1;
                case 11: return 10;// Bube
                case 12: return 10; // Dame
                case 13: return 10; // König
                default:
                    return karte;
            }
        }
       
        // Der Croupier zieht Karten und gibt die final Punktzahl zurück.
        private static int CroupierZiehen()
        {
            Console.WriteLine("Croupier zieht");
            Random random = new Random();
            int ergebnis = 0;
            do
            {
                int karte = random.Next(1, 14);
                ergebnis += KartenWert(false, karte);
                Console.WriteLine($"Gezogen: {KarteZuString(false, karte)}. Punkte: {ergebnis}");
            } while (ergebnis <= 16);

            return ergebnis;
        }

        private static int SpielerKartenZiehen()
        {
            int spielerPunkte = 0;
            Random random = new Random();
            bool weiterZiehen = false;
            
            // Ziehen mindestens zwei Karten. Möglicherweise mehr, wenn es der Spieler so will
            for (int i = 0; (i < 2 || weiterZiehen) && spielerPunkte < 21; i++)
            {
                int karte = random.Next(1, 14);
                int wert = KartenWert(true, karte, spielerPunkte);
                Console.WriteLine($"Gezogen: {KarteZuString(true, karte, spielerPunkte)} Ihr Ergebnis ist: {spielerPunkte + wert}");
                spielerPunkte += wert;
                
                // Den Spieler fragen, ob er noch eine Karte will
                if (i >= 1)
                {
                    Console.WriteLine("Noch eine Karte? (j|J für Ja eingeben)");
                    string antwort = Console.ReadLine();
                    weiterZiehen = antwort == "j" || antwort == "J";
                }
            }

            return spielerPunkte;
        }

        private static bool EinsaetzeSetzen(ref int einsatz1, int credit1, ref int einsatz2, int credit2)
        {
            // Ein Einsatz von 0 beendet das Spiel 
            Console.WriteLine("~~  Spieler 1:");
            Console.WriteLine("Wie viel möchten Sie setzen? Setzen sie 0 um das Spiel zu beenden");
            einsatz1 = EinsatzErfagen(credit1);
            if (einsatz1 == 0) return true;
            Console.WriteLine("~~  Spieler 2:");
            Console.WriteLine("Wie viel möchten Sie setzen? Setzen sie 0 um das Spiel zu beenden");
            einsatz2 = EinsatzErfagen(credit2);
            if (einsatz2 == 0) return true;
            
            return false;
        }

        /// <summary>
        /// Erfrage einen Einsatz vom Nutzer der zwischen 0 und einschließlich hoechsterEinsatz liegt.
        /// </summary>
        /// <param name="hoechsterEinsatz"></param>
        /// <returns>Eine Ganzzahl in [0; hoechsterEinsatz]</returns>
        private static int EinsatzErfagen(int hoechsterEinsatz) 
        {
            int einsatz;
            do
            {
                Console.WriteLine($"Bitte geben Sie einene Wert zwischen 0 und {hoechsterEinsatz} ein:");
                string eingabe = Console.ReadLine();
                bool erfolg = Int32.TryParse(eingabe, out einsatz);
                if (!erfolg)
                {
                    Console.WriteLine("Ungültige Eingabe");
                }
                else if (einsatz < 0)
                {
                    Console.WriteLine("Zu kleiner Wert");
                } else if (einsatz > hoechsterEinsatz)
                {
                    Console.WriteLine("Zu großer Wert");
                }
            } while (einsatz < 0 && einsatz <= hoechsterEinsatz);

            return einsatz;
        }
    }
}
