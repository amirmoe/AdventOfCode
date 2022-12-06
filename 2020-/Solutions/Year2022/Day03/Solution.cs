using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2022.Day03;

internal class Day03 : ASolution
{
    public Day03() : base(03, 2022, "Rucksack Reorganization")
    {
    }


    protected override string SolvePartOne()
    {
        return Input
            .SplitByNewline()
            .Select(row =>
            {
                var dictionary = row
                    .Take(row.Length / 2)
                    .Distinct()
                    .ToDictionary(x => x, y => y);
                var duplicate = row
                    .Skip(row.Length / 2)
                    .First(x => dictionary.ContainsKey(x));
                return CharScore(duplicate);
            })
            .Sum()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        return Input
            .SplitByNewline()
            .Chunk(3)
            .Select(chunk =>
            {
                var dict = chunk
                    .First()
                    .Distinct()
                    .ToDictionary(x => x, y => 1);

                chunk
                    .Skip(1)
                    .ToList()
                    .ForEach(row =>
                    {
                        row
                            .Distinct()
                            .ToList()
                            .ForEach(c =>
                            {
                                if (dict.ContainsKey(c))
                                    dict[c]++;
                            });
                    });

                return CharScore(dict
                    .ToList()
                    .First(x => x.Value == 3)
                    .Key);
            })
            .Sum()
            .ToString();
    }

    private static int CharScore(char c)
    {
        var score = c % 32;
        return char.IsUpper(c) ? score + 26 : score;
    }
}