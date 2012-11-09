[xml]$config = Get-Content Packages.xml

$projects = $config.packages.package
$nugetExe = Resolve-Path .nuget\nuget.exe
    
Remove-Item build\*.nupkg

foreach($project in $projects)
{
    Write-Host "Packing project «$project»..."
    Set-Location -Path "$project"

    & $nugetExe pack $project.csproj -Build -Symbols -Properties Configuration=Release

    Write-Host "Moving nuget package to the build folder..."
    Move-Item .\*.nupkg ..\build\ -Force

    Set-Location ..
}

$packages = Get-ChildItem build\*.nupkg
foreach($package in $packages)
{
	if ($config.packages.server)
	{
		& $nugetExe push $package -s $config.packages.server $config.packages.apiKey
	}
	else
	{
		& $nugetExe push $package
	}
}

Write-Host "Operation completed!"