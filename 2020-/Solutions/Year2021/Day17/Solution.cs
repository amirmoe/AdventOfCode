using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021.Day17;

internal class Day17 : ASolution
{
    public Day17() : base(17, 2021, "Trick Shot")
    {
    }

    protected override string SolvePartOne()
    {
        return Process(GetTargetArea(Input)).Ys.Max().ToString();
    }

    protected override string SolvePartTwo()
    {
        return Process(GetTargetArea(Input)).Hits.Count.ToString();
    }

    private static (List<int> Ys, List<(int, int)> Hits) Process((int xMin, int xMax, int yMin, int yMax) targetArea)
    {
        var ys = new List<int>();
        var hits = new List<(int, int)>();
        for (var i = -1000; i < 1000; i++)
        for (var j = -1000; j < 1000; j++)
        {
            var (hit, highestY) = Shoot(targetArea, (i, j));
            if (!hit) continue;
            ys.Add(highestY);
            hits.Add((i, j));
        }

        return (ys, hits);
    }

    private static (bool hit, int highestY) Shoot((int xMin, int xMax, int yMin, int yMax) targetArea, (int x, int y) velocity)
    {
        var position = (0, 0);
        var hitsTargetArea = false;
        var highestY = 0;
        while (!ProbeIsPast(targetArea, velocity, position))
        {
            position = (position.Item1 + velocity.x, position.Item2 + velocity.y);

            if (position.Item2 > highestY)
                highestY = position.Item2;

            if (ProbeHitsTargetArea(targetArea, position))
            {
                hitsTargetArea = true;
                break;
            }

            var newXVelocity = velocity.x == 0 ? 0 : velocity.x > 0 ? velocity.x - 1 : velocity.x + 1;
            var newYVelocity = velocity.y - 1;
            velocity = (newXVelocity, newYVelocity);
        }

        return (hitsTargetArea, highestY);
    }


    private static bool ProbeIsPast((int xMin, int xMax, int yMin, int yMax) targetArea, (int x, int y) velocity, (int x, int y) position)
    {
        if (velocity.y <= 0 && position.y < targetArea.yMin)
            return true;

        if (velocity.x >= 0 && position.x > targetArea.xMax)
            return true;

        if (velocity.x <= 0 && position.x < targetArea.xMin)
            return true;

        return false;
    }

    private static bool ProbeHitsTargetArea((int xMin, int xMax, int yMin, int yMax) targetArea, (int x, int y) position)
    {
        return targetArea.xMin <= position.x && position.x <= targetArea.xMax &&
               targetArea.yMin <= position.y && position.y <= targetArea.yMax;
    }

    private static (int xMin, int xMax, int yMin, int yMax) GetTargetArea(string input)
    {
        var dimensionBoundaries = input.Replace("target area: ", string.Empty).Split(", ");
        var x = dimensionBoundaries[0]
            .Replace("x=", string.Empty)
            .Split("..")
            .Select(n => Convert.ToInt32(n))
            .ToList();
        var y = dimensionBoundaries[1]
            .Replace("y=", string.Empty)
            .Split("..")
            .Select(n => Convert.ToInt32(n))
            .ToList();
        return (x[0], x[1], y[0], y[1]);
    }
}