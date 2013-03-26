using Ultimate2D;

namespace UltimateCheckers
{
    public interface IBoardObject : IColorable, IVisual
    {
        BoardObjectPosition Position { get; }
        SpriteRenderer Sprite { get; }
    }
}
