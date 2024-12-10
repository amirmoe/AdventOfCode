using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2024.Day08;

internal class Day08 : ASolution
{
    public Day08() : base(08, 2024, "Resonant Collinearity")
    {
    }

    protected override string SolvePartOne()
    {
        return Solution(Input, false);
    }


    protected override string SolvePartTwo()
    {
        return Solution(Input, true);
    }

    private static string Solution(string input, bool part2)
    {
        var map = new Map<string>(input);
        var antennasDict = new Dictionary<string, List<Coordinate>>();
        foreach (var (coordinate, value) in map.Values())
        {
            if (value == ".") continue;
            if (antennasDict.ContainsKey(value))
                antennasDict[value].Add(coordinate);
            else
                antennasDict.Add(value, [coordinate]);
        }

        var count = 0;
        foreach (var (_, coordinates) in antennasDict)
        {
            var pairs = coordinates.SelectMany(
                (_, index) => coordinates.Skip(index + 1),
                (first, second) => (first, second));

            foreach (var (first, second) in pairs)
                (part2
                        ? GetAntiNodesWithinMap(first, second, map)
                        : GetAntiNodes(first, second))
                    .ForEach(antiNode =>
                    {
                        if (!map.ContainsCoordinate(antiNode)) return;
                        if (map.Value(antiNode) == "#") return;

                        count++;
                        map.SetValue(antiNode, "#");
                    });
        }

        return count.ToString();
    }

    private static List<Coordinate> GetAntiNodes(Coordinate a, Coordinate b)
    {
        return
        [
            new Coordinate(a.Y - (b.Y - a.Y), a.X - (b.X - a.X)),
            new Coordinate(b.Y + (b.Y - a.Y), b.X + (b.X - a.X))
        ];
    }

    private static List<Coordinate> GetAntiNodesWithinMap(Coordinate a, Coordinate b, Map<string> map)
    {
        var antiNodes = new List<Coordinate>();
        var deltaX = b.X - a.X;
        var deltaY = b.Y - a.Y;

        TraverseInDirection(1);
        TraverseInDirection(-1);

        return antiNodes;

        void TraverseInDirection(int direction)
        {
            var step = 0;
            while (true)
            {
                var node = GetSymmetricNode(b, step * direction);
                if (!map.ContainsCoordinate(node))
                    break;

                antiNodes.Add(node);
                step++;
            }
        }

        Coordinate GetSymmetricNode(Coordinate origin, int step)
        {
            return new Coordinate(
                origin.X + step * deltaX,
                origin.Y + step * deltaY
            );
        }
    }
}