using Microsoft.Xna.Framework;

namespace UltimateAnimate.EntityModel.Animating
{
    public class Animation2Info
    {
        public Vector2 PositionOffset { get; set; }
        public float RotationOffset { get; set; }

        public Animation2Info(Vector2 positionOffset, float rotationOffset)
        {
            PositionOffset = positionOffset;
            RotationOffset = rotationOffset;
        }
    }
}
