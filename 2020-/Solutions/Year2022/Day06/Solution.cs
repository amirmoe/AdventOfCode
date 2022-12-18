using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022.Day06;

internal class Day06 : ASolution
{
    public Day06() : base(06, 2022, "Tuning Trouble")
    {
    }

    protected override string SolvePartOne()
    {
        return Solve(Input, 4);
    }

    protected override string SolvePartTwo()
    {
        return Solve(Input, 14);
    }

    private static string Solve(string input, int numberOfDistinct)
    {
        for (var i = 0; i <input.Length-3; i++)
        {
            var list = Enumerable.Range(i, numberOfDistinct).Select(n => input[n]).ToList();
            if (list.Distinct().Count() == numberOfDistinct)
                return (i + numberOfDistinct).ToString();
        }
        return "Error";
    }
}