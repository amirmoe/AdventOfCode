using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2021.Day10;

internal class Day10 : ASolution
{
    private static readonly Dictionary<char, char> CharacterDictionary = new() { { '<', '>' }, { '[', ']' }, { '{', '}' }, { '(', ')' } };

    public Day10() : base(10, 2021, "Smoke Basin")
    {
    }

    protected override string SolvePartOne()
    {
        var scoreDictionary = new Dictionary<char, int> { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        return Input
            .SplitByNewline().ToList()
            .Select(ProcessLine)
            .Where(x => x.failingCharacter.HasValue)
            .Select(x => scoreDictionary[x.failingCharacter.Value])
            .Sum()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        var scoreDictionary = new Dictionary<char, int> { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };

        var scores = Input
            .SplitByNewline().ToList()
            .Select(ProcessLine)
            .Where(x => !x.failingCharacter.HasValue)
            .Select(x =>
                x.remainingStack.Aggregate<char, long>(0, (current, c) =>
                    current * 5 + scoreDictionary[CharacterDictionary[c]]))
            .OrderByDescending(x => x)
            .ToList();

        return scores[scores.Count / 2].ToString();
    }

    private static (char? failingCharacter, List<char> remainingStack) ProcessLine(string line)
    {
        var stack = new List<char>();
        foreach (var c in line)
            if (CharacterDictionary.ContainsKey(c))
            {
                stack.Insert(0, c);
            }
            else
            {
                var current = stack[0];
                stack.RemoveAt(0);
                if (CharacterDictionary[current] != c) return (c, stack);
            }

        return (null, stack);
    }
}