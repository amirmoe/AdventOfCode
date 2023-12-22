using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2023.Day10;

internal class Day10 : ASolution
{
    public Day10() : base(10, 2023, "Pipe Maze", false)
    {
    }

    protected override string SolvePartOne()
    {

        
        var start = FindStart(Input);
        var map = MapFunctions.GetMapFromInput(Input);
        var visited = BFS(map, start);
        return (visited.Count/2).ToString();
    }

    protected override string SolvePartTwo()
    {
        return null;
    }

    private List<Coordinate> BFS(Map map, Coordinate root)
    {
        var queue = new Queue<Coordinate>();
        var visited = new HashSet<Coordinate>{root};
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var coordinate = queue.Dequeue();
            var neighbours = FindConnectingNeighbours(map, coordinate).ToList();
            neighbours.ForEach(c =>
            {
                if (!visited.Contains(c))
                {
                    visited.Add(c);
                    queue.Enqueue(c);
                }
            });
        }

        return visited.ToList();
    }

    private static Coordinate FindStart(string input)
    {
        var width = input.SplitByNewline().First().Length;
        var index = input.ReplaceLineEndings(string.Empty).IndexOf("S", StringComparison.Ordinal);
        return new Coordinate(index / width, index % width);
    }

    private static IEnumerable<Coordinate> FindConnectingNeighbours(Map map, Coordinate point)
    {
        var pointValue = map.Value(point);
        var connectingNeighbour = new List<Coordinate>();
        foreach (var neighbourDelta in MapFunctions.NeighbourDeltas)
        {
            var neighbourCoordinate = new Coordinate(point.Y + neighbourDelta.Coordinate.Y, point.X + neighbourDelta.Coordinate.X);
            if (!map.ContainsCoordinate(neighbourCoordinate)) continue;

            var neighbourValue = map.Value(neighbourCoordinate);
            var direction = neighbourDelta.Direction;
            if (direction == Direction.North && pointValue is "|" or "J" or "L" or "S" && neighbourValue is "|" or "7" or "F")
                connectingNeighbour.Add(neighbourCoordinate);

            if (direction == Direction.South && pointValue is "|" or "7" or "F" or "S" && neighbourValue is "|" or "J" or "L")
                connectingNeighbour.Add(neighbourCoordinate);

            if (direction == Direction.West && pointValue is "-" or "J" or "7" or "S" && neighbourValue is "-" or "L" or "F")
                connectingNeighbour.Add(neighbourCoordinate);

            if (direction == Direction.East && pointValue is "-" or "L" or "F" or "S" && neighbourValue is "-" or "J" or "7")
                connectingNeighbour.Add(neighbourCoordinate);
        }

        return connectingNeighbour;
    }
}