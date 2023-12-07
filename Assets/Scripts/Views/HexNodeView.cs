using System;
using AStarExample.CoreLogic;
using UnityEngine;

namespace AStarExample.Views
{
    public class HexNodeView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
    
        private AStarNode _aStarNode;
        public AStarNode AStarNode => _aStarNode;
        public Action<AStarNode> OnCellClicked;
    
        public void Setup(AStarNode aStarNode)
        {
            _aStarNode = aStarNode;
            name = $"{aStarNode.Row}{aStarNode.Column}";
        
            if(!_aStarNode.Walkable)
                SetUnwalkableColor();
        }

        private void OnMouseDown()
        {
            OnCellClicked?.Invoke(_aStarNode);
        }
    
        private void SetUnwalkableColor()
        {
            meshRenderer.material.color = Color.black;
        }

        public void SetHighlightColor(Color color)
        {
            meshRenderer.material.color = color;
        }
    }
}

