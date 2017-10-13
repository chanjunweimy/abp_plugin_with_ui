$currentPath = (Get-Item -Path "./" -Verbose).FullName
$releasePath = Join-Path $currentPath ".\Todo.PluginDownloader\bin\Release\netcoreapp2.0\*.*"
$downloaderPath = Join-Path $currentPath "..\angular\downloader"

dotnet restore
dotnet build -c Release

if (!(Test-Path $downloaderPath)) {
    mkdir $downloaderPath
}
Copy-Item $releasePath $downloaderPath -Force -Recurse