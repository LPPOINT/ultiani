using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ultimate2D;
using UltimateAnimate.EntityModel.Animating;
using UltimateAnimate.VectorModel;

namespace UltimateAnimate.EntityModel
{
    public class VectorSpriteEntity : EntityBase
    {


        public Texture2D Texture { get; private set; }
        public BasicEffect Effect { get; private set; }

        private readonly RasterizerState wireframeRasterizerState = new RasterizerState { CullMode = CullMode.None, FillMode = FillMode.WireFrame };
        public Matrix ViewMatrix { get; private set; }
        public Matrix ProjectionMatrix { get; private set; }

        private VertexPositionTexture[] positions;
        private int[] indexes;

        private Vector2 position;
        public override Vector2 Position
        {
            get { return position; } 
            set
            {
                position = value;

            }
        }

        private float rotation;
        public override float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;

            }
        }

        private Vector2 scale;
        public override Vector2 Scale
        {
            get { return scale; }
            set
            {
                scale = value;

            }
        }

        private Color color;
        public override Color Color
        {
            get { return color; }
            set
            {
                color = value;
               
            }
        }


        public override void CreateCache(TimeSpan time)
        {
            var packet = CachePacket.CreateDefault(this);
            packet.Objects.Add("boning", Form.Clone());
            Cache.Add(time, packet);

        }
        public override void LoadCache(CachePacket packet)
        {
            Form = packet.Objects["boning"] as TransformatableBoneForm;
            Form.TransformChanged += UpdateTransform;
            CachePacket.LoadDefault(this, packet);
            UpdateTransform(null, null);
        }

        protected override void UpdateAnimation(Animation3Info animating)
        {
            Position = Position + (animating.PositionOffset * (float)GameTimeInfo.LastUpdateDelta.TotalMilliseconds);
            Rotation += animating.RotationOffset * (float)GameTimeInfo.LastUpdateDelta.TotalMilliseconds;
            Scale = Scale + (animating.ScaleOffset*(float) GameTimeInfo.LastRenderDelta.TotalMilliseconds);
        }
        private void UpdateTransform(object sender, EventArgs eventArgs)
        {
            positions = Form.GetTrangleStripVertexesPositions();
            indexes = Form.GetTrangleStripPrimitiveIndexses();
        }

        protected override void Update()
        {

        }
        protected override void Render()
        {
            GraphicResourses.Graphics.RasterizerState = RasterizerState.CullNone;
            GraphicResourses.Graphics.BlendState = BlendState.AlphaBlend;
            GraphicResourses.Graphics.DepthStencilState = DepthStencilState.None;

            Effect.View = ViewMatrix;
            Effect.Projection = ProjectionMatrix;
            Effect.DiffuseColor = Color.White.ToVector3();
            Effect.Texture = Texture;
            Effect.TextureEnabled = true;
            Effect.CurrentTechnique.Passes[0].Apply();

            GraphicResourses.Graphics.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleStrip,
                    positions,
                    0,
                    positions.Length,
                    indexes,
                    0,
                    indexes.Length - 2);
        }

        protected override void RenderWireframe()
        {
            GraphicResourses.Graphics.RasterizerState = wireframeRasterizerState;
            GraphicResourses.Graphics.BlendState = BlendState.Opaque;

            Effect.TextureEnabled = false;
            Effect.DiffuseColor = Color.Black.ToVector3();
            Effect.CurrentTechnique.Passes[0].Apply();


            GraphicResourses.Graphics.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleStrip,
                    positions,
                    0,
                    positions.Length,
                    indexes,
                    0,
                    indexes.Length - 2);

        }

        public VectorSpriteEntity(LineList form, Texture2D texture) : base(form)
        {

            Form.TransformChanged += UpdateTransform;
            Texture = texture;
            Effect = new BasicEffect(GraphicResourses.Graphics);
   
            ViewMatrix = Matrix.Identity;
            ProjectionMatrix = Matrix.CreateOrthographic(800, 600, -1.0f, 1.0f);

            UpdateTransform(null, null);

        }

    }
}
