using System;

namespace AdventOfCode.Solutions.Year2020.Day12
{
    internal class Day12 : ASolution
    {
        public Day12() : base(12, 2020, "Rain Risk")
        {
            // DebugInput = "F10\nN3\nF7\nR90\nF11";
        }

        protected override string SolvePartOne()
        {
            var instructions = Input.SplitByNewline();
            var ship = new Ship();

            foreach (var instruction in instructions)
            {
                var action = instruction[0];
                var value = Convert.ToInt32(instruction.Substring(1));

                switch (action)
                {
                    case 'N':
                        ship.Y += value;
                        break;
                    case 'S':
                        ship.Y -= value;
                        break;
                    case 'E':
                        ship.X += value;
                        break;
                    case 'W':
                        ship.X -= value;
                        break;
                    case 'L':
                        ship.Rotation = (ship.Rotation + value + 360) % 360;
                        break;
                    case 'R':
                        ship.Rotation = (ship.Rotation - value + 360) % 360;
                        break;
                    case 'F':
                        switch (ship.Rotation)
                        {
                            case 90:
                                ship.Y += value;
                                break;
                            case 270:
                                ship.Y -= value;
                                break;
                            case 0:
                                ship.X += value;
                                break;
                            case 180:
                                ship.X -= value;
                                break;
                        }

                        break;
                }

                // Console.WriteLine($"Ship is at ({ship.X},{ship.Y}), Rotation {ship.Rotation}");
            }

            return ship.ManhattanDistance.ToString();
        }

        protected override string SolvePartTwo()
        {
            var instructions = Input.SplitByNewline();
            var ship = new Ship();

            foreach (var instruction in instructions)
            {
                var action = instruction[0];
                var value = Convert.ToInt32(instruction.Substring(1));

                switch (action)
                {
                    case 'N':
                        ship.WpY += value;
                        break;
                    case 'S':
                        ship.WpY -= value;
                        break;
                    case 'E':
                        ship.WpX += value;
                        break;
                    case 'W':
                        ship.WpX -= value;
                        break;
                    case 'L':
                        ship.RotateWp(value);
                        break;
                    case 'R':
                        ship.RotateWp((-value + 360) % 360);
                        break;
                    case 'F':
                        ship.X += value * ship.WpX;
                        ship.Y += value * ship.WpY;
                        break;
                }

                // Console.WriteLine($"Ship is at ({ship.X},{ship.Y}), Wp is at ({ship.WpX},{ship.WpY})");
            }

            return ship.ManhattanDistance.ToString();
        }
    }
}