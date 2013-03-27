using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UltimateAnimate.Debug;
using UltimateAnimate.EntityModel.Animating;

namespace UltimateAnimate.AnimationModel
{
    public class AnimationKeys
    {

        public Dictionary<TimeSpan, EntityAnimationInfo> Keys { get; private set; }
        public AnimationManager AnimationManager { get; private set; }

        private void SortKeysByTime()
        {
            var sorted = new SortedDictionary<TimeSpan, EntityAnimationInfo>(Keys, new TimeSpanComparer());
            Keys = new Dictionary<TimeSpan, EntityAnimationInfo>(sorted);
        }

        private TimeSpan NextKeyTime(TimeSpan time)
        {
            var list = Keys.Keys.Where(timeSpan => timeSpan > time).ToList();
            var sortedList = new SortedSet<TimeSpan>(list, new TimeSpanComparer());
            TimeSpan value;

            try
            {
                value = sortedList.ToArray()[0];
            }
            catch
            {
                return new TimeSpan(0, 0, 0, 0);
            }
            return value;
        }

        public EntityHandler CreateHandler(TimeLine timeLine)
        {
            SortKeysByTime();
            var handler = new EntityHandler(timeLine);
            foreach (var entityAnimationInfo in Keys)
            {
                EntityAnimationInfo next;
                var nextKeyTime = NextKeyTime(entityAnimationInfo.Key);
                if (Keys.TryGetValue(nextKeyTime, out next))
                {
                    var duration = nextKeyTime - entityAnimationInfo.Key;
                    var posAnim = AnimationManager.GetVectorStepAnimation(entityAnimationInfo.Value.PositionOffset,
                                                                          next.PositionOffset,
                                                                         duration);
                    var rotAnim = AnimationManager.GetFloatStepAnimation(entityAnimationInfo.Value.RotationOffset,
                                                                         next.RotationOffset, duration);
                    var scaleAnim = AnimationManager.GetVectorStepAnimation(entityAnimationInfo.Value.ScaleOffset,
                                                                            next.ScaleOffset, duration);
                    handler.AddAnimation(entityAnimationInfo.Key, new EntityAnimationInfo(posAnim, rotAnim, scaleAnim, null));
                }
            }
            return handler;
        }

        public AnimationKeys(Dictionary<TimeSpan, EntityAnimationInfo> keys) 
        {
            if (keys != null)
               Keys = keys;
            else Keys = new Dictionary<TimeSpan, EntityAnimationInfo>();
            AnimationManager = new AnimationManager();
        }
    }
}
