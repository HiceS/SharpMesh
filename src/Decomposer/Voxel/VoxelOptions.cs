using SharpMesh.Data;

namespace SharpMesh.Decomposer.Voxel
{
    public class VoxelOptions : DecomposerOptions
    {


        /// <summary>
        /// The shape to voxelize the object into.
        /// </summary>
        public BaseShape VoxelShape { get; }
        
        /// <summary>
        /// Size of the individual elements.
        /// </summary>
        public int Size { get; } 

        public override string ToString()
        {
            return " ";
        }

        /// <summary>
        /// Constructs a VoxelOption with the additional parameter needed to do the computation.
        /// </summary>
        /// <param name="precision">Accuracy</param>
        /// <param name="debug"></param>
        /// <param name="size">Element Sizing</param>
        /// <param name="timeout"></param>
        /// <param name="voxelShape">Shape of the Voxel</param>
        public VoxelOptions(float precision, bool debug = false, int size = 1, int timeout = 0, BaseShape voxelShape = BaseShape.Box) : base(precision, timeout, debug)
        {
            Size = size;
            VoxelShape = voxelShape;
        }
    }
}