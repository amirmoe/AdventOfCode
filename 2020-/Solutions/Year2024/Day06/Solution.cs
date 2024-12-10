using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2024.Day06;

internal class Day06 : ASolution
{
    public Day06() : base(06, 2024, "Guard Gallivant")
    {
    }


    protected override string SolvePartOne()
    {
        var map = new Map<string>(Input);
        var start = map.Values().First(x => x.Value == "^").Key;
        return VisitedByGuard(map, start).VisitedDictionary.Select(x => x.Key).ToList().Count.ToString();
    }


    protected override string SolvePartTwo()
    {
        var map = new Map<string>(Input);
        var start = map.Values().First(x => x.Value == "^").Key;
        
        var loops = 0;
        foreach (var (coordinate, value) in map.Values())
        {
            if (value != ".") continue;
            map.SetValue(coordinate, "#");
            if (VisitedByGuard(map, start).Loop) loops++;
            map.SetValue(coordinate, ".");
        }

        return loops.ToString();
    }


    private static Direction GetNewDirection(Direction currentDirection, int rotationDegrees)
    {
        var directions = NeighbourHelper.CardinalDirections;
        var steps = rotationDegrees / 90;
        var currentIndex = Array.IndexOf(directions, currentDirection);
        var newIndex = (currentIndex + steps) % directions.Length;
        return directions[newIndex];
    }

    private static (Dictionary<Coordinate, HashSet<Direction>>VisitedDictionary, bool Loop) VisitedByGuard(Map<string> map, Coordinate start)
    {
        var visitedDictionary = new Dictionary<Coordinate, HashSet<Direction>>();

        var currentLocation = start;
        var currentDirection = Direction.North;
        var loop = false;
        
        while (true)
        {
            if (visitedDictionary.ContainsKey(currentLocation) && visitedDictionary[currentLocation].Contains(currentDirection))
            {
                loop = true;
                break;
            }

            if (visitedDictionary.ContainsKey(currentLocation))
                visitedDictionary[currentLocation].Add(currentDirection);
            else
                visitedDictionary.Add(currentLocation, [currentDirection]);

            var neighbour = currentLocation.Neighbour(currentDirection);
            map.TryGetCoordinateValue(neighbour, out var neighbourValue);

            if (neighbourValue is "." or "^")
            {
                currentLocation = neighbour;
            }
            else if (neighbourValue == "#")
            {
                var newDirection = GetNewDirection(currentDirection, 90);
                var newLocation = currentLocation.Neighbour(newDirection);
                if (map.TryGetCoordinateValue(newLocation, out var newLocationValue) && newLocationValue == "#")
                {
                    newDirection = GetNewDirection(currentDirection, 180);
                    newLocation = currentLocation.Neighbour(newDirection);
                }
                currentDirection = newDirection;
                currentLocation = newLocation;
            }
            else if (neighbourValue == null)
            {
                break;
            }
        }

        return (visitedDictionary, loop);
    }
}