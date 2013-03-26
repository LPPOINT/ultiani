using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Ultimate2D
{
    public class Texture2DLoader
    {

        public static GraphicsDevice GraphicsDevice { get; set; }
        private static readonly Dictionary<string, Texture2D> loadedTextures = new Dictionary<string, Texture2D>();

        private static Texture2D GetTextureFromLoadedTextures(string path)
        {
            try
            {
                return loadedTextures[path];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Texture2D GetTexture(string path)
        {

            if (GetTextureFromLoadedTextures(path) == null)
            {
                var tex = CreateTexture2D(path);
                return tex;
            }
            return GetTextureFromLoadedTextures(path);
        }
        private static Texture2D CreateTexture2D(string path)
        {
            Texture2D tex = null;
            try
            {
                tex = Texture2D.FromStream(GraphicsDevice, new FileStream(path, FileMode.Open));
                loadedTextures[path] = tex;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return tex;
        }
    }
}
