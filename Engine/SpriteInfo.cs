using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ultimate2D
{
        public class SpriteInfo

        {
            public Texture2D Texture { get; private set; }
            public SpriteEffects Effects { get; set; }
            public Color SpriteColor { get; set; }
            public Rectangle Source { get; set; }

            public Rectangle SpriteRectangle { get; private set; }

            private float x;
            private float y;

            public float X { get { return x; } set { x = value; OnPositionChanged(); } }
            public float Y { get { return y; } set { y = value; OnPositionChanged(); } }

            private float width;
            private float height;

            public float Width
            {
                get
                {
                    return width;
                }
                set
                {
                    if (value < 0)
                        value = 0;
                    width = value;
                    UpdateScaleBySize(Width, Height);
                }
            }
            public float Height
            {
                get { return height; }
                set
                {
                    if (value < 0)
                        value = 0;
                    height = value;
                    UpdateScaleBySize(Width, Height);
                }
            }

            private float scaleX;
            private float scaleY;

            public float ScaleX { get { return scaleX; } set { scaleX = value; UpdateSizeByScale(ScaleX, ScaleY); } }
            public float ScaleY { get { return scaleY; } set { scaleY = value; UpdateSizeByScale(ScaleX, ScaleY); } }

            public bool IsMouseOver
            {
                get
                {
                    var mouse = Mouse.GetState();
                    return SpriteRectangle.Contains(mouse.X, mouse.Y);
                }
            }

            public Vector2 Origin { get; set; }
            public float Rotation { get; set; }
            public float Layer { get; set; }

            public SpriteAnimation Animation { get; private set; }

            public event EventHandler<EventArgs> PositionChanged;
            protected virtual void OnPositionChanged()
            {
                var handler = PositionChanged;
                if (handler != null) handler(this, EventArgs.Empty);
            }

            public event EventHandler<EventArgs> SizeChanged;
            protected virtual void OnSizeChanged()
            {
                var handler = SizeChanged;
                if (handler != null) handler(this, EventArgs.Empty);
            }

            private void UpdateScaleBySize(float sizeW, float sizeH)
            {
                scaleX = sizeW / Texture.Width;
                scaleY = sizeH / Texture.Height;
                OnSizeChanged();
            }
            private void UpdateSizeByScale(float scaleW, float scaleH)
            {
                width = Texture.Width / scaleW;
                height = Texture.Height / scaleH;
                OnSizeChanged();
            }
            private void UpdateRectangle(object sender, EventArgs eventArgs)
            {
                SpriteRectangle = new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
            }


            public SpriteInfo(Texture2D texture, float x = 0, float y = 0, SpriteAnimation animation = null) : this(texture, SpriteEffects.None, Color.White, new Rectangle(0, 0, texture.Width, texture.Height), x, y, 1, 1, new Vector2(0, 0), 0, 1, animation  )
            {
                
            }
            public SpriteInfo(Texture2D texture, SpriteEffects effects, Color spriteColor, Rectangle source, float x, float y, float scaleX, float scaleY, Vector2 origin, float rotation, float layer, SpriteAnimation animation = null)
            {
                Animation = animation;
                if (Animation != null) Animation.Sprite = this;
                Texture = texture;
                Effects = effects;
                SpriteColor = spriteColor;
                Source = source;
                X = x;
                Y = y;
                ScaleX = scaleX;
                ScaleY = scaleY;
                Origin = origin;
                Rotation = rotation;
                Layer = layer;
                UpdateSizeByScale(ScaleX, ScaleY);

                PositionChanged += UpdateRectangle;
                SizeChanged += UpdateRectangle;
                UpdateRectangle(null, null);
            }
        }
}
