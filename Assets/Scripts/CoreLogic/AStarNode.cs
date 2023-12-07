using System.Collections.Generic;

namespace AStarExample.CoreLogic
{
    public abstract class AStarNode
    {
        public List<AStarNode> Neighbors { get; set; } = new();
        public bool Walkable { get; set; } = true;
        public AStarNode Connection { get; private set; }
        public float GCost { get; private set; }
        public float HCost { get; protected set; }
        public float FCost => GCost + HCost;

        public int Row { get; private set; }
        public int Column { get; private set; }

        private const int Cost = 1;
        private const int TypeCost = 0;

        protected AStarNode()
        {
            
        }
    
        public void SetConnection(AStarNode pathNode) => Connection = pathNode;
        public void SetGCost(float g) => GCost = g;

        public float GetDistance(AStarNode targetPathNode)
        {
            return Cost + TypeCost;
        }

        public void Initialize(int row, int column)
        {
            Row = row;
            Column = column;
            GCost = float.MaxValue;
        }
        
        public abstract void SetNeighborNodes(List<AStarNode> nodes);
        public abstract void SetHCost(AStarNode target);
    }

    public enum NeighborSide
    {
        LEFT,
        RIGHT
    }
}