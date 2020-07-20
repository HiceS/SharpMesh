using System;
using NUnit.Framework;
using SharpMesh.Data;

namespace SharpMeshTests
{
    public class Tests
    {
        [Test]
        public void SVector3_test()
        {
            var v1 = new SVector3<float>(new float[] {1.0f, 2.0f, 3.0f});
            
            var x = v1.X;
            var y = v1.Y;
            var z = v1.Z;

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
    }
}