using UltimateAnimate.EntityModel.Animating;

namespace UltimateAnimate.VectorModel
{
    public class TransformatableBoneForm : LineList
    {
        public enum BoneTransformType
        {
            ByLine,
            ByPoint
        }

        public void TransformBone(int boneIndex, BoneTransformType transformType, Animation2Info transform)
        {
            
        }


        public TransformatableBoneForm(LineList lineList) 
            : base(lineList.points.ToArray())
        {
        }
    }
}
