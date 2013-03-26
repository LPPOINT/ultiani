using System;

namespace UltimateAnimate.Debug
{
    public abstract class DebugInput
    {
        public event EventHandler<EventArgs> Activated;
        protected virtual void OnActivated()
        {
            var handler = Activated;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
