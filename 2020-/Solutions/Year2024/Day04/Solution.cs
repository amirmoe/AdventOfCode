﻿using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2024.Day04;

internal class Day04 : ASolution
{
    public Day04() : base(04, 2024, "Ceres Search")
    {
    }

    protected override string SolvePartOne()
    {
        var map = new Map<string>(Input);
        return map
            .Values()
            .Select(kvp =>
            {
                var (coordinate, value) = kvp;
                if (value == "X")
                    return NeighbourHelper
                        .AllNeighbourDirections
                        .Select(direction => XmasSearch(map, coordinate, direction, "MAS", 0))
                        .Count(found => found);
                return 0;
            })
            .Sum()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        var check = new List<string> { "M", "S" };
        var map = new Map<string>(Input);

        return map.Values().Select(kvp =>
            {
                var (coordinate, value) = kvp;
                if (value != "A") return false;

                var northWestValue = map.TryGetCoordinateValue(coordinate.Neighbour(Direction.NorthWest), out var nw) ? nw : null;
                var southEastValue = map.TryGetCoordinateValue(coordinate.Neighbour(Direction.SouthEast), out var se) ? se : null;
                var northEastValue = map.TryGetCoordinateValue(coordinate.Neighbour(Direction.NorthEast), out var ne) ? ne : null;
                var southWestValue = map.TryGetCoordinateValue(coordinate.Neighbour(Direction.SouthWest), out var sw) ? sw : null;

                var diagonalValues1 = new List<string>
                {
                    northWestValue,
                    southEastValue
                };

                var diagonalValues2 = new List<string>
                {
                    northEastValue,
                    southWestValue
                };

                return check.All(diagonalValues1.Contains) && check.All(diagonalValues2.Contains);
            })
            .Count(found => found)
            .ToString();
    }


    private static bool XmasSearch(
        Map<string> map,
        Coordinate coordinate,
        Direction direction,
        string word,
        int index)
    {
        var neighbourCoordinate = coordinate.Neighbour(direction);
        if (map.TryGetCoordinateValue(neighbourCoordinate, out var neighbourValue) && neighbourValue == word[index].ToString())
            return index == word.Length - 1 || XmasSearch(map, neighbourCoordinate, direction, word, index + 1);

        return false;
    }
}