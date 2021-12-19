using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2020.Day11
{
    internal class Day11 : ASolution
    {
        public Day11() : base(11, 2020, "Seating System")
        {
            // DebugInput =
            //     "L.LL.LL.LL\nLLLLLLL.LL\nL.L.L..L..\nLLLL.LL.LL\nL.LL.LL.LL\nL.LLLLL.LL\n..L.L.....\nLLLLLLLLLL\nL.LLLLLL.L\nL.LLLLL.LL";
        }

        protected override string SolvePartOne()
        {
            return Answer();
        }

        protected override string SolvePartTwo()
        {
            return Answer(true);
        }

        private string Answer(bool part2 = false)
        {
            var rows = Input.SplitByNewline();

            var map = GetMap(rows);
            while (true)
            {
                var nextGeneration = SeatHelper.GetNextGeneration(map, part2);
                var equal =
                    map.Rank == nextGeneration.Rank &&
                    Enumerable.Range(0, map.Rank).All(dimension =>
                        map.GetLength(dimension) == nextGeneration.GetLength(dimension)) &&
                    map.Cast<string>().SequenceEqual(nextGeneration.Cast<string>());
                map = nextGeneration;
                if (equal)
                    break;
            }

            return SeatHelper.GetOccupiedSeats(map).ToString();
        }

        private static string[,] GetMap(IReadOnlyList<string> rows)
        {
            var mapWidth = rows[0].Length + 2;
            var mapHeight = rows.Count + 2;
            var map = new string[mapHeight, mapWidth];

            for (var i = 0; i < mapHeight; i++)
            for (var j = 0; j < mapWidth; j++)
                if (i == 0 || j == 0 || i == mapHeight - 1 || j == mapWidth - 1)
                    map[i, j] = ".";
                else
                    map[i, j] = rows[i - 1][j - 1].ToString();
            return map;
        }

        // private static void DebugPrintMatrix(string[,] arr)
        // {
        //     var rowCount = arr.GetLength(0);
        //     var colCount = arr.GetLength(1);
        //     for (var row = 0; row < rowCount; row++)
        //     {
        //         for (var col = 0; col < colCount; col++)               
        //             Console.Write($"{arr[row, col]}\t");
        //         Console.WriteLine();
        //     } 
        // }
    }
}