using Microsoft.Xna.Framework;

namespace UltimateAnimate.EntityModel.Animating
{
    public class EntityAnimationInfo : Animation3Info
    {
        public BoneAnimationInfo BoneAnimationInfo { get; set; }

        public override object Clone()
        {
            var baseClone = base.Clone() as Animation3Info;
            if (baseClone != null)
            {
                BoneAnimationInfo boneInfo = null;
                if (BoneAnimationInfo != null)
                {
                    var bone = BoneAnimationInfo.Clone();
                        if (bone is BoneAnimationInfo)
                            boneInfo = bone as BoneAnimationInfo;

                }
                return new EntityAnimationInfo(baseClone.PositionOffset, baseClone.RotationOffset, baseClone.ScaleOffset,
                                               boneInfo);
            }
            return null;
        }

        public EntityAnimationInfo(Vector2 positionOffset, float rotationOffset, Vector2 scaleOffset, BoneAnimationInfo boneAnimationInfo)
            : base(positionOffset, rotationOffset, scaleOffset)
        {
            BoneAnimationInfo = boneAnimationInfo;
        }
    }
}
