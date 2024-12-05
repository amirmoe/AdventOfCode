using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2024.Day01;

internal class Day01 : ASolution
{
    public Day01() : base(01, 2024, "Historian Hysteria")
    {
    }


    protected override string SolvePartOne()
    {
        var left = new List<int>();
        var right = new List<int>();

        Input.SplitByNewline().ToList().ForEach(row =>
        {
            var values = row.Split("   ", StringSplitOptions.RemoveEmptyEntries);
            left.Add(int.Parse(values[0]));
            right.Add(int.Parse(values[1]));
        });

        left.Sort();
        right.Sort();

        return left
            .Select((leftValue, i) => Math.Abs(leftValue - right[i]))
            .Sum()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        var left = new List<int>();
        var right = new Dictionary<int, int>();

        Input.SplitByNewline().ToList().ForEach(row =>
        {
            var values = row.Split(new[] { "   " }, StringSplitOptions.RemoveEmptyEntries);
            var leftValue = int.Parse(values[0]);
            var rightValue = int.Parse(values[1]);
            left.Add(leftValue);
            if (!right.TryAdd(rightValue, 1))
                right[rightValue]++;
        });

        left.Sort();

        return left
            .Where(right.ContainsKey)
            .Sum(leftValue => leftValue * right[leftValue])
            .ToString();
    }
}