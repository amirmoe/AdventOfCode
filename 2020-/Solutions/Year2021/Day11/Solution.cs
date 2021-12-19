using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2021.Day11;

internal class Day11 : ASolution
{
    private static readonly (int deltaY, int deltaX)[] NeighbourDeltas =
        { (1, 0), (0, 1), (-1, 0), (0, -1), (-1, -1), (1, 1), (-1, 1), (1, -1) };

    public Day11() : base(11, 2021, "Dumbo Octopus")
    {
    }

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        var map = GetMap(lines);
        var coordinates = map.Keys.Select(x => x).ToList();

        return Enumerable.Range(0, 100)
            .Select(i =>
            {
                var flashes = coordinates.Sum(coordinate => IncreaseLevel(map, coordinate));
                CleanMap(coordinates, map);
                return flashes;
            })
            .ToList()
            .Sum()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        var lines = Input.SplitByNewline();
        var map = GetMap(lines);
        var coordinates = map.Keys.Select(x => x).ToList();
        return Enumerable.Range(1, int.MaxValue)
            .First(i =>
            {
                coordinates.ForEach(coordinate => IncreaseLevel(map, coordinate));
                CleanMap(coordinates, map);
                return map.Values.All(x => x == 0);
            })
            .ToString();
    }


    private static int IncreaseLevel(IDictionary<(int y, int x), int> map, (int y, int x) coordinate)
    {
        var flashes = 0;
        map[coordinate] += 1;

        if (map[coordinate] == 10)
        {
            flashes++;
            foreach (var (deltaY, deltaX) in NeighbourDeltas)
                if (map.ContainsKey((coordinate.y + deltaY, coordinate.x + deltaX)))
                    flashes += IncreaseLevel(map, (coordinate.y + deltaY, coordinate.x + deltaX));
        }

        return flashes;
    }

    private static void CleanMap(IEnumerable<(int y, int x)> coordinates, IDictionary<(int y, int x), int> map)
    {
        coordinates
            .Where(c => map[c] >= 10)
            .ToList()
            .ForEach(c => map[c] = 0);
    }


    private static Dictionary<(int y, int x), int> GetMap(IReadOnlyList<string> inputLines)
    {
        var map = new Dictionary<(int, int), int>();
        for (var i = 0; i < inputLines.Count; i++)
        for (var j = 0; j < inputLines[0].Length; j++)
            map.Add((i, j), int.Parse(inputLines[i][j].ToString()));
        return map;
    }
}