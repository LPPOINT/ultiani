using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Ultimate2D;
using UltimateAnimate.Debug;
using UltimateAnimate.EntityModel;
using UltimateAnimate.VectorModel;

namespace UltimateAnimate.DebugHelping
{
    public static class DebugHelp
    {

        public static BaseEntity Sample { get; private set; }

        public static DebugPositionBatch PositionBatchDebug { get; private set; }
        public static DebugEnityBatching EntityBatchingDebug { get; private set; }
        public static DebugEntityState EntityStateDebug { get; private set; }
        public static DebugEntityCache CacheDubug { get; private set; }
        public static DebugEntityAnimation AnimationDebug { get; private set; }
        public static DebugTimeLine DebugTimeLine { get; private set; }

        public static void Initialize()
        {
            if(DebugWindow.IsWindowCreated == false)
                DebugWindow.CreateDebugWindow();
            DebugTimeLine = new DebugTimeLine();
            CacheDubug = new DebugEntityCache();
            AnimationDebug = new DebugEntityAnimation();
            PositionBatchDebug = new DebugPositionBatch();
            EntityBatchingDebug = new DebugEnityBatching();
            EntityStateDebug = new DebugEntityState();
            Sample = new TexturableEntity(new QuadLineList(new Vector2(0, 0), 100, 100 ), Texture2DLoader.GetTexture("Cell.png"));
        }
        public static void SetupDefault(bool positionDebug, bool batchingDebug, bool stateDebug, bool animationDebug, bool cacheDebug = true, bool timeDebug = true)
        {
            if(timeDebug) DebugTimeLine.Initialize(new KeyValuePair<TimeSpan, string>(new TimeSpan(0, 0, 0, 1), "one second reached"), new KeyValuePair<TimeSpan, string>(new TimeSpan(0, 0,0,2), "two second reached" ));
            if(positionDebug) PositionBatchDebug.Initialize(new DebugBatching(), new DebugCommandInput("positionBatch: start batching by index"), Sample);
            if(batchingDebug) EntityBatchingDebug.Initialize(new DebugCommandInput("entityBatching: save cache"), new DebugCommandInput("entityBatching: loadCache"), Sample);
            if(stateDebug) EntityStateDebug.Initialize(Sample, new DebugCommandInput("entityState: split wireframe state"), new DebugCommandInput("entityState: split visible state"), new DebugCommandInput("entityState: split logic state"));
            if(animationDebug) AnimationDebug.Initialize(Sample, new DebugCommandInput("entityAnimation: split animations"));
            if(cacheDebug) CacheDubug.Initialize(new DebugCommandInput("entityCache: save"), new DebugCommandInput("entityCache: load"), Sample, false);
        }

    }
}
