using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UltimateAnimate.VectorModel
{
    public class LineList : ICloneable
    {
        private const int topLeft = 0;
        private const int topRight = 1;
        private const int bottomRight = 2;
        private const int bottomLeft = 3;

        public enum TranslationType
        {
            ByPoint,
            ByLine
        }
        public enum LineSide
        {
            Start,
            End
        }

        internal List<Line> lines;
        public IReadOnlyCollection<Line> Lines { get { return lines.AsReadOnly(); } }

        internal List<Vector2> points;
        public IReadOnlyCollection<Vector2> StartPoints { get { return points.AsReadOnly(); } }

        public Vector2[] CurrentPoints
        {
            get
            {
                return Lines.Select(line => line.Start).ToArray();
            }
        }

        public event EventHandler<EventArgs> TransformChanged;
        protected virtual void OnTransformChanged()
        {
            var handler = TransformChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void TranslateLine(int lineIndex, TranslationType type, Vector2 translate, LineSide side = LineSide.Start)
        {

            if (side == LineSide.End)
            {
                TranslateLine(lineIndex + 1, type, translate);
                return;
            }

                lines[lineIndex].Start += translate;
            if (type == TranslationType.ByLine)
                lines[lineIndex].End += translate;
            if (lineIndex != 0)
            {
                if (type == TranslationType.ByLine)
                    if (lineIndex < lines.Count - 1)
                        lines[lineIndex + 1].Start += translate;
                lines[lineIndex - 1].End = lines[lineIndex].Start;
            }
            OnTransformChanged();
        }

        private int count = -1;
        private Vector2 GetTextureCoordinate()
        {
            count++;
            if(count == 0) return new Vector2(0, 0);
            if(count == 1) return new Vector2(0, 1);
            if(count == 2) return new Vector2(1, 1);
            if(count == 3) return new Vector2(1, 0);
            count = -1;
            return GetTextureCoordinate();
        }
        public VertexPositionTexture[] GetTrangleStripVertexesPositions() 
        {
            return Lines.Select(line => new VertexPositionTexture(new Vector3(line.Start.X, line.Start.Y, 0), GetTextureCoordinate())).ToArray();
        }
        public int[] GetTrangleStripPrimitiveIndexses()
        {

            var  indexData = new int[6];
            indexData[0] = topLeft;
            indexData[1] = bottomRight;
            indexData[2] = bottomLeft;

            indexData[3] = topLeft;
            indexData[4] = topRight;
            indexData[5] = bottomRight;
            return indexData;
        }

        public Line this[int index]
        {
            get { return lines[index]; }
        }

        public LineList(params Vector2[] points)
        {
            lines = new List<Line>();
            this.points = new List<Vector2>(points);
            var curIndex = 0;
            foreach (var vector2 in points)
            {
                if (curIndex == points.Length) 
                    break;
                lines.Add(new Line(vector2, points[curIndex++]));
            }
        }

        public object Clone()
        {
            return new TransformatableBoneForm(new LineList(CurrentPoints.ToArray()));
        }
    }
}
