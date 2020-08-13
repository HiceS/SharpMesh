using System.Collections.Generic;
using SharpMesh.Data;

namespace SharpMesh.Decomposer
{
    /// <summary>
    /// A set of functions to find mesh bounds
    /// </summary>
    internal static class FindBounds
    {
        // To find the bounding
        internal static Vector VectorBounds(IEnumerable<Vector<float>> verts)
        {
            var temp = new Vector(0.0f, 0.0f, 0.0f);
            
            foreach (var vert in verts)
            {
                if (temp.X < vert.X)
                    temp.X = vert.X;
                
                if (temp.Y < vert.Y)
                    temp.Y = vert.Y;

                if (temp.Z < vert.Z)
                    temp.Z = vert.Z;
            }
            
            return temp;
        }
    }
}