using System;

namespace AdventOfCode.Solutions.Year2020.Day08
{
    public class Instruction
    {
        public Operation Operation { get; set; }
        public int Argument { get; set; }
    }

    public enum Operation
    {
        Acc,
        Jmp,
        Nop
    }
}