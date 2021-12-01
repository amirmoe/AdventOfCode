using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AdventOfCode.Solutions.Year2020.Day17
{
    internal class Day17 : ASolution
    {
        public Day17() : base(17, 2020, "Conway Cubes")
        {
            // DebugInput = ".#.\n..#\n###";
        }

        protected override string SolvePartOne()
        {
            var lines = Input.SplitByNewline();
            return GetActivePointsCount(lines).ToString();
        }

        protected override string SolvePartTwo()
        {
            var lines = Input.SplitByNewline();
            return GetActivePointsCount(lines, true).ToString();
        }

        //Would love a generic data structure for dimensions instead
        private static int GetActivePointsCount(string[] lines, bool part2 = false)
        {
            var activePoints = new List<(int, int, int, int)>();

            for (var i = 0; i < lines.Length; i++)
            for (var j = 0; j < lines[0].Length; j++)
                if (lines[i][j] == '#')
                    activePoints.Add((j, i, 0, 0));

            for (var i = 0; i < 6; i++) activePoints = GetNextGeneration(activePoints, part2);

            return activePoints.Count;
        }


        private static List<(int, int, int, int)> GetNextGeneration(
            IReadOnlyCollection<(int, int, int, int)> activePoints, bool part2)
        {
            var activePointsCopy =
                JsonConvert.DeserializeObject<List<(int, int, int, int)>>(JsonConvert.SerializeObject(activePoints));

            var neighbourDictionary = new Dictionary<(int, int, int, int), int>();
            foreach (var activePoint in activePoints)
            {
                var neighbours = GetNeighbours(activePoint, part2);

                var neighbourCount = 0;
                foreach (var neighbour in neighbours)
                    if (activePoints.Contains(neighbour))
                    {
                        neighbourCount++;
                    }
                    else
                    {
                        if (!neighbourDictionary.ContainsKey(neighbour))
                            neighbourDictionary.Add(neighbour, 1);
                        else
                            neighbourDictionary[neighbour] = neighbourDictionary[neighbour] + 1;
                    }

                if (!(neighbourCount == 2 || neighbourCount == 3))
                    activePointsCopy.Remove(activePoint);
            }

            foreach (var (key, value) in neighbourDictionary)
                if (value == 3)
                    activePointsCopy.Add(key);

            return activePointsCopy;
        }

        private static IEnumerable<(int, int, int, int)> GetNeighbours((int, int, int, int) point, bool part2)
        {
            var list = new List<(int, int, int, int)>();

            var (x, y, z, w) = point;
            for (var i = x - 1; i <= x + 1; i++)
            for (var j = y - 1; j <= y + 1; j++)
            for (var k = z - 1; k <= z + 1; k++)
                if (part2)
                    for (var l = w - 1; l <= w + 1; l++)
                        list.Add((i, j, k, l));
                else
                    list.Add((i, j, k, 0));

            list.Remove((x, y, z, w));
            return list;
        }
    }
}