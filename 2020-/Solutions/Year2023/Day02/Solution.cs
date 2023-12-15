using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2023.Day02;

internal class Day02 : ASolution
{
    private const string Red = "red";
    private const string Blue = "blue";
    private const string Green = "green";

    public Day02() : base(02, 2023, "Cube Conundrum")
    {
    }

    protected override string SolvePartOne()
    {
        var dict = new Dictionary<string, int>
        {
            { Red, 12 },
            { Green, 13 },
            { Blue, 14 }
        };

        return Input
            .SplitByNewline()
            .Select(line =>
                {
                    var game = int.Parse(Regex.Match(line, @"(?<=Game )\d+").Value);
                    var valid = line
                        .Split(": ")
                        .Last()
                        .Split("; ")
                        .Select(subset => subset.Split(", ").Select(pick =>
                        {
                            var pickSplit = pick.Split(" ");
                            var number = int.Parse(pickSplit.First());
                            var color = pickSplit.Last();
                            switch (color)
                            {
                                case Red when number > dict[Red]:
                                case Green when number > dict[Green]:
                                case Blue when number > dict[Blue]:
                                    return false;
                                default:
                                    return true;
                            }
                        }))
                        .SelectMany(x => x)
                        .All(x => x);
                    return valid ? game : 0;
                })
            .Sum()
            .ToString() ;
    }

    protected override string SolvePartTwo()
    {
        return Input
            .SplitByNewline()
            .Select(line =>
            {
                var dict = new Dictionary<string, int>
                {
                    { Red, 0 },
                    { Green, 0 },
                    { Blue, 0 }
                };

                line
                    .Split(": ")
                    .Last()
                    .Split("; ")
                    .ToList()
                    .ForEach(subset => subset.Split(", ").ToList().ForEach(pick =>
                    {
                        var pickSplit = pick.Split(" ");
                        var number = int.Parse(pickSplit.First());
                        var color = pickSplit.Last();
                        switch (color)
                        {
                            case Red when number > dict[Red]:
                                dict[Red] = number;
                                break;
                            case Green when number > dict[Green]:
                                dict[Green] = number;
                                break;
                            case Blue when number > dict[Blue]:
                                dict[Blue] = number;
                                break;
                        }
                    }));
                return dict[Red]*dict[Green]*dict[Blue];
            })
            .Sum()
            .ToString() ;
    }
}