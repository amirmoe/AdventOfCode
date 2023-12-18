using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2023.Day08;

internal class Day08 : ASolution
{
    public Day08() : base(08, 2023, "Haunted Wasteland", false)
    {
    }


    protected override string SolvePartOne()
    {
        var instructions = Input.SplitByNewline().First();
        var dict = GetDictionary(Input);
        var count = 0;
        var step = "AAA";
        while (step != "ZZZ")
        {
            var instruction = instructions[count % instructions.Length];
            step = instruction == 'L' ? dict[step].Left : dict[step].Right;
            count++;
        }
        
        return count.ToString();
    }

    protected override string SolvePartTwo()
    {
        var instructions = Input.SplitByNewline().First();
        var dict = GetDictionary(Input);

        var keys = dict.Where(x => x.Key.EndsWith("A")).Select(x => x.Key).ToList();
        var cycles = new List<List<string>>();
        for (var i = 0; i < keys.Count; i++)
        {
            cycles.Add(new List<string>());
            var count = 0;
            var step = keys[i];
            while (!step.EndsWith("Z"))
            {
                cycles[i].Add(step);
                var instruction = instructions[count % instructions.Length];
                step = instruction == 'L' ? dict[step].Left : dict[step].Right;
                count++;
            }
        }

        return cycles
            .Select(x => Convert.ToInt64(x.Count))
            .Aggregate(HelperFunctions.FindLcm)
            .ToString();

    }

    private static Dictionary<string, (string Left, string Right)> GetDictionary(string input)
    {
        var dict = new Dictionary<string, (string Left, string Right)>();
        input
            .SplitByNewline()
            .Skip(1)
            .ToList()
            .ForEach(line =>
            {
                var kv = line.Split("=", StringSplitOptions.TrimEntries);
                var values = kv.Last().Replace("(", string.Empty).Replace(")", string.Empty).Split(",", StringSplitOptions.TrimEntries);
                dict.Add(kv.First(), (values.First(), values.Last()));
            });
        return dict;
    }
}