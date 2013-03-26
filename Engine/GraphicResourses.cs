using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ultimate2D
{
    public static class GraphicResourses
    {
        public static SpriteBatch MainSpriteBatch2D { get; internal set; }

        private static GraphicsDevice graphics;
        public static GraphicsDevice Graphics
        {
            get
            {
                return graphics;
            }
            internal
            set
            {
                graphics = value;
                TextureLoader = new Texture2DLoader();
                Texture2DLoader.GraphicsDevice = value;
            }
        }

        public static Matrix ViewMatrix { get; internal set; }
        public static Matrix ProjectionMatrix { get; internal set; }

        public static Texture2DLoader TextureLoader { get; internal set; }




    }
}
