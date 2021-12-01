using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2020.Day16
{
    public class Rule
    {
        public string Name { get; set; }
        public IEnumerable<RuleInterval> Intervals { get; set; } = new List<RuleInterval>();
    }

    public class RuleInterval
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }
}