using Microsoft.Xna.Framework;

namespace UltimateAnimate.EntityModel.Animating
{
    public class Animation3Info : Animation2Info
    {

        public Vector2 ScaleOffset { get;  set; }

        public Animation3Info(Vector2 positionOffset, float rotationOffset, Vector2 scaleOffset) : base(positionOffset, rotationOffset)
        {

            ScaleOffset = scaleOffset;
        }
    }
}
