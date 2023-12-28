using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2023.Day10;

internal class Day10 : ASolution
{
    public Day10() : base(10, 2023, "Pipe Maze")
    {
    }

    protected override string SolvePartOne()
    {
        var start = FindStart(Input);
        var map = new Map(Input);
        var visited = GetLoop(map, start);
        return (visited.Count / 2).ToString();
    }

    protected override string SolvePartTwo()
    {
        var start = FindStart(Input);
        var map = new Map(Input);
        var visited = GetLoop(map, start);
        map
            .Values()
            .ForEach(x =>
            {
                if (!visited.Contains(x.Key))
                    map.AddCoordinate(x.Key, ".");
            });
        map.AddCoordinate(start, DetermineStart(map, start));

        var insideLoop = new List<Coordinate>();
        foreach (var kvp in map.Values())
        {
            if (kvp.Value != ".")
                continue;

            var coordinateChecking = kvp.Key;
            var edges = 0;
            var wallStart = string.Empty;
            for (var i = coordinateChecking.X + 1; i < map.Width; i++)
            {
                var check = map.Value(new Coordinate(coordinateChecking.Y, i));
                switch (check)
                {
                    case "|":
                        edges++;
                        break;
                    case "F" or "L":
                        wallStart = check;
                        break;
                    case "J" or "7":
                    {
                        if ((wallStart == "F" && check == "J") || (wallStart == "L" && check == "7"))
                        {
                            edges++;
                            wallStart = string.Empty;
                        }

                        break;
                    }
                }
            }

            if (edges > 0 && edges % 2 != 0)
                insideLoop.Add(coordinateChecking);
        }

        return insideLoop.Count.ToString();
    }

    private List<Coordinate> GetLoop(Map map, Coordinate root)
    {
        var queue = new Queue<Coordinate>();
        var visited = new HashSet<Coordinate> { root };
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

    private static string DetermineStart(Map map, Coordinate point)
    {
        var pointValue = map.Value(point);
        var connectingNeighbour = new List<Direction>();
        foreach (var neighbourDelta in MapFunctions.NeighbourDeltas)
        {
            var neighbourCoordinate = new Coordinate(point.Y + neighbourDelta.Coordinate.Y, point.X + neighbourDelta.Coordinate.X);
            if (!map.ContainsCoordinate(neighbourCoordinate)) continue;

            var neighbourValue = map.Value(neighbourCoordinate);
            var direction = neighbourDelta.Direction;
            if (direction == Direction.North && pointValue is "|" or "J" or "L" or "S" && neighbourValue is "|" or "7" or "F")
                connectingNeighbour.Add(Direction.North);

            if (direction == Direction.South && pointValue is "|" or "7" or "F" or "S" && neighbourValue is "|" or "J" or "L")
                connectingNeighbour.Add(Direction.South);

            if (direction == Direction.West && pointValue is "-" or "J" or "7" or "S" && neighbourValue is "-" or "L" or "F")
                connectingNeighbour.Add(Direction.West);

            if (direction == Direction.East && pointValue is "-" or "L" or "F" or "S" && neighbourValue is "-" or "J" or "7")
                connectingNeighbour.Add(Direction.East);
        }

        if (connectingNeighbour.Contains(Direction.North) && connectingNeighbour.Contains(Direction.East))
            return "L";
        if (connectingNeighbour.Contains(Direction.North) && connectingNeighbour.Contains(Direction.West))
            return "J";
        if (connectingNeighbour.Contains(Direction.North) && connectingNeighbour.Contains(Direction.South))
            return "|";
        if (connectingNeighbour.Contains(Direction.South) && connectingNeighbour.Contains(Direction.East))
            return "F";
        if (connectingNeighbour.Contains(Direction.South) && connectingNeighbour.Contains(Direction.West))
            return "7";
        return "-";
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