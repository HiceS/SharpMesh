using System.Collections.Generic;
using SharpMesh.Data;

namespace SharpMesh.Decomposer
{
    public class Bounds
    {
        public float max_X;
        public float min_X;
        public float max_Y;
        public float min_Y;
        public float max_Z;
        public float min_Z;

        public Bounds()
        {
            
        }

    }
    
    
    /// <summary>
    /// A set of functions to find mesh bounds
    /// </summary>
    internal static class FindBounds
    {
        // To find the bounding
        internal static Bounds VectorBounds(IEnumerable<Vector<float>> verts)
        {
            var tempB = new Bounds();
            
            foreach (var vert in verts)
            {

                if (vert.X < tempB.min_X)
                {
                    tempB.min_X = vert.X;
                }
                
                if (vert.X > tempB.max_X)
                {
                    tempB.max_X = vert.X;
                }
                
                if (vert.Y < tempB.min_Y)
                {
                    tempB.min_Y = vert.Y;
                }
                
                if (vert.Y > tempB.max_Y)
                {
                    tempB.max_Y = vert.Y;
                }
                
                if (vert.Z < tempB.min_Z)
                {
                    tempB.min_Z = vert.Z;
                }
                
                if (vert.Z > tempB.max_Z)
                {
                    tempB.max_Z = vert.Z;
                }
            }
            
            return tempB;
        }
    }
}