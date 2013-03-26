namespace UltimateAnimate.EntityModel
{
    public class EntityLogicPositionBatch : EntityPositionBatch
    {
        protected override int GetEntityPosition(EntityBase entity)
        {
            return entity.UpdatePosition;
        }
        protected override void MakeAction(EntityBase entity)
        {
            entity.UpdateTick();
        }
    }
}
