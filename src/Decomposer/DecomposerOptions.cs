namespace SharpMesh.Decomposer
{
    public abstract class DecomposerOptions
    {
        /// <summary>
        /// Constructs a Decomposer Option
        /// </summary>
        /// <param name="precision"></param>
        /// <param name="timeout"></param>
        protected DecomposerOptions(float precision, int timeout = 0)
        {
            Precision = precision;
            Timeout = timeout;
        }
        
        /// <summary>
        /// The Precisions required for this work defined by some Units.
        /// </summary>
        public float Precision { get; set; }

        /// <summary>
        /// Timeout for this calculation where the information is no longer necessary and we can cancel the compute.
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Returns the Options in a neat printable list.
        /// </summary>
        /// <returns></returns>
        public abstract override string ToString();
    }
}