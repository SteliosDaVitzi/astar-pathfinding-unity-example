using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AStarExample.CoreLogic
{
    public class AStarPathfinder : MonoBehaviour
    {
        public static List<AStarNode> FindPath(AStarNode startNode, AStarNode targetNode)
        {
            var openList = new List<AStarNode>() { startNode };
            var closedList = new List<AStarNode>();
            
            startNode.SetGCost(0);

            while (openList.Any())
            {
                var current = openList[0];
                
                foreach(var t in openList)
                    if (t.FCost < current.FCost || (t.FCost == current.FCost && t.HCost < current.HCost))
                        current = t;
                
                closedList.Add(current);
                openList.Remove(current);
                
                if (current == targetNode)
                    return ReconstructPath(new List<AStarNode>(), startNode, targetNode);

                var validNeighbors = current.Neighbors.Where(neighbor => neighbor.Walkable && !closedList.Contains(neighbor)).ToList();
                
                foreach (var neighbor in validNeighbors)
                {
                    neighbor.SetHCost(targetNode);

                    if (closedList.Contains(neighbor)) continue;
                    
                    var tentativeGCost = current.GCost + current.GetDistance(neighbor);
                    
                    if (openList.Contains(neighbor))
                    {
                        if(tentativeGCost < neighbor.GCost)
                            neighbor.SetGCost(tentativeGCost);
                    }
                    else
                    {
                        neighbor.SetConnection(current);
                        neighbor.SetGCost(tentativeGCost);
                        openList.Add(neighbor);
                    }
                }
            }

            return null;
        }
        
        private static List<AStarNode> ReconstructPath(List<AStarNode> path, AStarNode startNode, AStarNode currentNode)
        {
            if (currentNode == startNode) return path;
            
            path.Add(currentNode);
            currentNode = currentNode.Connection;
            ReconstructPath(path, startNode, currentNode);
            
            return path;
        }
    }
}