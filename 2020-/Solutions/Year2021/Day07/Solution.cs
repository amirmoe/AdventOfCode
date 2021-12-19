using System;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2021.Day07;

internal class Day07 : ASolution
{
    public Day07() : base(07, 2021, "The Treachery of Whales")
    {
    }

    protected override string SolvePartOne()
    {
        return Solve(Input).ToString();
    }

    protected override string SolvePartTwo()
    {
        return Solve(Input, false).ToString();
    }

    private static int Solve(string input, bool pt1 = true)
    {
        var crabs = input.ToIntArray(",").ToList();
        var leastSum = int.MaxValue;
        for (var i = crabs.Min(); i < crabs.Max(); i++)
        {
            var sum = crabs
                .Select(crab => pt1 ? PartOneFuelCost(crab, i) : PartTwoFuelCost(crab, i))
                .Sum();

            if (leastSum > sum)
                leastSum = sum;
        }

        return leastSum;
    }

    private static int PartOneFuelCost(int crab, int destination)
    {
        return Math.Abs(crab - destination);
    }

    private static int PartTwoFuelCost(int crab, int destination)
    {
        return Enumerable.Range(1, Math.Abs(crab - destination)).Sum();
    }
}