using System;

namespace Prog2Aufgabe2
{
    class Program
    {
        static void Main(string[] args)
        {
            Fleet myFleet = new Fleet();

            myFleet.Add(new Truck(new Driver("Carolin Schnell"), "N-MF-343"));
            Truck t2 = new Truck(new Driver("Manfred Fahr"), "N-MF-345");
            myFleet.Add(t2);
            myFleet.Add(new Truck(new Driver("Joerg Mueller"), "N-MF-344"));
            myFleet.Add(new Truck(new Driver("Hans John"), "N-MF-348"));
            myFleet.Add(new Truck(new Driver("Efraim Malek"), "N-MF-347"));
            myFleet.Add(new Truck(new Driver("Amrei Hams"), "N-MF-340"));

            
            myFleet.Add(new Truck(new Driver("Maria Race"), "N-MF-350"));
            myFleet.Add(new Truck(new Driver("Jana Oelm"), "N-MF-348"));

            myFleet.ListTrucks();
           
            myFleet.AddClient("Hansen", "N-MF-350", "N-MF-344","N-MF-345");
            myFleet.AddClient("Maier", "N-MF-348", "N-MF-343","N-MF-345");
            Console.WriteLine();
            Console.WriteLine(t2);
            myFleet.RemoveClient("Hansen");
            myFleet.ListTrucksOfClient("Maier");
            Console.WriteLine();
            myFleet.ListTrucksOfClient("Hansen");
            Console.WriteLine();
            string[] sList =myFleet.ReturnTrucksNoClients();
            foreach (string s in sList)
            {
                Console.WriteLine(s);
            }
        }
    }

    class Truck
    {
        private LinkedList<string> _clients;
        private Driver _driver;
        public string Numberplate { private set; get; }
        
        public Truck(Driver driver, string numberplate)
        {
            _clients = new LinkedList<string>();
            Numberplate = numberplate;
            _driver = driver;
        }

        public void AddClient(string clientName)
        {
            _clients.Add(clientName);
        }

        public void RemoveClient(string clientName)
        {
            _clients.Remove(clientName);
        }

        public bool HasClients()
        {
            return !_clients.IsEmpty();

        }

        public bool HasClient(string clientName)
        {
            return _clients.Contains(clientName);
        }

        public override string ToString()
        {
            return $"{Numberplate} {_driver.Name} {_clients}";
        }
        
        class LinkedList<TA> where TA : class
        {
            private Elem<TA> _start;
            public int Length { private set; get; }

            private class Elem<TA>
            {
                public TA Value;
                public Elem<TA> Next { get; set; }

                public Elem(TA value, Elem<TA> next)
                {
                    Value = value;
                    Next = next;
                }
            }

            public LinkedList()
            {
                _start = null;
                Length = 0;
            }

            public void Add(TA elem)
            {
                Length += 1;
                _start = new Elem<TA>(elem, _start);
            }

            /* Remove all items from the list which are equal to elem */
            public void Remove(TA elem)
            {
                Elem<TA> prevElem = null;
                for (Elem<TA> currentElem = _start; currentElem != null; currentElem = currentElem.Next)
                {
                    if (currentElem.Value == elem)
                    {
                        if (prevElem == null)
                        {
                            _start = currentElem.Next;
                        }
                        else
                        {
                            prevElem.Next = currentElem.Next;
                        }
                    }

                    prevElem = currentElem;
                }
            }

            public bool IsEmpty()
            {
                return _start == null;
            }

            public bool Contains(TA elem)
            {
                for (Elem<TA> e = _start; e != null; e = e.Next)
                {
                    if (e.Value == elem)
                    {
                        return true;
                    }
                }

                return false;
            }

            public override string ToString()
            {
                string res = "[ ";
                for (Elem<TA> e = _start; e != null; e = e.Next)
                {
                    res += e.Value.ToString();
                    res += " ";

                }

                res += "]";
                return res;
            }
        }
    }

    class Driver
    {
        public string Name { get; }
        
        public Driver(string name)
        {
            Name = name;
        }
    }

    class Fleet
    {
        class TruckElem
        {
            public TruckElem Next;
            public Truck Truck;
            
            public TruckElem(Truck truck, TruckElem next)
            {
                Next = next;
                Truck = truck;
            }
        }

        private TruckElem _truckList;
        private int truckListLength = 0;
       
        /* Insert a truck into the fleet sorted alphabetically.
         * Return 0 on success;
         * Return -1 if the truck is already in the fleet.
         */
        public int Add(Truck t)
        {
            TruckElem prevElem = null;
            for (TruckElem currentElem = _truckList; currentElem != null; currentElem = currentElem.Next)
            {
                int comp = currentElem.Truck.Numberplate.CompareTo(t.Numberplate);
                if (comp == 0) // THe truck already is in the fleet
                {
                    return -1;
                } 
                if (comp < 0) // The truck should be alphabetically first
                {
                    if (prevElem != null)
                        prevElem.Next = new TruckElem(t, currentElem);
                    else
                        _truckList = new TruckElem(t, currentElem); // Insert at the start of the list
                    truckListLength += 1;
                    return 0; // Success
                }

                prevElem = currentElem;
            }
            
            if (prevElem == null)
                _truckList = new TruckElem(t, null); // Insert at the start of the list
            else
                prevElem.Next = new TruckElem(t, null); // Insert to the end of the list
            truckListLength += 1;
            
            return 0;
        }
        
        /* Remove the first matching truck from the fleet (should be the only one).
         * Return 0 on success and -1 otherwise.
         */
        public int Remove(String numberplate)
        {
            TruckElem prevElem = null;
            for (TruckElem currentElem = _truckList; currentElem != null; currentElem = currentElem.Next)
            {
                if (currentElem.Truck.Numberplate == numberplate) // found a match
                {
                    if (prevElem == null) // start of list
                    {
                        _truckList = currentElem.Next;
                    }
                    else // element in the list
                    {
                        prevElem.Next = currentElem.Next;
                    }

                    truckListLength -= 1;
                    return 0;
                }

                prevElem = currentElem;
            }
            return -1; // numberplate not found
        }

        /* Add the client to all trucks in the fleet with a matching numberplate.
         * If there's no truck matching a numberplate, print that to the console.
         */
        public void AddClient(String clientName, params String[] numberplates)
        {
            for (int i = 0; i < numberplates.Length; i++)
            {
                Truck t = GetTruck(numberplates[i]);
                if (t == null)
                {
                    Console.WriteLine($"There's no truck with the numberplate {numberplates[i]}");
                }
                else
                {
                    t.AddClient(clientName);
                }
            }
        }

        /* Remove the client from all trucks in the fleet.
         */
        public void RemoveClient(String clientName)
        {
            for (TruckElem elem = _truckList; elem != null; elem = elem.Next)
            {
                elem.Truck.RemoveClient(clientName);
            }
        }

        public void ListTrucks()
        {
            for (TruckElem elem = _truckList; elem != null; elem = elem.Next)
            {
                Console.WriteLine(elem.Truck);
            }
        }

        /* List all trucks which are assigned to the given client.
         */
        public void ListTrucksOfClient(string client)
        {
            for (TruckElem elem = _truckList; elem != null; elem = elem.Next)
            {
                if (elem.Truck.HasClient(client))
                {
                    Console.WriteLine(elem.Truck);
                }
            }
        }

        public string[] ReturnTrucksNoClients()
        {
            string[] trucks = new string[truckListLength];
            int i = 0;
            for (TruckElem elem = _truckList; elem != null; elem = elem.Next)
            {
                if (!elem.Truck.HasClients())
                {
                    trucks[i++] = elem.Truck.Numberplate;
                }
            }
            return trucks;
        }

        /* Return the first truck in the fleet with a matching numberplate (should be the only one).
         */
        private Truck GetTruck(string numberplate)
        {
            for (TruckElem elem = _truckList; elem != null; elem = elem.Next)
            {
                if (elem.Truck.Numberplate == numberplate)
                {
                    return elem.Truck;
                }
            }
            
            return null;
        }
    }
}