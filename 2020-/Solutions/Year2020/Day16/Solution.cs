using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2020.Day16
{
    internal class Day16 : ASolution
    {
        public Day16() : base(16, 2020, "Ticket Translation")
        {
            // DebugInput =
            //     "class: 1-3 or 5-7\nrow: 6-11 or 33-44\nseat: 13-40 or 45-50\n\nyour ticket:\n7,1,14\n\nnearby tickets:\n7,3,47\n40,4,50\n55,2,20\n38,6,12";

            // DebugInput =
            //     "class: 0-1 or 4-19\nrow: 0-5 or 8-19\nseat: 0-13 or 16-19\n\nyour ticket:\n11,12,13\n\nnearby tickets:\n3,9,18\n15,1,5\n5,14,9";
        }

        protected override string SolvePartOne()
        {
            var input = Input.SplitByNewline(false, "\n\n");

            var rules = GetRules(input[0]);
            var tickets = input[2].SplitByNewline().Skip(1)
                .Select(x => x.Split(",")
                    .Select(y => Convert.ToInt32(y)));

            var errors = new List<int>();
            foreach (var ticket in tickets)
                errors.AddRange(ticket.Where(number => !rules.Any(rule =>
                    rule.Intervals.Any(interval => interval.Min <= number && interval.Max >= number))));

            return errors.Sum().ToString();
        }

        protected override string SolvePartTwo()
        {
            var input = Input.SplitByNewline(false, "\n\n");

            var rules = GetRules(input[0]);
            var tickets = input[2].SplitByNewline().Skip(1).Concat(input[1].SplitByNewline().Skip(1))
                .Select(x => x.Split(",")
                    .Select(y => Convert.ToInt32(y)).ToList()).ToList();

            var correctTickets = tickets.Where(ticket => ticket.All(number =>
                    rules.Any(rule =>
                        rule.Intervals.Any(interval => interval.Min <= number && interval.Max >= number))))
                .ToList();


            var dictionary = new Dictionary<int, List<string>>();
            var numberOfColumns = correctTickets[0].Count;

            for (var i = 0; i < numberOfColumns; i++)
                foreach (var rule in rules)
                {
                    if (!correctTickets.All(ticket =>
                        rule.Intervals.Any(interval => interval.Min <= ticket[i] && interval.Max >= ticket[i])))
                        continue;
                    if (dictionary.ContainsKey(i))
                        dictionary[i].Add(rule.Name);
                    else
                        dictionary.Add(i, new List<string> {rule.Name});
                }

            var finalColumns = new Dictionary<string, int>();

            var iteration = 0;
            while (finalColumns.Count < numberOfColumns)
            {
                var k = iteration % numberOfColumns;

                if (dictionary[k].Count == 1)
                {
                    var columnName = dictionary[k].First();
                    finalColumns.Add(columnName, k);

                    foreach (var (_, value) in dictionary)
                        if (value.Contains(columnName))
                            value.Remove(columnName);
                }

                iteration++;
            }

            var departureRules = rules.Where(x => x.Name.StartsWith("departure"));
            var indices = departureRules.Select(x => finalColumns[x.Name]);

            var products = indices.Select(index => (double) tickets.Last()[index]).ToList();
            return products.Aggregate((double) 1, (x, y) => x * y).ToString(CultureInfo.InvariantCulture);
            ;
        }

        private static List<Rule> GetRules(string ruleSection)
        {
            var ruleStrings = ruleSection.SplitByNewline();

            return (from rule in ruleStrings
                select rule.Split(": ")
                into ruleNameSplit
                let intervals = ruleNameSplit[1].Split(" or ")
                let ruleIntervals = intervals.Select(interval => interval.Split("-")
                        .Select(x => Convert.ToInt32(x))
                        .ToList())
                    .Select(boundaries => new RuleInterval {Min = boundaries[0], Max = boundaries[1]})
                    .ToList()
                select new Rule {Name = ruleNameSplit[0], Intervals = ruleIntervals}).ToList();
        }
    }
}