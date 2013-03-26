using UltimateAnimate.Debug;
using UltimateAnimate.EntityModel;

namespace UltimateAnimate.DebugHelping
{
    public static class DebugEntityState
    {
        public static EntityBase Entity { get; set; }

        private static bool SplitBool(bool value)
        {
            return !value;
        }

        public static void Initialize(EntityBase entity, DebugInput wireframeState, DebugInput visibleState, DebugInput logicState = null)
        {
            Entity = entity;

            wireframeState.Activated +=
                (sender, args) => Entity.IsWireframeVisible = SplitBool(Entity.IsWireframeVisible);
            visibleState.Activated += 
                (sender, args) => Entity.IsVisible = SplitBool(Entity.IsVisible);

            if (logicState!=null)
            {
                logicState.Activated += (sender, args) => Entity.IsLogical = SplitBool(Entity.IsLogical);
            }
        }

    }
}
