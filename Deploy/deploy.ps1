$ScriptDir = Split-Path $MyInvocation.MyCommand.Path -Parent

$ServerDestination = "$ScriptDir\Aurora4xAutomation\Server"
$ClientDestination = "$ScriptDir\Aurora4xAutomation\Client"

# requires Powershell 3 and .NET 4.5
function ZipFiles( $destinationPath, $sourcePath )
{
   Add-Type -Assembly System.IO.Compression.FileSystem
   $compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
   [System.IO.Compression.ZipFile]::CreateFromDirectory($sourcePath,
        $destinationPath, $compressionLevel, $false)
}

# Clean last release
Remove-Item "$ScriptDir\Aurora4xAutomation" -Recurse -ErrorAction Ignore
Remove-Item "$ScriptDir\Aurora4xAutomation.zip" -ErrorAction Ignore

# Make server release folder
Copy-Item "$ScriptDir\..\Server\bin\Release" "$ServerDestination" -Recurse -ErrorAction Ignore
Remove-Item "$ServerDestination\*.pdb"
Remove-Item "$ServerDestination\*.vshost.exe"
Remove-Item "$ServerDestination\*.vshost.exe.config"
Remove-Item "$ServerDestination\*.vshost.exe.manifest"

# Make client release folder
Copy-Item "$ScriptDir\..\Client\bin\Release" "$ClientDestination" -Recurse -ErrorAction Ignore
Remove-Item "$ClientDestination\*.pdb"
Remove-Item "$ClientDestination\*.vshost.exe"
Remove-Item "$ClientDestination\*.vshost.exe.config"
Remove-Item "$ClientDestination\*.vshost.exe.manifest"

ZipFiles "$ScriptDir\Aurora4xAutomation.zip" "$ScriptDir\Aurora4xAutomation"

