using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2022.Day04;

internal class Day04 : ASolution
{
    public Day04() : base(04, 2022, "Camp Cleanup")
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

    private static string Solution(string input, bool all)
    {
        return input
            .SplitByNewline()
            .Select(row =>
            {
                var pair1 = row.Split(",")[0];
                var pair2 = row.Split(",")[1];
                var a = int.Parse(pair1.Split("-")[0]);
                var b = int.Parse(pair1.Split("-")[1]);
                var c = int.Parse(pair2.Split("-")[0]);
                var d = int.Parse(pair2.Split("-")[1]);
                var numbers1 = Enumerable.Range(a, b - a + 1).ToList();
                var numbers2 = Enumerable.Range(c, d - c + 1).ToList();
                var dict1 = numbers1.ToDictionary(x => x, _ => 0);
                var dict2 = numbers2.ToDictionary(x => x, _ => 0);
                var contains1 = all ? numbers1.All(x => dict2.ContainsKey(x)) : numbers1.Any(x => dict2.ContainsKey(x));
                var contains2 = all ? numbers2.All(x => dict1.ContainsKey(x)) : numbers2.Any(x => dict1.ContainsKey(x));
                return contains1 || contains2 ? 1 : 0;
            })
            .Sum()
            .ToString();
    }
}