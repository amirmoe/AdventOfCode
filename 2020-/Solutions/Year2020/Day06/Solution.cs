using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2020.Day06
{
    internal class Day06 : ASolution
    {
        public Day06() : base(06, 2020, "Custom Customs")
        {
        }

        protected override string SolvePartOne()
        {
            var groups = Input.SplitByNewline(false, "\n\n");

            var allAnswers = groups.Select(g => g.Replace("\n",
                    ""))
                .Select(answers => answers.Select(x => x)
                    .Distinct()
                    .Count())
                .ToList();


            return allAnswers.Sum().ToString();
        }

        protected override string SolvePartTwo()
        {
            var groups = Input.SplitByNewline(false, "\n\n");

            var allAnswers = new List<int>();

            foreach (var group in groups)
            {
                var answers = group.SplitByNewline();
                var listOfLists = answers.Select(answer => answer.ToCharArray().ToList()).ToList();
                var intersection =
                    listOfLists.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
                allAnswers.Add(intersection.Count);
            }

            return allAnswers.Sum().ToString();
        }
    }
}