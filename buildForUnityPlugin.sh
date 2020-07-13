echo "Building..."
dotnet build -o SharpMeshUnityPlugin/SharpMesh
cd SharpMeshUnityPlugin/SharpMesh
echo "Success! Cleaning up..."
find . -type f,d ! -name "." ! -name 'SharpMesh.dll' -delete
echo "Done!"
