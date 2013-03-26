using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimateAnimate.Common;

namespace UltimateAnimate.VectorModel
{
    public class LineList : ICloneable
    {
        private const int TOP_LEFT = 0;
        private const int TOP_RIGHT = 1;
        private const int BOTTOM_RIGHT = 2;
        private const int BOTTOM_LEFT = 3;

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

        public void Scale(Vector2 scale)
        {
            
        }
        public void Rotate(float angle)
        {
            
        }
        public void Translate(Vector2 translation)
        {
            foreach (var line in Lines)
            {
                line.Start += translation;
                OnTransformChanged();
            }
        }

        public void TranslateLine(int lineIndex, TranslationType type, Vector2 translate, LineSide side = LineSide.Start)
        {

            var timer = new SpendTimeInfo();
            timer.Start();

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
            var a = timer.GetDuration();
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
            var list = new List<VertexPositionTexture>();
            foreach (var line in Lines)
            {
                list.Add(new VertexPositionTexture(new Vector3(line.Start.X, line.Start.Y, 0), GetTextureCoordinate()));
            }
            return list.ToArray();
        }
        public int[] GetTrangleStripPrimitiveIndexses()
        {
            //var list = new List<int>();

            //for (var i = 0; i <= lines.Count-1; i++)
            //{
            //    list.Add(i);   
            //}
            //return list.ToArray();
            var  indexData = new int[6];
            indexData[0] = TOP_LEFT;
            indexData[1] = BOTTOM_RIGHT;
            indexData[2] = BOTTOM_LEFT;

            indexData[3] = TOP_LEFT;
            indexData[4] = TOP_RIGHT;
            indexData[5] = BOTTOM_RIGHT;
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
