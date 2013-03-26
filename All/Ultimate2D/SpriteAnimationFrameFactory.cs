using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Ultimate2D
{
    public class SpriteAnimationFrameFactory : ISpriteAnimationFrameFactory
    {

        public int FrameSizeX { get; private set; }
        public int FrameSizeY { get; private set; }
        public int OffsetX { get; private set; }
        public int OffsetY { get; private set; }
        public int FrameCountX { get; private set; }
        public int FrameCountY { get; private set; }

        public SpriteAnimationFrame[] GetFrames(SpriteInfo spriteInfo)
        {
            var frames = new List<SpriteAnimationFrame>();
            for (var x = 0; x <= FrameCountX; x++)
            {
                for (var y = 0; y <= FrameCountY; y++)
                {
                    frames.Add(new SpriteAnimationFrame(new Rectangle(FrameSizeX*x, FrameSizeY*y, FrameSizeX, FrameSizeY)));
                }
            }
            return frames.ToArray();
        }

        public SpriteAnimationFrameFactory(int frameSizeX, int frameSizeY, int offsetX = 0, int offsetY = 0)
        {
            OffsetY = offsetY;
            OffsetX = offsetX;
            FrameSizeY = frameSizeY;
            FrameSizeX = frameSizeX;
            FrameCountX = 5;
            FrameCountY = 5;
        }
    }
}
