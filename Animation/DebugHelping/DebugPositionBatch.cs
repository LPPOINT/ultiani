using System;
using System.Collections.Generic;
using UltimateAnimate.Common;
using UltimateAnimate.Debug;
using UltimateAnimate.EntityModel;

namespace UltimateAnimate.DebugHelping
{
    public static class DebugPositionBatch
    {
        public static EntityPositionBatch PositionBatch { get; set; }
        public static List<EntityBase> Entitys { get; set; }

        public static void CreateBatching()
        {

            var timer = new SpendTimeInfo();
            timer.Start();

            DebugWindow.AddLine("----Debugging start!----");
            PositionBatch.Begin();

            foreach (var entityBase in Entitys)
            {
               DebugWindow.AddLine("----Entity added with id = " + entityBase.GlobalIndex + " ----");
               PositionBatch.AddEntity(entityBase);
            }

            PositionBatch.End();
            DebugWindow.AddLine("----Debugging complete! ( " + (int)timer.GetDuration().TotalMilliseconds + "ms  spended ) ----");
        }

        public static void Initialize(EntityPositionBatch positionBatch, DebugInput batchingInput, params EntityBase[] entitys)
        {
            PositionBatch = positionBatch;
            Entitys = new List<EntityBase>(entitys);
            batchingInput.Activated += (sender, args) => CreateBatching();
        }

    }
}
