using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2022.Day07;

internal class Day07 : ASolution
{
    private const string ChangeDirectoryCommand = "$ cd";
    private const string ListDirectoryCommand = "$ ls";

    public Day07() : base(07, 2022, "No Space Left On Device")
    {
    }

    protected override string SolvePartOne()
    {
        var rootNode = GetFileSystem(Input);

        var q = new Queue<Node>(new[] { rootNode });
        var smallDirectories = new List<Node>();
        while (q.Any())
        {
            var node = q.Dequeue();
            if (!node.Children.Any()) continue;

            if (node.GetSize() <= 100000)
                smallDirectories.Add(node);
            node.Children.ToList().ForEach(c => q.Enqueue(c));
        }

        return smallDirectories
            .Select(x => x.GetSize())
            .Sum()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        var rootNode = GetFileSystem(Input);


        var q = new Queue<Node>(new[] { rootNode });
        var directorySizes = new List<long>();
        while (q.Any())
        {
            var node = q.Dequeue();
            if (!node.Children.Any()) continue;

            directorySizes.Add(node.GetSize());
            node.Children.ToList().ForEach(c => q.Enqueue(c));
        }

        directorySizes.Sort();
        var needToClear = 30000000 - (70000000 - rootNode.GetSize());

        return directorySizes
            .First(x => x > needToClear)
            .ToString();
    }

    private static Node GetFileSystem(string input)
    {
        var rows = new Queue<string>(input.SplitByNewline());

        var rootNode = new Node(rows.Dequeue().Replace(ChangeDirectoryCommand, string.Empty).Trim());
        var currentNode = rootNode;
        while (rows.Any())
        {
            var row = rows.Dequeue();

            if (row.StartsWith(ListDirectoryCommand))
                while (rows.Any() && !rows.Peek().Contains('$'))
                    currentNode.AddChild(rows.Dequeue());

            if (row.StartsWith(ChangeDirectoryCommand))
            {
                var dir = row.Replace(ChangeDirectoryCommand, string.Empty).Trim();
                currentNode = dir == ".." ? currentNode.Parent : currentNode.Children.First(x => x.Value == $"dir {dir}");
            }
        }

        return rootNode;
    }
}

public class Node
{
    private readonly List<Node> _children = new();

    public Node(string value)
    {
        Value = value;
    }

    public Node Parent { get; private init; }
    public string Value { get; }
    private long? Size { get; set; }

    public IEnumerable<Node> Children => _children.AsReadOnly();

    public void AddChild(string value)
    {
        var node = new Node(value) { Parent = this };
        _children.Add(node);
    }

    public long GetSize()
    {
        if (Size is not null)
            return Size.Value;

        var size = Children.Any()
            ? Children.Select(child => child.GetSize()).Sum()
            : long.Parse(Value.Split(" ").First());

        Size = size;
        return size;
    }
}

