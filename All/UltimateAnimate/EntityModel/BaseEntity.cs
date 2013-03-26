using System;
using System.Collections.Generic;
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

        public TimeSpan TotalVisibleTime { get; private set; }

        public abstract Vector2 Position { get; set; }
        public abstract float Rotation { get; set; }
        public abstract Vector2 Scale { get; set; }

        public abstract Color Color { get; set; }

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
            Position += animating.PositionOffset;
            Rotation += animating.RotationOffset;
            Scale += animating.ScaleOffset;

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
            IsLogical = true;
            IsVisible = true;
            IsWireframeVisible = true;
            GlobalIndex = lastGI++;
        }
    }
}
