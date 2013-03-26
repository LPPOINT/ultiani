using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using UltimateAnimate.AnimationModel;
using UltimateAnimate.EntityModel;
using UltimateAnimate.EntityModel.Animating;

namespace UltimateAnimate.DebugHelping
{
    public static class DebugEntityHandler
    {
        public static BaseEntity Entity { get; private set; }

        public static void Initialize(BaseEntity entity, TimeLine timeLine)
        {
            Entity = entity;

            var animation1 = new EntityAnimationInfo(new Vector2(0.4f, 0.4f), 0, Vector2.Zero, null);
            var animation2 = new EntityAnimationInfo(new Vector2(-0.4f, -0.4f), 0, Vector2.Zero, null);

            var handler = new EntityHandler(timeLine,
                                            new Dictionary<TimeSpan, EntityAnimationInfo>
                                                   {{new TimeSpan(0, 0, 0, 1), animation1}, 
                                                   {new TimeSpan(0, 0, 0, 7), animation2}});
            Entity.Handler = handler;
        }

    }
}
