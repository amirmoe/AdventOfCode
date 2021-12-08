using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021.Day06
{
    internal class Day06 : ASolution
    {
        public Day06() : base(06, 2021, "Lanternfish")
        {
        }

        protected override string SolvePartOne()
        {
            var fishList = Input.ToIntArray().ToList();

            for (var i = 0; i < 80; i++)
            {
                var newFishList = new List<int>();
                var newBornFish = new List<int>();
                foreach (var fish in fishList)
                {
                    var newFish = fish - 1;
                    if (newFish < 0)
                    {
                        newFishList.Add(6);
                        newBornFish.Add(8);
                    }
                    else
                    {
                        newFishList.Add(newFish);
                    }
                }

                fishList = newFishList.Concat(newBornFish).ToList();
            }


            return fishList.Count.ToString();
        }

        protected override string SolvePartTwo()
        {
            var fishList = Input.ToIntArray().ToList();

            var dict = new Dictionary<int, double>();
            for (var i = 0; i < 9; i++) dict.Add(i, 0);

            foreach (var fish in fishList) dict[fish]++;

            for (var i = 0; i < 256; i++)
            {
                var zeros = dict[0];
                dict[0] = 0;
                for (var j = 1; j < 9; j++) dict[j - 1] = dict[j];
                dict[6] += zeros;
                dict[8] = zeros;
            }

            return dict.Values.Sum().ToString(CultureInfo.InvariantCulture);
        }
    }
}