using System;
using System.Collections;
using System.Collections.Generic;

namespace UltimateAnimate.EntityModel
{
    public class EntityBatch : IList<BaseEntity>
    {
        public EntityIndexBatch UpdateBatch { get; private set; }
        public EntityIndexBatch RenderBatch { get; private set; }

        private readonly List<BaseEntity> entities; 

        public void Update()
        {
            UpdateBatch.Begin();
            foreach (var baseEntity in entities)
            {
                UpdateBatch.AddEntity(baseEntity);
            }
            UpdateBatch.End();
        }
        public void Render()
        {
            RenderBatch.Begin();
            foreach (var baseEntity in entities)
            {
                RenderBatch.AddEntity(baseEntity);
            }
            RenderBatch.End();
        }

        public void SaveCache(TimeSpan cacheTime)
        {
            foreach (var baseEntity in entities)
            {
                baseEntity.CreateCache(cacheTime);
            }
        }
        public void LoadCache(TimeSpan cacheTime)
        {
            foreach (var baseEntity in entities)
            {
                baseEntity.LoadCache(cacheTime);
            }
        }

        public IEnumerator<BaseEntity> GetEnumerator()
        {
            return entities.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(BaseEntity item)
        {
            entities.Add(item);
        }
        public void Clear()
        {
            entities.Clear();
        }
        public bool Contains(BaseEntity item)
        {
            return entities.Contains(item);
        }
        public void CopyTo(BaseEntity[] array, int arrayIndex)
        {
            entities.CopyTo(array, arrayIndex);
        }
        public bool Remove(BaseEntity item)
        {
            return entities.Remove(item);
        }
        public int Count { get { return entities.Count; } }
        public bool IsReadOnly { get { return false; } }
        public int IndexOf(BaseEntity item)
        {
            return entities.IndexOf(item);
        }
        public void Insert(int index, BaseEntity item)
        {
            entities.Insert(index, item);
        }
        public void RemoveAt(int index)
        {
            entities.RemoveAt(index);
        }

        public BaseEntity this[int listIndex]
        {
            get { return entities[listIndex]; }
            set { entities[listIndex] = value; }
        }

        public EntityBatch()
        {
            entities = new List<BaseEntity>();
            UpdateBatch = new UpdateBatch();
            RenderBatch = new RenderBatch();
        }
    }
}
