using System;

namespace UltimateAnimate.Common
{
    public class FpsCutManager
    {
        public int FramesPerSecond { get; private set; }
        public TimeSpan DurationBetweenUpdates { get; private set; }
        private TimeSpan currentDuration;

        private int fps;
        private TimeSpan currentFpsTimer;

        public event EventHandler<FpsUpdatedEventArgs> FpsUpdate;
        protected virtual void OnFpsUpdate(FpsUpdatedEventArgs e)
        {
            EventHandler<FpsUpdatedEventArgs> handler = FpsUpdate;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<EventArgs> Update;
        private  void OnUpdate()
        {
            EventHandler<EventArgs> handler = Update;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        internal void AddTime(TimeSpan time)
        {

            currentFpsTimer += time;
            if (currentFpsTimer >= new TimeSpan(0, 0, 0, 1))
            {
                OnFpsUpdate(new FpsUpdatedEventArgs(fps));
                currentFpsTimer = new TimeSpan();
                fps = 0;
            }

            currentDuration += time;
            if (currentDuration > DurationBetweenUpdates)
            {
                currentDuration = new TimeSpan();
                fps++;
                OnUpdate();
            }
        }

        public FpsCutManager(int fps)
        {
            FramesPerSecond = fps;
            currentFpsTimer = new TimeSpan();
            DurationBetweenUpdates = new TimeSpan(0, 0, 0, 0, (int) (new TimeSpan(0, 0, 0, 1).TotalMilliseconds / fps));
        }

    }

    public class FpsUpdatedEventArgs : EventArgs
    {
        public FpsUpdatedEventArgs(int fps)
        {
            Fps = fps;
        }

        public int Fps { get; private set; }
    }

}
