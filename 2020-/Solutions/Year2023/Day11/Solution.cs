using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2023.Day11;

internal class Day11 : ASolution
{
    public Day11() : base(11, 2023, "Cosmic Expansion", false)
    {
    }

    protected override string SolvePartOne()
    {
        return Solve(GetGalaxies(Input, 2));
    }
    
    protected override string SolvePartTwo()
    {
        return Solve(GetGalaxies(Input, 1000000));
    }

    private static string Solve(List<Coordinate> galaxies)
    {
        return galaxies
            .SelectMany((fst, i) => galaxies.Skip(i + 1).Select(snd => (fst, snd)))
            .Select(x => Convert.ToInt64(Math.Abs(x.fst.Y - x.snd.Y) + Math.Abs(x.fst.X - x.snd.X)))
            .Sum()
            .ToString();
    }

    private static List<Coordinate> GetGalaxies(string input, int magnitude)
    {
        var rows = input.SplitByNewline();
        var height = rows.Length;
        var width = rows.First().Length;
        var emptyCols = new HashSet<int>();
        var emptyRows = new HashSet<int>();
        
        for (var i = 0; i < height; i++)
            if (rows[i].ToCharArray().All(x => x == '.'))
                emptyRows.Add(i);
        for (var i = 0; i < width; i++)
            if (rows.Select(x => x[i]).All(x => x == '.'))
                emptyCols.Add(i);
        

        // var map = new Map();
        var galaxies = new List<Coordinate>();
        var offsetRow = 0;
        for (var i = 0; i < height; i++)
        {
            var offsetCol = 0;
            for (var j = 0; j < width; j++)
            {
                var value = rows[i][j].ToString();
                var coordinate = new Coordinate(i+offsetRow, j+offsetCol);
                if (value == "#")
                    galaxies.Add(coordinate);
                // map.AddCoordinate(coordinate, value);
                //
                // if (emptyCols.Contains(j))
                // {
                //     map.AddCoordinate(new Coordinate(i+offsetRow, j+offsetCol+1), value);
                //     if (emptyRows.Contains(i))
                //         map.AddCoordinate(new Coordinate(i+offsetRow+1, j+offsetCol+1), value);
                // }
                //
                // if (emptyRows.Contains(i))
                //     map.AddCoordinate(new Coordinate(i+offsetRow+1, j+offsetCol), value);

                if (emptyCols.Contains(j)) offsetCol += magnitude-1;
            }
            if (emptyRows.Contains(i)) offsetRow += magnitude-1;
        }

        return galaxies;
    }
    
}