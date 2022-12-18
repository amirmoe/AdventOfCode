using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2022.Day05;

internal class Day05 : ASolution
{
    public Day05() : base(05, 2022, "Supply Stacks")
    {
    }

    protected override string SolvePartOne()
    {
        return Solution(Input, false);
    }

    protected override string SolvePartTwo()
    {
        return Solution(Input, true);
    }

    private static string Solution(string input, bool solution2)
    {
        var inputParts = input.Split("\n\n");
        var listOfStacks = GetStacks(inputParts.First());
        var instructions = GetInstructions(inputParts.Last());

        foreach (var instruction in instructions)
        {
            var tempStack = new Stack<char>();
            Enumerable.Range(0, instruction.Count).ToList().ForEach(_ => { tempStack.Push(listOfStacks[instruction.From].Pop()); });
            foreach (var c in solution2 ? tempStack : tempStack.Reverse()) listOfStacks[instruction.To].Push(c);
        }

        return string.Join(string.Empty, listOfStacks.Select(x => x.Pop()));
    }

    private static List<Stack<char>> GetStacks(string input)
    {
        var rowsFirstPart = input.SplitByNewline().Reverse().Skip(1).ToList();
        var numberOfStacks = (rowsFirstPart.First().Length + 1) / 4;
        var listOfStacks = Enumerable.Range(0, numberOfStacks).Select(_ => new Stack<char>()).ToList();
        foreach (var row in rowsFirstPart)
        {
            var boxPlaces = row.Chunk(4).ToList();
            for (var i = 0; i < boxPlaces.Count; i++)
                if (boxPlaces[i][1] != 32)
                    listOfStacks[i].Push(boxPlaces[i][1]);
        }

        return listOfStacks;
    }

    private static List<Instruction> GetInstructions(string input)
    {
        return input
            .SplitByNewline()
            .Select(row => row.Split(" "))
            .Select(splitInstruction =>
                new Instruction(
                    int.Parse(splitInstruction[1]),
                    int.Parse(splitInstruction[3]) - 1,
                    int.Parse(splitInstruction[5]) - 1))
            .ToList();
    }
}

public class Instruction
{
    public Instruction(int count, int from, int to)
    {
        Count = count;
        From = from;
        To = to;
    }

    public int Count { get; }
    public int From { get; }
    public int To { get; }
}