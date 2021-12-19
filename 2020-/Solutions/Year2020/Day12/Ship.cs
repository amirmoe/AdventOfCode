using System;

namespace AdventOfCode.Solutions.Year2020.Day12
{
    public class Ship
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Rotation { get; set; }
        public int ManhattanDistance => Utilities.Utilities.ManhattanDistance((0, 0), (X, Y));
        
        //part 2
        public int WpX { get; set; }
        public int WpY { get; set; }

        public Ship()
        {
            WpX = 10;
            WpY = 1;
        }
        
        public void RotateWp(int angle)
        {
            var radians = Math.PI * angle / 180.0;
            var c = Convert.ToInt32(Math.Round(Math.Cos(radians), 2));
            var s = Convert.ToInt32(Math.Round(Math.Sin(radians), 2));
            
            var x = WpX;
            var y = WpY;
            WpX = Convert.ToInt32(x * c - y * s);
            WpY = Convert.ToInt32(x * s + y * c);
        }
    }
}