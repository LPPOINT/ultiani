using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Ultimate2D;

namespace UltimateAnimate
{
    public class VertexAnimationApplication : Game
    {

        private GraphicsDeviceManager graphics;
        private BasicEffect effect;
        private Texture2D texture;
        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private List<VertexPositionColor> pointsList;
        private List<int> lineList; 

        protected override void Initialize()
        {
            effect = new BasicEffect(graphics.GraphicsDevice);
            SetUpCamera();
            base.Initialize();
        }
        private void SetUpCamera()
        {
            viewMatrix = Matrix.Identity;
            projectionMatrix = Matrix.CreateOrthographic(Window.ClientBounds.Width, Window.ClientBounds.Height, -1.0f, 1.0f);
        }
        protected override void LoadContent()
        {

            Texture2DLoader.GraphicsDevice = GraphicsDevice;
            texture = Texture2DLoader.GetTexture("Cell.png");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.RasterizerState = RasterizerState.CullNone;  
            GraphicsDevice.BlendState = BlendState.AlphaBlend;    
            GraphicsDevice.DepthStencilState = DepthStencilState.None; 

            effect.View = viewMatrix;
            effect.Projection = projectionMatrix;
            effect.Texture = texture;
            effect.TextureEnabled = false;
            effect.DiffuseColor = Color.White.ToVector3();
            effect.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.DrawUserIndexedPrimitives(
                    PrimitiveType.LineList,
                    pointsList.ToArray(),
                    0, 
                    4,  
                    lineList.ToArray(), 
                    0,  
                    5);


            base.Draw(gameTime);
        }

      public VertexAnimationApplication()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef;


          pointsList = new List<VertexPositionColor>();
          pointsList.Add(new VertexPositionColor(new Vector3(0f, 0f, 0), Color.WhiteSmoke));
          pointsList.Add(new VertexPositionColor(new Vector3(0f, 20f, 0), Color.Red));
          pointsList.Add(new VertexPositionColor(new Vector3(20f, 20f, 0), Color.Black));
          pointsList.Add(new VertexPositionColor(new Vector3(20f, 0f, 0), Color.Green));

          lineList = new List<int>();

          lineList.Add(0);
          lineList.Add(1);

          lineList.Add(1);
          lineList.Add(2);

          lineList.Add(2);
          lineList.Add(3);

          lineList.Add(3);
          lineList.Add(4);

          lineList.Add(4);
          lineList.Add(2);

            
        }
    }
}