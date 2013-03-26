namespace UltimateAnimate.Debug
{
    public class DebugButtonInput : DebugInput
    {
        public DebugButtonInput(DebugWindow.UseButtons button)
        {
            Button = button;
            if (DebugWindow.IsWindowCreated)
                DebugWindow.UseButtonPressed += (sender, args) =>
                                                    {
                                                        if (args.Button == button)
                                                            OnActivated();
                                                    };
        }

        public DebugWindow.UseButtons Button { get; private set; }
    }
}
