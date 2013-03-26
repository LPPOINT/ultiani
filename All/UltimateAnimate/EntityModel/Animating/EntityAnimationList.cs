using System;
using System.Collections.Generic;

namespace UltimateAnimate.EntityModel.Animating
{
    public class EntityAnimationList
    {
        public Dictionary<TimeSpan, EntityAnimationInfo> Animations { get; private set; }

        public EntityAnimationList(Dictionary<TimeSpan, EntityAnimationInfo> animations)
        {
            Animations = animations;
        }

    }
}
