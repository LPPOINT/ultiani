namespace UltimateAnimate.Debug
{
    public class DebugCommandInput : DebugInput
    {
        public DebugCommandInput(string command)
        {
            Command = command;
            if (DebugWindow.IsWindowCreated)
            {
                DebugWindow.AddCommand(command);
                DebugWindow.CommandSubmitted += (sender, args) =>
                                                    {
                                                        if (args.Command == command)
                                                            OnActivated();
                                                    };
            }
        }

        public string Command { get; private set; }
    }
}
