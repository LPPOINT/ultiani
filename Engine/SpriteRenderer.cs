using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ultimate2D
{
    public class SpriteRenderer : IVisual
    {

        public SpriteBatch Batch { get;  set; }
        public SpriteInfo Info { get;  set; }

        public void Update()
        {
            if(Info.Animation!=null)
                Info.Animation.UpdateAnimation(GameTimeInfo.LastUpdateDelta);
        }
        public void Render()
        {
            Batch.Draw(Info.Texture,
                new Vector2(Info.X, Info.Y), 
                Info.Source,
                Info.SpriteColor, 
                Info.Rotation,
                Info.Origin,
                new Vector2(Info.ScaleX, Info.ScaleY),
                Info.Effects,
                Info.Layer);
        }

        public SpriteRenderer()
        {
        }
        public SpriteRenderer(SpriteBatch batch, SpriteInfo info)
        {
            Info = info;
            Batch = batch;
        }
    }
}
