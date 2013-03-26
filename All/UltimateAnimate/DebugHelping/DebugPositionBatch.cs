using System;
using System.Collections.Generic;
using UltimateAnimate.Common;
using UltimateAnimate.Debug;
using UltimateAnimate.EntityModel;

namespace UltimateAnimate.DebugHelping
{
    public  class DebugPositionBatch
    {
        public  EntityIndexBatch Batch { get; set; }
        public  List<BaseEntity> Entitys { get; set; }

        public  void CreateBatching()
        {

            var timer = new SpendTimeInfo();
            timer.Start();

            DebugWindow.AddLine("----Debugging start!----");
            Batch.Begin();

            foreach (var entityBase in Entitys)
            {
               DebugWindow.AddLine("----BaseEntity added with id = " + entityBase.GlobalIndex + " ----");
               Batch.AddEntity(entityBase);
            }

            Batch.End();
            DebugWindow.AddLine("----Debugging complete! ( " + (int)timer.GetDuration().TotalMilliseconds + "ms  spended ) ----");
        }

        public  void Initialize(EntityIndexBatch batch, DebugInput batchingInput, params BaseEntity[] baseEntities)
        {
            Batch = batch;
            Entitys = new List<BaseEntity>(baseEntities);
            batchingInput.Activated += (sender, args) => CreateBatching();
        }

    }
}
