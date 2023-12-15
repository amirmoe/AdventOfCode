using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2023.Day03;

internal class Day03 : ASolution
{
    public Day03() : base(03, 2023, "Gear Ratios")
    {
    }

    protected override string SolvePartOne()
    {
        var width = Input.SplitByNewline().First().Length;
        var height = Input.SplitByNewline().Length;
        var numbers = new Dictionary<((int y, int x) coordinate, int length), int>();
        var specialCharacters = new Dictionary<(int y, int x), string>();

        var input = Input.ReplaceLineEndings(string.Empty);


        Regex.Matches(input, @"\d+")
            .ToList()
            .ForEach(match => { numbers.Add(((match.Index / width, match.Index % width), match.Value.Length), int.Parse(match.Value)); });

        Regex.Matches(input, @"[^0-9.]+")
            .ToList()
            .ForEach(match => { specialCharacters.Add((match.Index / width, match.Index % width), match.Value); });

        var special = new List<int>();
        foreach (var kvp in numbers)
        {
            var neighbours = GetNeighbours(kvp.Key.coordinate, kvp.Key.length, width, height);
            if (neighbours.Any(x => specialCharacters.ContainsKey(x)))
                special.Add(kvp.Value);
        }


        return special.Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        var width = Input.SplitByNewline().First().Length;
        var height = Input.SplitByNewline().Length;
        var input = Input.ReplaceLineEndings(string.Empty);

        var numbers = new Dictionary<(int y, int x), string>();
        var specialCharacters = new Dictionary<(int y, int x), string>();


        Regex.Matches(input, @"\d+")
            .ToList()
            .ForEach(match =>
            {
                for (var i = 0; i < match.Value.Length; i++)
                    numbers.Add(((match.Index + i) / width, (match.Index + i) % width), match.Value);
            });

        Regex.Matches(input, @"\*")
            .ToList()
            .ForEach(match => { specialCharacters.Add((match.Index / width, match.Index % width), match.Value); });

        var special = new List<int>();
        foreach (var kvp in specialCharacters)
        {
            var neighbouringCoordinates = GetNeighbours(kvp.Key, kvp.Value.Length, width, height);

            var neighbouringNumbers = neighbouringCoordinates.Where(cord => numbers.ContainsKey(cord)).DistinctBy(cord => numbers[cord])
                .ToList();
            if (neighbouringNumbers.Count == 2)
                special.Add(int.Parse(numbers[neighbouringNumbers.First()]) * int.Parse(numbers[neighbouringNumbers.Last()]));
        }

        return special.Sum().ToString();
    }

    private static IEnumerable<(int y, int x)> GetNeighbours((int y, int x) coordinate, int range, int width, int height)
    {
        var list = new List<(int y, int x)>();

        var rangeInX = Enumerable.Range(coordinate.x - 1, range + 2).ToList();
        foreach (var x in rangeInX)
        {
            list.Add((coordinate.y - 1, x));
            list.Add((coordinate.y + 1, x));
        }

        list.Add((coordinate.y, coordinate.x - 1));
        list.Add((coordinate.y, coordinate.x + range));

        list = list.Where(c => 0 <= c.x && c.x < width &&
                               0 <= c.y && c.y < height).ToList();
        return list;
    }
}