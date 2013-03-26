using System;
using Ultimate2D;

namespace UltimateAnimate
{
    class Start
    {
        static void Main()
        {
            var game = new GameApplication(new GameApplicationInfo("Hello", true, false, false, 800, 600));
            game.CurrentGameState = new State2();
            game.Run();

        }
    }
}
