# List installed software
#Get-ItemProperty HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall\* |  Select-Object DisplayName, DisplayVersion, Publisher, InstallDate | Format-Table
#Get-ItemProperty HKLM:\Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\* | Select-Object DisplayName, DisplayVersion, Publisher, InstallDate | Format-Table

$file = $Env:BUILD_SOURCESDIRECTORY + "\Version.txt"
$fileVersion = [version](Get-Content $file | Select -First 1)
Write-Host "Version is $fileVersion"

Write-Host "Compression level is $Env:CompressionLevel"
& "$Env:BUILD_SOURCESDIRECTORY\BUILD\7z\7z.exe" a -t7z -mx$($Env:CompressionLevel) -mmt -ms -sfx "$($Env:BUILD_STAGINGDIRECTORY)\drop\$fileVersion" -xr!logs
