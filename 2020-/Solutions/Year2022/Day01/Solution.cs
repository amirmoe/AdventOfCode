using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2022.Day01;

internal class Day01 : ASolution
{
    public Day01() : base(01, 2022, "Calorie Counting")
    {
    }

    protected override string SolvePartOne()
    {
        return Input
            .Split("\r\n\r\n")
            .Select(l => l.SplitByNewline())
            .Select(items => items
                .Select(int.Parse)
                .Sum())
            .Max()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        return Input
            .Split("\r\n\r\n")
            .Select(l => l.SplitByNewline())
            .Select(items => items
                .Select(int.Parse)
                .Sum())
            .OrderByDescending(x => x)
            .Take(3)
            .Sum()
            .ToString();
    }
}