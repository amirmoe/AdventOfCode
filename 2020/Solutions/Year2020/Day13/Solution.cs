using System;
using System.Globalization;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020.Day13
{
    internal class Day13 : ASolution
    {
        public Day13() : base(13, 2020, "Shuttle Search")
        {
            // DebugInput = "939\n7,13,x,x,59,x,31,19";
            // DebugInput = "939\n1789,37,47,1889";
        }

        protected override string SolvePartOne()
        {
            var lines = Input.SplitByNewline();
            var time = Convert.ToInt32(lines[0]);
            var startTime = time;
            var buses = lines[1].Split(',').Where(x => x != "x").Select(x => Convert.ToInt32(x)).ToList();

            var busId = 0;
            while (true)
            {
                foreach (var bus in buses.Where(bus => time % bus == 0))
                {
                    busId = bus;
                    break;
                }

                if (busId != 0)
                    break;
                time++;
            }

            return ((time - startTime) * busId).ToString();
        }

        protected override string SolvePartTwo()
        {
            var busesInput = Input.SplitByNewline()[1];
            var buses = busesInput.Split(',').Select(x => x == "x" ? "1" : x)
                .Select(x => Convert.ToInt32(x)).ToList();

            var time = (double) 0;
            var stepSize = (double) buses.First();
            for (var i = 1; i < buses.Count; i++)
            {
                while (Math.Abs((time + i) % buses[i]) > 0.0001) time += stepSize;

                stepSize *= buses[i];
            }

            return time.ToString(CultureInfo.InvariantCulture);
        }
    }
}