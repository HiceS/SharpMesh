using System.Collections.Generic;
using System.Linq;

namespace SharpMesh.Data
{
    /// <summary>
    /// Creates a simple box Mesh with a size and position.
    /// </summary>
    public class BoxMesh : Mesh<float>
    {

        public Vector position;
        public float size;
        public bool stagedForDeletion = false;
        /// <summary>
        /// Constructs a Box based off specific variables.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="position"></param>
        public BoxMesh(Vector position_, float size_ = 1.0f)
        {
            position = position_;
            size = size_;
            // TODO: Create a set of math interfaces to Vector
            /*
            position.X *= size;
            position.Y *= size;
            position.Z *= size;
            */

            /*
            Vector[] vert =
            {
                new Vector(0.0f + position.X, 0.0f + position.Y, 0.0f + position.Z),
                new Vector(1.0f + position.X, 0.0f + position.Y, 0.0f + position.Z),
                new Vector(1.0f + position.X, 1.0f + position.Y, 0.0f + position.Z),
                new Vector(0.0f + position.X, 1.0f + position.Y, 0.0f + position.Z),
                new Vector(0.0f + position.X, 1.0f + position.Y, 1.0f + position.Z),
                new Vector(1.0f + position.X, 1.0f + position.Y, 1.0f + position.Z),
                new Vector(1.0f + position.X, 0.0f + position.Y, 1.0f + position.Z),
                new Vector(0.0f + position.X, 0.0f + position.Y, 1.0f + position.Z)
            };
            */
            Vector[] vert =
            {
                new Vector((0.0f * size) + position.X, (0.0f * size) + position.Y, (0.0f * size) + position.Z),
                new Vector((1.0f * size) + position.X, (0.0f * size) + position.Y, (0.0f * size) + position.Z),
                new Vector((1.0f * size) + position.X, (1.0f * size) + position.Y, (0.0f * size) + position.Z),
                new Vector((0.0f * size) + position.X, (1.0f * size) + position.Y, (0.0f * size) + position.Z),
                new Vector((0.0f * size) + position.X, (1.0f * size) + position.Y, (1.0f * size) + position.Z),
                new Vector((1.0f * size) + position.X, (1.0f * size) + position.Y, (1.0f * size) + position.Z),
                new Vector((1.0f * size) + position.X, (0.0f * size) + position.Y, (1.0f * size) + position.Z),
                new Vector((0.0f * size) + position.X, (0.0f * size) + position.Y, (1.0f * size) + position.Z)
            };
            
            Vertices = new List<Vector<float>>(vert);

            int[] tri =
            {
                0, 2, 1,
                0, 3, 2,
                2, 3, 4,
                2, 4, 5,
                1, 2, 5,
                1, 5, 6,
                0, 7, 4,
                0, 4, 3,
                5, 4, 7,
                5, 7, 6,
                0, 6, 7,
                0, 1, 6
            };
            
            Triangles = tri.ToList();
        }
    }
}