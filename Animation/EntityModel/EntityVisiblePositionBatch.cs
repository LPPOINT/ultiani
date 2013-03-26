namespace UltimateAnimate.EntityModel
{
    public class EntityVisiblePositionBatch : EntityPositionBatch
    {
        protected override int GetEntityPosition(EntityBase entity)
        {
            return entity.RenderPosition;
        }
        protected override void MakeAction(EntityBase entity)
        {
            entity.RenderTick();
        }
    }
}
