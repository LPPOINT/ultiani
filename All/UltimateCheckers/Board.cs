using Microsoft.Xna.Framework;
using Ultimate2D;

namespace UltimateCheckers
{
    public class Board : IVisual
    {
        public BoardObjectCollection<Cell> Cells { get; private set; }
        public BoardObjectCollection<Checker> Checkers { get; private set; }
 
        private void CreateCells()
        {
            var currentColor = GameColor.Black;

            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    CreateCell(new Cell(currentColor, new BoardObjectPosition((BoardPositionLine)x, (BoardPositionLine)y)));
                    currentColor = GetOtherColor(currentColor);
                }

                currentColor = GetOtherColor(currentColor);

            }
        }
        public GameColor GetOtherColor(GameColor color)
        {
            if (color == GameColor.Black) return GameColor.White;
            if (color == GameColor.White) return GameColor.Black;
            return GameColor.Black;
        }
        public void GetOtherColor(ref GameColor color)
        {
            if(color == GameColor.Black) color = GameColor.White;
            if(color == GameColor.White) color = GameColor.Black;
        }
        internal void CreateCell(Cell cell)
        {
            Cells.objects.Add(cell);
        }
        internal void CreateChecker(Checker checker, Cell ownerCell)
        {
            checker.ParentCell = ownerCell;
            ownerCell.InnerChecker = checker;
            Checkers.objects.Add(checker);
        }

        public Vector2 GetCellPosition(BoardObjectPosition position)
        {
            
        }
        public BoardObjectPosition GetCellPosition(Vector2 screenPosition)
        {
            
        }

        public void Update()
        {
            foreach (var cell in Cells)
            {
                cell.Update();
            }
            foreach (var checker in Checkers)
            {
                checker.Update();
            }
        }
        public void Render()
        {
            foreach (var cell in Cells)
            {
                cell.Render();
            }
            foreach (var checker in Checkers)
            {
                checker.Render();
            }
        }
    }
}
