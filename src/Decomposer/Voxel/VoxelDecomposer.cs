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

			//having established our bounds, we now want to adjust those bounds
			//currently the default is 1, which means that one side of a cube = 1 unit of measurement.
			//so one cube ocupies one unit squared.
			//if we wanted a higher resolution, meaning 2:1 and up we would extend the bounds based on that ratio

			//for simplicity sake so a cube won't be a size of .5 by .5,
			//we would actually double the size of input object
			//in other words with a resolution of 1 for an input object 12 by 12,
			// we would start with 144 cubes and a size of 12 by 12
			//but with a resolution of 2 for an input object of 12 by 12, 
			//we would start with 576 cubes and a size of 24 by 24

			//once the decomposition is complete we will scale it back down
			
			
			bounds.min_x *= VoxelOptions.size;
			bounds.max_x *= VoxelOptions.size;
			bounds.min_y *= VoxelOptions.size;
			bounds.max_y *= VoxelOptions.size;
			bounds.min_z *= VoxelOptions.size;
			bounds.max_z *= VoxelOptions.size;
			
            //build maximum size 3 dimensional quadrilateral

            result.Mesh = new List<Mesh<float>>
            {
                i = 0;
				for(var x = bounds.min_x; x =< bounds.max_x; x++){
					for(var y = bounds.min_y; y =< bounds.max_y; y++){
						for(var z = bounds.min_z; z =< bounds.max_z; z++){
							new BoxMesh(v1 = new Vector(x, y, z),
										v2 = new Vector(x, y + 1, z),
										v3 = new Vector(x + 1, y + 1, z),
										v4 = new Vector(x + 1, y, z),
										v5 = new Vector(x, y, z + 1),
										v6 = new Vector(x + 1, y, z + 1),
										v7 = new Vector(x + 1, y + 1, z + 1),
										v8 = new Vector(x, y + 1, z + 1)
										)
						}
					}
				}
            };
			
			//Decompose mesh one side at a time
			for(var i = 1; i =< 6; i++){
				// i = a side of the mesh
				var a1, a2, b1, b2, c1, c2;
				// 1 represents starting point for x,y,z
				// 2 represents the ending point
				var x, y, z;
				if(i == 1){
					a1 = bound.min_x;
					a2 = bound.max_x;
					b1 = bound.min_y;
					b2 = bound.max_y;
					c1 = bound.min_z;
					c2 = bound.max_z;
				}else if(i == 2){
					a1 = bound.max_y;
					a2 = bound.min_y;
					b1 = bound.min_x;
					b2 = bound.max_x;
					c1 = bound.min_z;
					c2 = bound.max_z;
				}else if(i == 3){
					a1 = bound.max_x;
					a2 = bound.min_x;
					b1 = bound.max_y;
					b2 = bound.min_y;
					c1 = bound.min_z;
					c2 = bound.max_z;
				}else if(i == 4){
					a1 = bound.min_y;
					a2 = bound.max_y;
					b1 = bound.max_x;
					b2 = bound.min_x;
					c1 = bound.min_z;
					c2 = bound.max_z;
				}else if(i == 5){
					a1 = bound.min_z;
					a2 = bound.max_z;
					b1 = bound.min_x;
					b2 = bound.max_x;
					c1 = bound.min_y;
					c2 = bound.max_y;
				}else{
					a1 = bound.max_z;
					a2 = bound.min_z;
					b1 = bound.min_x;
					b2 = bound.max_x;
					c1 = bound.max_y;
					c2 = bound.min_y;
				}
				for(var c = c1; c <= c2; c++){
					for(var b = b1; b <= b2; b++){
						for(var a = a1; a <= a2; a++){
							if(i == 1 || i == 3){
								x = a;
								y = b;
								z = c;
							}else if(i == 2 || i == 4){
								x = b;
								y = a;
								z = c;
							}else{
								x = b;
								y = c;
								z = a;
							}
							var found = false;
							for(BoxMesh in result){
								if(BoxMesh.v1.x == x && BoxMesh.v1.y == y && BoxMesh.v1.z == z){
									for(var vert in verts){
										if((vert.x * VoxelOptions.size) >= x && (vert.x * VoxelOptions.size) < (x + 1)){
											if((vert.y * VoxelOptions.size) >= y && (vert.y * VoxelOptions.size) < (y + 1)){
												if((vert.z * VoxelOptions.size) >= z && (vert.z * VoxelOptions.size) < (z + 1)){
													found = true;
													break;
												}
											}
										}
									}
									if(found == false){
										delete BoxMesh;
									}
									break;
								}
							}
						}
					}
				}
			}
			//scale back down
			for(BoxMesh in Result){
				BoxMesh.v1.x /= VoxelOptions.size;
				BoxMesh.v1.y /= VoxelOptions.size;
				BoxMesh.v1.z /= VoxelOptions.size;
				BoxMesh.v2.x /= VoxelOptions.size;
				BoxMesh.v2.y /= VoxelOptions.size;
				BoxMesh.v2.z /= VoxelOptions.size;
				BoxMesh.v3.x /= VoxelOptions.size;
				BoxMesh.v3.y /= VoxelOptions.size;
				BoxMesh.v3.z /= VoxelOptions.size;
				BoxMesh.v4.x /= VoxelOptions.size;
				BoxMesh.v4.y /= VoxelOptions.size;
				BoxMesh.v4.z /= VoxelOptions.size;
				BoxMesh.v5.x /= VoxelOptions.size;
				BoxMesh.v5.y /= VoxelOptions.size;
				BoxMesh.v5.z /= VoxelOptions.size;
				BoxMesh.v6.x /= VoxelOptions.size;
				BoxMesh.v6.y /= VoxelOptions.size;
				BoxMesh.v6.z /= VoxelOptions.size;
			}
								
					//side 1
					

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