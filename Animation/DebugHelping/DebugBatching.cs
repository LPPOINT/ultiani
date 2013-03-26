using UltimateAnimate.EntityModel;

namespace UltimateAnimate.DebugHelping
{
    public class DebugBatching : EntityPositionBatch
    {
        protected override int GetEntityPosition(EntityBase entity)
        {
            return entity.GlobalIndex;
        }

        protected override void MakeAction(EntityBase entity)
        {
            Debug.DebugWindow.AddLine(" ------called entity with id = " + entity.GlobalIndex);
        }
    }
}
