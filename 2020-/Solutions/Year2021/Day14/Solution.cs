using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;
using AdventOfCode.Utilities.Extensions;

namespace AdventOfCode.Solutions.Year2021.Day14;

internal class Day14 : ASolution
{
    public Day14() : base(14, 2021, "Transparent Origami")
    {
    }

    protected override string SolvePartOne()
    {
        return Solve(Input, 10)
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        return Solve(Input, 40)
            .ToString();
    }

    private static long Solve(string input, int steps)
    {
        var lines = input.Split("\n\n");
        var word = lines[0];
        var translationDictionary = lines[1]
            .SplitByNewline()
            .Select(instruction => instruction.Split(" -> "))
            .ToDictionary(kvp => kvp[0], kvp => char.Parse(kvp[1]));

        var countDictionary = new Dictionary<string, long>();
        Enumerable.Range(0, word.Length - 1)
            .ToList()
            .ForEach(i => countDictionary.AddSafely($"{word[i]}{word[i + 1]}", 1));


        var characterCountDictionary = new Dictionary<char, long>();
        word.ToCharArray()
            .ToList()
            .ForEach(c => characterCountDictionary.AddSafely(c, 1));


        for (var i = 0; i < steps; i++)
        {
            var newDictionary = new Dictionary<string, long>();
            foreach (var (key, value) in countDictionary)
            {
                var firstKey = $"{key[0]}{translationDictionary[key]}";
                var secondKey = $"{translationDictionary[key]}{key[1]}";
                newDictionary.AddSafely(firstKey, value);
                newDictionary.AddSafely(secondKey, value);
                characterCountDictionary.AddSafely(translationDictionary[key], value);
            }

            countDictionary = newDictionary;
        }

        var max = characterCountDictionary.Max(x => x.Value);
        var min = characterCountDictionary.Min(x => x.Value);
        return max - min;
    }
}