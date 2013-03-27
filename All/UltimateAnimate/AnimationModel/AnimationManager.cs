using System;
using Microsoft.Xna.Framework;

namespace UltimateAnimate.AnimationModel
{
    public class AnimationManager
    {
        public Vector2 GetVectorStepAnimation(Vector2 start, Vector2 end, TimeSpan duration)
        {
            return (start - end)/(float)duration.TotalMilliseconds;
        }
        public float GetFloatStepAnimation(float start, float end, TimeSpan duration)
        {
            return (float) ((start - end)/duration.TotalMilliseconds);
        }
    }
}
