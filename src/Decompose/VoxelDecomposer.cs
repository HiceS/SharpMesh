using System;
using System.Collections.Generic;
using System.Reflection;
using SharpMesh.Data;

namespace SharpMesh.Decompose
{
    /// <summary>
    /// Primitive VoxelDecomposer Decomposition Function
    /// </summary>
    internal class VoxelFunction : DecomposerFunction
    {
        public static void Map(Mesh<float> data)
        {
            if (data.Vertices[0] == null) return;
            
            data.Vertices[0].X += 1.0f;
            data.Vertices[0].Y += 1.0f;
            data.Vertices[0].Z += 1.0f;
        }

        public override string ToString()
        {
            return "VoxelFunction";
        }
    }

    internal class DensityFunction : DecomposerFunction
    {
        public static void Map(Mesh<float> data)
        {
            if (data.Vertices[0] == null) return;
            
            data.Vertices.Add(new Vector<float>(new[] {1.0f, 1.0f, 1.0f}));
            
            data.Vertices[1].X = data.Vertices[0].X;
            data.Vertices[1].Y += data.Vertices[0].Y;
            data.Vertices[1].Z += data.Vertices[0].Z;
        }

        public override string ToString()
        {
            return "DensityFunction";
        }
    }

    /// <summary>
    /// VoxelDecomposer Decomposition Concrete Factory.
    /// usage: new VoxelDecomposer().Decompose(mesh);
    /// </summary>
    public class VoxelDecomposer : Decomposer
    {
        public override bool Decompose<T>(in Mesh<T> mesh)
        {
            try
            {
                if (mesh.Vertices[0].GetType().GetTypeInfo().GenericTypeArguments[0] == typeof(float))
                {
                    // The reason to do this would be if we wanted to for instance print out all of the function names to the user.
                    Functions.Add(new VoxelFunction());
                    Functions.Add(new DensityFunction());
                    
                    VoxelFunction.Map(mesh as Mesh<float>);
                    DensityFunction.Map(mesh as Mesh<float>);
                }
            }
            catch (Exception e)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            var ret = "Voxelization Decomposition using primitives";
            
            foreach (var func in Functions)
            {
                ret += $"\n\t - {func}";
            }

            return ret;

        }
    }
}