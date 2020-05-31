using System;

namespace AdventOfCode2019.Day10
{
    public class Coordinate
    {
        public readonly int X;
        public readonly int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Returns the angle between two vectors
        /// </summary>
        public static double GetAngle(Coordinate a, Coordinate b)
        {
            // |A·B| = |A| |B| COS(θ)
            // |A×B| = |A| |B| SIN(θ)

            return Math.Atan2(Cross(a,b), Dot(a,b));
        }
        
        private static double Dot(Coordinate a, Coordinate b)
        {
            return a.X*b.X+a.Y*b.Y;
        }

        private static double Cross(Coordinate a, Coordinate b)
        {
            return a.X*b.Y-a.Y*b.X;
        }
    }
}