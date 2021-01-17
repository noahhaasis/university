/* Author: Noah Haasis */
using System;

namespace Aufgabe1
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Test Point
            Point myPoint1 = new Point(2, 2);
            Point myPoint2 = new Point(-4, -4);
            myPoint2 = -myPoint2;
            myPoint1 *= 2;
            if (myPoint2 == myPoint1)
            {
                Console.WriteLine("identical points");
            }

            // Test Polygon
            Polygon myLine = new Polygon(new Point(2, 1), new Point(2, 2), new Point(3, 3));
            myLine[0] = new Point(1, 1);
            Console.WriteLine("polygon points: ");
            for (int i = 0; i < myLine.NumberPoints; i++)
            {
                Console.WriteLine(myLine[i]);
            }

            Console.WriteLine($"Length Polygon: {(double) myLine}");
            Point myPoint3 = new Point(200, 200);
        }
    }

    class Polygon
    {
        private Point[] Points;
        public readonly int NumberPoints;
        public Polygon(params Point[] points)
        {
            Points = points;
            NumberPoints = points.Length;
        }

        public Point this[int i]
        {
            /* Fail if the index i is out of bounds. */
            get
            {
                if (i >= NumberPoints)
                {
                    Console.WriteLine("index out of range");
                    Environment.Exit(-1);
                }
                return Points[i];
            }
            set
            {
                if (i >= NumberPoints)
                {
                    Console.WriteLine("index out of range");
                    Environment.Exit(-1);
                }
                Points[i] = value;
            }
        }

        /* Sum up the distance between the individual points to get the polygons total length. */
        public static implicit operator double(Polygon p)
        {
            double totalDistance = 0;
            for (int i = 0; i < p.NumberPoints - 1; i++)
            {
                totalDistance += p[i].Distance(p[i+1]);
            }
            return totalDistance;
        }
    }

    class Point
    {
        private int X;
        private int Y;

        private const int MaxX = 100;
        private const int MaxY = 100;
        
        public Point(int x, int y)
        {
            if (x > MaxX || y > MaxY)
            {
                Console.WriteLine("coordinate out of range");
                Environment.Exit(-1);
            }
            X = x;
            Y = y;
        }
        
        /* Calculate the distance between two points using the pythagorean theorem */
        public double Distance(Point b)
        {
            double xdelta = b.X - X;
            double ydelta = b.Y - Y;
            return Math.Sqrt(xdelta*xdelta + ydelta*ydelta); // c = sqrt(a^2 + b^2)
        }

        public static Point operator -(Point a)
        {
            a.X = -a.X;
            a.Y = -a.Y;
            return a;
        }
        
        public static Point operator *(Point a, int s)
        {
            a.X *= s;
            a.Y *= s;
            return a;
        }
        
        public static bool operator ==(Point a, Point b)
        {
            if (a is null || b is null) return false;
            return a.X == b.X && a.Y == b.Y;
        }
        
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"x: {Y} y: {Y}";
        }
    }
}
