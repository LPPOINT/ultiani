using Ultimate2D;

namespace UltimateCheckers
{
    public class Cell : IBoardObject
    {

        public GameColor Color { get; private set; }
        public BoardObjectPosition Position { get; private set; }
        public SpriteRenderer Sprite { get; private set; }
        public Checker InnerChecker { get; internal set; }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void Render()
        {
            throw new System.NotImplementedException();
        }

        public Cell(GameColor color, BoardObjectPosition position)
        {
            Position = position;
            Color = color;
        }
    }
}
