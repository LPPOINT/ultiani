namespace Ultimate2D
{
    public class GameApplicationInfo
    {
        public GameApplicationInfo(string windowTitle, bool isMouseVisible, bool isResizingAllowed, bool isFixedFps, int windowHeight, int windowWidth)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
            IsFixedFPS = isFixedFps;
            IsResizingAllowed = isResizingAllowed;
            IsMouseVisible = isMouseVisible;
            WindowTitle = windowTitle;
        }

        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public string WindowTitle { get; set; }
        public bool IsMouseVisible { get; set; }
        public bool IsResizingAllowed { get; set; }
        public bool IsFixedFPS { get; set; }
    }
}
