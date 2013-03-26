using Ultimate2D;

namespace UltimateCheckers
{
    public class Checker : IBoardObject
    {


        public Cell ParentCell { get; internal set; }
        public GameColor Color { get; private set; }
        public BoardObjectPosition Position { get; private set; }
        public SpriteRenderer Sprite { get; private set; }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
        public void Render()
        {
            throw new System.NotImplementedException();
        }

        public Checker(GameColor color)
        {
            Color = color;
        }
    }
}
