using Ultimate2D;
using UltimateAnimate.Debug;
using UltimateAnimate.EntityModel;

namespace UltimateAnimate.DebugHelping
{
    public  class DebugEnityBatching
    {
        public  EntityBatch Batch { get; private set; }

        public  void Render()
        {
            Batch.Render();
        }
        public  void Update()
        {
            Batch.Update();
        }

        public  void Initialize(DebugInput saveCacheCmd, DebugInput loadCacheCmd, params BaseEntity[] entities)
        {
            Batch = new EntityBatch();

            foreach (var baseEntity in entities)
            {
                Batch.Add(baseEntity);
            }

            saveCacheCmd.Activated += (sender, args) => Batch.SaveCache(GameTimeInfo.TotalTime);
            loadCacheCmd.Activated += (sender, args) => Batch.LoadCache(GameTimeInfo.TotalTime);
        }
    }
}
