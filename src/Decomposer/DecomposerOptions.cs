namespace SharpMesh.Decomposer
{
    public abstract class DecomposerOptions
    {
        /// <summary>
        /// Constructs a Decomposer Option
        /// </summary>
        /// <param name="precision"></param>
        /// <param name="timeout"></param>
        /// <param name="debug"></param>
        protected DecomposerOptions(float precision, int timeout = 0, bool debug = false)
        {
            Precision = precision;
            Timeout = timeout;
            Debug = debug;
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
        /// Debug Output to Console.
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// Returns the Options in a neat printable list.
        /// </summary>
        /// <returns></returns>
        public abstract override string ToString();
    }
}