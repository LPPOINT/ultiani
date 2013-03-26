using System;

namespace UltimateAnimate.Debug
{
    public class UseButtonEventArgs : EventArgs
    {
        public UseButtonEventArgs(DebugWindow.UseButtons button)
        {
            Button = button;
        }

        public DebugWindow.UseButtons Button { get; private set; }
    }
}
