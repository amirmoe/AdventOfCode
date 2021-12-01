using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2020.Day11
{
    public static class SeatHelper
    {
        public static string[,] GetNextGeneration(string[,] map, bool part2)
        {
            var mapCopy = map.Clone() as string[,];

            for (var i = 1; i < map.GetLength(0) - 1; i++)
            for (var j = 1; j < map.GetLength(1) - 1; j++)
            {
                var occupiedAdjacentSeats =
                    part2 ? GetOccupiedAdjacentSeats2(map, i, j) : GetOccupiedAdjacentSeats(map, i, j);

                mapCopy[i, j] = map[i, j] switch
                {
                    "L" when occupiedAdjacentSeats == 0 => "#",
                    "#" when occupiedAdjacentSeats >= (part2 ? 5 : 4) => "L",
                    _ => mapCopy[i, j]
                };
            }

            return mapCopy;
        }

        public static int GetOccupiedSeats(string[,] map)
        {
            var count = 0;
            for (var i = 1; i < map.GetLength(0) - 1; i++)
            for (var j = 0; j < map.GetLength(1) - 1; j++)
                if (map[i, j] == "#")
                    count++;
            return count;
        }

        private static int GetOccupiedAdjacentSeats(string[,] map, int i, int j)
        {
            var count = 0;
            for (var k = i - 1; k <= i + 1; k++)
            for (var l = j - 1; l <= j + 1; l++)
            {
                if (k == i && l == j)
                    continue;

                if (map[k, l] == "#")
                    count++;
            }

            return count;
        }

        private static int GetOccupiedAdjacentSeats2(string[,] map, int i, int j)
        {
            var count = 0;
            var height = map.GetLength(0);
            var width = map.GetLength(1);

            var directions = new List<(int, int)>
                {(-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1)};

            foreach (var (yDirection, xDirection) in directions)
            {
                var y = i;
                var x = j;

                while (true)
                {
                    y += yDirection;
                    x += xDirection;
                    if (IsEdge(y, x, height, width))
                        break;

                    if (map[y, x] == "#")
                    {
                        count++;
                        break;
                    }

                    if (map[y, x] == "L") break;
                }
            }

            return count;
        }

        private static bool IsEdge(int y, int x, int maxY, int maxX)
        {
            return y == 0 || x == 0 || y == maxY || x == maxX;
        }
    }
}