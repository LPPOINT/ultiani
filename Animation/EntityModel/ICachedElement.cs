namespace UltimateAnimate.EntityModel
{
    public interface ICachedElement
    {
        void SaveCache(CachePacket cache);
        void RestoreCache(CachePacket cache);
    }
}
