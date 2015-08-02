Write-Host "BUILD_SOURCESDIRECTORY: $Env:BUILD_SOURCESDIRECTORY"
$file = $Env:BUILD_SOURCESDIRECTORY + "\Version.txt"
$fileVersion = [version](Get-Content $file | Select -First 1)
$newVersion = "{0}.{1}.{2}.{3}" -f $fileVersion.Major, $fileVersion.Minor, $fileVersion.Build, ($fileVersion.Revision + 1)
Write-Host "Calculated version: $newVersion"
$newVersion | Set-Content $file
$newFileVersion = [version](Get-Content $file | Select -First 1)
Write-Host "Stored version: $newFileVersion"