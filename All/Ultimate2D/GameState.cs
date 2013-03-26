using System;

namespace Ultimate2D
{
    public abstract class GameState
    {

        public GameApplication Application { get; internal set; }
        public TimeSpan TotalTime { get; private set; }

        internal void UpdateTick(TimeSpan delta)
        {
            UpdateCount++;
            TotalTime += delta;
            Update(delta);
        }
        internal void RenderTick(TimeSpan delta)
        {
            RenderCount++;
            TotalTime += delta;
            Render(delta);
        }

        public int RenderCount { get; private set; }
        public int UpdateCount { get; private set; }

        public abstract void Update(TimeSpan delta);
        public abstract void Render(TimeSpan delta);
        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }
    }
}
