using System.Collections.Generic;
using FlyweightPattern.Factory;
using FlyweightPattern.Model;

namespace FlyweightPattern.Forests
{
    public class Forest
    {
        private List<Tree> _trees = new();
        private TreeFactory _treeFactory = new();

        public void PlantTree(int x, int y, string name, string color, string texture)
        {
            var type = _treeFactory.GetTreeType(name, color, texture);
            var tree = new Tree(x, y, type);
            _trees.Add(tree);
        }

        public void DisplayForest()
        {
            foreach (var tree in _trees)
            {
                tree.Display();
            }
        }
    }
}
