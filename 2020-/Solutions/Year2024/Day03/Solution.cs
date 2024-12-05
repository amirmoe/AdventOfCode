using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2024.Day03;

internal class Day03 : ASolution
{
    public Day03() : base(03, 2024, "Mull It Over")
    {
    }


    protected override string SolvePartOne()
    {
        return Regex.Matches(Input, @"mul\(\d{1,3},\d{1,3}\)").Select(match =>
            {
                var numbers = match.Value.Substring(4, match.Value.Length - 5).Split(',').Select(int.Parse).ToList();
                return numbers.First() * numbers.Last();
            })
            .Sum()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        var enableMull = true;

        return Regex.Matches(Input, @"(?:mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\))").Select(match =>
                {
                    if (match.Value.Contains("don't"))
                    {
                        enableMull = false;
                    }
                    else if (match.Value.Contains("do"))
                    {
                        enableMull = true;
                    }
                    else if (enableMull)
                    {
                        var numbers = match.Value.Substring(4, match.Value.Length - 5).Split(',').Select(int.Parse).ToList();
                        return numbers.First() * numbers.Last();
                    }

                    return 0;
                }
            )
            .Sum()
            .ToString();
    }
}