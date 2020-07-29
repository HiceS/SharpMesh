using System;
using NUnit.Framework;
using SharpMesh.Data;

namespace SharpMeshTests
{
    public class Tests
    {

        [Test]
        public void Vector_test()
        {
            var v1 = new Vector<float>(new float[] {1.0f, 2.0f, 3.0f});
            
            var x = v1.X;
            var y = v1.Y;
            var z = v1.Z;

            v1[0] = 1.0f; // test indexer

            Assert.AreEqual(expected: 3, actual: v1.Order);     // test order function
            Assert.AreEqual(3.0f, v1[2]);        // test indexer get

            // Adds the coordinates and then subtracts them from expected result and accounts for some small variance
            if (Math.Abs((x + y + z) - 6.0f) < 0.001f)
            {
                var sum = 0.0f;
                
                foreach (var point in v1)
                    sum += point;
                
                if (Math.Abs(sum - 6.0f) < 0.001f) { Assert.Pass(); }
            }
            
            Assert.Fail();
        }
        
        [Test]
        public void Mesh_test()
        {
            // sample Vector of order 3
            var v1 = new Vector<float>(new float[] {1.0f, 2.0f, 3.0f});

            // default constructor
            var mesh = new Mesh<float>();
            
            // Test Properties
            mesh.Vertices.Add(v1);
            mesh.Triangles.Add(1);
            
            Assert.AreEqual(mesh.Order, v1.Order);
            
            // Test shallow copy constructor,
            var mesh2 = new Mesh<float>(mesh);
            
            Assert.AreEqual(mesh2.Order, mesh.Vertices[0].Order);
        }
    }
}