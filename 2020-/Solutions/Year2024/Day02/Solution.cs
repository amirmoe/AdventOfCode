using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2024.Day02;

internal class Day02 : ASolution
{
    public Day02() : base(02, 2024, "Red-Nosed Reports")
    {
    }


    protected override string SolvePartOne()
    {
        return Solution(Input, 0);
    }

    protected override string SolvePartTwo()
    {
        return Solution(Input, 1);
    }

    private static string Solution(string input, int maxNumberOfViolations)
    {
        return input.SplitByNewline().Select(row =>
        {
            var levels = row.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var isIncreasing = IsValidSequence(levels, maxNumberOfViolations, true);
            var isDecreasing = IsValidSequence(levels, maxNumberOfViolations, false);
            return isIncreasing || isDecreasing;
        }).Count(x => x).ToString();
    }

    private static bool IsValidSequence(IReadOnlyList<int> levels, int maxNumberOfViolations, bool isIncreasing)
    {
        var violations = 0;
        var a = levels[0];
        var b = levels[1];

        for (var i = 2; i < levels.Count; i++)
        {
            var c = levels[i];

            var allValidLevels =
                isIncreasing
                    ? a < b && b - a <= 3 && b < c && c - b <= 3
                    : a > b && a - b <= 3 && c < b && b - c <= 3;

            var secondAndThirdLevelsValid =
                isIncreasing
                    ? b < c && c - b <= 3
                    : c < b && b - c <= 3;

            var firstAndThirdLevelsValid =
                isIncreasing
                    ? a < c && c - a <= 3
                    : a > c && a - c <= 3;

            if (allValidLevels) // XXX
            {
                a = b;
                b = c;
                continue;
            }

            if (secondAndThirdLevelsValid) // !XX
            {
                a = b;
                b = c;
            }
            else if (firstAndThirdLevelsValid) // X!X
            {
                b = c;
            }

            violations++;

            if (violations > maxNumberOfViolations)
                return false;
        }

        return true;
    }
}