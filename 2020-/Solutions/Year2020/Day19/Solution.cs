using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020.Day19
{

    class Day19 : ASolution
    {

        public Day19() : base(19, 2020, "Monster Messages")
        {
            // DebugInput = "0: 4 1 5\n1: 2 3 | 3 2\n2: 4 4 | 5 5\n3: 4 5 | 5 4\n4: \"a\"\n5: \"b\"\n\nababbb\nbababa\nabbbab\naaabbb\naaaabbb";
            // DebugInput = "0: 1 2\n1: \"a\"\n2: 1 3 | 3 1\n3: \"b\"";
        }

        protected override string SolvePartOne()
        {
            // var sections = Input.SplitByNewline(false,"\n\n");
            // var dictionary = ParseDictionary(sections[0].SplitByNewline());
            // dictionary = TranslateDictionary(dictionary, 0);
            // var count = sections[1].SplitByNewline().Count(line => dictionary[0].Contains(line));
            // return count.ToString();
            return "";
        }

        protected override string SolvePartTwo()
        {
            // Takes time
            // var sections = Input.SplitByNewline(false,"\n\n");
            // var dictionary = ParseDictionary(sections[0].SplitByNewline());
            // dictionary = TranslateDictionary(dictionary, 8);
            // dictionary = TranslateDictionary(dictionary, 11);
            // dictionary = SolvePart2(dictionary, sections[1].SplitByNewline().Max(x => x.Length));
            // dictionary = TranslateDictionary(dictionary, 0);
            //
            // var count = sections[1].SplitByNewline().Count(line => dictionary[0].Contains(line));
            // return count.ToString();
            
            
            
            
            var sections = Input.SplitByNewline(false,"\n\n");
            var dictionary = ParseDictionary(sections[0].SplitByNewline());
            
            for (var i = 1; i <= 2; i++)
            {
                dictionary[8].Add(string.Join(" ", Enumerable.Repeat("42", i+1)));
                var str = string.Join(" ", Enumerable.Repeat("42", i)) +" "+ string.Join(" ", Enumerable.Repeat("31", i));
                dictionary[11].Add($"{42} {str} {31}");
            }
            
            
            dictionary = TranslateDictionary(dictionary, 0);
            
            var count = sections[1].SplitByNewline().Count(line => dictionary[0].Contains(line));
            return count.ToString();
        }

        private static Dictionary<int, List<string>> ParseDictionary(IEnumerable<string> lines)
        {
            var dictionary = new Dictionary<int, List<string>>();
            foreach (var line in lines)
            {
                var kv = line.Split(":");
                var values = kv[1].Split("|");
                dictionary.Add(Convert.ToInt32(kv[0]), values.Select(x =>
                {
                    x = x.Replace("\"", "");
                    return x.Trim();
                }).ToList());
            }

            return dictionary;
        }

        private static Dictionary<int, List<string>> TranslateDictionary(Dictionary<int, List<string>> dictionary, int key)
        {
            if (IsRuleTranslated(dictionary, key))
                return dictionary;

            foreach (var list in dictionary[key])
            {
                var words = new List<string>{""};
                var rules = list.Split(" ");
                foreach (var rule in rules)
                {
                    var innerRuleKey = Convert.ToInt32(rule);
                    var solutions = TranslateDictionary(dictionary, innerRuleKey);
                    var newWords = new List<string>();
                    for (var i = 0; i < words.Count; i++)
                    {
                        for (var j = 0; j < solutions[innerRuleKey].Count; j++)
                        {
                            newWords.Add(words[i]+solutions[innerRuleKey][j]);
                        }
                    }
                    words = newWords;
                }

                if (IsRuleTranslated(dictionary, key))
                {
                    dictionary[key].AddRange(words);
                } else
                {
                    dictionary[key] = words;
                }
            }

            return dictionary;
        }

        private static bool IsRuleTranslated(IReadOnlyDictionary<int, List<string>> dictionary, int key)
        {
            return dictionary[key][0].Contains("a") || dictionary[key][0].Contains("b");
        }

        private static Dictionary<int, List<string>> SolvePart2(Dictionary<int, List<string>> dictionary, int maxWordSize)
        {
            var newWords = new List<string>();
            var shortestWordSize = dictionary[8].Min(x => x.Length);

            for (var i = 1; i < (int)Math.Ceiling((double) maxWordSize / shortestWordSize); i++)
            {
                foreach (var word in dictionary[8])
                {
                    newWords.Add(word + string.Concat(Enumerable.Repeat(word, i)));
                }
            }

            dictionary[8] = newWords;


            newWords = new List<string> ();
            newWords.AddRange(dictionary[11]);

            for (var i = 0; i < dictionary[42].Count; i++)
            {
                for (var j = 0; j < dictionary[31].Count; j++)
                {
                    for (var k = 1; k < (int)Math.Ceiling((double) maxWordSize / (dictionary[42][i]+dictionary[31][j]).Length); k++)
                    {
                        newWords.Add($"{dictionary[42][i]}{string.Concat(Enumerable.Repeat(dictionary[42][i],k))}{string.Concat(Enumerable.Repeat(dictionary[31][j],k))}{dictionary[31][j]}");
                    }
                }
            }

            dictionary[11] = newWords;

            return dictionary;
        }
    }
}
