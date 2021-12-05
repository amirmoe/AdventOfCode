using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021.Day05
{
    internal class Day05 : ASolution
    {
        public Day05() : base(05, 2021, "Hydrothermal Venture")
        {
        }

        protected override string SolvePartOne()
        {
            return Solution(Input).ToString();
        }

        protected override string SolvePartTwo()
        {
            return Solution(Input, true).ToString();
        }

        private static int Solution(string input, bool diagonals = false)
        {
            // x1,y1 -> x2,y2
            var lines = input.SplitByNewline();
            var coordinatePairs = new List<(int, int, int, int)>();
            var highestY = 0;
            var highestX = 0;
            foreach (var line in lines)
            {
                var coordSplit = line.Split(" -> ").ToList();
                var numbers = new List<int>();
                coordSplit.ForEach(y => y.Split(",").ToList().ForEach(x => numbers.Add(int.Parse(x))));
                coordinatePairs.Add((numbers[0], numbers[1], numbers[2], numbers[3]));
                if (highestX < numbers[0])
                    highestX = numbers[0];
                if (highestX < numbers[2])
                    highestX = numbers[2];
                if (highestY < numbers[1])
                    highestY = numbers[1];
                if (highestY < numbers[3])
                    highestY = numbers[3];
            }

            var array = new int[highestY + 1, highestX + 1];

            foreach (var coordinates in coordinatePairs)
            {
                if (!diagonals)
                    if (coordinates.Item1 != coordinates.Item3 && coordinates.Item2 != coordinates.Item4)
                        continue;

                var x = coordinates.Item1;
                var y = coordinates.Item2;
                while (x != coordinates.Item3 || y != coordinates.Item4)
                {
                    array[y, x]++;

                    if (x < coordinates.Item3)
                        x++;
                    if (x > coordinates.Item3)
                        x--;
                    if (y < coordinates.Item4)
                        y++;
                    if (y > coordinates.Item4)
                        y--;
                }

                array[coordinates.Item4, coordinates.Item3]++;
            }

            var sum = 0;
            for (var i = 0; i < array.GetLength(0); i++)
            for (var j = 0; j < array.GetLength(1); j++)
                if (array[i, j] > 1)
                    sum++;

            return sum;
        }
    }
}