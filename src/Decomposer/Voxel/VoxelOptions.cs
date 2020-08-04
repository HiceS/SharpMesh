using SharpMesh.Data;

namespace SharpMesh.Decomposer.Voxel
{
    public class VoxelOptions : DecomposerOptions
    {


        /// <summary>
        /// The shape to voxelize the object into.
        /// </summary>
        private BaseShape VoxelShape { get; }

        public override string ToString()
        {
            return " ";
        }

        /// <summary>
        /// Constructs a VoxelOption with the additional parameter needed to do the computation.
        /// </summary>
        /// <param name="precision"></param>
        /// <param name="timeout"></param>
        /// <param name="voxelShape"></param>
        public VoxelOptions(float precision, int timeout = 0, BaseShape voxelShape = BaseShape.Box) : base(precision, timeout)
        {
            VoxelShape = voxelShape;
        }
    }
}