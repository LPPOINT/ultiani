using System;
using System.Collections.Generic;
using System.Linq;
using Ultimate2D;
using UltimateAnimate.Debug;
using UltimateAnimate.EntityModel;
using UltimateAnimate.EntityModel.Animating;

namespace UltimateAnimate.AnimationModel
{
    public class EntityHandler
    {
        private Dictionary<TimeSpan, EntityAnimationInfo> Animations { get;  set; }
        public BaseEntity Entity { get; internal set; }
        public TimeLine TimeLine { get; private set; }

        public event EventHandler<AnimationAddEventArgs> AnimationAdded;
        protected virtual void OnAnimationAdded(AnimationAddEventArgs e)
        {
            EventHandler<AnimationAddEventArgs> handler = AnimationAdded;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<AnimationEventArgs> AnimationRemoved;
        protected virtual void OnAnimationRemoved(AnimationEventArgs e)
        {
            EventHandler<AnimationEventArgs> handler = AnimationRemoved;
            if (handler != null) handler(this, e);
        }

        public void AddAnimation(TimeSpan time, EntityAnimationInfo animationInfo)
        {
            Animations.Add(time, animationInfo);
            TimeLine.AddHook(time, Hook);
            OnAnimationAdded(new AnimationAddEventArgs(animationInfo, time));
        }
        public void RemoveAnimation(TimeSpan time)
        {
            var item = Animations[time];
            Animations.Remove(time);
            OnAnimationRemoved(new AnimationEventArgs(item));
        }

        private bool ShouldActivate(TimeSpan someTime, TimeSpan animationTime)
        {
            if (someTime == animationTime) return true;
            if (someTime > animationTime)
            {
                return (someTime - animationTime) < TimeLine.Offset;
            }
            if (someTime < animationTime)
            {
                return (animationTime - someTime) < TimeLine.Offset;
            }
            return false;
        }
        private bool ShouldActivate(TimeSpan time)
        {
            if (Animations.ContainsKey(time)) return true;
            return Animations.Select(entityAnimationInfo => entityAnimationInfo.Key).Any(animTime => ShouldActivate(time, animTime));
        }
        private TimeSpan FindNearTime(TimeSpan time)
        {
            foreach (var entityAnimationInfo in Animations)
            {
                if (entityAnimationInfo.Key == time) return time;

                if (entityAnimationInfo.Key > time)
                {
                    if ((entityAnimationInfo.Key - time) < TimeLine.Offset)
                        return entityAnimationInfo.Key;
                }
                if (entityAnimationInfo.Key < time)
                {
                    if ((time - entityAnimationInfo.Key) < TimeLine.Offset)
                        return entityAnimationInfo.Key;
                }
            }
            return new TimeSpan();
        }

        private TimeSpan dbgLastHookTime;

        internal void Hook(TimeSpan time)
        {
            if(Entity == null)
                throw new Exception("open animation error: cant fint parent entity");
            EntityAnimationInfo animation;
            
            DebugWindow.AddLine("Hooked! Time from last hook is: " + (int)(GameTimeInfo.TotalTime - dbgLastHookTime).TotalMilliseconds);
            dbgLastHookTime = GameTimeInfo.TotalTime;

            if (ShouldActivate(time) 
                && Animations.TryGetValue(FindNearTime(time), out animation))
            {
                Entity.Animating = animation;
            } else throw new ArgumentException("hook was created, but animation not found (bad argument time or Animation list error)", "time");
        }

        public EntityHandler(TimeLine timeLine, IDictionary<TimeSpan, EntityAnimationInfo> anims = null)
        {
            dbgLastHookTime = new TimeSpan();
            TimeLine = timeLine;
            Animations = new Dictionary<TimeSpan, EntityAnimationInfo>();
            if (anims != null)
            {
                Animations = new Dictionary<TimeSpan, EntityAnimationInfo>(anims);
                foreach (var entityAnimationInfo in anims)
                {
                    TimeLine.AddHook(entityAnimationInfo.Key, Hook);
                }
            }
        }
    }

    public class AnimationEventArgs : EventArgs
    {
        public AnimationEventArgs(EntityAnimationInfo animationInfo)
        {
            AnimationInfo = animationInfo;
        }

        public EntityAnimationInfo AnimationInfo { get; private set; }
    }
    public class AnimationAddEventArgs : AnimationEventArgs
    {
        public AnimationAddEventArgs(EntityAnimationInfo animationInfo, TimeSpan time) : base(animationInfo)
        {
            Time = time;
        }

        public TimeSpan Time { get; private set; }
    }
}
