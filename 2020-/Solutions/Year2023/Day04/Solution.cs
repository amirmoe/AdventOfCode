using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;
using AdventOfCode.Utilities.Extensions;

namespace AdventOfCode.Solutions.Year2023.Day04;

internal class Day04 : ASolution
{
    public Day04() : base(04, 2023, "Scratchcards")
    {
    }

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        var points = new List<int>();
        foreach (var line in lines)
        {
            var card = line.Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Last()
                .Split("|", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var winningNumbers = card.First().Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToHashSet();
            var yourNumbers = card.Last().Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();
            var yourWinningNumbers = yourNumbers.Where(x => winningNumbers.Contains(x));
            points.Add(Convert.ToInt32(Math.Pow(2, yourWinningNumbers.Count() - 1)));
        }

        return points.Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        var lines = Input.SplitByNewline();

        var scratchcardDict = new Dictionary<int, int>();
        foreach (var line in lines)
        {
            var cardNumber = int.Parse(line.Split(":").First().Replace("Card ", string.Empty));
            var card = line.Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Last()
                .Split("|", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var winningNumbers = card.First().Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToHashSet();
            var yourNumbers = card.Last().Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();
            var yourWinningNumbers = yourNumbers.Where(x => winningNumbers.Contains(x)).ToList();

            var copies = scratchcardDict.ContainsKey(cardNumber) ? scratchcardDict[cardNumber] : 0;

            scratchcardDict.AddSafely(cardNumber, 1);

            for (var i = 1; i <= yourWinningNumbers.Count; i++) scratchcardDict.AddSafely(cardNumber + i, 1 + copies);
        }

        return scratchcardDict.ToList().Select(x => x.Value).Sum().ToString();
    }
}