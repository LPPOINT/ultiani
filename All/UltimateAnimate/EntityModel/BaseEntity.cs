using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Xna.Framework;
using Ultimate2D;
using UltimateAnimate.AnimationModel;
using UltimateAnimate.EntityModel.Animating;
using UltimateAnimate.VectorModel;


namespace UltimateAnimate.EntityModel
{
    public abstract class BaseEntity
    {

        public int RenderPosition { get; set; }
        public int UpdatePosition { get; set; }

        public TransformatableBoneForm Form { get; set; }

        private EntityHandler handler;
        public  EntityHandler Handler
        {
            get { return handler; }
            set
            {
                handler = value;
                Handler.Entity = this;
            }
        }

        public EntityAnimationInfo Animating { get; set; }

        public int GlobalIndex { get; private set; }
        private static int lastGI = 0;

        public bool IsLogical { get; set; }
        public bool IsVisible { get; set; }
        public bool IsWireframeVisible { get; set; }

        public BaseEntity Parent { get; internal set; }

        public List<BaseEntity> childs;
        public ReadOnlyCollection<BaseEntity> Childs { get { return childs.AsReadOnly(); } }

        public void AddChild(BaseEntity child)
        {
            childs.Add(child);
            child.Parent = this;
        }
        public void RemoveChild(BaseEntity child)
        {
            child.Parent = null;
            childs.Remove(child);
        }

        public BaseEntity GetChildById(int id)
        {
            return Childs.FirstOrDefault(baseEntity => baseEntity.GlobalIndex == id);
        }
        public BaseEntity GetChildByIndex(int index)
        {
            try
            {
                return childs[index];
            }
            catch (Exception e)
            {
                
            }
            return null;
        }

        public TimeSpan TotalVisibleTime { get; private set; }

        public abstract Vector2 Position { get; set; }
        public abstract float Rotation { get; set; }
        public abstract Vector2 Scale { get; set; }

        public abstract Color Color { get; set; }

        public event EventHandler<EntityEventArgs> ChildAdded;
        protected virtual void OnChildAdded(EntityEventArgs e)
        {
            var handler1 = ChildAdded;
            if (handler1 != null) handler1(this, e);
        }

        public event EventHandler<EntityEventArgs> ChildRemoved;
        protected virtual void OnChildRemoved(EntityEventArgs e)
        {
            var handler1 = ChildRemoved;
            if (handler1 != null) handler1(this, e);
        }

        public event EventHandler<EventArgs> RenderCalled;
        protected virtual void OnRenderCalled()
        {
            var handler = RenderCalled;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> RenderWireframeCalled; 
        protected virtual void OnRenderWireframeCalled()
        {
            var handler = RenderWireframeCalled;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> UpdateCalled;
        protected virtual void OnUpdateCalled()
        {
            var handler = UpdateCalled;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        internal void UpdateTick()
        {
            TotalVisibleTime += GameTimeInfo.LastUpdateDelta;
            if (IsLogical)
            {
                if(Animating != null)
                   UpdateAnimation(Animating);
                Update();
                OnUpdateCalled();
                foreach (var baseEntity in Childs)
                {
                    baseEntity.UpdateTick();
                }
            }
        }
        internal void RenderTick()
        {
            if (IsVisible)
            {
                Render();
                OnRenderCalled();
            }
            if (IsWireframeVisible)
            {
                RenderWireframe();
                OnRenderWireframeCalled();
            }
            if (IsVisible == false) return;
            foreach (var baseEntity in Childs)
            {
                baseEntity.RenderTick();
            }
        }

        protected Dictionary<TimeSpan, CachePacket> Cache; 
        public int CacheTimes { get; set; }

        public abstract void CreateCache(TimeSpan time);
        public abstract void LoadCache(CachePacket packet);

        public void LoadCache(TimeSpan time)
        {
            try
            {
                LoadCache(Cache[time]);
            }
            catch(Exception ex)
            {
                Debug.DebugWindow.AddLine(ex.Message);
            }
        }

        public bool HasCache(TimeSpan time)
        {
            CachePacket packet;
            Cache.TryGetValue(time, out packet);
            return packet != null;
        }
        public  CachePacket GetNearCache(TimeSpan time)
        {
            if (HasCache(time)) return Cache[time];
            return null; // TODO:
        }

        protected virtual void UpdateAnimation(EntityAnimationInfo animating)
        {
            Position += animating.PositionOffset * (float) GameTimeInfo.LastUpdateDelta.TotalMilliseconds;
            Rotation += animating.RotationOffset * (float)GameTimeInfo.LastUpdateDelta.TotalMilliseconds;
            Scale += animating.ScaleOffset * (float)GameTimeInfo.LastUpdateDelta.TotalMilliseconds;

            if(animating.BoneAnimationInfo == null) return;

            foreach (var kv in animating.BoneAnimationInfo.BoneAnimations)
            {
                Form.TranslateLine(kv.Key, LineList.TranslationType.ByPoint, kv.Value.PositionOffset);
            }

        }

        protected abstract void Update();
        protected abstract void Render();
        protected abstract void RenderWireframe();

        protected BaseEntity(LineList form)
        {
            Cache = new Dictionary<TimeSpan, CachePacket>();
            Form = new TransformatableBoneForm(form);
            childs = new List<BaseEntity>();
            IsLogical = true;
            IsVisible = true;
            IsWireframeVisible = true;
            GlobalIndex = lastGI++;
        }
    }

    public class EntityEventArgs : EventArgs
    {
        public EntityEventArgs(BaseEntity entity)
        {
            Entity = entity;
        }

        public BaseEntity Entity { get; private set; }
    }

}
