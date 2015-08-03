# List installed software
#Get-ItemProperty HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall\* |  Select-Object DisplayName, DisplayVersion, Publisher, InstallDate | Format-Table
#Get-ItemProperty HKLM:\Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\* | Select-Object DisplayName, DisplayVersion, Publisher, InstallDate | Format-Table

$file = $Env:BUILD_SOURCESDIRECTORY + "\Version.txt"
$fileVersion = [version](Get-Content $file | Select -First 1)
Write-Host "Version is $fileVersion"

$binaries = "$Env:BUILD_STAGINGDIRECTORY\$fileVersion"
Move-Item "$Env:BUILD_STAGINGDIRECTORY\*" $binaries

$process = "$Env:BUILD_SOURCESDIRECTORY\BUILD\7z\7z.exe"
Start-Process -FilePath $process -ArgumentList "a -t7z -m0=lzma2 -mx=$Env:CompressionLevel -mfb=64 -md=128m -ms=on -sfx ""$Env:BUILD_STAGINGDIRECTORY\$fileVersion.exe"" ""$binaries""" -Wait -NoNewWindow -PassThru

