using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Ultimate2D;
using UltimateAnimate.Debug;
using UltimateAnimate.EntityModel;
using UltimateAnimate.VectorModel;

namespace UltimateAnimate.DebugHelping
{
    public static class DebugEntityCache
    {
        public static EntityBase Entity { get; set; }
        private static TimeSpan lastCacheSaveTime;

        public static void SaveCache()
        {
            lastCacheSaveTime = GameTimeInfo.TotalTime;
            Entity.CreateCache(lastCacheSaveTime);
        }
        public static void LoadCache()
        {
            Entity.LoadCache(lastCacheSaveTime);
        }

        public static void Initialize(DebugInput saveInput, DebugInput loadInput, EntityBase entity = null, bool animateEntiy = true)
        {
            if (entity != null)
                Entity = entity;
            if(!DebugWindow.IsWindowCreated)
                DebugWindow.CreateDebugWindow();
            lastCacheSaveTime = new TimeSpan();

            loadInput.Activated += (sender, args) => LoadCache();
            saveInput.Activated += (sender, args) => SaveCache();


            if (animateEntiy)
            {
                if (entity != null)
                    entity.UpdateCalled += (sender, args) =>
                                               {
                                                   entity.Form.TranslateLine(1, LineList.TranslationType.ByLine,
                                                                             new Vector2(0.1f, 0.1f));
                                                   entity.Form.TranslateLine(3, LineList.TranslationType.ByLine,
                                                                             new Vector2(-0.1f, -0.1f));
                                               };
            }


        }

    }
}
