namespace AdventOfCode.Solutions.Year2021.Day01
{
    internal class Day01 : ASolution
    {
        public Day01() : base(01, 2021, "Sonar Sweep")
        {
            // DebugInput = "199\n200\n208\n210\n200\n207\n240\n269\n260\n263";
        }

        protected override string SolvePartOne()
        {
            var list = Input.ToIntArray("\n");
            var increased = 0;
            for (var i = 1; i < list.Length; i++)
                if (list[i] > list[i - 1])
                    increased++;
            return increased.ToString();
        }

        protected override string SolvePartTwo()
        {
            var list = Input.ToIntArray("\n");
            var increased = 0;
            for (var i = 1; i < list.Length - 2; i++)
            {
                var sum1 = list[i - 1] + list[i] + list[i + 1];
                var sum2 = list[i] + list[i + 1] + list[i + 2];

                if (sum2 > sum1)
                    increased++;
            }

            return increased.ToString();
        }
    }
}