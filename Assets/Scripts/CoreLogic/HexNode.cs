using System;
using System.Collections.Generic;

namespace AStarExample.CoreLogic
{
    public class HexNode : AStarNode
    {
        public HexNode()
        {
        
        }

        public override void SetHCost(AStarNode target)
        {
            HCost = Math.Abs(Column - target.Column) + Math.Abs(Row - target.Row);
        }
    
        public override void SetNeighborNodes(List<AStarNode> nodes)
        {
            foreach (var possibleNeighbor in nodes)
            {
                var neighborSide = Row % 2 == 0 ? NeighborSide.LEFT : NeighborSide.RIGHT;

                if ((possibleNeighbor.Row == Row + 1 && possibleNeighbor.Column == Column) ||
                    (possibleNeighbor.Row == Row - 1 && possibleNeighbor.Column == Column) ||
                    (possibleNeighbor.Row == Row && possibleNeighbor.Column == Column + 1) ||
                    (possibleNeighbor.Row == Row && possibleNeighbor.Column == Column - 1))
                {
                    Neighbors.Add(possibleNeighbor);
                }

                if (neighborSide == NeighborSide.LEFT)
                {
                    if ((possibleNeighbor.Row == Row + 1 && possibleNeighbor.Column == Column + 1) ||
                        (possibleNeighbor.Row == Row - 1 && possibleNeighbor.Column == Column + 1))
                    {
                        Neighbors.Add(possibleNeighbor);
                    }
                }
                else if (neighborSide == NeighborSide.RIGHT)
                {
                    if ((possibleNeighbor.Row == Row + 1 && possibleNeighbor.Column == Column - 1) ||
                        (possibleNeighbor.Row == Row - 1 && possibleNeighbor.Column == Column - 1))
                    {
                        Neighbors.Add(possibleNeighbor);
                    }
                }
            }
        }
    }
}
