using FlyweightPattern.Flyweights;

namespace FlyweightPattern.Model
{
    public class Tree
    {
        private int _x;
        private int _y;
        private TreeType _type;

        public Tree(int x, int y, TreeType type)
        {
            _x = x;
            _y = y;
            _type = type;
        }

        public void Display()
        {
            _type.Display(_x, _y);
        }
    }
}
