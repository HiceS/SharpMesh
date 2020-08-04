using System.Collections.Generic;
using SharpMesh.Data;

namespace SharpMesh.Decomposer
{
    /// <summary>
    /// This will be accessible when the decomposition is finished.
    /// </summary>
    public class DecomposerResult<T>
    {
        /// <summary>
        /// Is there an error?
        /// </summary>
        public bool FinishedWithError;
        
        /// <summary>
        /// How much time did it take?
        /// </summary>
        public double TimeTaken;
        
        /// <summary>
        /// List of Meshes to now render.
        /// </summary>
        public List<Mesh<T>> Mesh;
    }
    
    /// <summary>
    /// This will be accessible when the decomposition is finished.
    /// The base is with the type float.
    /// </summary>
    public class DecomposerResult : DecomposerResult<float> { }
}