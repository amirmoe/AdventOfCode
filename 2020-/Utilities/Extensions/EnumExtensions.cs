using System;

namespace AdventOfCode.Utilities.Extensions;

public static class EnumExtensions
{
    public static int ToInt(this Enum p)
    {
        return (int)(object)p;
    }
}