using System;
using System.Collections.Generic;

namespace UltimateAnimate.AnimationModel
{
    class TimeSpanComparer 
        : Comparer<TimeSpan>
    {
        public override int Compare(TimeSpan x, TimeSpan y)
        {
            if (x > y)
                return 1;
            if (x == y)
                return 0;
            if (x < y)
                return -1;
            return -1;
        }
    }
}
