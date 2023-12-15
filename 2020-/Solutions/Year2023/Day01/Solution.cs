using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2023.Day01;

internal class Day01 : ASolution
{
    private static readonly Dictionary<string, string> NumberMapping = new
        ()
        {
            { "one", "1" },
            { "two", "2" },
            { "three", "3" },
            { "four", "4" },
            { "five", "5" },
            { "six", "6" },
            { "seven", "7" },
            { "eight", "8" },
            { "nine", "9" }
        };


    private static string[] _numbers = Enumerable.Range(1, 9).Select(x => x.ToString()).ToArray();

    public Day01() : base(01, 2023, "Trebuchet?!")
    {
    }


    protected override string SolvePartOne()
    {
        var expression = string.Join("|", _numbers);
        return Solve(Input, expression);
    }

    protected override string SolvePartTwo()
    {
        var expression = string.Join("|", _numbers.Concat(NumberMapping.Keys));
        return Solve(Input, expression);
    }

    private static string Solve(string input, string expression)
    {
        return input.SplitByNewline().Select(line => GetValue(line, expression)).Sum().ToString();
    }

    private static int GetValue(string line, string expression)
    {
        var firstMatchedValue = Regex.Match(line, expression).Value;
        var lastMatchedValue = Regex.Match(line, expression, RegexOptions.RightToLeft).Value;
        var firstConverted = NumberMapping.ContainsKey(firstMatchedValue) ? NumberMapping[firstMatchedValue] : firstMatchedValue;
        var secondConverted = NumberMapping.ContainsKey(lastMatchedValue) ? NumberMapping[lastMatchedValue] : lastMatchedValue;
        return int.Parse(firstConverted + secondConverted);
    }
}