# SharpMesh ![.NET Core](https://github.com/HiceS/SharpMesh/workflows/.NET%20Core/badge.svg)
Library for generating convex decomposition for 3D meshes, written and supplied in .NET

## Building

1. ` dotnet restore `
2. ` dotnet build --release `

## Unity Plugin

To use the Unity plugin, make sure to recurse submodules. If you haven't cloned yet:

```
$ git clone --recurse-submodules https://github.com/HiceS/SharpMesh
```

Otherwise:

```
$ git submodule update --init --recursive
```

Then read SharpMeshUnityPlugin/README.md

#### Packaging

- ` dotnet pack --configuration Release `
- ` dotnet nuget push "bin/Release/Particular.1.0.0.nupkg" --source "github" `

#### LICENSE

We are using the MIT open source license with no modifications of original intentions, feel free to view the license in the LICENSE.md file.
