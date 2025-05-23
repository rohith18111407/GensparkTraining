using System;

namespace FlyweightPattern.Flyweights
{
    public class TreeType
    {
        public string Name { get; }
        public string Color { get; }
        public string Texture { get; }

        public TreeType(string name, string color, string texture)
        {
            Name = name;
            Color = color;
            Texture = texture;
        }

        public void Display(int x, int y)
        {
            Console.WriteLine($"Drawing '{Name}' tree at ({x},{y}) with color {Color} and texture {Texture}");
        }
    }
}
