using Microsoft.Xna.Framework;

namespace Ultimate2D
{
    public class SpriteAnimationFrame 
    {
        public SpriteAnimation Animation { get; internal set; }
        public Rectangle FrameBounds { get; private set; }

        public SpriteAnimationFrame(Rectangle frameBounds)
        {
            FrameBounds = frameBounds;
        }

    }
}
