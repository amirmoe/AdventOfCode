using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2020.Day10
{

    class Day10 : ASolution
    {

        public Day10() : base(10, 2020, "Adapter Array")
        {
            // DebugInput = "16\n10\n15\n5\n1\n11\n7\n19\n6\n12\n4\n";
            // DebugInput = "28\n33\n18\n42\n31\n14\n46\n20\n48\n47\n24\n23\n49\n45\n19\n38\n39\n11\n1\n32\n25\n35\n8\n17\n7\n9\n4\n2\n34\n10\n3";
        }

        protected override string SolvePartOne()
        {
            var jolts = Input.ToIntArray("\n").ToList();
            jolts.Add(0);
            jolts.Add(jolts.Max() + 3);
            jolts.Sort();

            var one = 0;
            var three = 0;
            for (var i = 0; i < jolts.Count-1; i++)
            {
                var difference = jolts[i + 1] - jolts[i];
                switch (difference)
                {
                    case 3:
                        three++;
                        break;
                    case 1:
                        one++;
                        break;
                }
            }
            
            return (one*three).ToString();
        }

        protected override string SolvePartTwo()
        {
            var jolts = Input.ToIntArray("\n").ToList();
            jolts.Add(0);
            jolts.Add(jolts.Max() + 3);
            jolts.Sort();
            
            var dictionary = new Dictionary<int, double>();
            return GetNumberOfArrangements(0, jolts, dictionary).ToString(CultureInfo.InvariantCulture);
        }

        private static double GetNumberOfArrangements(int position, IReadOnlyList<int> jolts, IDictionary<int, double> dictionary)
        {
            if (position == jolts.Count - 1)
                return 1;

            if (dictionary.ContainsKey(position))
                return dictionary[position];
            
            var total = (double)0;
            for (var i = position+1; i < jolts.Count; i++)
            {
                if (jolts[i] - jolts[position] <= 3)
                {
                    total += GetNumberOfArrangements(i, jolts, dictionary);
                }
            }

            dictionary.Add(position, total);
            return total;
        }
    }
}
