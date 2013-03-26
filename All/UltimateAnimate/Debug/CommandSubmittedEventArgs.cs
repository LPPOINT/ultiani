using System;

namespace UltimateAnimate.Debug
{
    public class CommandSubmittedEventArgs : EventArgs
    {
        public string Command { get; private set; }
        public string CommandArgs { get; private set; }

        public CommandSubmittedEventArgs(string command, string commandArgs)
        {
            CommandArgs = commandArgs;
            Command = command;
        }
    }
}
