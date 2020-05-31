using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day10
{
    public class Day10Solution
    {

        private int MostNeighboursCount(Dictionary<Coordinate, int> dict)
        {
            return dict.Select(keyPair => keyPair.Value).Concat(new[] {0}).Max();
        }
        
        private Coordinate MostNeighboursCoordinate(Dictionary<Coordinate, int> dict)
        {
            var coordinate = new Coordinate(0,0);
            var biggest = 0;
            foreach (var (key, value) in dict.Where(keyPair => keyPair.Value > biggest))
            {
                biggest = value;
                coordinate = key;
            }
            return coordinate;
        }
        
        public string Answer1(string[] input)
        {
            var field = new Field(input[0].Length, input.Length, input);
            var asteroidDict = field.FindDetectableAsteroids();
            return MostNeighboursCount(asteroidDict).ToString();
        }
        
        public string Answer2(string[] input)
        {
            var field = new Field(input[0].Length, input.Length, input);
            var asteroidDict = field.FindDetectableAsteroids();
            var coordinate = MostNeighboursCoordinate(asteroidDict);
            var list = field.GetVaporizingOrder(coordinate);
            //var count = 0;
            // foreach (var asteroid in list)
            // {
            //     count++;
            //     Console.WriteLine($"The {count} asteroid to be vaporized is at {asteroid.X},{asteroid.Y}.");
            // }
            
            return (list[199].X*100 + list[199].Y).ToString();
        }
    }
}