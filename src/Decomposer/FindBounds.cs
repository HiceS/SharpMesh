using System.Collections.Generic;
using SharpMesh.Data;

namespace SharpMesh.Decomposer
{
    /// <summary>
    /// Boundary sizing
    ///
    /// These really should be Vector2
    /// </summary>
    public class Bounds
    {
        /// <summary>
        /// Max X value
        /// </summary>
        public float max_X;
        
        /// <summary>
        /// Min X value
        /// </summary>
        public float min_X;
        
        /// <summary>
        /// Max Y value
        /// </summary>
        public float max_Y;
        
        /// <summary>
        /// Min Y value
        /// </summary>
        public float min_Y;
        
        /// <summary>
        /// Max Z value
        /// </summary>
        public float max_Z;
        
        /// <summary>
        /// Min Z value
        /// </summary>
        public float min_Z;

        public override string ToString()
        {
            var str = "Bounds:\n";
            str += $"\t- max X: {max_X}\n";
            str += $"\t- min X: {min_X}\n";
            str += $"\t- max Y: {max_Y}\n";
            str += $"\t- min Y: {min_Y}\n";
            str += $"\t- max Z: {max_Z}\n";
            str += $"\t- min Z: {min_Z}\n";
            
            return str;
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