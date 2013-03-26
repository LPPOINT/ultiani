using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ultimate2D;
using UltimateAnimate.VectorModel;

namespace UltimateAnimate.EntityModel
{
    public class TexturableEntity : BaseEntity
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
                var translation = position - value;
                position = value;
                var index = 0;
                foreach (var line in Form.Lines)
                {
                   Form.TranslateLine(index, LineList.TranslationType.ByPoint, translation);
                   index++;
                }

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
            var formClone = Form.Clone();
            if(formClone == null)
                throw new Exception("create cahce error: cant find form clone");
            packet.Objects.Add("boning", formClone);
            Cache.Add(time, packet);

        }
        public override void LoadCache(CachePacket packet)
        {
            Form = packet.Objects["boning"] as TransformatableBoneForm;
            if (Form != null)
                Form.TransformChanged += UpdateTransform;
            else throw new Exception("Load cache error: cant find entity form");
            CachePacket.LoadDefault(this, packet);
            UpdateTransform(null, null);
        }

        private void UpdateTransform(object sender, EventArgs eventArgs)
        {
            positions = Form.GetTrangleStripVertexesPositions();
            indexes = Form.GetTrangleStripPrimitiveIndexses();
            if(positions == null || indexes == null)
                throw new Exception("entity update transform error: bad form indexes or primitiveses");
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

        public TexturableEntity(LineList form, Texture2D texture) : base(form)
        {

            Form.TransformChanged += UpdateTransform;
            Texture = texture;
            Effect = new BasicEffect(GraphicResourses.Graphics);
   
            ViewMatrix = Matrix.Identity;
            ProjectionMatrix = Matrix.CreateOrthographic(600, 800, -1.0f, 1.0f);

            UpdateTransform(null, null);

        }

    }
}
