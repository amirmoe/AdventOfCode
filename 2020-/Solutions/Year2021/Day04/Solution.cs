using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2021.Day04;

internal class Day04 : ASolution
{
    public Day04() : base(04, 2021, "Giant Squid")
    {
    }

    protected override string SolvePartOne()
    {
        return Solution(Input).ToString();
    }

    protected override string SolvePartTwo()
    {
        return Solution(Input, false).ToString();
    }

    private static int Solution(string input, bool firstWinner = true)
    {
        var sections = input.Split("\n\n");
        var numbers = sections[0].ToIntArray(",");

        var boards = sections.Skip(1).Select(GetBoard).ToList();

        var calledNumbersPerBoard = new List<List<(int, int)>>();
        boards.ForEach(_ => calledNumbersPerBoard.Add(new List<(int, int)>()));

        var score = 0;
        foreach (var number in numbers)
        {
            CallNumbers(boards, calledNumbersPerBoard, number);
            var completedBoardIndexes = CheckForCompleted(calledNumbersPerBoard);
            if (completedBoardIndexes.Count == 0)
                continue;

            if (!firstWinner)
                if (boards.Count > 1)
                {
                    foreach (var completedBoardIndex in completedBoardIndexes.OrderByDescending(x => x))
                    {
                        boards.RemoveAt(completedBoardIndex);
                        calledNumbersPerBoard.RemoveAt(completedBoardIndex);
                    }

                    continue;
                }

            var unmarkedScore = GetUnmarkedScore(boards, calledNumbersPerBoard, completedBoardIndexes.First());
            score = unmarkedScore * number;
            break;
        }

        return score;
    }


    private static int GetUnmarkedScore(List<Dictionary<int, (int, int)>> boards,
        List<List<(int, int)>> calledNumbersPerBoard, int index)
    {
        return boards[index].Where(kvp => !calledNumbersPerBoard[index].Contains(kvp.Value)).Sum(kvp => kvp.Key);
    }

    private static void CallNumbers(List<Dictionary<int, (int, int)>> boards,
        List<List<(int, int)>> calledNumbersPerBoard, int number)
    {
        for (var i = 0; i < boards.Count; i++)
            if (boards[i].ContainsKey(number))
                calledNumbersPerBoard[i].Add(boards[i][number]);
    }

    private static List<int> CheckForCompleted(List<List<(int, int)>> calledNumbersPerBoard)
    {
        var completedBoardIndex = new List<int>();
        for (var i = 0; i < calledNumbersPerBoard.Count; i++)
        {
            for (var j = 0; j < 5; j++)
            {
                if (calledNumbersPerBoard[i].Count(x => x.Item1 == j) != 5) continue;
                completedBoardIndex.Add(i);
                break;
            }

            for (var j = 0; j < 5; j++)
            {
                if (calledNumbersPerBoard[i].Count(x => x.Item2 == j) != 5) continue;
                completedBoardIndex.Add(i);
                break;
            }
        }

        return completedBoardIndex.Distinct().ToList();
    }

    private static Dictionary<int, (int, int)> GetBoard(string boardLines)
    {
        var board = new Dictionary<int, (int, int)>();
        var lines = boardLines.SplitByNewline();

        for (var i = 0; i < 5; i++)
        {
            var line = lines[i].Trim();
            var values = line.Split(new[] { "  ", " " }, StringSplitOptions.None);
            for (var j = 0; j < 5; j++) board[int.Parse(values[j])] = (i, j);
        }

        return board;
    }
}