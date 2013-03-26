using System;
using System.Collections.Generic;
using System.Linq;

namespace UltimateAnimate.EntityModel
{
    public abstract class EntityIndexBatch
    {

        protected abstract int GetEntityPosition(BaseEntity baseEntity);
        protected abstract void MakeAction(BaseEntity baseEntity);

        private readonly List<BaseEntity> entitys; 

        public void Begin()
        {
            entitys.Clear();
        }
        public void AddEntity(BaseEntity baseEntity)
        {
            entitys.Add(baseEntity);
        }
        public void End()
        {
            var arr = entitys.Select(GetEntityPosition).ToArray();
            Array.Sort(arr);
            var updateList = (from i in arr from entityBase in entitys where GetEntityPosition(entityBase) == i select entityBase).ToList();
            foreach (var entityBase in updateList)
            {
                MakeAction(entityBase);
            }
        }

        protected EntityIndexBatch()
        {
            entitys = new List<BaseEntity>();
        }
    }

    public class RenderBatch : EntityIndexBatch
    {
        protected override int GetEntityPosition(BaseEntity baseEntity)
        {
            return baseEntity.RenderPosition;
        }
        protected override void MakeAction(BaseEntity baseEntity)
        {
            baseEntity.RenderTick();
        }
    }
    public class UpdateBatch : EntityIndexBatch
    {
        protected override int GetEntityPosition(BaseEntity baseEntity)
        {
            return baseEntity.UpdatePosition;
        }
        protected override void MakeAction(BaseEntity baseEntity)
        {
            baseEntity.UpdateTick();
        }
    }
}
