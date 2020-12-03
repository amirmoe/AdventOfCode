using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020.Day03
{

    class Day03 : ASolution
    {

        public Day03() : base(03, 2020, "Toboggan Trajectory")
        {
//              DebugInput =
//                 @"..##.......
// #...#...#..
// .#....#..#.
// ..#.#...#.#
// .#...##..#.
// ..#.##.....
// .#.#.#....#
// .#........#
// #.##...#...
// #...##....#
// .#..#...#.#";
        }

        protected override string SolvePartOne()
        {
            var rows = Input.SplitByNewline();
            
            var map = GetMap(rows, 1, 3);
            var counter = GetTreeCount(map, 1, 3);

            return counter.ToString();
        }

        protected override string SolvePartTwo()
        {
            var rows = Input.SplitByNewline();
            
            var ySlopes = new [] {1, 1, 1, 1, 2};
            var xSlopes = new [] {1, 3, 5, 7, 1};
            var trees = new List<int>();
            for (var i = 0; i < ySlopes.Length; i++)
            {
                var map = GetMap(rows, ySlopes[i], xSlopes[i]);
                trees.Add(GetTreeCount(map, ySlopes[i], xSlopes[i]));
            }
            
            return trees.Aggregate((a, x) => a * x).ToString();
            
        }

        private static string[,] GetMap(IReadOnlyList<string> rows, int ySlope, int xSlope)
        {
            var mapWidth = rows[0].Length;
            var mapHeight = rows.Count;

            var stepsYAxis = mapHeight-1/ySlope;

            var widthNeededMultiplier = (int) Math.Ceiling((double) (xSlope * stepsYAxis + 1) / mapWidth);
            
            var map = new string[mapHeight,mapWidth*widthNeededMultiplier];
            var height = 0;
            foreach (var row in rows)
            {
                var multipliedRow = string.Concat(Enumerable.Repeat(row, widthNeededMultiplier));

                for (var i = 0; i < multipliedRow.Length; i++)
                {
                    map[height, i] = multipliedRow[i].ToString();
                }
                height++;
            }

            return map;
        }

        private static int GetTreeCount(string[,] map, int ySlope, int xSlope)
        {
            var counter = 0;
            var stepX = xSlope;
            var stepY = ySlope;
            while (stepY <= map.GetLength(0)-1)
            {
                if (map[stepY, stepX] == "#")
                    counter++;

                stepX += xSlope;
                stepY += ySlope;
            }

            return counter;
        }
    }
}
