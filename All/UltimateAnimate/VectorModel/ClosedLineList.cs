using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace UltimateAnimate.VectorModel
{
    public class ClosedLineList : LineList
    {

        private static Vector2[] GetVectors(Vector2[] points)
        {
            if (points[0] == points[points.Length - 1]) 
                return points;
            var list = new List<Vector2>(points) {points[0]};
            return list.ToArray();

        }

        public ClosedLineList(params Vector2[] points) 
            : base(GetVectors(points))
        {
        }
    }
}
