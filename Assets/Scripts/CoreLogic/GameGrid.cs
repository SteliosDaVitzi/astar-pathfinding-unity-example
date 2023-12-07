using System.Collections.Generic;

namespace AStarExample.CoreLogic
{
    public class GameGrid<T> where T: AStarNode, new()
    {
        private readonly int _rows;
        public int Rows => _rows;
        
        private readonly int _columns;
        public int Columns => _columns;
    
        private readonly List<AStarNode> _nodes = new();
        public List<AStarNode> Nodes => _nodes;
        
        public GameGrid(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
    
            GenerateGrid();
            SetRandomObstacles(25);
        }
    
        private void GenerateGrid()
        {
            for (var i = 0; i < _rows; i++)
                for (var j = 0; j < _columns; j++)
                    _nodes.Add(CreateNode(i,j));
    
            foreach (var node in _nodes)
                node.SetNeighborNodes(_nodes);
        }
    
        private void SetRandomObstacles(int mapPercentage)
        {
            var nodesToBeBlocked = (_nodes.Count * mapPercentage) / 100;
            
            for (var i = 0; i < nodesToBeBlocked; i++)
                SetRandomNodeUnwalkable();
        }
    
        private void SetRandomNodeUnwalkable()
        {
            var randomCellIndex = UnityEngine.Random.Range(0, _nodes.Count);
            
            if (_nodes[randomCellIndex].Walkable)
                _nodes[randomCellIndex].Walkable = false;
            else
                SetRandomNodeUnwalkable();
        }
        
        private T CreateNode(int i, int j)
        {
            var node = new T();
            node.Initialize(i, j);
            return node;
        }
    }
}
