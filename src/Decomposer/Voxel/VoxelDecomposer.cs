using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharpMesh.Data;

namespace SharpMesh.Decomposer.Voxel
{
    /// <summary>
    /// This should voxelize a given Mesh(float) ideally.
    /// </summary>
    public sealed class VoxelDecomposer : Decomposer<float>
    {

        /// <summary>
        /// Options supplied to the decomposition.
        /// </summary>
        public readonly VoxelOptions Options;

        /// <summary>
        /// Simple constructor that takes in the mesh to be operated on.
        /// Operation is started on construction.
        /// It would be super awesome to be able to register callbacks when this is done in other code.
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="options"></param>
        public VoxelDecomposer(Mesh<float> mesh, VoxelOptions options) : base(mesh)
        {
            Options = options;
        }

        /// <summary>
        /// Internal method for computing the mesh with multiple operations.
        /// Or just 1 it really doesn't matter much.
        /// </summary>
        /// <returns></returns>
        private static async Task<DecomposerResult> Compute(Mesh<float> mesh, VoxelOptions options)
        {
            mesh.Vertices[0].X += 1.0f;

            // dummy await call for now.
            await Task.Delay(0);
            
            // This is where you can split your algorithm into multiple threads or queues and then you can chose to rejoin them at the end with an await.
            // I think that would cause an awaitable object to exist.
            
            return new DecomposerResult();
        }

        /// <summary>
        /// This is the synchronous section of the compute.
        ///
        /// Just implement this to start.
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private static DecomposerResult ComputeSync(Mesh<float> mesh, VoxelOptions options)
        {
            var bounds = FindBounds.VectorBounds(mesh.Vertices);

            if (options.Debug)
            {
                Console.WriteLine("Starting Decomposition ------ \n");
                Console.WriteLine($"\t -Size : {options.Size}");
                Console.WriteLine($"\t -Shape : {options.VoxelShape.ToString()}");
                // Debug output could be part of the voxel options
                Console.WriteLine($"\t -Bounds: {bounds}");
            }

            // test for now
            var result = new DecomposerResult();
            
            result.Mesh = new List<Mesh<float>>
            {
                new BoxMesh(new Vector(0, 2, 0)),
                new BoxMesh(new Vector(0, 3, 0)),
                new BoxMesh(new Vector(0, 4, 0)),
            };

            result.FinishedWithError = false;

            return result;
        }

        public override Task<DecomposerResult> RunAsync()
        {
            // This is what actually runs the Task assigned.
            // If you just want to do CPU bound work you can call an await directly after this to ensure the work was complete.
            Result = Task.Run(() => Compute(Mesh, Options), CancellationTokenSource.Token);

            return Result;
        }

        public override DecomposerResult Run()
        {
            // This would mean it's not implemented yet.
            return ComputeSync(Mesh, Options);
        }
    }
}