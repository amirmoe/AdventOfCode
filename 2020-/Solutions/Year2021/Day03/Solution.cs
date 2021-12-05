using System;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021.Day03
{
    internal class Day03 : ASolution
    {
        public Day03() : base(03, 2021, "Binary Diagnostic")
        {
        }

        protected override string SolvePartOne()
        {
            var lines = Input.SplitByNewline();

            var sb = new StringBuilder();
            for (var i = 0; i < lines[0].Length; i++)
            {
                var zeros = 0;
                var ones = 0;
                foreach (var t in lines)
                    if (t[i] == '0')
                        zeros++;
                    else
                        ones++;

                sb.Append(zeros > ones ? "0" : "1");
            }

            var gammaString = sb.ToString();
            var gamma = Convert.ToInt32(gammaString, 2);
            var notGamma = Convert.ToString(~gamma, 2);
            var epsilonString = notGamma[^gammaString.Length..];
            var epsilon = Convert.ToInt32(epsilonString, 2);

            return (gamma * epsilon).ToString();
        }

        protected override string SolvePartTwo()
        {
            var lines = Input.SplitByNewline();

            var possibleOxygens = lines.ToList();
            for (var i = 0; i < lines[0].Length; i++)
            {
                var zeros = 0;
                var ones = 0;
                foreach (var t in possibleOxygens)
                    if (t[i] == '0')
                        zeros++;
                    else
                        ones++;


                possibleOxygens = possibleOxygens.Where(x => x[i] == (zeros > ones ? '0' : '1')).ToList();

                if (possibleOxygens.Count == 1)
                    break;
            }


            var possibleCo2 = lines.ToList();
            for (var i = 0; i < lines[0].Length; i++)
            {
                var zeros = 0;
                var ones = 0;
                foreach (var t in possibleCo2)
                    if (t[i] == '0')
                        zeros++;
                    else
                        ones++;


                possibleCo2 = possibleCo2.Where(x => x[i] == (ones >= zeros ? '0' : '1')).ToList();

                if (possibleCo2.Count == 1)
                    break;
            }

            var oxygen = Convert.ToInt32(possibleOxygens.First(), 2);
            var co2 = Convert.ToInt32(possibleCo2.First(), 2);
            return (oxygen * co2).ToString();
        }
    }
}