using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Utilities;

public static class MapFunctions
{
    public static readonly (Direction Direction, (int Y, int X) Coordinate)[] NeighbourDeltas =
        { (Direction.South, (1, 0)), (Direction.East, (0, 1)), (Direction.North, (-1, 0)), (Direction.West, (0, -1)) };

    public static Map GetMapFromInput(string input)
    {
        var map = new Map();
        var lines = input.SplitByNewline();
        for (var i = 0; i < lines.Length; i++)
        for (var j = 0; j < lines[i].Length; j++)
            map.AddCoordinate(new Coordinate(i, j), lines[i][j].ToString());

        return map;
    }
}

public class Coordinate : IEquatable<Coordinate>
{
    public Coordinate(int y, int x)
    {
        Y = y;
        X = x;
    }

    public int Y { get; }
    public int X { get; }

    public bool Equals(Coordinate other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Y == other.Y && X == other.X;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Y, X);
    }
    
    public override string ToString()
    {
        return $"(Y:{Y},X:{X})";
    }
}

public class Map
{
    public int Height { get; set; }
    public int Width { get; set; }
    private Dictionary<Coordinate, string> Coordinates { get; } = new();

    public void AddCoordinate(Coordinate coordinate, string value)
    {
        if (Coordinates.ContainsKey(coordinate))
        {
            Coordinates[coordinate] = value;
        }
        else
        {
            Coordinates.Add(coordinate, value);
            UpdateSize(coordinate);
        }
    }

    private void UpdateSize(Coordinate coordinate)
    {
        if (coordinate.Y + 1 > Height)
            Height = coordinate.Y + 1;
        if (coordinate.X + 1 > Width)
            Width = coordinate.X + 1;
    }

    public bool ContainsCoordinate(Coordinate coordinate)
    {
        return Coordinates.ContainsKey(coordinate);
    }

    public string Value(Coordinate coordinate)
    {
        return Coordinates[coordinate];
    }

    public List<KeyValuePair<Coordinate, string>> Values()
    {
        return Coordinates.ToList();
    }

    public void Print()
    {
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                Console.Write(Coordinates[new Coordinate(i,j)]);
            }
            Console.WriteLine();
        }
    }
}

public enum Direction
{
    North,
    South,
    West,
    East
}