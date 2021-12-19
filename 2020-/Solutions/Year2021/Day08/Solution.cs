using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2021.Day08;

internal class Day08 : ASolution
{
    public Day08() : base(08, 2021, "Seven Segment Search", true)
    {
    }

    protected override string SolvePartOne()
    {
        return Input
            .SplitByNewline().ToList()
            .Select(x => x.Split(" | ")[1]).ToList()
            .SelectMany(x => x.Split(" "))
            .Sum(x =>
            {
                switch (x.Length)
                {
                    case 2:
                    case 4:
                    case 3:
                    case 7:
                        return 1;
                }

                return 0;
            })
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        return Input
            .SplitByNewline().ToList()
            .Select(x =>
            {
                var split = x.Split(" | ");
                return (split[0], split[1]);
            }).Select(inputLine =>
            {
                var dictionary = GetDictionary(inputLine.Item1);
                return int.Parse(string.Join(string.Empty,
                    inputLine.Item2.Split(" ").ToList().Select(x =>
                            dictionary.First(kvp => kvp.Value.OrderBy(c => c).SequenceEqual(x.ToCharArray().OrderBy(c => c))).Key)
                        .Select(x => x.ToString())));
            })
            .Sum()
            .ToString();
    }

    private static Dictionary<int, char[]> GetDictionary(string input)
    {
        var dict = new Dictionary<int, char[]>();
        var signalPatterns = input.Split(" ").ToList();
        dict.Add(1, signalPatterns.First(x => x.Length == 2).ToCharArray());
        dict.Add(4, signalPatterns.First(x => x.Length == 4).ToCharArray());
        dict.Add(7, signalPatterns.First(x => x.Length == 3).ToCharArray());
        dict.Add(8, signalPatterns.First(x => x.Length == 7).ToCharArray());

        var nine = signalPatterns.First(x => x.Length == 6 && dict[4].All(c => x.ToCharArray().Contains(c)));
        dict.Add(9, nine.ToCharArray());

        var three = signalPatterns.First(x => x.Length == 5 && dict[7].All(c => x.ToCharArray().Contains(c)));
        dict.Add(3, three.ToCharArray());

        var two = signalPatterns.First(x => x.Length == 5 && !x.ToCharArray().All(c => dict[9].Contains(c)));
        dict.Add(2, two.ToCharArray());

        signalPatterns.Remove(nine);
        signalPatterns.Remove(three);
        signalPatterns.Remove(two);

        var five = signalPatterns.First(x => x.Length == 5);
        dict.Add(5, five.ToCharArray());

        var six = signalPatterns.First(x => x.Length == 6 && dict[5].All(c => x.ToCharArray().Contains(c)));
        dict.Add(6, six.ToCharArray());

        signalPatterns.Remove(six);
        var zero = signalPatterns.First(x => x.Length == 6);
        dict.Add(0, zero.ToCharArray());

        return dict;
    }
}