using System;
using System.Collections.Generic;
using SharpMesh.Data;

namespace SharpMesh.Decompose
{
    /// <summary>
    /// The Decomposer has the ability to Decompose() which will return the final result.
    /// </summary>
    public abstract class Decomposer
    {
        /// <summary>
        /// List of Functions that are applied as a part of this decomposition.
        /// </summary>
        protected List<DecomposerFunction> Functions { get; } = new List<DecomposerFunction>();

        // Removing this for now to remove unneeded generic typing
/*
        /// <summary>
        /// The meshes that we want to decompose.
        /// The same meshes that we will get back most likely.
        /// Not sure if this is necessary but I will leave in for now.
        /// </summary>
        private List<Mesh<T>> _meshes;

        /// <summary>
        /// Factory Constructor that takes a List of Meshes to modify,
        /// For instance you could give the decomposer all of the meshes in the unity scene.
        /// </summary>
        /// <param name="meshes"></param>
        protected Decomposer(in List<Mesh<T>> meshes)
        {
            _meshes = meshes;
            
            this.Decompose();
        }

        /// <summary>
        /// Factory Constructor that takes a single mesh,
        /// This is the most common one.
        /// </summary>
        /// <param name="meshes"></param>
        protected Decomposer(in Mesh<T> meshes)
        {
            _meshes = new List<Mesh<T>>
            {
                meshes
            };
        }
*/

        /// <summary>
        /// This will call all functions in order.
        /// </summary>
        public abstract bool Decompose<T>(in Mesh<T> mesh);
        
        public abstract override string ToString();
    }

    /// <summary>
    ///  The Decomposer is made up of many DecomposerFunctions.
    ///
    /// This should really just be a float at this point since it's the most efficient to compute in parallel.
    /// </summary>
    public abstract class DecomposerFunction
    {
        public abstract override string ToString();

        // This will be supplied with a Map Function for the return and input data.
    }
}