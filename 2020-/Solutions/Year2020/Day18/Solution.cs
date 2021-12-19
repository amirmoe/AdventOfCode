using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2020.Day18
{
    internal class Day18 : ASolution
    {
        public Day18() : base(18, 2020, "Operation Order")
        {
            // DebugInput = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";
        }

        protected override string SolvePartOne()
        {
            var equations = Input.SplitByNewline();
            return Solve(equations).ToString(CultureInfo.InvariantCulture);
        }

        protected override string SolvePartTwo()
        {
            var equations = Input.SplitByNewline();
            return Solve(equations, true).ToString(CultureInfo.InvariantCulture);
        }

        private static double Solve(IEnumerable<string> equations, bool part2 = false)
        {
            var sum = (double) 0;
            foreach (var equation in equations)
            {
                var equationCopy = equation;
                while (equationCopy.Contains("("))
                {
                    var start = equationCopy.LastIndexOf('(');
                    var end = equationCopy.IndexOf(')', start);
                    var subEquation = equationCopy.Substring(start, end - start + 1);


                    var number = part2
                        ? Calculate2(subEquation.Substring(1, subEquation.Length - 2))
                        : Calculate(subEquation.Substring(1, subEquation.Length - 2));
                    equationCopy = equationCopy.Remove(start, subEquation.Length);
                    equationCopy = equationCopy.Insert(start, number.ToString(CultureInfo.InvariantCulture));
                }

                sum += part2 ? Calculate2(equationCopy) : Calculate(equationCopy);
            }

            return sum;
        }


        private static double Calculate(string equation)
        {
            equation = equation.Replace(" ", "");
            var sum = (double) 0;
            var regex = new Regex(@"\d+");
            var parts = new List<object>();

            foreach (Match c in regex.Matches(equation))
                if (c.Index == 0)
                {
                    sum = Convert.ToDouble(c.Value);
                }
                else
                {
                    parts.Add(equation[c.Index - 1].ToString());
                    parts.Add(Convert.ToDouble(c.Value));
                }

            while (parts.Any())
            {
                var operation = parts[0];
                var number = (double) parts[1];

                if ((string) operation == "+") sum += number;
                if ((string) operation == "*") sum *= number;

                parts.RemoveRange(0, 2);
            }

            return sum;
        }


        private static double Calculate2(string equation)
        {
            equation = equation.Replace(" ", "");
            var regex = new Regex(@"\d+");
            var parts = new List<object>();

            foreach (Match c in regex.Matches(equation))
                if (c.Index == 0)
                {
                    parts.Add(Convert.ToDouble(c.Value));
                }
                else
                {
                    parts.Add(equation[c.Index - 1].ToString());
                    parts.Add(Convert.ToDouble(c.Value));
                }

            while (parts.Contains("+"))
            {
                var additionIndex = parts.IndexOf("+");
                parts[additionIndex] = (double)
                                       parts[additionIndex - 1] + (double) parts[additionIndex + 1];
                parts.RemoveAt(additionIndex + 1);
                parts.RemoveAt(additionIndex - 1);
            }

            var sum = (double) parts[0];
            parts.RemoveAt(0);

            while (parts.Any())
            {
                var number = (double) parts[1];
                sum *= number;
                parts.RemoveRange(0, 2);
            }

            return sum;
        }
    }
}