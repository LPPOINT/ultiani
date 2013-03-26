using System;

namespace UltimateAnimate.Common
{
    public class SpendTimeInfo
    {
        private DateTime startTime;

        public void Start()
        {
             startTime = DateTime.Now;
        }
        public void Stop()
        {
           Start(); // TODO: WTF?
        }
        public TimeSpan GetDuration()
        {
            return (DateTime.Now - startTime);
        }

    }
}
