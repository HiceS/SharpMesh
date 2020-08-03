using System.Threading.Tasks;
using SharpMesh.Data;

namespace SharpMesh.Async
{
    /// <summary>
    /// This should voxelize a given Mesh(float) ideally.
    /// </summary>
    public sealed class VoxelDecomposer : Async.Decomposer
    {

        private Mesh<float> _mesh;

        /// <summary>
        /// Simple constructor that takes in the mesh to be operated on.
        /// Operation is started on construction.
        /// It would be super awesome to be able to register callbacks when this is done in other code.
        /// </summary>
        /// <param name="mesh"></param>
        public VoxelDecomposer(Mesh<float> mesh)
        {
            // This really should make sure to copy all data due to being async.
            _mesh = mesh;
            
            // this.Compute();
        }

        /// <summary>
        /// Cancels the decomposition.
        /// </summary>
        public void Cancel()
        {
            // modify the cancellationToken that is stored internally.
        }

        /// <summary>
        /// Internal method for computing the mesh with multiple operations.
        /// Or just 1 it really doesn't matter much.
        /// </summary>
        /// <returns></returns>
        protected override async Task<DecompResult> Compute()
        {
            _mesh.Vertices[0].X += 1.0f;
            
            return new DecompResult();
        }
    }
}