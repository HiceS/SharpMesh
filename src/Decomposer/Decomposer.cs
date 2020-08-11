using System.Threading;
using System.Threading.Tasks;
using SharpMesh.Data;

namespace SharpMesh.Decomposer
{
    /// <summary>
    /// There is a Decomposer Object
    /// The Object must be extended by an algorithm
    /// The result field will be a set of properties defining the decomposition results
    ///     - time
    ///     - List(Mesh)
    ///     - accuracy
    ///     - etc.
    /// </summary>
    public abstract class Decomposer<T>
    {
        /// <summary>
        /// The result from the finished simulation.
        /// Check against this for the various properties of a finished decomposition.
        /// </summary>
        protected Task<DecomposerResult> Result;

        /// <summary>
        /// In order to cancel the compute.
        /// </summary>
        protected readonly CancellationTokenSource CancellationTokenSource;

        /// <summary>
        /// Mesh assigned to this decomposition.
        /// </summary>
        protected readonly Mesh<T> Mesh;
    
        /// <summary>
        /// Runs the decomposition in an Async.
        /// Hopefully Awaitable.
        /// </summary>
        /// <returns></returns>
        public abstract Task<DecomposerResult> RunAsync();
        
        /// <summary>
        /// Runs the synchronous code that is blocking and returns a result.
        /// </summary>
        /// <returns></returns>
        public abstract DecomposerResult Run();

        /// <summary>
        /// Base Decomposer class
        /// </summary>
        /// <param name="mesh"></param>
        protected Decomposer(Mesh<T> mesh)
        {
            CancellationTokenSource = new CancellationTokenSource();
            Mesh = mesh;
        }

        /// <summary>
        /// Cancels the current async operation happening.
        /// </summary>
        protected void Cancel()
        {
            CancellationTokenSource.Cancel();
        }
        
        // This doesn't force an override anyway so im getting rid of it, it also does a boxing allocation which restricts the options parameter.
        /*
        /// <summary>
        /// Internal method to handle the decompositions and return a result that will be checked against.
        /// The purpose here is to define a number of static methods to operate on a mesh.
        /// Then you can await for them all to be done in this method.
        /// Otherwise it's somewhat sync but still not really in terms of unity.
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<DecomposerResult> Compute(Mesh<T> mesh)
        {
            return new DecomposerResult();
        }
        */
    }
}