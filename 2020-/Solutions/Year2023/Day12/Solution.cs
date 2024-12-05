using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2023.Day12;

internal class Day12 : ASolution
{
    public Day12() : base(12, 2023, "Hot Springs", true)
    {
    }

    protected override string SolvePartOne()
    {
        var s = Input.SplitByNewline().Select(line =>
        {
            var parts = line.Split(" ");
            var formatOne = parts.First();
            var formatOneChars = formatOne.ToCharArray().Select(x => x.ToString());
            
            
            // Create permutations av formatOne
            
            //check matches.
            
            var matches = new Regex(@"(.)\1*").Matches(formatOne).Select(x => x.Value.Length);
            var formatTwo = parts.Last().Split(",").Select(int.Parse);

            

            
            
            
            return 1;
        });
        
        return null;
    }
    
    protected override string SolvePartTwo()
    {
        return null;
    }
    
    
}