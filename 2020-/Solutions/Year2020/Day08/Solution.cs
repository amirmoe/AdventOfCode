using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AdventOfCode.Solutions.Year2020.Day08
{

    class Day08 : ASolution
    {

        public Day08() : base(08, 2020, "Handheld Halting")
        {
            // DebugInput = "nop +0\nacc +1\njmp +4\nacc +3\njmp -3\nacc -99\nacc +1\njmp -4\nacc +6";
        }

        protected override string SolvePartOne()
        {
            var instructions = Input.SplitByNewline().Select(Parse).ToList();
            var program = new BootProgram(instructions);
            return program.Accumulator.ToString();
        }

        protected override string SolvePartTwo()
        {

            var acc = 0;
            var instructions = Input.SplitByNewline().Select(Parse).ToList();
            for (var i = 0; i < instructions.Count; i++)
            {
                var newInstructions = JsonConvert.DeserializeObject<List<Instruction>>(JsonConvert.SerializeObject(instructions));
                switch (newInstructions[i].Operation)
                {
                    case Operation.Jmp:
                        newInstructions[i].Operation = Operation.Nop;
                        break;
                    case Operation.Nop:
                        newInstructions[i].Operation = Operation.Jmp;
                        break;
                    case Operation.Acc:
                        continue;
                    default:
                        continue;
                }
                
                var program = new BootProgram(newInstructions);
                if (program.InfiniteLoop)
                {
                    continue;
                }

                acc = program.Accumulator;
                break;
            }
            
            return acc.ToString();
        }

        private Instruction Parse(string instruction)
        {
            var split = instruction.Split(" ");
            var op = split[0];
            var arg = split[1];
            Enum.TryParse(char.ToUpper(op[0]) + op.Substring(1), out Operation operation);
            return new Instruction { Operation = operation, Argument = Convert.ToInt32(arg)};
        }
    }
}
