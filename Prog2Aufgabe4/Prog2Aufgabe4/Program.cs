using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Reflection;

namespace Prog2Aufgabe4
{
    public interface IMyMap<TKey, TValue> : IEnumerable<Element<TKey,  TValue>> where TKey : IComparable<TKey>, IEquatable<TKey>{
        //Return number of Key-Value pairs in linkedlist
        long Length();
        
        //Get and Set Values in linkedlist
        //Get: Throws a KeyNotFoundException if key is not in linkedlist
        //Set: if the key exists, the value should be changed. If the key
        //does not exist,the key should be newly added to the linked
        //list by using the Add()-Method.
        TValue this[TKey key]{get;set;}
        
        // Returns an array of all Keys in List.
        TKey[] GetKeys();
        
        // Returns an array of all Values in List.
        TValue[] GetValues();
        
        //Returns a Pair-Element<TKey, TValue> when a key is found.
        //Returns null if key not found.
        Element<TKey, TValue> FindElement(TKey key);
        
        //Adds a new Key/Value-Pair to the linkedlist.
        //Add() sorts the keys in the linkedlist according to the CompareTo(TKey)-Method.
        //Throws a DuplicateKeyException when Key is already in linked list.
        void Add(TKey newKey, TValue newValue);
        
        //Removes Key/Value-Pair in the Linkedlist when key is found.
        //Remove(TKey key) employs Equals(TKey)-Method
        //Throws KeyNotFoundException if key is not in linkedlist.
        void Remove(TKey key);
    }
    
    class MyMap<TKey, TValue> : IMyMap<TKey, TValue> 
        where TKey : IComparable<TKey>
        , IEquatable<TKey>
    {

        private LinkedList<Element<TKey, TValue>> items;

        private class LinkedList<T>
        {
            public T elem;
            public LinkedList<T> next;

            public LinkedList(T elem, LinkedList<T> next)
            {
                this.elem = elem;
                this.next = next;
            }
        }
        public IEnumerator<Element<TKey, TValue>> GetEnumerator()
        {
            for (var curr = items; curr != null; curr = curr.next)
            {
                yield return curr.elem;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public long Length()
        {
            if (items == null) return 0;
            var head = items;
            int length = 0;
            while(head != null)
            {
                length += 1;
                head = head.next;
            }

            return length;
        }

        public TValue this[TKey key]
        {
            get => FindElement(key).Second;
            set => FindElement(key).Second = value;
        }

        public TKey[] GetKeys()
        {
            TKey[] keys = new TKey[Length()];
            var head = items;
            for (int i = 0; head != null; i++)
            {
                keys[i] = head.elem.First;
                head = head.next;
            }

            return keys;
        }

        public TValue[] GetValues()
        {
            TValue[] keys = new TValue[Length()];
            var head = items;
            for (int i = 0; head != null; i++)
            {
                keys[i] = head.elem.Second;
                head = head.next;
            }

            return keys;
        }

        public Element<TKey, TValue> FindElement(TKey key)
        {
            for (var head = items; head != null; head = head.next)
            {
                if (head.elem.First.Equals(key))
                {
                    return head.elem;
                }
            }

            return null;
        }

        // TODO: Insert sorted using the ComparedTo Method
        // Throw duplicate key exception
        public void Add(TKey newKey, TValue newValue)
        {
            // Try to insert to an existing key
            for (var head = items; head != null; head = head.next)
            {
                if (head.elem.First.Equals(newKey))
                {
                    head.elem.Second = newValue;
                    return;
                }
            }
            
            // Key not found; add new Element
            var elem = new Element<TKey, TValue>(newKey, newValue);
            items = new LinkedList<Element<TKey, TValue>>(elem, items);
        }

        public void Remove(TKey key)
        {
            LinkedList<Element<TKey, TValue>> prev= null;
            var head = items;
            while (head != null)
            {
                if (head.elem.First.Equals(key))
                {
                    if (prev == null) // The pair to delete is at the start
                    {
                        items = head.next;
                    }
                    else // Remove element
                    {
                        prev.next = head.next;
                    }

                    return; // There should only be one Element with a matching key
                }
                prev = head;
                head = head.next;
            }
        }
    }
    
    public class Element<T, T1>
    {
        public T First;
        public T1 Second;
        
        public Element(T a, T1 b)
        {
            First = a;
            Second = b;
        }
    }

    class Person : IComparable<Person>, IEquatable<Person>
    {
        public string FirstName;
        public string LastName;

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public bool Equals(Person other)
        {
            return other.FirstName.Equals(FirstName) && other.LastName.Equals(LastName);
        }

        public int CompareTo(Person other)
        {
            return other.FirstName.CompareTo(FirstName);
        }
    }

    class PhoneNumber
    {
        private string LocationCode;
        private string Number;

        public PhoneNumber(string locationCode, string number)
        {
            Number = number;
            LocationCode = locationCode;
        }
        
    }
    
    
    class Program
    {
        static void Main(string[] args)
        {
            MyMap<Person,PhoneNumber> myMap =new MyMap<Person,PhoneNumber>();
            myMap[new Person("Hans","Fallada")]=new PhoneNumber("0911","325435345");
            myMap[new Person("Elvira","Mueller")]=new PhoneNumber("0176","4556924");
            myMap.Add(new Person("Annelise","Schmitt"),new PhoneNumber("0172","14357654"));
            myMap.Add(new Person("Xaver","Gustav"),new PhoneNumber("0171","69044543"));
            myMap.Add(new Person("Jens","Meyer"),new PhoneNumber("0174","9345634"));
            myMap.Add(new Person("Babsi","Becker"),new PhoneNumber("0911","745435"));
            myMap.Add(new Person("Torben","Schultz"),new PhoneNumber("0172","2309543"));
            Console.WriteLine();
            Console.WriteLine("Elements in Map: "+myMap.Length());
            Console.WriteLine();
            Console.WriteLine("Getting all Keys:");
            foreach(Person p in myMap.GetKeys())
            {
                Console.WriteLine(p);
            }
            Console.WriteLine();
            Console.WriteLine("Getting all Values:");
            foreach(PhoneNumber p in myMap.GetValues())
            {
                Console.WriteLine(p);
            }
            Console.WriteLine();
            Console.WriteLine("Removing Jens Meyer:");
            myMap.Remove(new Person("Jens","Meyer"));
            Console.WriteLine();
            Console.WriteLine("Trying to remove again:");
            try
            {
                myMap.Remove(new Person("Jens", "Meyer"));
                
            }
            catch(KeyNotFoundException e)
            {
                Console.WriteLine("Catched: "+e.Message);
            }
            Console.WriteLine("Trying to add Torben Schultz again:");
            try
            {
                myMap.Add(new Person("Torben", "Schultz"), new PhoneNumber("0176", "5743245"));
            }
            catch (DuplicateKeyException e)
            {
                Console.WriteLine("Catched: "+e.Message);
            }
            Console.WriteLine("Changing Phone number via this-operator: ");
            myMap[new Person("Torben", "Schultz")] = new PhoneNumber("0173", "666666");
            Console.WriteLine();
            Console.WriteLine("Elements in Map: "+myMap.Length());
            Console.WriteLine();
            foreach(Element<Person,PhoneNumber> e in myMap)
            {
                Console.WriteLine(e);
            }
        }
    }

    class DuplicateKeyException : Exception
    {
    }
}

