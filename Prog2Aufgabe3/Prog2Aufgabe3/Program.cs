using System;

/*
 * Note:
 * Since there *really* isn't any logic in here,
 * no comments are necessary. All the information in
 * this program is encoded in the domain.
 */

namespace Prog2Aufgabe3
{
    class Program
    {
        static void Main(string[] args){
            Bar bar = new Bar();
            Console.WriteLine();
            Console.WriteLine("Start ordering");
            //TODO: Catch Exceptions
            Beverage tea_12 = bar.Order(12, new Tea(200));
            Tea t = tea_12 as Tea;
            t.AddSugar();
            t.AddMilk(200);
            Console.WriteLine(t.ToString());
            t.Drink(399);
            Console.WriteLine(t.ToString());

            try
            {
                Beverage coffee_15 = bar.Order(15, new Coffee(100));
                Coffee c = coffee_15 as Coffee;
                c.AddSugar();
                c.AddSugar();
            }
            catch (AlreadySugarInsideException e)
            {
                Console.WriteLine("Catched Exception: " + e.GetType());
                Console.WriteLine("Coffee already has sugar inside");
            }
            
            Beverage cola_13 = bar.Order(13, new Cola(250));
            try
            {
                Beverage beer_14 = bar.Order(14, new Beer(1000));
            }
            catch (NoAlcoholicBeverageException e)
            {
                Console.WriteLine("Catched Exception: " + e.GetType());
                Console.WriteLine(e);
            }

            try
            {
                Beverage wine_64 = bar.Order(64, new Wine(125));
                wine_64.Drink(126);
            }
            catch (NotEnoughException e)
            {
                Console.WriteLine("Catched Exception: " + e.GetType());
                Console.WriteLine(e);
            }
            
            try
            {
                Beverage whiksey_20 = bar.Order(20, new Whiskey(50));
                whiksey_20.Drink(50);
                Console.WriteLine(whiksey_20.ToString());
                Beverage whiskey_cola = bar.Order(17, new Whiskey(300));
            }
            catch (NoAlcoholicBeverageException e)
            {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e);
            }
        }
    }

    abstract class Beverage
    {
        protected int ml;

        protected Beverage(int ml)
        {
            this.ml = ml;
        }

        public void Drink(int amount)
        {
            if (amount > ml)
            {
                throw new NotEnoughException();
            }
            ml -= amount;
        }
    }

    interface IHotBeverage
    {
        public void AddMilk(int amount);
        public void AddSugar();
        public bool ContainsSugar();
    }

    interface ISoftDrink
    {
        bool ContainsCaffeine();

    }

    interface IAlcoholicBeverage
    {
        bool IsBooze();
    }

    class Tea: Beverage, IHotBeverage
    {
        private bool hasSugar;
        private bool containsMilk;
        
        public void AddMilk(int amount)
        {
            containsMilk = true;
            ml += amount;
        }

        public void AddSugar()
        {
            if (hasSugar)
            {
                throw new AlreadySugarInsideException();
            }
            hasSugar = true;
        }

        public bool ContainsSugar() => hasSugar;

        public override string ToString()
        {
            return $"Tea with {ml} ml contains Sugar: {ContainsSugar()} and was mixed with milk: {containsMilk}";
        }

        public Tea(int ml) : base(ml)
        {
        }
    }

    class AlreadySugarInsideException : MyException
    {
    }


    class Coffee: Beverage, IHotBeverage
    {
        private bool hasSugar;
        private bool containsMilk;
        
        public override string ToString()
        {
            return $"Coffee with {ml} ml contains Sugar: {ContainsSugar()} and was mixed with milk: {containsMilk}";
        }

        public Coffee(int ml) : base(ml)
        {
        }

        public void AddMilk(int amount)
        {
            containsMilk = false;
            ml += amount;
        }

        public void AddSugar()
        {
            if (hasSugar)
            {
                throw new AlreadySugarInsideException();
            }
            hasSugar = true;
        }

        public bool ContainsSugar() => hasSugar;
    }

    class Beer: Beverage, IAlcoholicBeverage
    {
        public override string ToString()
        {
            return "Beer"; // TODO
        }

        public Beer(int ml) : base(ml)
        {
        }

        public bool IsBooze()
        {
            return false;
        }
    }

    class Wine: Beverage, IAlcoholicBeverage
    {
        public override string ToString()
        {
            return $"Wine with {ml} ml is booze: {IsBooze()}"; // TODO
        }

        public Wine(int ml) : base(ml)
        {
        }

        public bool IsBooze() => false;
    }

    class Cola: Beverage, ISoftDrink
    {
        public override string ToString()
        {
            return $"Cola with {ml} ml contains caffeine: {ContainsCaffeine()}"; // TODO
        }

        public bool ContainsCaffeine() => true;

        public Cola(int ml) : base(ml)
        {
        }
    }

    class Whiskey : Beverage, IAlcoholicBeverage
    {
        public Whiskey(int ml) : base(ml)
        {
        }

        public bool IsBooze() => true;
        
        public override string ToString()
        {
            return $"Whiskey with {ml} ml is booze: {IsBooze()}";
        }
    }

    class WhiskeyCola: Beverage, ISoftDrink, IAlcoholicBeverage
    {
        public override string ToString()
        {
            return "WhiskeyCola"; // TODO
        }

        public bool ContainsCaffeine() => true;

        public WhiskeyCola(int ml) : base(ml)
        {
        }

        public bool IsBooze() => true;
    }
    class NotEnoughException : MyException
    {
        public override string ToString()
        {
            return "Not enought to drink";
        }
    }

    class Bar
    {
        public Beverage Order(int customerage, Beverage bev)
        {
            // Throw an exception if the custormer is to young to buy alcohol ( >= 16 ) or booze ( >= 18 )
            if (bev is IAlcoholicBeverage)
            {
                IAlcoholicBeverage alcoholicBeverage = (IAlcoholicBeverage)bev;
                if (customerage < 16  || alcoholicBeverage.IsBooze() && customerage < 18)
                { 
                    throw new NoAlcoholicBeverageException(customerage);
                }
            }
            
            Console.WriteLine(bev);
            return bev;
        }
    }

    class NoAlcoholicBeverageException : MyException
    {
        private int age;
        public NoAlcoholicBeverageException(int age)
        {
            this.age = age;
        }

        public override string ToString()
        {
            if (age < 16)
            {
                return $"A Customer with {age} is not allowed to drink alcoholic beverages";
            }
            return $"A Customer with {age} is not allowed to drink booze";
        }
    }

    class MyException : Exception
    {
    }
}