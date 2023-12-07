using AStarExample.CoreLogic;
using UnityEngine;
using AStarExample.Views;

namespace AStarExample
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private HexGridView gridView;
    
        private GameGrid<HexNode> _grid;
    
        void Start()
        {
            NewGame();
        }

        public void NewGame()
        {
            _grid = new GameGrid<HexNode>(15,15);
            gridView.Setup(_grid);
        }
    }
}

