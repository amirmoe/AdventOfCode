using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Utilities;

public static class NeighbourHelper
{
    public static readonly (Direction Direction, (int Y, int X) Delta)[] CardinalNeighbourDeltas =
    {
        (Direction.North, (-1, 0)),
        (Direction.East, (0, 1)),
        (Direction.South, (1, 0)),
        (Direction.West, (0, -1))
    };

    public static readonly Direction[] CardinalDirections = CardinalNeighbourDeltas.Select(x => x.Direction).ToArray();

    public static readonly (Direction Direction, (int Y, int X) Delta)[] DiagonalNeighbourDeltas =
    {
        (Direction.NorthWest, (-1, -1)),
        (Direction.NorthEast, (-1, 1)),
        (Direction.SouthWest, (1, -1)),
        (Direction.SouthEast, (1, 1))
    };

    public static readonly (Direction Direction, (int Y, int X) Delta)[] AllNeighbourDeltas =
        CardinalNeighbourDeltas.Concat(DiagonalNeighbourDeltas).ToArray();

    public static readonly IEnumerable<Direction> AllNeighbourDirections =
        AllNeighbourDeltas.Select(x => x.Direction);

    public static readonly Dictionary<Direction, (int Y, int X)> AllNeighbourDeltaMap =
        AllNeighbourDeltas
            .ToDictionary(pair => pair.Direction, pair => pair.Delta);
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

    public IEnumerable<Coordinate> CardinalNeighbours<T>(Map<T> map = null)
    {
        return NeighbourHelper
            .CardinalNeighbourDeltas
            .ToList()
            .Select(delta => new Coordinate(Y + delta.Delta.Y, X + delta.Delta.X))
            .Where(coordinate => map == null || map.ContainsCoordinate(coordinate));
    }

    public IEnumerable<Coordinate> DiagonalNeighbours<T>(Map<T> map = null)
    {
        return NeighbourHelper
            .CardinalNeighbourDeltas
            .ToList()
            .Select(delta => new Coordinate(Y + delta.Delta.Y, X + delta.Delta.X))
            .Where(coordinate => map == null || map.ContainsCoordinate(coordinate));
    }

    public IEnumerable<Coordinate> AllNeighbours<T>(Map<T> map = null)
    {
        return NeighbourHelper
            .CardinalNeighbourDeltas
            .ToList()
            .Select(delta => new Coordinate(Y + delta.Delta.Y, X + delta.Delta.X))
            .Where(coordinate => map == null || map.ContainsCoordinate(coordinate));
    }

    public Coordinate Neighbour(Direction direction)
    {
        var delta = NeighbourHelper.AllNeighbourDeltaMap[direction];
        return new Coordinate(Y + delta.Y, X + delta.X);
    }
}

public class Map<T>
{
    public Map(string input)
    {
        var lines = input.SplitByNewline();
        for (var i = 0; i < lines.Length; i++)
        for (var j = 0; j < lines[i].Length; j++)
            AddCoordinate(new Coordinate(i, j), ConvertValue(lines[i][j]));
    }

    public Map()
    {
    }

    public int Height { get; set; }
    public int Width { get; set; }
    private Dictionary<Coordinate, T> Coordinates { get; } = new();
    
    private static T ConvertValue(char c)
    {
        if (typeof(T) == typeof(int))
            return (T)(object)int.Parse(c.ToString());
        if (typeof(T) == typeof(char))
            return (T)(object)c;
        if (typeof(T) == typeof(string))
            return (T)(object)c.ToString();

        throw new InvalidOperationException($"Unsupported type: {typeof(T)}");
    }

    public void AddCoordinate(Coordinate coordinate, T value)
    {
        if (!Coordinates.TryAdd(coordinate, value))
            Coordinates[coordinate] = value;
        else
            UpdateSize(coordinate);
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

    public T Value(Coordinate coordinate)
    {
        return Coordinates[coordinate];
    }

    public void SetValue(Coordinate coordinate, T value)
    {
        Coordinates[coordinate] = value;
    }

    public bool TryGetCoordinateValue(Coordinate coordinate, out T value)
    {
        return Coordinates.TryGetValue(coordinate, out value);
    }

    public List<KeyValuePair<Coordinate, T>> Values()
    {
        return Coordinates.ToList();
    }

    public void Print()
    {
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++) Console.Write(Coordinates[new Coordinate(i, j)]);
            Console.WriteLine();
        }
    }
}

public enum Direction
{
    North,
    South,
    West,
    East,
    NorthWest,
    NorthEast,
    SouthWest,
    SouthEast
}