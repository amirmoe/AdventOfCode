using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;
using AdventOfCode.Utilities.Extensions;

namespace AdventOfCode.Solutions.Year2021.Day12;

internal class Day12 : ASolution
{
    public Day12() : base(12, 2021, "Passage Pathing")
    {
    }

    protected override string SolvePartOne()
    {
        var map = GetMap(Input);
        return PathCount(map, new List<string> { "start" }, "start", true)
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        var map = GetMap(Input);
        return PathCount(map, new List<string> { "start" }, "start", false)
            .ToString();
    }

    private static Dictionary<string, List<string>> GetMap(string input)
    {
        var map = new Dictionary<string, List<string>>();

        input
            .SplitByNewline()
            .Select(line => line.Split("-"))
            .ToList()
            .ForEach(split =>
            {
                map.AddSafely(split[0], split[1]);
                map.AddSafely(split[1], split[0]);
            });
        return map;
    }

    private static int PathCount(IReadOnlyDictionary<string, List<string>> map, ICollection<string> visitedCaves, string currentCave,
        bool aSmallCaveWasVisitedTwice)
    {
        if (currentCave == "end")
            return 1;

        var res = 0;
        foreach (var cave in map[currentCave])
        {
            var isBigCave = cave.ToUpper() == cave;
            var seen = visitedCaves.Contains(cave);

            if (!seen || isBigCave)
            {
                var newVisited = new List<string>(visitedCaves) { cave };
                res += PathCount(map, newVisited, cave, aSmallCaveWasVisitedTwice);
            }
            else if (cave != "start" && !aSmallCaveWasVisitedTwice)
            {
                res += PathCount(map, visitedCaves, cave, true);
            }
        }

        return res;
    }
}