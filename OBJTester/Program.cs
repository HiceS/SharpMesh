using System;
using System.IO;

using SharpMesh;

using ObjLoader.Loader.Loaders;

namespace OBJTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();
            
            var fileStream = new FileStream("Resources/Dodec.obj", FileMode.Open, FileAccess.Read);
            var result = objLoader.Load(fileStream);

            Console.WriteLine(result.Vertices.Count);
        }
    }
}