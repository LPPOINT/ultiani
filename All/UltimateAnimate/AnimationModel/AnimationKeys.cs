using System;
using System.Collections.Generic;
using UltimateAnimate.EntityModel.Animating;

namespace UltimateAnimate.AnimationModel
{
    public struct AnimationKeys
    {

        public Dictionary<TimeSpan, EntityAnimationInfo> Keys { get; private set; }
        public AnimationManager AnimationManager { get; private set; }

        private void SortKeysByTime()
        {
            var newDict = new SortedDictionary<TimeSpan, EntityAnimationInfo>();
            var currentMin = new TimeSpan();
            foreach (var entityAnimationInfo in Keys)
            {
                if()
            }
        } 
        private KeyValuePair<TimeSpan, EntityAnimationInfo> NextKey(TimeSpan time)
        {
          
        }
        private KeyValuePair<TimeSpan, EntityAnimationInfo> PrevKey(TimeSpan time)
        {
            
        }

        public EntityHandler CreateHandler(TimeLine timeLine)
        {
            SortKeysByTime();
            var handler = new EntityHandler(timeLine);
            foreach (var entityAnimationInfo in Keys)
            {
                var next = NextKey(entityAnimationInfo.Key);
                if (next.Value != null)
                {
                    var posAnim = AnimationManager.GetVectorStepAnimation(entityAnimationInfo.Value.PositionOffset,
                                                                          next.Value.PositionOffset,
                                                                          next.Key - entityAnimationInfo.Key);
                    var scaleAnim = AnimationManager.GetVectorStepAnimation(entityAnimationInfo.Value.ScaleOffset,
                                                                          next.Value.ScaleOffset,
                                                                          next.Key - entityAnimationInfo.Key);
                    var rotAnim = AnimationManager.GetFloatStepAnimation(entityAnimationInfo.Value.RotationOffset,
                                                                          next.Value.RotationOffset,
                                                                          next.Key - entityAnimationInfo.Key);
                    // TODO: Make bone animation
                    handler.AddAnimation(entityAnimationInfo.Key, new EntityAnimationInfo(posAnim, rotAnim, scaleAnim, null));
                   
                }
            }
            return handler;
        }
    }
}
