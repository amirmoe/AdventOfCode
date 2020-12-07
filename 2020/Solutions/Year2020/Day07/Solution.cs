using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020.Day07
{

    class Day07 : ASolution
    {

        public Day07() : base(07, 2020, "Handy Haversacks")
        {
            // DebugInput =
            //     "light red bags contain 1 bright white bag, 2 muted yellow bags.\ndark orange bags contain 3 bright white bags, 4 muted yellow bags.\nbright white bags contain 1 shiny gold bag.\nmuted yellow bags contain 2 shiny gold bags, 9 faded blue bags.\nshiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.\ndark olive bags contain 3 faded blue bags, 4 dotted black bags.\nvibrant plum bags contain 5 faded blue bags, 6 dotted black bags.\nfaded blue bags contain no other bags.\ndotted black bags contain no other bags.";
        }
        
        

        protected override string SolvePartOne()
        {
            var dictionary = GetDictionary(Input.SplitByNewline());
            return dictionary.Keys.Count(key => ContainsBag(key, "shiny gold", dictionary)).ToString();

        }

        protected override string SolvePartTwo()
        {
            var dictionary = GetDictionary(Input.SplitByNewline());
            return CountBags("shiny gold", dictionary).ToString();
        }

        private static bool ContainsBag(string color, string search, IReadOnlyDictionary<string, List<Bag>> dictionary)
        {
            if (dictionary[color].Any(x => x.Color == search))
                return true;

            return dictionary[color]
                .Select(x => x.Color)
                .Any(containedColor => ContainsBag(containedColor,
                    search,
                    dictionary));
        }
        
        private static int CountBags(string color, IReadOnlyDictionary<string, List<Bag>> dictionary)
        {
            return dictionary[color].Sum(bag => bag.Count + bag.Count * CountBags(bag.Color, dictionary));
        }

        private static Dictionary<string, List<Bag>> GetDictionary(IEnumerable<string> rows)
        {
            var dictionary = new Dictionary<string, List<Bag>>();
            
            foreach (var row in rows)
            {
                var bag = Regex.Match(row, @"(\w+ \w+) bags contain (.*)");
                var contents = Regex.Matches(bag.Groups[2].Value, @"(\d+) (\w+ \w+) bags?");
                
                dictionary.Add(bag.Groups[1]
                        .Value,
                    contents.Select(x => new Bag
                        {
                            Count = Convert.ToInt32(x.Groups[1]
                                .Value),
                            Color = x.Groups[2]
                                .Value
                        })
                        .ToList());
            }

            return dictionary;
        }
    }
}
