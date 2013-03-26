using System;
using System.Collections.Generic;
using System.Linq;

namespace UltimateAnimate.EntityModel
{
    public abstract class EntityPositionBatch
    {

        protected abstract int GetEntityPosition(EntityBase entity);
        protected abstract void MakeAction(EntityBase entity);

        private readonly List<EntityBase> entitys; 

        public void Begin()
        {
            entitys.Clear();
        }
        public void AddEntity(EntityBase entity)
        {
            entitys.Add(entity);
        }
        public void End()
        {
            var list = new List<int>();
            foreach (var entityBase in entitys)
            {
                list.Add(GetEntityPosition(entityBase));
            }
            var arr = list.ToArray();
            Array.Sort(arr);
            var updateList = (from i in arr from entityBase in entitys where GetEntityPosition(entityBase) == i select entityBase).ToList();
            foreach (var entityBase in updateList)
            {
                MakeAction(entityBase);
            }
        }

        protected EntityPositionBatch()
        {
            entitys = new List<EntityBase>();
        }
    }
}
