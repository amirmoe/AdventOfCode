using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2020.Day01
{

    class Day01 : ASolution
    {

        public Day01() : base(01, 2020, "Report Repair")
        {
            // DebugInput = "1721\n979\n366\n299\n675\n1456";
        }

        protected override string SolvePartOne()
        {
            var list = Input.ToIntArray("\n");

            for (var i = 0; i < list.Length; i++)
            {
                for (var j = i+1; j < list.Length; j++)
                {
                    if (list[i] + list[j] == 2020)
                    {
                        return (list[i] * list[j]).ToString();
                    }
                }
            }
            
            return null;
        }

        protected override string SolvePartTwo()
        {
            var list = Input.ToIntArray("\n");

            for (var i = 0; i < list.Length; i++)
            {
                for (var j = i+1; j < list.Length; j++)
                {
                    for (var k = 0; k < j+1; k++)
                    {
                        if (list[i] + list[j] + list[k] == 2020)
                        {
                            return (list[i] * list[j] * list[k]).ToString();
                        }
                    }
                }
            }
            
            return null;        }
    }
}
