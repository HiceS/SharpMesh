# Particular
Library for generating convex decomposition for 3D meshes, written and supplied in .NET

## Building

1. ` dotnet restore `
2. ` dotnet build --release `

#### Packaging

- ` dotnet pack --configuration Release `
- ` dotnet nuget push "bin/Release/Particular.1.0.0.nupkg" --source "github" `
