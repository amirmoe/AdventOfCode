using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2022.Day07;

internal class Day07 : ASolution
{
    public Day07() : base(07, 2022, "No Space Left On Device",  true)
    {
    }

    protected override string SolvePartOne()
    {

        var rows = new Queue<string>(Input.SplitByNewline().Skip(1));

        while (rows.Any())
        {
            var row = rows.Dequeue();
            
            if (row.Contains("$ ls"))
            {
                var filesInFolder = new List<string>();
                while (rows.Any() || !rows.Peek().Contains('$'))
                {
                    filesInFolder.Add(rows.Dequeue());
                }
            }



        }
        
        
        foreach (var row in rows)
        {
            if (row.Contains("$ ls"))
            {
                
            }
            
            
        }
        
        
        return "";
    }

    protected override string SolvePartTwo()
    {

        return "";
    }
}