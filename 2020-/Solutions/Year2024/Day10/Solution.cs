using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2024.Day10;

internal class Day10 : ASolution
{
    public Day10() : base(10, 2024, "Hoof It")
    {
    }

    protected override string SolvePartOne()
    {
        var map = new Map<int>(Input);
        return map
            .Values()
            .Where(x => x.Value == 0)
            .Select(x =>
                DepthFirstSearch(map, x.Key)
                    .Select(y => y.Last())
                    .Distinct()
                    .Count())
            .Sum()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        var map = new Map<int>(Input);
        return map
            .Values()
            .Where(x => x.Value == 0)
            .Select(x =>
                DepthFirstSearch(map, x.Key).Count)
            .Sum()
            .ToString();
    }


    private static List<List<Coordinate>> DepthFirstSearch(Map<int> map, Coordinate start)
    {
        var allPaths = new List<List<Coordinate>>();

        var stack = new Stack<(Coordinate Current, List<Coordinate> Path)>();

        stack.Push((start, [start]));

        while (stack.Count > 0)
        {
            var (current, path) = stack.Pop();

            if (map.TryGetCoordinateValue(current, out var currentValue) && currentValue == 9)
            {
                allPaths.Add([..path]);
                continue;
            }

            foreach (var direction in NeighbourHelper.CardinalDirections)
            {
                var neighbour = current.Neighbour(direction);
                var neighbourInMap = map.TryGetCoordinateValue(neighbour, out var neighbourValue);

                if (!path.Contains(neighbour) && neighbourInMap && neighbourValue == map.Value(current) + 1)
                {
                    var newPath = new List<Coordinate>(path) { neighbour };
                    stack.Push((neighbour, newPath));
                }
            }
        }

        return allPaths;
    }
}