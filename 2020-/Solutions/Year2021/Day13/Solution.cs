using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2021.Day13;

internal class Day13 : ASolution
{
    public Day13() : base(13, 2021, "Transparent Origami")
    {
    }

    protected override string SolvePartOne()
    {
        var (map, folds) = GetMapAndFolds(Input);

        map = Fold(map, folds[0]);

        var count = 0;
        for (var i = 0; i < map.GetLength(0); i++)
        for (var j = 0; j < map.GetLength(1); j++)
            count += map[i, j] == "#" ? 1 : 0;

        return count.ToString();
    }

    protected override string SolvePartTwo()
    {
        var (map, folds) = GetMapAndFolds(Input);

        folds.ForEach(fold => map = Fold(map, fold));

        var output = new StringBuilder(Environment.NewLine);
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++) output.Append($"{map[i, j]} ");
            output.Append(Environment.NewLine);
        }

        return output.ToString();
    }

    private static string[,] Fold(string[,] map, string fold)
    {
        var foldAlong = int.Parse(fold.Split("=")[1]);
        string[,] newMap;
        switch (fold[0])
        {
            case 'x':
                newMap = new string[map.GetLength(0), foldAlong];
                for (var i = 0; i < map.GetLength(0); i++)
                for (var j = 0; j < foldAlong; j++)
                {
                    var index = 2 * foldAlong - j;
                    newMap[i, j] = map[i, index] == "#" ? "#" : map[i, j];
                }

                map = newMap;
                break;
            case 'y':
                newMap = new string[foldAlong, map.GetLength(1)];
                for (var i = 0; i < foldAlong; i++)
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    var index = 2 * foldAlong - i;
                    newMap[i, j] = map[index, j] == "#" ? "#" : map[i, j];
                }

                map = newMap;
                break;
        }

        return map;
    }

    private static (string[,] map, List<string> folds) GetMapAndFolds(string input)
    {
        var lines = input.Split("\r\n\r\n");

        var instructions = lines[0]
            .SplitByNewline()
            .Select(x => x.Split(","))
            .Select(x => (int.Parse(x[1]), int.Parse(x[0])))
            .ToList();

        var folds = lines[1]
            .SplitByNewline()
            .Select(x => x.Replace("fold along ", string.Empty))
            .ToList();

        var xMax = instructions.Max(x => x.Item2);
        var yMax = instructions.Max(x => x.Item1);

        var map = GetMap(instructions, yMax, xMax);
        return (map, folds);
    }


    private static string[,] GetMap(List<(int y, int x)> instructions, int yMax, int xMax)
    {
        var map = new string[yMax + 1, xMax + 1];
        for (var i = 0; i <= yMax; i++)
        for (var j = 0; j <= xMax; j++)
            map[i, j] = ".";

        instructions.ForEach(cord =>
        {
            var (y, x) = cord;
            map[y, x] = "#";
        });

        return map;
    }
}