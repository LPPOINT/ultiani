using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using UltimateAnimate.EntityModel.Animating;


namespace UltimateAnimate.EntityModel
{
    public class CachePacket
    {
        public Dictionary<string, Vector2> Vectors { get; private set; }
        public Dictionary<string, float> Floats { get; private set; }
        public Dictionary<string, int> Ints { get; private set; }
        public Dictionary<string, bool> Bools { get; private set; }
        public Dictionary<string, TimeSpan> Times { get; private set; }
        public Dictionary<string, object> Objects { get; private set; }
        public Dictionary<string, Color> Colors { get; private set; }
        public Animation3Info Animating { get; set; }

        public static CachePacket CreateDefault(EntityBase entity)
        {
            var newCache = new CachePacket();

            newCache.Vectors.Add("position", entity.Position);
            newCache.Vectors.Add("scale", entity.Scale);
            newCache.Floats.Add("rotation", entity.Rotation);
            newCache.Colors.Add("color", entity.Color);
            newCache.Bools.Add("isVisible", entity.IsVisible);
            newCache.Bools.Add("isLogical", entity.IsLogical);
            newCache.Ints.Add("renderLayer", entity.RenderPosition);
            newCache.Ints.Add("updateLayer", entity.UpdatePosition);
            newCache.Animating = entity.Animating;

            return newCache;
        }
        public static void LoadDefault(EntityBase entity, CachePacket cache)
        {
            entity.Scale = cache.Vectors["scale"];
            entity.Position = cache.Vectors["position"];
            entity.Rotation = cache.Floats["rotation"];
            entity.Color = cache.Colors["color"];
            entity.IsLogical = cache.Bools["isLogical"];
            entity.IsVisible = cache.Bools["isVisible"];
            entity.RenderPosition = cache.Ints["renderLayer"];
            entity.UpdatePosition = cache.Ints["updateLayer"];
            entity.Animating = cache.Animating;

        }

        public CachePacket()
        {
            Colors = new Dictionary<string, Color>();
            Objects = new Dictionary<string, object>();
            Vectors = new Dictionary<string, Vector2>();
            Floats = new Dictionary<string, float>();
            Ints = new Dictionary<string, int>();
            Bools = new Dictionary<string, bool>();
            Times = new Dictionary<string, TimeSpan>();
        }
    }
}
