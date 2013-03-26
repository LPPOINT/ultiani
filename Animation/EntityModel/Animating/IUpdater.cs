using System;

namespace UltimateAnimate.EntityModel.Animating
{
    public interface IUpdater
    {
        EntityBase Entity { get; }

        void Update(TimeSpan delta);
    }
}
