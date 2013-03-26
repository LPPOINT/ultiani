using System;
using Microsoft.Xna.Framework;

namespace UltimateAnimate.EntityModel.Animating
{
    public class Animation2Info : ICloneable
    {
        public Vector2 PositionOffset { get; set; }
        public float RotationOffset { get; set; }

        public Animation2Info(Vector2 positionOffset, float rotationOffset)
        {
            PositionOffset = positionOffset;
            RotationOffset = rotationOffset;
        }

        public virtual object Clone()
        {
            return new Animation2Info(PositionOffset, RotationOffset);
        }
    }
}
