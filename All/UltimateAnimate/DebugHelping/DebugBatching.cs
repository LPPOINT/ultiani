using UltimateAnimate.EntityModel;

namespace UltimateAnimate.DebugHelping
{
    public class DebugBatching : EntityIndexBatch
    {
        protected override int GetEntityPosition(BaseEntity baseEntity)
        {
            return baseEntity.GlobalIndex;
        }

        protected override void MakeAction(BaseEntity baseEntity)
        {
            Debug.DebugWindow.AddLine(" ------called BaseEntity with id = " + baseEntity.GlobalIndex);
        }
    }
}
