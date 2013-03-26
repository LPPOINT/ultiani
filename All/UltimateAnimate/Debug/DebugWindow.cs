using System;
using System.Collections.Generic;

namespace UltimateAnimate.Debug
{
    public static class DebugWindow
    {

        public enum UseButtons
        {
            Button1,
            Button2,
            Button3
        }

        public static bool IsWindowCreated { get; private set; }
        private static DebugForm debugForm;

        public static event EventHandler<UseButtonEventArgs> UseButtonPressed;
        internal static void OnUseButtonPressed(UseButtonEventArgs e)
        {
            var handler = UseButtonPressed;
            if (handler != null) handler(null, e);
        }

        public static event EventHandler<UseButtonEventArgs> UseButtonMouseOver;
        internal static void OnUseButtonMouseOver(UseButtonEventArgs e)
        {

            foreach (var info in infos)
            {
                if(info.Key == e.Button)
                    debugForm.SetInfoText(info.Value);
            }

            var handler = UseButtonMouseOver;
            if (handler != null) handler(null, e);
        }

        public static event EventHandler<CommandSubmittedEventArgs> CommandSubmitted;
        internal static void OnCommandSubmitted(CommandSubmittedEventArgs e)
        {

            foreach (var assot in assots)
            {
                if(assot.Value == e.Command) 
                    OnUseButtonPressed(new UseButtonEventArgs(assot.Key));

            }

            var handler = CommandSubmitted;
            if (handler != null) 
                handler(null, e);
        }

        public static event EventHandler<EventArgs> WindowCreated;
        private static void OnWindowCreated()
        {
            var handler = WindowCreated;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        private static readonly Dictionary<UseButtons, string> assots = new Dictionary<UseButtons, string>(); 
        private static readonly Dictionary<UseButtons, string> infos = new Dictionary<UseButtons, string>(); 

        public static void AssosicateUseButton(UseButtons button, string command)
        {
            try
            {
                
                assots.Add(button, command);
            }
            catch (ArgumentException)
            {
                RemoveCommand(assots[button]);
                assots[button] = command;
            }

            AddCommand(command);
        }
        public static void SetButtonInfo(UseButtons button, string info)
        {
            infos.Add(button, info);
        }

        public static void CreateDebugWindow()
        {
            if (IsWindowCreated) return; 
            IsWindowCreated = true;
            debugForm = new DebugForm();
            debugForm.Show();
            OnWindowCreated();
        }
        public static void AddLine(string line)
        {
            debugForm.AddLine(line);
        }
        public static void AddCommand(string command)
        {
            debugForm.AddCommand(command);
        }
        public static void RemoveCommand(string command)
        {
            debugForm.RemoveCommand(command);
        }

    }
}
