@ECHO OFF
REM nuget setapikey {apikey} -Source https://api.nuget.org/v3/index.json

REM dotnet build --configuration Debug --output ".builds\Debug"
REM dotnet build --configuration Release --output ".builds\Release"
REM dotnet nuget push ".builds\Debug\*.nupkg" --source C:\Users\%USERNAME%\source\repos\nuget
REM dotnet nuget push ".builds\Release\*.nupkg" --skip-duplicate --source https://api.nuget.org/v3/index.json
REM dotnet build "\BDMCommandLine" --configuration Debug --output ".builds\Debug"



dotnet build BDMCommandLine --configuration Debug --output ".builds\Debug"
dotnet build BDMCommandLine --configuration Release --output ".builds\Debug"
dotnet nuget push ".builds\Debug\BDMCommandLine*.nupkg" --skip-duplicate --source 
	
"C:\Users\" + + "\source\repos\nuget
dotnet nuget push ".builds\Release\BDMCommandLine*.nupkg" --skip-duplicate --source https://api.nuget.org/v3/index.json


nuget setapikey oy2lztlvf2mz2237l3yaglllm4q36mjezgb2teqqgronbi -Source https://api.nuget.org/v3/index.json