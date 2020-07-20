using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SharpMesh.Data
{
    /// <summary>
    /// 3 Point Vector class that can be any T scalar type.
    /// Implements Enumerable to work a little better.
    /// Readonly as a transition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SVector3<T> : IEnumerable
    {
        private readonly IList<T> _pointList;

        /// <summary>
        /// X coordinate
        /// </summary>
        public T X => _pointList[0];
        
        /// <summary>
        /// Y coordinate
        /// </summary>
        public T Y => _pointList[1];
        
        /// <summary>
        /// Z coordinate
        /// </summary>
        public T Z => _pointList[2];

        /// <summary>
        /// Constructor that can take in a IEnumerable<T> to populate data. Floats most likely.
        /// </summary>
        /// <param name="arr"></param>
        public SVector3(IEnumerable<T> arr)
        {
            _pointList = new Collection<T>();
            
            foreach (var items in arr)
            {
                _pointList.Add(items);
            }
        }
        
        /// <summary>
        /// Enumeration object for the vector list of points.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var point in _pointList)
            {
                yield return point;
            }
        }
 
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}