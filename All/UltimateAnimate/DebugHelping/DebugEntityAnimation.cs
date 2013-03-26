using System.Collections.Generic;
using Microsoft.Xna.Framework;
using UltimateAnimate.Debug;
using UltimateAnimate.EntityModel;
using UltimateAnimate.EntityModel.Animating;

namespace UltimateAnimate.DebugHelping
{
    public class DebugEntityAnimation
    {
        public BaseEntity Entity { get; private set; }

        public EntityAnimationInfo Animation1 { get; set; }
        public EntityAnimationInfo Animation2 { get; set; }
        private bool firstAnim;

        public void Initialize(BaseEntity entity, DebugInput splitAnimations = null)
        {
            Entity = entity;
            Animation1 =  new EntityAnimationInfo(new Vector2(0.4f, 0.4f), 0, Vector2.Zero, null);
            Animation2 = new EntityAnimationInfo(new Vector2(-0.4f, -0.4f), 0, Vector2.Zero, null);

            Entity.Animating = Animation1;

            if (splitAnimations != null)
            {
                splitAnimations.Activated += (sender, args) =>
                    {
                        firstAnim = !firstAnim;
                        Entity.Animating = firstAnim ? Animation2 : Animation1;
                        DebugWindow.AddLine("animations splitted");
                    };
            }
        }
    }
}
