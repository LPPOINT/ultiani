using System;
using Microsoft.Xna.Framework;
using Ultimate2D;

namespace UltimateTest
{
    public class TestState : GameState
    {
        private SpriteRenderer spriteRenderer;

        public override void LoadContent()
        {
            var animation = new SpriteAnimation(new SpriteAnimationFrameFactory(100, 100), new TimeSpan(0, 0, 0, 1), 0, true);
            spriteRenderer = new SpriteRenderer(GraphicResourses.MainSpriteBatch2D, 
                new SpriteInfo(Texture2DLoader.GetTexture("Cell.png"), 0, 0, animation));
        }

        public override void Update(TimeSpan delta)
        {
           GraphicResourses.Graphics.Clear(Color.CornflowerBlue);
            spriteRenderer.Update();
        }

        public override void Render(TimeSpan delta)
        {
            spriteRenderer.Render();
        }
    }
}
