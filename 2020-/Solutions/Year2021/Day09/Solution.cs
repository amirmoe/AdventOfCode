using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021.Day09
{
    internal class Day09 : ASolution
    {
        private static readonly (int deltaY, int deltaX)[] NeighbourDeltas = { (1, 0), (0, 1), (-1, 0), (0, -1) };

        public Day09() : base(09, 2021, "Smoke Basin")
        {
        }

        protected override string SolvePartOne()
        {
            var lines = Input.SplitByNewline();
            var map = GetMap(lines);
            return GetLowestPoints(map, lines.Length - 1, lines[0].Length - 1)
                .Select(point => map[point] + 1)
                .Sum()
                .ToString();
        }

        protected override string SolvePartTwo()
        {
            var lines = Input.SplitByNewline();
            var map = GetMap(lines);
            return GetLowestPoints(map, lines.Length - 1, lines[0].Length - 1)
                .Select(x => CalculateBasinSize(map, x))
                .OrderByDescending(x => x)
                .Take(3)
                .Aggregate((result, item) => result * item)
                .ToString();
        }

        private static int CalculateBasinSize(IReadOnlyDictionary<(int y, int x), int> map, (int y, int x) lowPoint)
        {
            var queue = new List<(int y, int x)> { lowPoint };
            var seen = new List<(int y, int x)> { lowPoint };
            while (queue.Count > 0)
            {
                var (y, x) = queue.First();
                queue.RemoveAt(0);
                foreach (var (deltaY, deltaX) in NeighbourDeltas)
                {
                    var neighbour = (y + deltaY, x + deltaX);
                    if (!map.ContainsKey(neighbour) || seen.Contains(neighbour) || map[neighbour] == 9) continue;
                    queue.Add(neighbour);
                    seen.Add(neighbour);
                }
            }

            return seen.Count;
        }

        private static Dictionary<(int y, int x), int> GetMap(IReadOnlyList<string> inputLines)
        {
            var map = new Dictionary<(int, int), int>();
            for (var i = 0; i < inputLines.Count; i++)
            for (var j = 0; j < inputLines[0].Length; j++)
                map.Add((i, j), int.Parse(inputLines[i][j].ToString()));
            return map;
        }

        private static IEnumerable<(int, int)> GetLowestPoints(IReadOnlyDictionary<(int, int), int> map, int maxY, int maxX)
        {
            var points = new List<(int, int)>();
            for (var i = 0; i <= maxY; i++)
            for (var j = 0; j <= maxX; j++)
                if (NeighbourDeltas.All(neighbourDelta =>
                {
                    var (deltaY, deltaX) = neighbourDelta;
                    var neighbour = (i + deltaY, j + deltaX);
                    return !map.ContainsKey(neighbour) || map[(i, j)] < map[neighbour];
                }))
                    points.Add((i, j));
            return points;
        }
    }
}