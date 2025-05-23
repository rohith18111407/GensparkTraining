using System.Collections.Generic;
using FlyweightPattern.Flyweights;

namespace FlyweightPattern.Factory
{
    public class TreeFactory
    {
        private Dictionary<string, TreeType> _treeTypes = new();

        public TreeType GetTreeType(string name, string color, string texture)
        {
            string key = $"{name}_{color}_{texture}";
            if (!_treeTypes.ContainsKey(key))
            {
                _treeTypes[key] = new TreeType(name, color, texture);
            }
            return _treeTypes[key];
        }
    }
}
