using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UltimateCheckers
{
    public class BoardObjectCollection<T> : IEnumerable<T> where T : IBoardObject
    {
        internal List<T> objects;
        public ReadOnlyCollection<T> Objects { get { return objects.AsReadOnly(); } } 

        public T GetObjectByPosition(BoardObjectPosition position)
        {
            foreach (var obj in objects.Where(obj => obj.Position == position))
            {
                return obj;
            }
            return default(T);
        }
        public ReadOnlyCollection<T> GetObjectsByColor(GameColor color)
        {
            var list = Objects.Where(o => o.Color == color).ToList();
            return list.AsReadOnly();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return objects.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public BoardObjectCollection()
        {
            objects = new List<T>();
        }
    }
}
