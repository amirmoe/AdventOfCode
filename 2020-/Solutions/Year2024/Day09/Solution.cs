using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2024.Day09;

internal class Day09 : ASolution
{
    public Day09() : base(09, 2024, "Disk Fragmenter")
    {
    }

    protected override string SolvePartOne()
    {
        var freeSpaceIndexes = new Queue<int>();
        var files = new Stack<(int index, string value)>();
        var disk = new List<string>();

        var count = 0;
        var isFreeSpace = false;

        foreach (var block in Input)
        {
            var multiplier = int.Parse(block.ToString());

            Enumerable.Range(0, multiplier).ToList().ForEach(_ =>
            {
                disk.Add(isFreeSpace ? "." : count.ToString());
                if (isFreeSpace) 
                    freeSpaceIndexes.Enqueue(disk.Count - 1);
                else 
                    files.Push((disk.Count - 1, count.ToString()));
            });

            if (isFreeSpace) count++;
            isFreeSpace = !isFreeSpace;
        }


        while (freeSpaceIndexes.Any())
        {
            var emptyIndex = freeSpaceIndexes.Dequeue();

            var file = files.Pop();
            if (emptyIndex > file.index)
                break;

            disk[emptyIndex] = file.value;
            disk[file.index] = ".";
        }

        return disk.Select((c, i) => c == "." ? 0 : long.Parse(c) * i).Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        var freeSpaceChunks = new List<(int index, int length)>();
        var fileChunks = new Stack<(int index, int length, string value)>();
        var disk = new List<string>();

        var count = 0;
        var isFreeSpace = false;

        foreach (var block in Input)
        {
            var multiplier = int.Parse(block.ToString());
            
            if (isFreeSpace)
                freeSpaceChunks.Add((disk.Count, multiplier));
            else
                fileChunks.Push((disk.Count, multiplier, count.ToString()));
            
            Enumerable.Range(0, multiplier).ToList().ForEach(_ =>
            {
                disk.Add(isFreeSpace ? "." : count.ToString());
            });
            
            if (isFreeSpace) count++;
            isFreeSpace = !isFreeSpace;
        }

        while (fileChunks.Any())
        {
            var fileChunk = fileChunks.Pop();

            var freeSpaceIndex = freeSpaceChunks.FindIndex(x => x.index < fileChunk.index && x.length >= fileChunk.length);
            if (freeSpaceIndex == -1)
                continue;

            var freeSpaceChunk = freeSpaceChunks[freeSpaceIndex];

            for (var i = 0; i < fileChunk.length; i++)
            {
                disk[freeSpaceChunk.index + i] = fileChunk.value;
                disk[fileChunk.index + i] = ".";
            }

            var remainingLength = freeSpaceChunk.length - fileChunk.length;
            if (remainingLength == 0)
                freeSpaceChunks.RemoveAt(freeSpaceIndex);
            else
                freeSpaceChunks[freeSpaceIndex] = (freeSpaceChunk.index + fileChunk.length, remainingLength);
        }
        

        return disk.Select((c, i) => c == "." ? 0 : long.Parse(c) * i).Sum().ToString();
    }
}