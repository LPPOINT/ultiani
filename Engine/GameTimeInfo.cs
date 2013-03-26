using System;

namespace Ultimate2D
{
    public static class GameTimeInfo
    {
        public static TimeSpan LastUpdateDelta { get; internal set; }
        public static TimeSpan LastRenderDelta { get; internal set; }
        public static TimeSpan TotalTime { get; internal set; }
    }
}
