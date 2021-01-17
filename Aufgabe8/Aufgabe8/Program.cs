using System;

namespace Weihnachten
{
    class Program
    {
        static void Test()
        {
            Kind k = new Kind("Aris", Verhalten.MeistLieb);
            k.WunschAnhaengen("Sandspielzeug");
            k.WunschAnhaengen("Bausteine");
            k.WunschAnhaengen("Puppen");
            Console.WriteLine(k);

            Kinderliste liste = new Kinderliste(10);
            liste.KindEintragen(k);

            liste.KindEintragen(new Kind("Anton", Verhalten.ImmerLieb,
                new string[] { "Spielzeugauto", "Bausteine" }));
            liste.KindEintragen(new Kind("Emma", Verhalten.Frech,
                new string[] { "Spielekonsole" }));
            liste.KindEintragen(new Kind("Mehmet", Verhalten.OftLieb,
                new string[] { "Bausteine", "Spielesammlung", "Sandspielzeug", "Computer" }));
            liste.KindEintragen(new Kind("Esra", Verhalten.MeistLieb,
                new string[] { "Handy", "Kinogutschein" }));
            liste.KindEintragen(new Kind("Anna", Verhalten.OftLieb,
                new string[] { "Musikstream", "Chemiebaukasten" }));
            liste.KindEintragen(new Kind("Azra", Verhalten.MeistLieb,
                new string[] { "Elektronik-Baukasten", "Computer", "Handy" }));
            liste.KindEintragen(new Kind("Hans", Verhalten.Frech,
                new string[] { "Spielzeugautos", "Kinogutscheine" }));
            liste.KindEintragen(new Kind("Egon", Verhalten.ImmerLieb,
                new string[] { "Puppen", "Spielekonsole" }));
            liste.KindEintragen(new Kind("Marie", Verhalten.MeistLieb,
                new string[] { "Handy", "Spielekonsole" }));

            Console.WriteLine($"Anzahl lieber Kinder: {liste.AnzahlLieberKinder}");
            Console.WriteLine("So oft wünschen sich Kinder ...");
            foreach (string wunsch in new string[] { "Handy", "Computer", "Spielekonsole", "Bausteine", "Puppen" })
                Console.WriteLine($"... {wunsch}:   {liste.ZaehleKinderMitWunsch(wunsch)}");
        }

        static void Main(string[] args)
        {
            Test();
        }
    }

    internal class Kinderliste
    {
        private int index;
        private Kind[] kinder;
        public Kinderliste(int i)
        {
            kinder = new Kind[i];
            index = 0;
        }

        public int AnzahlLieberKinder
        {
            get
            {
                int res = 0;
                foreach (Kind kind in kinder)
                {
                    res += kind.WarLieb ? 1 : 0;
                }

                return res;
            }
        }

        public void KindEintragen(Kind kind)
        {
            if (index >= kinder.Length) return;
            kinder[index++] = kind;
        }

        public int ZaehleKinderMitWunsch(string wunsch)
        {
            int count = 0;
            for (int i = 0; i < index; i++)
            {
                count += kinder[i].HatWunsch(wunsch) ? 1 : 0;
            }

            return count;
        }
    }

    internal enum Verhalten
    {
        ImmerLieb,
        MeistLieb,
        OftLieb,
        Frech
    }

    internal class Kind
    {
        public string Name { get; }
        public Verhalten Verhalten { get; private set; }
        public string[] Wuensche { get; }
        public bool Wunschlos { get; private set; }
        private int index;
       
        public Kind(string name, Verhalten verhalten, string[] wuensche = null)
        {
            Name = name;
            Verhalten = verhalten;
            Wunschlos = true;
            Wuensche = new string[3];
            index = 0;

            if (wuensche == null) return;
            foreach (string w in wuensche)
            {
                WunschAnhaengen(w);
            }
        }

        public void WunschAnhaengen(string wunsch)
        {
            if (index >= Wuensche.Length) return;
            Wunschlos = false;
            Wuensche[index++] = wunsch;
        }

        public void BessertSich(Verhalten neuesVerhalten)
        {
            // Enum values are integers ranging from 0 and counting up
            // so if the new behaviour is less than the old one it's better
            // , because the enum values are ordered from good to not so great.
            if (Verhalten.CompareTo(neuesVerhalten) == -1)
            {
                Verhalten = neuesVerhalten;
            }
        }

        public bool WarLieb => Verhalten != Verhalten.Frech;

        public override string ToString()
        {
            return $"{Name} ; {Verhalten}";
        }

        public bool HatWunsch(string wunsch)
        {
            for (int i = 0; i < index; i++)
            {
                if (Wuensche[i].Equals(wunsch)) return true;
            }

            return false;
        }
    }
}
