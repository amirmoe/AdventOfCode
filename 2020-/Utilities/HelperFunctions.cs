using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Utilities
{

    public static class HelperFunctions
    {

        public static int[] ToIntArray(this string str, string delimiter = "")
        {
            if(delimiter == "")
            {
                var result = new List<int>();
                foreach(var c in str) if(int.TryParse(c.ToString(), out var n)) result.Add(n);
                return result.ToArray();
            }
            else
            {
                return str
                    .Split(delimiter)
                    .Where(n => int.TryParse(n, out var v))
                    .Select(n => Convert.ToInt32(n))
                    .ToArray();
            }

        }
        
        public static string[] SplitByNewline(this string input, bool shouldTrim = false, string delimiter = "")
        {
            return input
                .Split(delimiter == "" ?  new[] { "\r", "\n", "\r\n" } : new []{ delimiter}, StringSplitOptions.None)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => shouldTrim ? s.Trim() : s)
                .ToArray();
        }

        public static string Reverse(this string str)
        {
            var arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static int ManhattanDistance((int x, int y) a, (int x, int y) b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        public static double FindGcd(double a, double b) => (a % b == 0) ? b : FindGcd(b, a % b);

        public static double FindLcm(double a, double b) => a * b / FindGcd(a, b);
        
        public static int Modulo(int x, int m) {
            return (x%m + m)%m;
        }

    }
}