using System;
using AdventOfCode.Infrastructure;
using AdventOfCode.Infrastructure.Helpers;
using AdventOfCode.Solutions;

namespace AdventOfCode
{
    public static class Program
    {
        private static readonly SolutionCollector Solutions = new SolutionCollector();

        private static void Main(string[] args)
        {
            foreach (var solution in Solutions)
            {
                Console.WriteLine(FormatHelper.FunctionFormat(solution));
            }
        }
    }
}
