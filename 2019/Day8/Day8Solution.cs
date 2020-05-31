using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day8
{
    public class Image
    {
        private List<Layer> Layers { get; }
        private int Width { get; }
        private int Height { get; }

        public Image(int width, int height, string input)
        {
            var layers = new List<Layer>();
            for (var i = 0; i < input.Length/(width*height); i++)
            {
                layers.Add(new Layer(width,height,input.Substring(i * width * height,width*height)));
            }

            Layers = layers;
            Height = height;
            Width = width;
        }
        
        public Layer FindLeastZeroLayer()
        {
            var mostNonZero = 0;
            var currentLayer = new Layer(Height,Width,string.Empty);  //ugly
            foreach (var layer in Layers)
            {
                var layerNonZero = NumberOfNonZero(layer);
                if (layerNonZero <= mostNonZero) continue;
                mostNonZero = layerNonZero;
                currentLayer = layer;
            }

            return currentLayer;
        }

        private int NumberOfNonZero(Layer layer)
        {
            var nonZero = 0;
            for (var i = 0; i < layer.Dimension.GetLength(0); i++)
            {
                for (var j = 0; j < layer.Dimension.GetLength(1); j++)
                {
                    if (layer.Dimension[i, j] != 0) nonZero++;
                }
            }

            return nonZero;
        }
        
        public int[,] GetImageArray()
        {
            var array = new int[Height, Width];

            for (var i = 0; i < Height; i++)    
            {                                                         
                for (var j = 0; j < Width; j++)
                {
                    for (var k = 0; k < Layers.Count; k++)
                    {
                        if (Layers[k].Dimension[i, j] == 1 || Layers[k].Dimension[i, j] == 0 || k == Layers.Count - 1)
                        {
                            array[i, j] = Layers[k].Dimension[i, j];
                            break;
                        }
                    }
                }                                                     
            }

            return array;
        }
    }

    public class Layer
    {
        public int[,] Dimension { get; }
        
        public Layer(int width, int height, string input)
        {
            var array = new int[height, width];
            for (var i = 0; i < input.Length; i++)
            {
                var y = i / width;
                var x = i % width;
                array[y, x] = int.Parse(input.Substring(i,1));
            }
            Dimension = array;
        }
    }

    public class Day8Solution
    {
        public string Answer1(string input, int pixelWidth, int pixelHeight)
        {    
            var image = new Image(pixelWidth,pixelHeight,input);
            var layer = image.FindLeastZeroLayer();
            var numberOnes = 0;
            var numberTwos = 0;
            for (var i = 0; i < layer.Dimension.GetLength(0); i++)
            {
                for (var j = 0; j < layer.Dimension.GetLength(1); j++)
                {
                    if (layer.Dimension[i, j] == 1) numberOnes++;
                    if (layer.Dimension[i, j] == 2) numberTwos++;

                }
            }

            return (numberOnes*numberTwos).ToString();
        }
        
        public string Answer2(string input, int pixelWidth, int pixelHeight)
        {    
            var image = new Image(pixelWidth,pixelHeight,input);
            var imageArray = image.GetImageArray();
            const string space = " ";
            for (var i = 0; i < imageArray.GetLength(0); i++)
            {
                for (var j = 0; j < imageArray.GetLength(1); j++)
                {
                    var character = imageArray[i, j] == 1 ? space : "█";
                    Console.Write($"{character}");
                }
                Console.Write(Environment.NewLine);
            }
            return "";
        }
    }
}