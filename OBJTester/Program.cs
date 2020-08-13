using System;
using System.Collections.Generic;
using System.IO;

using ObjLoader.Loader.Loaders;
using SharpMesh.Data;
using SharpMesh.Decomposer.Voxel;

namespace OBJTester
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            
            // parsing section

            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();

            var fileStream = new FileStream("Resources/Dodec.obj", FileMode.Open, FileAccess.Read);
            var result = objLoader.Load(fileStream);

            Console.WriteLine(result.Vertices.Count);

            
            // parser call
            
            var ourMesh = new Mesh();

            // Convert the vertices into the superior format
            foreach (var vert in result.Vertices)
            {
                ourMesh.Vertices.Add(new Vector(vert.X, vert.Y, vert.Z));
            }

            var ind = result.Groups[0]?.Faces;

            if (ind != null)
            {
                foreach (var face in ind)
                {
                    for (var j = 0; j < face.Count; j++)
                    {
                        ourMesh.Triangles.Add(face[j].VertexIndex);
                    }
                }
            }
            else
            {
                return;
            }
            
            Console.WriteLine(ourMesh.Triangles.Count);
            Console.WriteLine(ourMesh.Vertices.Count);

            var decomped = Decompose(ourMesh);
            
            Console.WriteLine(decomped);
        }

        private static List<Mesh> Decompose(Mesh mesh)
        {
            var decomposer = new VoxelDecomposer(mesh, new VoxelOptions(0.1f, debug: true));
            var result = decomposer.Run();
            Console.WriteLine(result.Mesh[0]);
            return null;
        }
    }
}