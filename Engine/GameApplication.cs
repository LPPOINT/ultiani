using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ultimate2D
{
    public class GameApplication : Game
    {
        private readonly GraphicsDeviceManager graphics;

        private GameState currentGameState;
        public GameState CurrentGameState
        {
            get
            {
                return currentGameState;
            }
            set
            {
                currentGameState = value;
                currentGameState.Application = this;
            }

        }


        protected override void LoadContent()
        {
 
            base.LoadContent();
            GraphicResourses.Graphics = graphics.GraphicsDevice;
            GraphicResourses.MainSpriteBatch2D = new SpriteBatch(GraphicResourses.Graphics);
            CurrentGameState.LoadContent();
        }
        protected override void UnloadContent()
        {
            base.UnloadContent();
            CurrentGameState.UnloadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            GameTimeInfo.TotalTime = gameTime.TotalGameTime;
            GameTimeInfo.LastUpdateDelta = gameTime.ElapsedGameTime;
            CurrentGameState.UpdateTick(gameTime.ElapsedGameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GameTimeInfo.LastRenderDelta = gameTime.ElapsedGameTime;
            GraphicResourses.MainSpriteBatch2D.Begin();
            CurrentGameState.RenderTick(gameTime.ElapsedGameTime);
            GraphicResourses.MainSpriteBatch2D.End();
            base.Draw(gameTime);
        }

        public GameApplication(GameApplicationInfo info)
        {

            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            IsMouseVisible = info.IsMouseVisible;
            Window.Title = info.WindowTitle;
            Window.AllowUserResizing = info.IsResizingAllowed;

        }

    }
}
