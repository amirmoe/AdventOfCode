using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2020.Day14
{
    internal class Day14 : ASolution
    {
        public Day14() : base(14, 2020, "Docking Data")
        {
            // DebugInput = "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X\nmem[8] = 11\nmem[7] = 101\nmem[8] = 0";

            // DebugInput =
            //     "mask = 000000000000000000000000000000X1001X\nmem[42] = 100\nmask = 00000000000000000000000000000000X0XX\nmem[26] = 1";
        }

        protected override string SolvePartOne()
        {
            var lines = Input.SplitByNewline();

            var memory = new Dictionary<int, double>();
            var mask = "";
            foreach (var line in lines)
            {
                var match = Regex.Match(line, @"(?<=mask = ).*");
                if (match.Success)
                {
                    mask = match.Value;
                }
                else
                {
                    var mem = Convert.ToInt32(Regex.Match(line, @"(?<=\[).*(?=\])").Value);
                    var value = Convert.ToInt32(Regex.Match(line, @"(?<=] = ).*").Value);
                    if (memory.ContainsKey(mem))
                        memory[mem] = ApplyMask(value, mask);
                    else
                        memory.Add(mem, ApplyMask(value, mask));
                }
            }

            return memory.Sum(mem => mem.Value).ToString(CultureInfo.InvariantCulture);
        }

        protected override string SolvePartTwo()
        {
            var lines = Input.SplitByNewline();

            var memory = new Dictionary<double, double>();
            var mask = "";
            foreach (var line in lines)
            {
                var match = Regex.Match(line, @"(?<=mask = ).*");
                if (match.Success)
                {
                    mask = match.Value;
                }
                else
                {
                    var mem = Convert.ToInt32(Regex.Match(line, @"(?<=\[).*(?=\])").Value);
                    var value = Convert.ToInt32(Regex.Match(line, @"(?<=] = ).*").Value);
                    var memories = ApplyMask2(mem, mask);

                    foreach (var convertedMemory in memories)
                        if (memory.ContainsKey(convertedMemory))
                            memory[convertedMemory] = value;
                        else
                            memory.Add(convertedMemory, value);
                }
            }

            return memory.Sum(mem => mem.Value).ToString(CultureInfo.InvariantCulture);
        }

        private static double ApplyMask(int value, string mask)
        {
            var binaryValue = Convert.ToString(value, 2).PadLeft(mask.Length, '0');
            var newValue = "";

            for (var i = 0; i < binaryValue.Length; i++)
                if (mask[i] == 'X')
                    newValue += binaryValue[i];
                else
                    newValue += mask[i];

            return Convert.ToInt64(newValue, 2);
        }

        private static IEnumerable<long> ApplyMask2(int value, string mask)
        {
            var binaryValue = Convert.ToString(value, 2).PadLeft(mask.Length, '0');
            var newValue = "";

            for (var i = 0; i < binaryValue.Length; i++)
                switch (mask[i])
                {
                    case 'X':
                        newValue += 'X';
                        break;
                    case '1':
                        newValue += '1';
                        break;
                    case '0':
                        newValue += binaryValue[i];
                        break;
                }

            var addresses = GetAddresses(newValue);
            return addresses.Select(x => Convert.ToInt64(x, 2));
        }

        private static IEnumerable<string> GetAddresses(string floatingMask)
        {
            if (floatingMask.Length == 0) return new[] {""};

            var firstChar = floatingMask[0];
            var rest = floatingMask.Substring(1);
            var addresses = GetAddresses(rest).ToList();
            if (firstChar == 'X')
            {
                var zeros = addresses.Select(x => '0' + x);
                var ones = addresses.Select(x => '1' + x);

                return zeros.Concat(ones);
            }

            return addresses.Select(x => firstChar + x);
        }
    }
}