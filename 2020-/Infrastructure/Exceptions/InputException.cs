using System;

namespace AdventOfCode.Infrastructure.Exceptions;

internal class InputException : Exception
{
    public InputException(string message) : base(message)
    {
    }
}