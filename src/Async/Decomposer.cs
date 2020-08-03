using System.Threading.Tasks;

namespace SharpMesh.Async
{
    /// <summary>
    /// There is a Decomposer Object
    /// The Object must be extended by an algorithm
    /// The result field will be a set of properties defining the decomposition results
    ///     - time
    ///     - List<Mesh>
    ///     - accuracy
    ///     - etc.
    /// </summary>
    public abstract class Decomposer
    {
        /// <summary>
        /// The result from the finished simulation.
        /// Check against this for the various properties of a finished decomposition.
        /// </summary>
        public Task<DecompResult> Result;

        /// <summary>
        /// Internal method to handle the decompositions and return a result that will be checked against.
        /// The purpose here is to define a number of static methods to operate on a mesh.
        /// Then you can await for them all to be done in this method.
        /// Otherwise it's somewhat sync but still not really in terms of unity.
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<DecompResult> Compute()
        {
            return new DecompResult();
        }
    }
}