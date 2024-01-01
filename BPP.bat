@ECHO OFF
REM nuget setapikey {apikey} -Source https://api.nuget.org/v3/index.json

dotnet build --configuration Debug --output ".builds\Debug"
dotnet build --configuration Release --output ".builds\Release"
dotnet nuget push ".builds\Debug\*.nupkg" --source C:\Users\%USERNAME%\source\repos\nuget
dotnet nuget push ".builds\Release\*.nupkg" --skip-duplicate --source https://api.nuget.org/v3/index.json
