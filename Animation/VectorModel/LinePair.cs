using Microsoft.Xna.Framework;

namespace UltimateAnimate.VectorModel
{
    public  class Line
    {
        public Vector2 Start { get; internal set; }
        public Vector2 End { get; internal set; }

        public Line(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }
    }
}
