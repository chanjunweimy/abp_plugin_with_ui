$currentPath = (Get-Item -Path "./" -Verbose).FullName

$pluginPath = Join-Path $currentPath ".\aspnet-core\Todo.DemoPlugin"
Set-Location $pluginPath
.\Todo.DemoPluginDeploy.ps1

$downloaderPath = Join-Path $currentPath ".\plugin-downloader"
Set-Location $downloaderPath
.\Todo.PluginDownloaderDeploy.ps1

Set-Location $currentPath