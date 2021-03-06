using System.Collections;
using System.Collections.Generic;

namespace SharpMesh.Data
{
    /// <summary>
    /// 3 Point Vector class that can be any T scalar type.
    /// Implements Enumerable to work a little better.
    /// Readonly as a transition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Vector<T> : IEnumerable
    {
        private readonly IList<T> _pointList;
        
        /// <summary>
        /// Indexer for the points 1-4.
        /// Will return any value of type T.
        /// </summary>
        /// <param name="index"></param>
        public T this[int index]
        {
            get => _pointList[index];
            set => _pointList[index] = value;
        }

        /// <summary>
        /// X coordinate
        /// </summary>
        public T X
        {
            get => _pointList[0];
            set => _pointList[0] = value;
        }

        /// <summary>
        /// Y coordinate
        /// Vector2 item. - 2D graphics vertices
        /// </summary>
        public T Y
        {
            get => _pointList[1];
            set => _pointList[1] = value;
        }

        /// <summary>
        /// Z coordinate
        /// Vector3 item. - 3D graphics vertices
        /// </summary>
        public T Z
        {
            get => _pointList[2];
            set => _pointList[2] = value;
        }

        /// <summary>
        /// W coordinate
        /// Vector4 item. - for extra attributes
        /// </summary>
        public T W => _pointList[3];

        /// <summary>
        /// Private accessor for order.
        /// </summary>
        private int _order;

        /// <summary>
        /// Returns the order of the vector,
        /// AKA. 3 - Vector3
        /// </summary>
        public int Order {
            get
            {
                // if it's a default value update it to reflect the size of the array.
                if (_order == 0)
                {
                    _order = _pointList.Count;
                }
                return _order;
            }

            set => _order = value;
        }

        /// <summary>
        /// Constructor that can take in a IEnumerable<T> to populate data. Floats most likely.
        /// TODO: add a null check.
        /// </summary>
        /// <param name="arr"></param>
        public Vector(IList<T> arr)
        {
            // this just copies by reference instead of creating a new object
            // MEM COPY
            _pointList = arr;
            
            // otherwise use the following
            // SHALLOW COPY
            // _pointList = new List<T>(arr);
            
            _order = arr.Count;
        }
        
        /// <summary>
        /// Vector3 Constructor with X, Y, Z
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector(T x, T y, T z)
        {
            // this just copies by reference instead of creating a new object
            // MEM COPY
            _pointList = new List<T> {x, y, z};

            // otherwise use the following
            // SHALLOW COPY
            // _pointList = new List<T>(arr);
            _order = _pointList.Count;
        }
        
        /// <summary>
        /// Vector2 Constructor with just a X, Y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector(T x, T y)
        {
            // this just copies by reference instead of creating a new object
            // MEM COPY
            _pointList = new List<T> {x, y};

            // otherwise use the following
            // SHALLOW COPY
            // _pointList = new List<T>(arr);
            _order = _pointList.Count;
        }

        
        /// <summary>
        /// Constructor that will create a Vector without any initial data.
        /// </summary>
        public Vector()
        {
            // be able to change this in the future
            _order = 0;

            _pointList = new List<T>();
        }

        /// <summary>
        /// Enumeration object for the vector list of points.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _pointList.GetEnumerator();
        }
 
        /// <summary>
        /// I'm not certain I know why or where this exists.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _pointList.GetEnumerator();
        }

        /// <summary>
        /// To string Override to pretty printing.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Vector({this.Order})\n\t- X: {X}\n\t- Y: {Y}\n\t- Z: {Z}";
        }

        // TODO how to operator overload generics?
        /*
        public static Vector<T> operator /(Vector<T> v1, Vector<T> v2)
        {
            return Vector<T>(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z);
        }
        */
    }

    /// <summary>
    /// Default Vector with the float type
    /// </summary>
    public class Vector : Vector<float>
    {
        /// <summary>
        /// Constructor that takes a float list and creates a base Vector from it.
        /// </summary>
        /// <param name="list"></param>
        public Vector(IList<float> list) : base(list) { }
        
        /// <summary>
        /// Construct a Vector3 with a float data
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector(float x, float y, float z) : base(x, y, z) { }
        
        /// <summary>
        /// Construct a Vector2 with float data
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector(float x, float y) : base(x, y) { }
    }
}