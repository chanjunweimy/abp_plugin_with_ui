dotnet restore

$currentPath = (Get-Item -Path "./" -Verbose).FullName
$uiProjectPath = Join-Path $currentPath ".\Todo.DemoPlugin.AngularUI"
$wwwrootPath = Join-Path $uiProjectPath ".\wwwroot\demoplugin"
$webcoreProjectPath = Join-Path $currentPath ".\Todo.DemoPlugin.Web.Core";
$metadetaJson = Join-Path $webcoreProjectPath "Todo.DemoPlugin.json"
$releasePath = Join-Path $webcoreProjectPath ".\bin\x64\Release\netcoreapp2.0"
$hostPath = Join-Path $currentPath "..\src\Todo.MainProject.Web.Host"
$pluginPath = Join-Path $hostPath "PlugIns"
$pluginDllPath = Join-Path $pluginPath "Todo.DemoPlugin"


Set-Location $uiProjectPath
yarn
ng build -bh /demoplugin/ -prod -aot

Set-Location $webcoreProjectPath
Compress-Archive -Path $wwwrootPath -DestinationPath demoplugin.zip -Force
dotnet msbuild /t:clean
dotnet msbuild /t:rebuild /p:Configuration=Release /p:Platform=x64

if (! (Test-Path $pluginPath)) {
    New-Item -ItemType Directory -Force -Path $pluginPath
}

if (! (Test-Path $pluginDllPath)) {
    New-Item -ItemType Directory -Force -Path $pluginDllPath
}

Copy-Item $metadetaJson $pluginPath -Force

$releaseItems = Join-Path $releasePath "Todo.DemoPlugin.*"
Copy-Item $releaseItems $pluginDllPath -Force

Set-Location $currentPath
