using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day10
{
    public class Field
    {
        private List<Coordinate> Asteroids { get; }
        
        public Field(int width, int height, IReadOnlyList<string> input)
        {    
            Asteroids = new List<Coordinate>();

            for (var i = 0; i < height; i++)
            {
                var line = input[i].ToCharArray();
                for (var j = 0; j < width; j++)
                {
                    if (line[j].ToString().Equals("#"))
                    {
                        Asteroids.Add(new Coordinate(j,i));
                    } 
                }
            }
        }
        public Dictionary<Coordinate, int> FindDetectableAsteroids()
        {
            var dict = new Dictionary<Coordinate, int>();
            foreach (var asteroid in Asteroids)
            {
                var number =
                    FindNumberOfDetectableAsteroids(asteroid, Asteroids.Where(x => x != asteroid).ToList());
                dict.Add(asteroid, number);
            }

            return dict;
        }

        private static int FindNumberOfDetectableAsteroids(Coordinate asteroid, List<Coordinate> otherAsteroids)
        {
            var visitedAngles = new List<double>();
            var aboveOrigin = new Coordinate(0, 1);
            
            
            foreach (var otherAsteroid in otherAsteroids)
            {
                var adjustedToOrigin = new Coordinate(otherAsteroid.X-asteroid.X, otherAsteroid.Y-asteroid.Y);
                var angle = Coordinate.GetAngle(aboveOrigin, adjustedToOrigin) * 180 / Math.PI + 180;
                
                
                if (!visitedAngles.Contains(angle))
                {
                    visitedAngles.Add(angle);
                }
            }
            return visitedAngles.Count;
        }

        private Coordinate GetClosestAsteroid(List<Coordinate> asteroidList, Coordinate asteroid)
        {
            var closest = asteroidList.First();
            var distance = GetDistance(asteroid, asteroidList.First());
            foreach (var otherAsteroid in asteroidList.Skip(1))
            {
                var newDistance =  GetDistance(asteroid, otherAsteroid);
                if (!(newDistance < distance)) continue;
                distance = newDistance;
                closest = otherAsteroid;
            }

            return closest;
        }

        private double GetDistance(Coordinate asteroid1, Coordinate asteroid2)
        {
            return Math.Sqrt(Math.Pow(asteroid2.X - asteroid1.X, 2) + Math.Pow(asteroid2.Y - asteroid1.Y, 2));
        }

        public List<Coordinate> GetVaporizingOrder(Coordinate asteroid)
        {
            var aboveOrigin = new Coordinate(0, 1);
            var asteroidDictionary = new Dictionary<double, List<Coordinate>>();
            var vaporizeOrder = new List<Coordinate>();
            
            foreach (var otherAsteroid in Asteroids.Where(x => x != asteroid))
            {
                var adjustedToOrigin = new Coordinate(otherAsteroid.X-asteroid.X, otherAsteroid.Y-asteroid.Y);
                var angle = Coordinate.GetAngle(aboveOrigin, adjustedToOrigin) * 180 / Math.PI + 180;

                if (Math.Abs(angle - 360) < 0.001)
                {
                    angle = 0;
                }

                if (asteroidDictionary.ContainsKey(angle))
                {
                    asteroidDictionary[angle].Add(otherAsteroid);
                }
                else
                {
                    asteroidDictionary.Add(angle, new List<Coordinate>{otherAsteroid});
                }
            }

            while (asteroidDictionary.Count > 0)
            {
                foreach (var (key, value) in asteroidDictionary.OrderBy(x => x.Key))
                {
                    var asteroidToVaporize = GetClosestAsteroid(value, asteroid);
                    vaporizeOrder.Add(asteroidToVaporize);
                    value.Remove(asteroidToVaporize);

                    if (!value.Any())
                    {
                        asteroidDictionary.Remove(key);
                    }
                }
            }
            
            return vaporizeOrder;
        }
    }
}