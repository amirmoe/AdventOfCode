using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2024.Day05;

internal class Day05 : ASolution
{
    public Day05() : base(05, 2024, "Print Queue")
    {
    }


    protected override string SolvePartOne()
    {
        return Solution(Input, true);
    }

    protected override string SolvePartTwo()
    {
        return Solution(Input, false);
    }

    private static string Solution(string input, bool part1)
    {
        var inputParts = input.Split("\n\n");
        var orderingRows = inputParts.First().SplitByNewline();
        var pageNumberRows = inputParts.Last().SplitByNewline();

        var orderingDict = GetOrderingDictionary(orderingRows);

        var middleValues = new List<int>();
        foreach (var row in pageNumberRows)
        {
            var visited = new HashSet<int>();
            var numbers = row.Split(",").Select(int.Parse).ToList();
            for (var i = 0; i < numbers.Count; i++)
            {
                if (orderingDict.ContainsKey(numbers[i]) && orderingDict[numbers[i]].Intersect(visited).Any())
                {
                    if (!part1)
                    {
                        var fixedNumbers = FixIncorrectRow(numbers, orderingDict);
                        middleValues.Add(fixedNumbers[fixedNumbers.Count / 2]);
                    }

                    break;
                }

                visited.Add(numbers[i]);

                if (part1 && i == numbers.Count - 1) middleValues.Add(numbers[numbers.Count / 2]);
            }
        }

        return middleValues.Sum().ToString();
    }

    private static Dictionary<int, HashSet<int>> GetOrderingDictionary(IEnumerable<string> orderingRows)
    {
        var dict = new Dictionary<int, HashSet<int>>();
        foreach (var row in orderingRows)
        {
            var parts = row.Split("|").Select(int.Parse).ToList();
            if (dict.ContainsKey(parts.First()))
                dict[parts.First()].Add(parts.Last());
            else
                dict.Add(parts.First(), new HashSet<int> { parts.Last() });
        }

        return dict;
    }


    private static List<int> FixIncorrectRow(List<int> numbers, IReadOnlyDictionary<int, HashSet<int>> orderingDict)
    {
        numbers.Sort((x, y) =>
        {
            var xPriority = orderingDict.ContainsKey(x) && orderingDict[x].Contains(y);
            var yPriority = orderingDict.ContainsKey(y) && orderingDict[y].Contains(x);
            if (xPriority) return -1;
            return yPriority ? 1 : 0;
        });
        return numbers;
    }
}