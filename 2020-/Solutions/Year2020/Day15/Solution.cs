using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020.Day15
{
    internal class Day15 : ASolution
    {
        public Day15() : base(15, 2020, "Rambunctious Recitation")
        {
            // DebugInput = "0,3,6";
        }

        protected override string SolvePartOne()
        {
            var startNumbers = Input.Split(",").Select(x => Convert.ToInt32(x)).ToList();
            return Solution(startNumbers, 2020);
        }

        protected override string SolvePartTwo()
        {
            var startNumbers = Input.Split(",").Select(x => Convert.ToInt32(x)).ToList();
            return Solution(startNumbers, 30000000);
        }

        private static string Solution(List<int> startNumbers, int endTurn)
        {
            var dictionary = new Dictionary<int, List<int>>();

            for (var i = 1; i <= startNumbers.Count(); i++) dictionary.Add(startNumbers[i - 1], new List<int> {i});

            var previousNumber = startNumbers[^1];
            for (var i = startNumbers.Count + 1; i <= endTurn; i++)
            {
                if (dictionary[previousNumber].Count == 1)
                    previousNumber = 0;
                else
                    previousNumber = dictionary[previousNumber][^1] - dictionary[previousNumber][^2];

                if (dictionary.ContainsKey(previousNumber))
                    dictionary[previousNumber].Add(i);
                else
                    dictionary.Add(previousNumber, new List<int> {i});
            }

            return previousNumber.ToString();
        }
    }
}