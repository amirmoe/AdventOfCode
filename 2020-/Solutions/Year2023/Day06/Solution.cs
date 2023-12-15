using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2023.Day06;

internal class Day06 : ASolution
{
    public Day06() : base(06, 2023, "Wait For It")
    {
    }

    protected override string SolvePartOne()
    {
        return Solve(GetRaces(Input));
    }

    protected override string SolvePartTwo()
    {
        return Solve(GetRaces(Input, true));
    }

    private static List<(long Time, long Distance)> GetRaces(string input, bool part2 = false)
    {
        var numbers = input
            .SplitByNewline()
            .Select(line =>
                {
                    if (part2)
                        return new List<long>
                        {
                            long.Parse(line
                                .Split(":")
                                .Last()
                                .Replace(" ", string.Empty))
                        };
                    return line
                        .Split(":")
                        .Last()
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(long.Parse)
                        .ToList();
                }
            )
            .ToList();
        var races = new List<(long Time, long Distance)>();
        for (var i = 0; i < numbers.First().Count; i++)
        {
            var times = numbers.First();
            var distances = numbers.Last();

            races.Add((times[i], distances[i]));
        }

        return races;
    }

    private static string Solve(List<(long Time, long Distance)> races)
    {
        var totalPossibilities = new List<int>();
        foreach (var race in races)
        {
            var possibilities = 0;
            for (var chargeTime = 0; chargeTime <= race.Time; chargeTime++)
            {
                var timeLeft = race.Time - chargeTime;
                var distance = timeLeft * chargeTime;
                if (distance > race.Distance)
                    possibilities++;
            }

            totalPossibilities.Add(possibilities);
        }

        return totalPossibilities.Aggregate((a, b) => a * b).ToString();
    }
}