using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2020.Day08
{
    public class BootProgram
    {
        public int Accumulator { get; private set; }
        private HashSet<int> ExecutedSteps { get; } = new HashSet<int>();
        public bool InfiniteLoop { get; set; }

        public BootProgram(IReadOnlyList<Instruction> instructions)
        {
            Run(instructions);
        }
        
        
        private void Run(IReadOnlyList<Instruction> instructions)
        {
            var step = 0;
            while (true)
            {
                if (step == instructions.Count)
                    break;
                
                if (ExecutedSteps.Contains(step) )
                {
                    InfiniteLoop = true;
                    break;
                }
                ExecutedSteps.Add(step);
                
                var instruction = instructions[step];
                switch (instruction.Operation)
                {
                    case Operation.Acc:
                        Accumulator += instruction.Argument;
                        step += 1;
                        break;
                    case Operation.Jmp:
                        step += instruction.Argument;
                        break;
                    case Operation.Nop:
                        step += 1;
                        break;
                    default:
                        Console.WriteLine("Not implemented yet!");
                        break;
                }

            }
        }
    }
}