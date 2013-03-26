using Ultimate2D;

namespace UltimateTest
{
    public class Start
    {
        static void Main()
        {
            var game = new GameApplication(new GameApplicationInfo("Hello", false, false, true, 800, 600));
            game.CurrentGameState = new TestState();
            game.Run();
        }
    }
}
