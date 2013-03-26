using System;
using System.Collections.Generic;

namespace UltimateAnimate.EntityModel.Animating
{
    public class BoneAnimationInfo : ICloneable
    {
        public Dictionary<int, Animation2Info> BoneAnimations { get; set; }

        public BoneAnimationInfo(Dictionary<int, Animation2Info> boneAnimations) 
        {
            BoneAnimations = boneAnimations;
        }

        public object Clone()
        {
            return new BoneAnimationInfo(new Dictionary<int, Animation2Info>(BoneAnimations));
        }
    }
}
