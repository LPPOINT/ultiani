using System;

namespace UltimateAnimate.Debug
{
    public class CommandSubmittedEventArgs : EventArgs
    {
        public string Command { get; private set; }

        public CommandSubmittedEventArgs(string command)
        {
            Command = command;
        }
    }
}
