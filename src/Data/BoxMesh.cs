using System.Collections.Generic;

namespace SharpMesh.Data
{
    /// <summary>
    /// Creates a simple box Mesh with a size and position.
    /// </summary>
    public class BoxMesh
    {
        /// <summary>
        /// Output Mesh of the Box
        /// </summary>
        public Mesh<float> Mesh { get; }

        /// <summary>
        /// Constructs a Box based off specific variables.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="position"></param>
        public BoxMesh(Vector position, int size = 1)
        {
            // TODO: Create a set of math interfaces to Vector
            position.X *= size;
            position.Y *= size;
            position.Z *= size;

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

            Mesh = new Mesh
            {
                Vertices = new List<Vector<float>>(vert),
                Triangles = { 
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
                }
            };
        }
    }
}