# List installed software
#Get-ItemProperty HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall\* |  Select-Object DisplayName, DisplayVersion, Publisher, InstallDate | Format-Table
#Get-ItemProperty HKLM:\Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\* | Select-Object DisplayName, DisplayVersion, Publisher, InstallDate | Format-Table

$file = $Env:BUILD_SOURCESDIRECTORY + "\Version.txt"
$fileVersion = [version](Get-Content $file | Select -First 1)
Write-Host "Version is $fileVersion"

$binaries = "$Env:BUILD_STAGINGDIRECTORY\$fileVersion"
Move-Item "$Env:BUILD_STAGINGDIRECTORY\*" $binaries

$process = "$Env:BUILD_SOURCESDIRECTORY\BUILD\7z\7z.exe"
$compressedFileName = "$fileVersion.exe"
$compressedFilePath = "$Env:BUILD_STAGINGDIRECTORY\$compressedFileName"
Start-Process -FilePath $process -ArgumentList "a -t7z -m0=lzma2 -mx=$Env:CompressionLevel -mfb=64 -md=128m -ms=on -sfx ""$compressedFilePath"" ""$binaries""" -Wait -NoNewWindow -PassThru

Write-Host "SYSTEM_TEAMFOUNDATIONCOLLECTIONURI: $Env:SYSTEM_TEAMFOUNDATIONCOLLECTIONURI"
Write-Host "SYSTEM_TEAMFOUNDATIONSERVERURI: $Env:SYSTEM_TEAMFOUNDATIONSERVERURI"
Write-Host "BUILD_BUILDID: $Env:BUILD_BUILDID"

$body = @{
  "tag_name"="v$fileVersion"
  "target_commitish"="master"
  "name"="v$fileVersion"
  "body"="#NIGHTLY build`n##Visual Studio`n* Name of the Build Definition: $Env:BUILD_DEFINITIONNAME`n* Build number: $Env:BUILD_BUILDNUMBER`n* Build URI: $Env:BUILD_BUILDURI"
  "draft"=[System.Convert]::ToBoolean($Env:GitDraft)
  "prerelease"=[System.Convert]::ToBoolean($Env:GitPreRelease)
}

$auth = @{
  "Authorization"="token " + $Env:GitToken
}

$releaseAnswer = Invoke-WebRequest -Headers $auth -UseBasicParsing -Uri "https://api.github.com/repos/Grisu-NOE/Infoscreen/releases" -Method Post -ContentType "application/json; charset=utf-8" -Body $($body | ConvertTo-Json -Depth 5 -Compress)
Write-Host "Release status code is $($releaseAnswer.StatusCode) $($releaseAnswer.StatusDescription)"

$releaseContent = $releaseAnswer.Content | ConvertFrom-Json
$uri = $releaseContent.upload_url.Replace("{?name}","?name=$compressedFileName")

switch([System.IO.Path]::GetExtension($compressedFileName))
{
  ".7z" { $contentType = "application/x-7z-compressed" }
  ".exe" { $contentType = "application/x-msdownload" }
  ".msi" { $contentType = "application/x-msi" }
  ".rar" { $contentType = "application/x-rar-compressed" }
  ".zip" { $contentType = "application/zip" }
  default {
    Write-Error "Extension not supported by script"
    exit 1
  }
}

$uploadAnswer = Invoke-WebRequest -Headers $auth -UseBasicParsing -Uri "$uri" -Method Post -ContentType "$contentType" -InFile "$compressedFilePath"
Write-Host "Upload status code is $($uploadAnswer.StatusCode) $($uploadAnswer.StatusDescription)"