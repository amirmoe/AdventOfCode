using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2022.Day08;

internal class Day08 : ASolution
{
    public Day08() : base(08, 2022, "Treetop Tree House", true)
    {
    }
    
    private static readonly (int deltaY, int deltaX)[] NeighbourDeltas =
        { (1, 0), (0, 1), (-1, 0), (0, -1) };

    protected override string SolvePartOne()
    {
        // var lines = Input.SplitByNewline();
        // var width = lines.First().Length;
        // var height = lines.Length;
        //
        // var map = GetMap(lines);
        //
        //
        // var points = new List<(int y, int x)>();
        // foreach (var point in map.Keys)
        // {
        //     if (IsEdge(point, height, width))
        //     {
        //         points.Add(point);
        //         continue;
        //     }
        //     
        //
        //     foreach (var neighbourDelta in NeighbourDeltas)
        //     {
        //         var blocked = false;
        //
        //             for (var i = point.y + (neighbourDelta.deltaY); i >= 0 ; i--)
        //             {
        //                 for (var j = point.x + (neighbourDelta.deltaX); i >= 0 ; i--)
        //                 {
        //
        //                     var point2 = (i, j);
        //                 }  
        //             }               
        //         
        //             for (var i = point.y + (neighbourDelta.deltaY); i <= height ; i++)
        //             {
        //                 for (var j = point.x + (neighbourDelta.deltaX); i <= height ; i++)
        //                 {
        //                     var point2 = (i, j);
        //
        //                 }  
        //             }         
        //             
        //         
        //         
        //             
        //     }
        //     
        //    
        //     
        //     
        //   
        //
        //     var nope = false;
        //     for (var i = point.y-1; i >= 0; i--)
        //     {
        //         var compare = map[(i, point.x)];
        //         if (compare >= map[point])
        //             nope = true;
        //     }
        //     
        //     if (!nope)
        //         points.Add(point);
        //     
        //     
        // }
        //
        //
        //
        //Unsolved
        
        
        return "";
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
    
    private static Dictionary<(int y, int x), int> GetMap(IReadOnlyList<string> inputLines)
    {
        var map = new Dictionary<(int, int), int>();
        for (var i = 0; i < inputLines.Count; i++)
        for (var j = 0; j < inputLines[0].Length; j++)
            map.Add((i, j), int.Parse(inputLines[i][j].ToString()));
        return map;
    }

    private static bool IsEdge((int y, int x) point, int height, int width)
    {
        return point.x == 0 || point.y == 0 || point.x == width - 1 || point.y == height - 1;
    }
}
