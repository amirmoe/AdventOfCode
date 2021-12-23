using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Utilities.Extensions;

namespace AdventOfCode.Solutions.Year2021.Day16;

internal class Day16 : ASolution
{
    public Day16() : base(16, 2021, "Packet Decoder")
    {
    }

    protected override string SolvePartOne()
    {
        return Process(GetQueueFromInput(Input), new List<long>()).versions.Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        return Process(GetQueueFromInput(Input), new List<long>()).result.ToString();
    }

    private static Queue<string> GetQueueFromInput(string input)
    {
        var queue = new Queue<string>();
        var binaryString = ToBinaryString(input);
        binaryString.ToCharArray().ToList().ForEach(x => queue.Enqueue(x.ToString()));
        return queue;
    }

    private static (List<long> versions, long result) Process(Queue<string> queue, List<long> versions)
    {
        var version = GetNumberFromQueue(queue, 3);
        var typeId = GetNumberFromQueue(queue, 3);
        versions.Add(version);

        if (typeId == 4) return (versions, GetLiteralNumber(queue));

        var length = queue.Dequeue();
        var subPacketResults = new List<long>();
        switch (length)
        {
            case "0":
                var steps = GetNumberFromQueue(queue, 15);
                var queueCount = queue.Count;
                while (queueCount - steps != queue.Count)
                {
                    var (longs, result) = Process(queue, versions);
                    versions = longs;
                    subPacketResults.Add(result);
                }

                break;
            case "1":
                var subPackets = GetNumberFromQueue(queue, 11);
                for (var i = 0; i < subPackets; i++)
                {
                    var (longs, result) = Process(queue, versions);
                    versions = longs;
                    subPacketResults.Add(result);
                }

                break;
        }

        return typeId switch
        {
            0 => (versions, subPacketResults.Sum()),
            1 => (versions, subPacketResults.Aggregate((result, item) => result * item)),
            2 => (versions, subPacketResults.Min()),
            3 => (versions, subPacketResults.Max()),
            5 => (versions, subPacketResults[0] > subPacketResults[1] ? 1 : 0),
            6 => (versions, subPacketResults[0] < subPacketResults[1] ? 1 : 0),
            7 => (versions, subPacketResults[0] == subPacketResults[1] ? 1 : 0),
            _ => throw new Exception("Pas possible.")
        };
    }


    private static long GetLiteralNumber(Queue<string> queue)
    {
        var sb = new StringBuilder();
        var lastIteration = string.Empty;
        while (lastIteration != "0")
        {
            lastIteration = queue.Dequeue();
            var literalPart = string.Join(string.Empty, queue.DequeueChunk(4));
            sb.Append(literalPart);
        }

        return Convert.ToInt64(sb.ToString(), 2);
    }

    private static long GetNumberFromQueue(Queue<string> queue, int numberOfBits)
    {
        return Convert.ToInt64(string.Join(string.Empty, queue.DequeueChunk(numberOfBits)), 2);
    }

    private static string ToBinaryString(string input)
    {
        return string.Join(string.Empty,
            input.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
            )
        );
    }
}