using System;
using System.Collections.Generic;

namespace UltimateAnimate.AnimationModel
{
    public class TimeLine
    {
        public TimeSpan MaxTime { get; private set; }
        public TimeSpan CurrentTime { get; private set; }

        public static TimeSpan Offset {get {return new TimeSpan(0, 0, 0, 0, 16);}}

        public delegate void TimeHook(TimeSpan hookedTime);
        private readonly Dictionary<TimeSpan, List<TimeHook>> hooks; 
        public void AddHook(TimeSpan time, TimeHook hook)
        {
            if(hooks.ContainsKey(time))
              hooks[time].Add(hook);
            else hooks.Add(time, new List<TimeHook> {hook});
        }

        private bool ShouldActivateHook(TimeSpan hookTime)
        {
            if (CurrentTime < hookTime)
            {
                return (hookTime - CurrentTime) < Offset;
            }
            if (CurrentTime > hookTime)
            {
                return (CurrentTime - hookTime) < Offset;
            }
            return true;
        }

        public void AddTime(TimeSpan time)
        {
            CurrentTime += time;
            foreach (var timeHook in hooks)
            {
                if (ShouldActivateHook(timeHook.Key))// TODO: THIS ERROR
                    foreach (var hook in timeHook.Value)
                    {
                        hook(CurrentTime);
                    }
            }

        }

        public TimeLine()
        {
            MaxTime = new TimeSpan(0, 0, 0, 5);
            CurrentTime = new TimeSpan();
            hooks = new Dictionary<TimeSpan, List<TimeHook>>();
        }
    }
}
