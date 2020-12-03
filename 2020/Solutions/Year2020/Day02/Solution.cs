using System.Linq;

namespace AdventOfCode.Solutions.Year2020.Day02
{
    internal class Day02 : ASolution
    {
        public Day02() : base(02, 2020, "Password Philosophy")
        {
            // DebugInput = "1-3 a: abcde\n1-3 b: cdefg\n2-9 c: ccccccccc";
        }

        protected override string SolvePartOne()
        {
            var rows = Input.SplitByNewline();

            var count = 0;
            foreach (var row in rows)
            {
                var toboggan = new TobogganPassword(row);

                var appearances = toboggan.Password.Count(x => x == toboggan.Letter);
                if (toboggan.FirstNumber <= appearances && appearances <= toboggan.SecondNumber)
                    count++;
            }

            return count.ToString();
        }

        protected override string SolvePartTwo()
        {
            var rows = Input.SplitByNewline();

            var count = 0;
            foreach (var row in rows)
            {
                var toboggan = new TobogganPassword(row);

                var firstLetterIs = toboggan.Password[toboggan.FirstNumber - 1] == toboggan.Letter;
                var secondLetterIs = toboggan.Password[toboggan.SecondNumber - 1] == toboggan.Letter;

                if (firstLetterIs && !secondLetterIs || !firstLetterIs && secondLetterIs)
                    count++;
            }

            return count.ToString();
        }
    }
}