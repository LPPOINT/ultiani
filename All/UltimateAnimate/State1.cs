using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ultimate2D;
using UltimateAnimate.Debug;
using UltimateAnimate.VectorModel;

namespace UltimateAnimate
{
    public class State1 : GameState
    {
        private LineList lineList;
        private BasicEffect effect;
        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Texture2D texture;
        private readonly RasterizerState wireframeRasterizerState = new RasterizerState() { CullMode = CullMode.None, FillMode = FillMode.WireFrame };


        private bool texVisible = true;
        private bool wireframeVisible = true;

        private VertexPositionTexture[] positions;
        private int[] indexes;

        public override void LoadContent()
        {
            DebugWindow.CreateDebugWindow();
            effect = new BasicEffect(GraphicResourses.Graphics);

            texture = Texture2DLoader.GetTexture("Cell.png");

            lineList = new LineList(new Vector2( 100,  100), new Vector2( 100, 200),  new Vector2(200,  200), new Vector2(200,  100));
            lineList.TransformChanged += ReTransformate;

            viewMatrix = Matrix.Identity;
            projectionMatrix = Matrix.CreateOrthographic(800, 600, -1.0f, 1.0f);

            positions = lineList.GetTrangleStripVertexesPositions();
            indexes = lineList.GetTrangleStripPrimitiveIndexses();

            DebugWindow.UseButtonPressed += ChangeVisibleState;

        }

        private void SplitBool(ref bool value)
        {
            if (value) value = false;
            else if (!value) value = true;
        }

        private void ChangeVisibleState(object sender, UseButtonEventArgs useButtonEventArgs)
        {
            if(useButtonEventArgs.Button == DebugWindow.UseButtons.Button1) SplitBool(ref texVisible);
            if(useButtonEventArgs.Button == DebugWindow.UseButtons.Button2) SplitBool(ref wireframeVisible);
        }

        private void ReTransformate(object sender, EventArgs eventArgs)
        {
            positions = lineList.GetTrangleStripVertexesPositions();
            indexes = lineList.GetTrangleStripPrimitiveIndexses();
        }

        public override void Update(TimeSpan delta)
        {
           lineList.TranslateLine(1, LineList.TranslationType.ByLine,  new Vector2(0.04f, 0.04f));
        }
        public override void Render(TimeSpan delta)
        {
            GraphicResourses.Graphics.Clear(Color.CornflowerBlue);

            GraphicResourses.Graphics.RasterizerState = RasterizerState.CullNone;
            GraphicResourses.Graphics.BlendState = BlendState.AlphaBlend;
            GraphicResourses.Graphics.DepthStencilState = DepthStencilState.None;

            effect.View = viewMatrix;
            effect.Projection = projectionMatrix;
            effect.DiffuseColor = Color.White.ToVector3();
            effect.Texture = texture;
            effect.TextureEnabled = true;
            effect.CurrentTechnique.Passes[0].Apply();

            if (texVisible)
            {

                GraphicResourses.Graphics.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleStrip,
                    positions,
                    0,
                    positions.Length,
                    indexes,
                    0,
                    indexes.Length - 2);
            }

            GraphicResourses.Graphics.RasterizerState = wireframeRasterizerState;    
            GraphicResourses.Graphics.BlendState = BlendState.Opaque;                  

            effect.TextureEnabled = false;
            effect.DiffuseColor = Color.Black.ToVector3();
            effect.CurrentTechnique.Passes[0].Apply();

            if (wireframeVisible)
            {

                GraphicResourses.Graphics.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleStrip,
                    positions,
                    0,
                    positions.Length,
                    indexes,
                    0,
                    indexes.Length - 2);
            }

        }
    }
}
