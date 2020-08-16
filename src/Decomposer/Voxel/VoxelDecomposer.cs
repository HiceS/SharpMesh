using System;
using System.Collections.Generic;
using System.Numerics;
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
		/*
		// async currently removed for Unity compatibility
        private static async Task<DecomposerResult> Compute(Mesh<float> mesh, VoxelOptions options)
        {
            mesh.Vertices[0].X += 1.0f;

            // dummy await call for now.
            await Task.Delay(0);
            
            // This is where you can split your algorithm into multiple threads or queues and then you can chose to rejoin them at the end with an await.
            // I think that would cause an awaitable object to exist.
            
            return new DecomposerResult();
        }
		*/

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
			var resolution = options.Resolution;

            if (options.Debug)
            {
                Console.WriteLine("Starting Decomposition ------ \n");
                Console.WriteLine($"\t -Size : {options.Resolution}");
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

			int min_X = (int)(bounds.min_X *= resolution);
			int max_X = (int)(bounds.max_X *= resolution);
			int min_Y = (int)(bounds.min_Y *= resolution);
			int max_Y = (int)(bounds.max_Y *= resolution);
			int min_Z = (int)(bounds.min_Z *= resolution);
			int max_Z = (int)(bounds.max_Z *= resolution);

			//build maximum size 3 dimensional quadrilateral

			/*
            result.Mesh = new List<Mesh<float>>
            {
                i = 0;
				for(var x = bounds.min_X; x =< bounds.max_X; x++){
					for(var y = bounds.min_Y; y =< bounds.max_Y; y++){
						for(var z = bounds.min_Z; z =< bounds.max_Z; z++){
						new BoxMesh(v1 = new Vector(x, y, z),
									v2 = new Vector(x, y + 1, z),
									v3 = new Vector(x + 1, y + 1, z),
									v4 = new Vector(x + 1, y, z),
									v5 = new Vector(x, y, z + 1),
									v6 = new Vector(x + 1, y, z + 1),
									v7 = new Vector(x + 1, y + 1, z + 1),
									v8 = new Vector(x, y + 1, z + 1)
									);
						}
					}
				}
            };
			*/
			var workingList = new List<Mesh<float>>();

            for(var x = min_X; x <= max_X; x++) {
                for(var y = min_Y; y <= max_Y; y++) {
                    for(var z = min_Z; z <= max_Z; z++) {
						// result.Mesh.Add(new BoxMesh(new Data.Vector(x + (0.5f * options.Size), y + (0.5f * options.Size), z + (0.5f * options.Size)), 1.0f));
						// result.Mesh.Add(new BoxMesh(new Data.Vector(x, y, z), 1.0f));
						workingList.Add(new BoxMesh(new Data.Vector(x, y, z), 1.0f));
                    }
                }
            }


            //Decompose mesh one side at a time
            //for (int i = 1; i <= 6; i++) {
				// i = a side of the mesh
				// type float
				int a1, a2, b1, b2, c1, c2;
				// 1 represents starting point for x,y,z
				// 2 represents the ending point
				int x, y, z;
				//if(i == 1){
				a1 = min_X;
				a2 = max_X;
				b1 = min_Y;
				b2 = max_Y;
				c1 = min_Z;
				c2 = max_Z;
				for (var c = c1; c <= c2; c++) {
					for (var b = b1; b <= b2; b++) {
						for (var a = a1; a <= a2; a++) {
                            x = a;
                            y = b;
                            z = c;
                            var found = false;
							foreach (BoxMesh m in workingList) {
								// TODO clean up later
								if((int)m.position.X == x && (int)m.position.Y == y && (int)m.position.Z == z) {
									foreach (var vert in mesh.Vertices){
										if((vert.X * resolution) >= x && (vert.X * resolution) < (x + 1)){
											if((vert.Y * resolution) >= y && (vert.Y * resolution) < (y + 1)){
												if((vert.Z * resolution) >= z && (vert.Z * resolution) < (z + 1)){
													found = true;
													break;
												}
											}
										}
									}
                                    if (found == false) {
                                        m.stagedForDeletion = true;
                                    }
									break;
								}
							}
                            if (found)
                                break;
						}
					}
				}
				//}else if(i == 2){
				a1 = max_Y;
				a2 = min_Y;
				b1 = min_X;
				b2 = max_X;
				c1 = min_Z;
				c2 = max_Z;
				for (var c = c1; c <= c2; c++) {
					for (var b = b1; b <= b2; b++) {
						for (var a = a1; a >= a2; a--) {
                            x = b;
                            y = a;
                            z = c;
                            var found = false;
							foreach (BoxMesh m in workingList) {
								// TODO clean up later
								if((int)m.position.X == x && (int)m.position.Y == y && (int)m.position.Z == z) {
									foreach (var vert in mesh.Vertices){
										if((vert.X * resolution) >= x && (vert.X * resolution) < (x + 1)){
											if((vert.Y * resolution) >= y && (vert.Y * resolution) < (y + 1)){
												if((vert.Z * resolution) >= z && (vert.Z * resolution) < (z + 1)){
													found = true;
													break;
												}
											}
										}
									}
                                    if (found == false) {
                                        m.stagedForDeletion = true;
                                    }
									break;
								}
							}
                            if (found)
                                break;
						}
					}
				}
				//}else if(i == 3){
				a1 = max_X;
				a2 = min_X;
				b1 = max_Y;
				b2 = min_Y;
				c1 = min_Z;
				c2 = max_Z;
				for (var c = c1; c <= c2; c++) {
					for (var b = b1; b >= b2; b--) {
						for (var a = a1; a >= a2; a--) {
                            x = a;
                            y = b;
                            z = c;
                            var found = false;
							foreach (BoxMesh m in workingList) {
								// TODO clean up later
								if((int)m.position.X == x && (int)m.position.Y == y && (int)m.position.Z == z) {
									foreach (var vert in mesh.Vertices){
										if((vert.X * resolution) >= x && (vert.X * resolution) < (x + 1)){
											if((vert.Y * resolution) >= y && (vert.Y * resolution) < (y + 1)){
												if((vert.Z * resolution) >= z && (vert.Z * resolution) < (z + 1)){
													found = true;
													break;
												}
											}
										}
									}
                                    if (found == false) {
                                        m.stagedForDeletion = true;
                                    }
									break;
								}
							}
                            if (found)
                                break;
						}
					}
				}
				//}else if(i == 4){
				a1 = min_Y;
				a2 = max_Y;
				b1 = max_X;
				b2 = min_X;
				c1 = min_Z;
				c2 = max_Z;
				for (var c = c1; c <= c2; c++) {
					for (var b = b1; b >= b2; b--) {
						for (var a = a1; a <= a2; a++) {
                            x = b;
                            y = a;
                            z = c;
                            var found = false;
							foreach (BoxMesh m in workingList) {
								// TODO clean up later
								if((int)m.position.X == x && (int)m.position.Y == y && (int)m.position.Z == z) {
									foreach (var vert in mesh.Vertices){
										if((vert.X * resolution) >= x && (vert.X * resolution) < (x + 1)){
											if((vert.Y * resolution) >= y && (vert.Y * resolution) < (y + 1)){
												if((vert.Z * resolution) >= z && (vert.Z * resolution) < (z + 1)){
													found = true;
													break;
												}
											}
										}
									}
                                    if (found == false) {
                                        m.stagedForDeletion = true;
                                    }
									break;
								}
							}
                            if (found)
                                break;
						}
					}
				}
				//}else if(i == 5){
				a1 = min_Z;
				a2 = max_Z;
				b1 = min_X;
				b2 = max_X;
				c1 = min_Y;
				c2 = max_Y;
				for (var c = c1; c <= c2; c++) {
					for (var b = b1; b <= b2; b++) {
						for (var a = a1; a <= a2; a++) {
                            x = b;
                            y = c;
                            z = a;
                            var found = false;
							foreach (BoxMesh m in workingList) {
								// TODO clean up later
								if((int)m.position.X == x && (int)m.position.Y == y && (int)m.position.Z == z) {
									foreach (var vert in mesh.Vertices){
										if((vert.X * resolution) >= x && (vert.X * resolution) < (x + 1)){
											if((vert.Y * resolution) >= y && (vert.Y * resolution) < (y + 1)){
												if((vert.Z * resolution) >= z && (vert.Z * resolution) < (z + 1)){
													found = true;
													break;
												}
											}
										}
									}
                                    if (found == false) {
                                        m.stagedForDeletion = true;
                                    }
									break;
								}
							}
                            if (found)
                                break;
						}
					}
				}
				//}else{
				a1 = max_Z;
				a2 = min_Z;
				b1 = min_X;
				b2 = max_X;
				c1 = max_Y;
				c2 = min_Y;
				//}
				for (var c = c1; c >= c2; c--) {
					for (var b = b1; b <= b2; b++) {
						for (var a = a1; a >= a2; a--) {
                            x = b;
                            y = c;
                            z = a;
                            var found = false;
							foreach (BoxMesh m in workingList) {
								// TODO clean up later
								if((int)m.position.X == x && (int)m.position.Y == y && (int)m.position.Z == z) {
									foreach (var vert in mesh.Vertices){
										if((vert.X * resolution) >= x && (vert.X * resolution) < (x + 1)){
											if((vert.Y * resolution) >= y && (vert.Y * resolution) < (y + 1)){
												if((vert.Z * resolution) >= z && (vert.Z * resolution) < (z + 1)){
													found = true;
													break;
												}
											}
										}
									}
                                    if (found == false) {
                                        m.stagedForDeletion = true;
                                    }
									break;
								}
							}
                            if (found)
                                break;
						}
					}
				}
			//}

			//scale back down
			result.Mesh = new List<Mesh<float>>();
			for (int i = 0; i < workingList.Count; i++)
            {
				// result.Mesh[i] = new BoxMesh(((BoxMesh)workingList[i]).position, ((BoxMesh)workingList[i]).size /= size);
				// result.Mesh.Add(new BoxMesh(((BoxMesh)workingList[i]).position /= new Data.Vector(size, size, size), ((BoxMesh)workingList[i]).size /= size));
				var workingBox = (BoxMesh)workingList[i];
				if (workingBox.stagedForDeletion)
					continue;
				var pos = new Data.Vector(workingBox.position.X / resolution, workingBox.position.Y / resolution, workingBox.position.Z / resolution);
				// result.Mesh.Add(new BoxMesh(pos, workingBox.size / size));
				result.Mesh.Add(new BoxMesh(pos, 1.0f / resolution));
            }
			/*
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
			*/
								
					//side 1
					

            result.FinishedWithError = false;

            return result;
        }

		/*
        public override Task<DecomposerResult> RunAsync()
        {
            // This is what actually runs the Task assigned.
            // If you just want to do CPU bound work you can call an await directly after this to ensure the work was complete.
            Result = Task.Run(() => Compute(Mesh, Options), CancellationTokenSource.Token);

            return Result;
        }
		*/

        public override DecomposerResult Run()
        {
            // This would mean it's not implemented yet.
            return ComputeSync(Mesh, Options);
        }
    }
}
