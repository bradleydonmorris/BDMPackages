ECHO %1
IF "%1" == "Debug" GOTO Debug
IF "%1" == "Release" GOTO Release

:Debug
dotnet build --configuration Debug --output "..\builds\Debug"
dotnet nuget push "..\builds\Debug\*.nupkg" --source C:\Users\bradley.morris\source\repos\nuget

:Release
dotnet nuget push ".builds\Release\*.nupkg" --skip-duplicate --source https://api.nuget.org/v3/index.json
