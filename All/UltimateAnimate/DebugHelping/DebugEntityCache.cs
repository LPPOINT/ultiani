using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Ultimate2D;
using UltimateAnimate.Debug;
using UltimateAnimate.EntityModel;
using UltimateAnimate.VectorModel;

namespace UltimateAnimate.DebugHelping
{
    public  class DebugEntityCache
    {
        public  BaseEntity BaseEntity { get; set; }
        private  TimeSpan lastCacheSaveTime;

        public  void SaveCache()
        {
            lastCacheSaveTime = GameTimeInfo.TotalTime;
            BaseEntity.CreateCache(lastCacheSaveTime);
            DebugWindow.AddLine("cache saved!");
        }
        public  void LoadCache()
        {
            BaseEntity.LoadCache(lastCacheSaveTime);
            DebugWindow.AddLine("cache loaded!");
        }

        public  void Initialize(DebugInput saveInput, DebugInput loadInput, BaseEntity baseEntity = null, bool animateEntiy = true)
        {
            if (baseEntity != null)
                BaseEntity = baseEntity;
            if(!DebugWindow.IsWindowCreated)
                DebugWindow.CreateDebugWindow();
            lastCacheSaveTime = new TimeSpan();

            loadInput.Activated += (sender, args) => LoadCache();
            saveInput.Activated += (sender, args) => SaveCache();


            if (animateEntiy)
            {
                if (baseEntity != null)
                    baseEntity.UpdateCalled += (sender, args) =>
                                               {
                                                   baseEntity.Form.TranslateLine(1, LineList.TranslationType.ByLine,
                                                                             new Vector2(0.1f, 0.1f));
                                                   baseEntity.Form.TranslateLine(3, LineList.TranslationType.ByLine,
                                                                             new Vector2(-0.1f, -0.1f));
                                               };
            }


        }

    }
}
