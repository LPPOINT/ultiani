namespace UltimateAnimate.Debug
{
    public class DebugCommandButtonInput : DebugInput
    {
        public DebugCommandButtonInput(string command, DebugWindow.UseButtons button)
        {
            Button = button;
            Command = command;

            if (DebugWindow.IsWindowCreated)
            {
                DebugWindow.AddCommand(Command);
                DebugWindow.AssosicateUseButton(Button, command);
                DebugWindow.UseButtonPressed += (sender, args) =>
                                                    {
                                                        if(args.Button == button)
                                                            OnActivated();
                                                    };
            }

        }

        public DebugWindow.UseButtons Button { get; private set; }
        public string Command { get; private set; }
    }
}
