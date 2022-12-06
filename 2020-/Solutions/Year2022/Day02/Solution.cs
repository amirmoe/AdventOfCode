using System;
using System.Linq;
using AdventOfCode.Utilities;
using AdventOfCode.Utilities.Extensions;
using static System.Enum;

namespace AdventOfCode.Solutions.Year2022.Day02;

internal class Day02 : ASolution
{
    public Day02() : base(02, 2022, "Rock Paper Scissors")
    {
    }


    protected override string SolvePartOne()
    {
        return Input
            .SplitByNewline()
            .Select(row =>
            {
                TryParse<OpponentOptions>(row.Split(" ")[0], out var opponentOption);
                TryParse<MyOptions>(row.Split(" ")[1], out var meOption);

                if (opponentOption.ToInt() == meOption.ToInt())
                    return GetOptionScore(meOption) + 3;
                if (HelperFunctions.Modulo(meOption.ToInt() - 1, 3) == opponentOption.ToInt())
                    return GetOptionScore(meOption) + 6;
                return GetOptionScore(meOption);
            })
            .Sum()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        return Input
            .SplitByNewline()
            .Select(row =>
            {
                TryParse<OpponentOptions>(row.Split(" ")[0], out var opponentOption);
                TryParse<MyOptions>(row.Split(" ")[1], out var meOption);

                return meOption switch
                {
                    MyOptions.X => HelperFunctions.Modulo(opponentOption.ToInt() - 1, 3) + 1,
                    MyOptions.Y => opponentOption.ToInt() + 1 + 3,
                    MyOptions.Z => HelperFunctions.Modulo(opponentOption.ToInt() + 1, 3) + 1 + 6,
                    _ => throw new ArgumentOutOfRangeException()
                };
            })
            .Sum()
            .ToString();
    }

    private static int GetOptionScore(MyOptions m)
    {
        return m.ToInt() + 1;
    }


    private enum OpponentOptions
    {
        A,
        B,
        C
    }

    private enum MyOptions
    {
        X,
        Y,
        Z
    }
}