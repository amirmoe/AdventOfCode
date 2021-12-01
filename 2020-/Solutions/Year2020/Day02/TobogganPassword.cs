using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020.Day02
{
    public class TobogganPassword
    {
        public TobogganPassword(string row)
        {
            var numbers = Regex.Matches(row, @"\d+");
            FirstNumber = Convert.ToInt32(numbers[0].Value);
            SecondNumber = Convert.ToInt32(numbers[1].Value);
            Letter = row.Split(" ")[1][0];
            Password = row.Split(":")[1].Trim();
        }

        public int FirstNumber { get; }
        public int SecondNumber { get; }
        public char Letter { get; }
        public string Password { get; }
    }
}