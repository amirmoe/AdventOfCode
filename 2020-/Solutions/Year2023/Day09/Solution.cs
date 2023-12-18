using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2023.Day09;

internal class Day09 : ASolution
{
    public Day09() : base(09, 2023, "Mirage Maintenance")
    {
    }

    protected override string SolvePartOne()
    {
        return Input
            .SplitByNewline()
            .Select(line =>
                line.Split(" ").Select(int.Parse).ToList())
            .Select(numbers =>
                numbers.Last() + Extrapolate(numbers))
            .ToList()
            .Sum()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        return Input
            .SplitByNewline()
            .Select(line =>
                line.Split(" ").Select(int.Parse).ToList())
            .Select(numbers =>
                numbers.First() - Extrapolate(numbers, true))
            .ToList()
            .Sum()
            .ToString();
    }

    private static int Extrapolate(IReadOnlyList<int> numbers, bool part2 = false)
    {
        var differences = new List<int>();
        for (var i = 0; i < numbers.Count - 1; i++)
            differences.Add(numbers[i + 1] - numbers[i]);

        if (differences.All(x => x == 0))
            return 0;

        return part2
            ? differences.First() - Extrapolate(differences, true)
            : differences.Last() + Extrapolate(differences);
    }
}