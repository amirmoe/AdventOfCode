using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2023.Day05;

internal class Day05 : ASolution
{
    public Day05() : base(05, 2023, "If You Give A Seed A Fertilizer", false)
    {
    }

    protected override string SolvePartOne()
    {
        var seeds = GetSeeds(Input);
        var almanac = GetAlmanac(Input);
        var locations = new List<long>();
        foreach (var seed in seeds)
        {
            var key = almanac
                .Aggregate(seed, (current, map) => GetValueFromMap(map, current));
            locations.Add(key);
        }
        return locations.Min().ToString();
    }

    protected override string SolvePartTwo()
    {
        var seedValues = GetSeeds(Input).ToList();
        var almanac = GetAlmanac(Input);
        var seedPairs = new List<(long start, long end)>();
        for (var i = 0; i <= seedValues.Count / 2; i += 2) seedPairs.Add((seedValues[i], seedValues[i] + seedValues[i + 1]));

        var queue = new Queue<(long start, long end)>(seedPairs);
        foreach (var map in almanac)
        {
            var newRanges = new List<(long start, long end)>();
            while (queue.Any())
            {
                var seedPair = queue.Dequeue();
                var overlap = false;

                foreach (var row in map)
                {
                    var overlapStart = Math.Max(seedPair.start, row.source);
                    var overlapEnd = Math.Min(seedPair.end, row.source + row.range);
                    if (overlapStart < overlapEnd)
                    {
                        overlap = true;
                        newRanges.Add((row.destination + (overlapStart - row.source), row.destination + (overlapEnd - row.source)));

                        if (overlapStart > seedPair.start)
                            queue.Enqueue((seedPair.start, overlapStart));
                        if (seedPair.end > overlapEnd)
                            queue.Enqueue((overlapEnd, seedPair.end));
                        break;
                    }
                }
                if (!overlap)
                    newRanges.Add((seedPair.start, seedPair.end));
            }

            queue = new Queue<(long start, long end)>(newRanges);
        }
        return queue.ToList().MinBy(x => x.start).start.ToString();
    }

    private static IEnumerable<long> GetSeeds(string input)
    {
        return input
            .SplitByNewline()
            .First()
            .Split(": ")
            .Last()
            .Split(" ")
            .Select(long.Parse);
    }

    private static List<List<(long destination, long source, long range)>> GetAlmanac(string input)
    {
        return input
            .Split(new[] { "\n\n", "\r\n\r\n" }, 2, StringSplitOptions.None)
            .Last()
            .Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.None)
            .Select(map =>
            {
                return map.SplitByNewline()
                    .Skip(1)
                    .Select(row =>
                    {
                        var values = row.Split(" ").Select(long.Parse).ToList();
                        var destination = values[0];
                        var source = values[1];
                        var range = values[2];
                        return (destination, source, range);
                    })
                    .ToList();
            })
            .ToList();
    }

    private long GetValueFromMap(List<(long destination, long source, long range)> map, long key)
    {
        foreach (var row in map)
            if (row.source <= key && key < row.source + row.range)
                return row.destination + (key - row.source);

        return key;
    }
}