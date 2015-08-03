# List installed software
#Get-ItemProperty HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall\* |  Select-Object DisplayName, DisplayVersion, Publisher, InstallDate | Format-Table
#Get-ItemProperty HKLM:\Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\* | Select-Object DisplayName, DisplayVersion, Publisher, InstallDate | Format-Table

$file = $Env:BUILD_SOURCESDIRECTORY + "\Version.txt"
$fileVersion = [version](Get-Content $file | Select -First 1)
Write-Host "Version is $fileVersion"

#$folder = $Env:AGENT_BUILDDIRECTORY + "\drop"
#New-Item -ItemType Directory -Force -Path $folder

$binaries = "$Env:BUILD_STAGINGDIRECTORY\$fileVersion"

Move-Item "$Env:BUILD_STAGINGDIRECTORY\*" $binaries

Write-Host "Compression level is $Env:CompressionLevel"
& "$Env:BUILD_SOURCESDIRECTORY\BUILD\7z\7z.exe" a -t7z -mx$($Env:CompressionLevel) -mmt -ms -sfx "$fileVersion.exe" $binaries -xr!logs
