using System;
using System.Collections.Generic;
using UltimateAnimate.AnimationModel;
using UltimateAnimate.Debug;

namespace UltimateAnimate.DebugHelping
{
    public class DebugTimeLine
    {
        public TimeLine TimeLine { get; private set; }

        public void AddTime(TimeSpan delta)
        {
            TimeLine.AddTime(delta);
        }

        public void Initialize(params KeyValuePair<TimeSpan, string>[] messages)
        {
            TimeLine = new TimeLine();
            foreach (var keyValuePair in messages)
            {
                var pair = keyValuePair;
                TimeLine.AddHook(pair.Key, time => DebugWindow.AddLine(pair.Value));
            }
        }

    }
}
