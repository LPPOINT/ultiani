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
        public EntityAnimationInfo Animating { get; set; }

        public static CachePacket CreateDefault(BaseEntity baseEntity)
        {
            var newCache = new CachePacket();

            newCache.Vectors.Add("position", baseEntity.Position);
            newCache.Vectors.Add("scale", baseEntity.Scale);
            newCache.Floats.Add("rotation", baseEntity.Rotation);
            newCache.Colors.Add("color", baseEntity.Color);
            newCache.Bools.Add("isVisible", baseEntity.IsVisible);
            newCache.Bools.Add("isLogical", baseEntity.IsLogical);
            newCache.Ints.Add("renderLayer", baseEntity.RenderPosition);
            newCache.Ints.Add("updateLayer", baseEntity.UpdatePosition);
            newCache.Animating = baseEntity.Animating.Clone() as EntityAnimationInfo;

            return newCache;
        }
        public static void LoadDefault(BaseEntity baseEntity, CachePacket cache)
        {
            baseEntity.Scale = cache.Vectors["scale"];
            baseEntity.Position = cache.Vectors["position"];
            baseEntity.Rotation = cache.Floats["rotation"];
            baseEntity.Color = cache.Colors["color"];
            baseEntity.IsLogical = cache.Bools["isLogical"];
            baseEntity.IsVisible = cache.Bools["isVisible"];
            baseEntity.RenderPosition = cache.Ints["renderLayer"];
            baseEntity.UpdatePosition = cache.Ints["updateLayer"];
            baseEntity.Animating = cache.Animating;

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
