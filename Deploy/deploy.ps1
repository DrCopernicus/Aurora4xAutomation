$ScriptDir = Split-Path $MyInvocation.MyCommand.Path -Parent

$ServerDestination = "$ScriptDir\Server"
$ClientDestination = "$ScriptDir\Client"

$Version = Read-Host "Target version"

# requires Powershell 3 and .NET 4.5
function ZipFiles( $destinationPath, $sourcePath )
{
   Add-Type -Assembly System.IO.Compression.FileSystem
   $compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
   [System.IO.Compression.ZipFile]::CreateFromDirectory($sourcePath,
        $destinationPath, $compressionLevel, $false)
}

# Clean last release
Remove-Item "$ServerDestination" -Recurse -ErrorAction Ignore
Remove-Item "$ClientDestination" -Recurse -ErrorAction Ignore
Remove-Item "$ServerDestination-*.zip" -ErrorAction Ignore
Remove-Item "$ClientDestination-*.zip" -ErrorAction Ignore

# Make server release folder
Copy-Item "$ScriptDir\..\Server\bin\Release" "$ServerDestination" -Recurse -ErrorAction Ignore
Remove-Item "$ServerDestination\*.pdb"
Remove-Item "$ServerDestination\*.vshost.exe"
Remove-Item "$ServerDestination\*.vshost.exe.config"
Remove-Item "$ServerDestination\*.vshost.exe.manifest"

ZipFiles "$ServerDestination-$Version.zip" "$ServerDestination"

# Make client release folder
Copy-Item "$ScriptDir\..\Client\bin\Release" "$ClientDestination" -Recurse -ErrorAction Ignore
Remove-Item "$ClientDestination\*.pdb"
Remove-Item "$ClientDestination\*.vshost.exe"
Remove-Item "$ClientDestination\*.vshost.exe.config"
Remove-Item "$ClientDestination\*.vshost.exe.manifest"

ZipFiles "$ClientDestination-$Version.zip" "$ClientDestination"

