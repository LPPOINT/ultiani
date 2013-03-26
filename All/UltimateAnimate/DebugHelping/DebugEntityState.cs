using UltimateAnimate.Debug;
using UltimateAnimate.EntityModel;

namespace UltimateAnimate.DebugHelping
{
    public  class DebugEntityState
    {
        public  BaseEntity BaseEntity { get; set; }

        private  bool SplitBool(bool value)
        {
            return !value;
        }

        public  void Initialize(BaseEntity baseEntity, DebugInput wireframeState, DebugInput visibleState, DebugInput logicState = null)
        {
            BaseEntity = baseEntity;

            wireframeState.Activated +=
                (sender, args) => BaseEntity.IsWireframeVisible = SplitBool(BaseEntity.IsWireframeVisible);
            visibleState.Activated += 
                (sender, args) => BaseEntity.IsVisible = SplitBool(BaseEntity.IsVisible);

            if (logicState!=null)
            {
                logicState.Activated += (sender, args) => BaseEntity.IsLogical = SplitBool(BaseEntity.IsLogical);
            }
        }

    }
}
