Param
(
    [Parameter(Mandatory=$true)]
    [ValidateSet("Debug", "Release")]
    [String] $Configuration = "Debug",

    [Parameter(Mandatory=$false)]
    [String] $Project
)
[String] $NuGetAPIKey = . ([IO.Path]::Combine($Env:UserProfile, "source\repos\bradleydonmorris\LifeBook\Keys\GetKey.ps1")) `
    -Name "BDMPackages.NuGet.APIKEY";
nuget setapikey $NuGetAPIKey -Source https://api.nuget.org/v3/index.json

[String] $NuGetSource = [IO.Path]::Combine($Env:UserProfile, "source\repos\nuget");
[Boolean] $IsLocalPublish = $false;
Switch ($Configuration)
{
    "Debug"
    {
        $NuGetSource = [IO.Path]::Combine($Env:UserProfile, "source\repos\nuget");
        $IsLocalPublish = $true;
    }
    "Release"
    {
        $NuGetSource = "https://api.nuget.org/v3/index.json";
        $IsLocalPublish = $false;
    }
}
If ($Project)
{
    [String] $ProjectFilePath = [IO.Path]::Combine($PWD, $Project, [String]::Format("{0}.csproj", $Project));
    If ([IO.File]::Exists($ProjectFilePath))
    {
        dotnet build $Project --configuration $Configuration --output ".builds\$Configuration"
        If ($IsLocalPublish)
        {
            dotnet nuget push ".builds\$Configuration\$Project*.nupkg" --source $NuGetSource
        }
        Else
        {
            dotnet nuget push ".builds\$Configuration\$Project*.nupkg" --skip-duplicate --source $NuGetSource
        }
    }
}
Else
{
    ForEach ($Directory In (Get-ChildItem -Directory))
    {
        [String] $ProjectName = $Directory.Name;
        [String] $ProjectFilePath = [IO.Path]::Combine($Directory.FullName, [String]::Format("{0}.csproj", $ProjectName));
        If ([IO.File]::Exists($ProjectFilePath))
        {
            $ProjectFilePath
        }
    }
}
