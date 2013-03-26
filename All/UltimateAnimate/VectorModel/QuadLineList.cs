using Microsoft.Xna.Framework;

namespace UltimateAnimate.VectorModel
{
    class QuadLineList : LineList
    {
        public float Height { get; set; }
        public float Width { get; set; }

        public QuadLineList(Vector2 leftTop, float width, float height) : base(leftTop, 
            new Vector2(leftTop.X + width, leftTop.Y), 
            new Vector2(leftTop.X + width, leftTop.Y + height), 
            new Vector2(leftTop.X, leftTop.Y + height))
        {
            
        }
    }
}
