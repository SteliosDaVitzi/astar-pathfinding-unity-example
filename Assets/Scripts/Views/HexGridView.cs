using System.Collections.Generic;
using System.Linq;
using AStarExample.CoreLogic;
using UnityEngine;

namespace AStarExample.Views
{
    public class HexGridView : MonoBehaviour
    {
        [SerializeField] private HexNodeView hexNodeViewPrefab;
        [SerializeField] private CameraResizer cameraResizer;

        private GameGrid<HexNode> _grid;

        private const float HexCellHeight = 0.86f;

        private AStarNode _originCell;
        private AStarNode _targetCell;

        private readonly List<HexNodeView> _hexNodeViews = new();

        private Color _highlightColor;
        private const float MinColorValue = 0.5f;
        private const float MaxColorValue = 1.5f;
        public void Setup(GameGrid<HexNode> gameGrid)
        {
            DestroyGrid();
        
            _grid = gameGrid;
        
            GenerateGrid();
            UpdatePosition();
        
            var renderers = _hexNodeViews.Select(x => x.GetComponent<Renderer>()).ToList();
            cameraResizer.SetupCamera(renderers);
        }

        private void DestroyGrid()
        {
            foreach (var hexNodeView in _hexNodeViews)
                Destroy(hexNodeView.gameObject);
        
            _hexNodeViews.Clear();
        
            transform.position = Vector3.zero;
        }

        private void GenerateGrid()
        {
            foreach (var cell in _grid.Nodes)
                CreateCell(cell);
        }

        private void UpdatePosition()
        {
            transform.position = new Vector3(-(_grid.Columns + 0.5f)/2f , -_grid.Rows, 0);
        }

        private void CreateCell(AStarNode cell)
        {
            var cellInstance = Instantiate(hexNodeViewPrefab, transform);
            cellInstance.Setup(cell);
        
            var posX = (cell.Row % 2 == 0 ? 1 : 0.5f) + cell.Column;
            var posZ = HexCellHeight * cell.Row;
        
            cellInstance.transform.position = new Vector3(posX , posZ, 0);
        
            cellInstance.OnCellClicked += OnHexGridCellClicked;
        
            _hexNodeViews.Add(cellInstance);
        }

        private void OnHexGridCellClicked(AStarNode aStarNode)
        {
            if (!aStarNode.Walkable) return;
        
            if (_originCell == null)
            {
                _originCell = aStarNode;

                _highlightColor = GetRandomColor();
            
                _hexNodeViews.Find(hexNodeView => hexNodeView.AStarNode == _originCell).SetHighlightColor(_highlightColor);
            
                return;
            }

            _targetCell = aStarNode;

            var path = AStarPathfinder.FindPath(_originCell, _targetCell);
        
            if (path != null)
            {
                foreach (var node in path)
                {
                    var targetCellView = _hexNodeViews.Find(x => x.AStarNode.Row == node.Row && x.AStarNode.Column == node.Column);
                
                    foreach (var cellView in _hexNodeViews)
                        if (cellView.AStarNode.Row == node.Row && cellView.AStarNode.Column == node.Column)
                            targetCellView.SetHighlightColor(_highlightColor);
                }
            }
            else
                Debug.Log("No path found");

            _originCell = null;
            _targetCell = null;
        }

        private Color GetRandomColor()
        {
            return new Color(
                UnityEngine.Random.Range(MinColorValue, MaxColorValue), 
                UnityEngine.Random.Range(MinColorValue, MaxColorValue), 
                UnityEngine.Random.Range(MinColorValue, MaxColorValue), 
                1.0f);
        }
    }
}
