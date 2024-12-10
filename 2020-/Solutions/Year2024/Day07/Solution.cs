using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2024.Day07;

internal class Day07 : ASolution
{
    private static readonly List<Func<long, long, long>> OperatorsPart1 = new()
    {
        (a, b) => a + b,
        (a, b) => a * b
    };

    private static readonly List<Func<long, long, long>> OperatorsPart2 = new(OperatorsPart1)
    {
        (a, b) => long.Parse($"{a}{b}")
    };

    public Day07() : base(07, 2024, "Bridge Repair", false)
    {
    }

    protected override string SolvePartOne()
    {
        return Solution(Input, OperatorsPart1);
    }

    protected override string SolvePartTwo()
    {
        return Solution(Input, OperatorsPart2);
    }

    private static string Solution(string input, List<Func<long, long, long>> operators)
    {
        return input
            .SplitByNewline()
            .ToList()
            .Select(row =>
            {
                var parts = row.Split(":");
                var result = long.Parse(parts.First());
                var operands = parts.Last().Trim().Split(" ").Select(long.Parse).ToList();
                return (result, operands);
            })
            .Select(row =>
            {
                var (result, operands) = row;

                var operationPermutations = GeneratePermutations(operators, operands.Count - 1);
                foreach (var operationPermutation in operationPermutations)
                {
                    var currentResult = operands.First();
                    for (var i = 0; i < operationPermutation.Count; i++)
                        currentResult = operationPermutation[i](currentResult, operands[i + 1]);

                    if (result == currentResult) return result;
                }

                return 0;
            })
            .Sum().ToString();
    }

    private static List<List<Func<long, long, long>>> GeneratePermutations(IReadOnlyList<Func<long, long, long>> operators, int number)
    {
        var results = new List<List<Func<long, long, long>>>();

        var totalPermutations = (int)Math.Pow(operators.Count, number);
        for (var i = 0; i < totalPermutations; i++)
        {
            var permutation = new List<Func<long, long, long>>();
            var current = i;

            for (var j = 0; j < number; j++)
            {
                permutation.Add(operators[current % operators.Count]);
                current /= operators.Count;
            }

            results.Add(permutation);
        }

        return results;
    }
}