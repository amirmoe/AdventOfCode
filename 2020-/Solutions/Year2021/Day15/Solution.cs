using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2021.Day15;

internal class Day15 : ASolution
{
    private static readonly (int deltaY, int deltaX)[] NeighbourDeltas =
        { (1, 0), (0, 1), (-1, 0), (0, -1) };

    public Day15() : base(15, 2021, "Chiton")
    {
    }

    protected override string SolvePartOne()
    {
        var map = GetMap(Input.SplitByNewline());
        return Solve(map)
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        var map = ScaleUpMap(GetMap(Input.SplitByNewline()));
        return Solve(map)
            .ToString();
    }

    private static int Solve(Dictionary<(int y, int x), int> map)
    {
        var goal = (map.Keys.Select(x => x.y).Max(), map.Keys.Select(x => x.x).Max());

        var queue = new PriorityQueue<(int y, int x), int>();
        var visited = new HashSet<(int, int)>();

        queue.Enqueue((0, 0), 0);
        while (queue.Count > 0)
        {
            if (queue.Peek() == goal)
                break;

            queue.TryDequeue(out var coordinate, out var priority);

            if (visited.Contains(coordinate))
                continue;

            visited.Add(coordinate);

            foreach (var (deltaY, deltaX) in NeighbourDeltas)
            {
                var neighbourCoordinate = (coordinate.y + deltaY, coordinate.x + deltaX);
                if (map.ContainsKey(neighbourCoordinate)) queue.Enqueue(neighbourCoordinate, map[neighbourCoordinate] + priority);
            }
        }

        queue.TryDequeue(out _, out var totalRisk);
        return totalRisk;
    }

    private static Dictionary<(int y, int x), int> GetMap(IReadOnlyList<string> inputLines)
    {
        var map = new Dictionary<(int, int), int>();
        for (var i = 0; i < inputLines.Count; i++)
        for (var j = 0; j < inputLines[0].Length; j++)
            map.Add((i, j), int.Parse(inputLines[i][j].ToString()));
        return map;
    }

    private static Dictionary<(int y, int x), int> ScaleUpMap(Dictionary<(int y, int x), int> map)
    {
        var yMax = map.Max(x => x.Key.y);
        var xMax = map.Max(x => x.Key.x);

        for (var i = 0; i <= yMax; i++)
        for (var j = 0; j <= xMax; j++)
        for (var k = 0; k < 5; k++)
        for (var l = 0; l < 5; l++)
        {
            if (k == 0 && l == 0)
                continue;

            var number = (map[(i, j)] + k + l) % 9;
            if (number == 0)
                number = 9;

            map.Add((i + k * (yMax + 1), j + l * (xMax + 1)), number);
        }

        return map;
    }
}