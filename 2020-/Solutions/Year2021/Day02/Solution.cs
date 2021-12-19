using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2021.Day02;

internal class Day02 : ASolution
{
    public Day02() : base(02, 2021, "Dive!")
    {
    }

    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();

        var horizontal = 0;
        var depth = 0;
        foreach (var line in lines)
        {
            var instructions = line.Split(' ');
            var direction = instructions[0];
            var units = int.Parse(instructions[1]);

            switch (direction)
            {
                case "forward":
                    horizontal += units;
                    break;
                case "down":
                    depth += units;
                    break;
                case "up":
                    depth -= units;
                    break;
            }
        }

        return (horizontal * depth).ToString();
    }

    protected override string SolvePartTwo()
    {
        var lines = Input.SplitByNewline();

        var horizontal = 0;
        var depth = 0;
        var aim = 0;
        foreach (var line in lines)
        {
            var instructions = line.Split(' ');
            var direction = instructions[0];
            var units = int.Parse(instructions[1]);

            switch (direction)
            {
                case "forward":
                    horizontal += units;
                    depth += aim * units;
                    break;
                case "down":
                    aim += units;
                    break;
                case "up":
                    aim -= units;
                    break;
            }
        }

        return (horizontal * depth).ToString();
    }
}