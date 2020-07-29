using System.Collections.Generic;

namespace SharpMesh.Data
{
    /// <summary>
    /// Mesh to do the calculations that we can supply back to Unity.
    /// Hopefully while keeping the same object references while blocking.
    /// </summary>
    public class Mesh<T>
    {
        /// <summary>
        /// vertices, world positions
        /// TODO: does set copy the array or the references to the previous array?
        /// </summary>
        public List<Vector<T>> Vertices { get; set; }
        
        /// <summary>
        /// triangles that correspond to vertices positions.
        /// TODO: does set copy the array or the references to the previous array?
        /// </summary>
        public List<int> Triangles { get; set; }

        /// <summary>
        /// Returns order of World Space
        /// e.g. 3 - Vector3 3D.
        /// This will get the order of the first vertex in the Vertices List
        /// Hopefully not Null.
        /// </summary>
        public int Order => Vertices[0]?.Order ?? 0;

        /// <summary>
        /// Default Constructor that will make empty vertices and triangles.
        /// </summary>
        public Mesh()
        {
            Vertices = new List<Vector<T>>();
            Triangles = new List<int>();
        }

        /// <summary>
        /// Copy constructor -- that will do a true copy of the mesh data.
        /// THIS HAS OVERHEAD.
        /// The reason is unity doesn't directly use the List, so we can have access to changing the original list while blocking.
        /// </summary>
        /// <param name="mesh"></param>
        public Mesh(Mesh<T> mesh)
        {
            // This will physically copy each value which is bad.\
            // SHALLOW COPY - can reorder but the elements are still referenced via memory.
            Vertices = new List<Vector<T>>(mesh.Vertices);
            Triangles = new List<int>(mesh.Triangles);
        }
    }
}