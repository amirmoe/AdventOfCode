using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020.Day09
{

    class Day09 : ASolution
    {

        public Day09() : base(09, 2020, "Encoding Error")
        {
            // DebugInput = "35\n20\n15\n25\n47\n40\n62\n55\n65\n95\n102\n117\n150\n182\n127\n219\n299\n277\n309\n576";
        }

        protected override string SolvePartOne()
        {
            var numbers = Input.ToIntArray("\n");
            return FindFirstNumberWhichIsNotASum(numbers).ToString();
        }

        protected override string SolvePartTwo()
        {
            var numbers = Input.ToIntArray("\n");
            var number = FindFirstNumberWhichIsNotASum(numbers);
            var set = FindContiguous(numbers, number).ToList();
            return (set.Min() + set.Max()).ToString();
        }

        private static int FindFirstNumberWhichIsNotASum(IReadOnlyList<int> numbers)
        {
            const int preamble = 25;

            for (var i = preamble; i < numbers.Count; i++)
            {
                if (!HasPairWithSum(numbers.Skip(i - preamble).Take(preamble), numbers[i]))
                    return numbers[i];
            }
            
            return 0;
        }

        private IEnumerable<int> FindContiguous(IReadOnlyList<int> numbers, int sum)
        {
            for (var i = 0; i < numbers.Count; i++)
            {
                var set = new List<int>();
                for (var j = i; j < numbers.Count; j++)
                {
                    set.Add(numbers[j]);
                    
                    if (set.Count < 2) continue;
                    var setSum = set.Sum();
                    if (setSum < sum)
                        continue;
                    if (setSum > sum)
                        break;

                    return set;
                }
            }

            return null;
        }
        
        private static bool HasPairWithSum(IEnumerable<int> numbers, int sum)
        {
            var complements = new HashSet<int>();
            foreach (var number in numbers)
            {
                if (complements.Contains(number))
                    return true;
                complements.Add(sum - number);
            }

            return false;
        }
    }
}
