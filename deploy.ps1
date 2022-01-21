dotnet build -c Debug ./Integration1C.UI/Integration1C.UI.csproj
dotnet publish -c Debug ./Integration1C.ConsoleClient/Integration1C.ConsoleClient.csproj
$version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$PSScriptRoot/Integration1C.UI/bin/Debug/netcoreapp3.1/publish/Integration1C.UI.exe").FileVersion
new-item -Path "./publish/Integration1C_v$version" -itemType Directory -Force	
Get-ChildItem "./Integration1C.UI/bin/Debug/netcoreapp3.1/publish" -File -Recurse |copy-item -destination "./publish/Integration1C_v$version" -Force
Get-ChildItem "./Integration1C.ConsoleClient/bin/Debug/netcoreapp3.1/publish" -File -Recurse |copy-item -destination "./publish/Integration1C_v$version" -Force
copy-item ./Integration1C.UI/bin/Debug/netcoreapp3.1/publish/ru "./publish/Integration1C_v$version" -Force
copy-item ./Integration1C.ConsoleClient/bin/Debug/netcoreapp3.1/publish/ru "./publish/Integration1C_v$version" 
copy-item ./Integration1C.UI/bin/Debug/netcoreapp3.1/publish/runtimes "./publish/Integration1C_v$version" -Recurse -Force
copy-item ./Integration1C.ConsoleClient/bin/Debug/netcoreapp3.1/publish/runtimes "./publish/Integration1C_v$version" -Recurse 
compress-archive -path "./publish/Integration1C_v$version" -DestinationPath "./publish/Integration1C_v$version.zip" -CompressionLevel Optimal -Force
