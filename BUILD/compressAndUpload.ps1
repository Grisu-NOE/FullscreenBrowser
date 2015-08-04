$baseUri = "https://api.github.com/repos/Grisu-NOE/Infoscreen"

$latestRelease = Invoke-WebRequest -UseBasicParsing -Uri "$baseUri/releases/latest" -Method Get
Write-Host "Latest release status code is $($latestRelease.StatusCode) $($latestRelease.StatusDescription)"
$latestReleaseContent = $latestRelease.Content | ConvertFrom-Json

$commits = Invoke-WebRequest -UseBasicParsing -Uri "$baseUri/commits?since=$($latestReleaseContent.created_at)" -Method Get
Write-Host "Commits status code is $($commits.StatusCode) $($commits.StatusDescription)"
$contents = $commits.Content | ConvertFrom-Json

$targetCommitish = "master"
foreach ($content in $contents) {
  if($content.commit.message -eq "//***NO_CI***//")
  {
	$targetCommitish = $content.sha
	break
  }
}

$file = "$Env:BUILD_SOURCESDIRECTORY\Version.txt"
$fileVersion = [version](Get-Content $file | Select -First 1)
Write-Host "Version is $fileVersion"

$binaries = "$Env:BUILD_STAGINGDIRECTORY\$fileVersion"
Move-Item "$Env:BUILD_STAGINGDIRECTORY\*" $binaries

$process = "$Env:BUILD_SOURCESDIRECTORY\BUILD\7z\7z.exe"
$compressedFileName = "$fileVersion.exe"
$compressedFilePath = "$Env:BUILD_STAGINGDIRECTORY\$compressedFileName"
Start-Process -FilePath $process -ArgumentList "a -t7z -m0=lzma2 -mx=$Env:CompressionLevel -mfb=64 -md=128m -ms=on -sfx ""$compressedFilePath"" ""$binaries""" -Wait -NoNewWindow -PassThru

$body = @{
  "tag_name"="v$fileVersion"
  "target_commitish"=$targetCommitish
  "name"="v$fileVersion"
  "body"="# NIGHTLY BUILD`n## Visual Studio`n* Name of the Build Definition: $Env:BUILD_DEFINITIONNAME`n* Build number: [$Env:BUILD_BUILDNUMBER]($Env:SYSTEM_TEAMFOUNDATIONCOLLECTIONURI$Env:SYSTEM_TEAMPROJECT/_build#_a=summary&buildId=$Env:BUILD_BUILDID)"
  "draft"=[System.Convert]::ToBoolean($Env:GitDraft)
  "prerelease"=[System.Convert]::ToBoolean($Env:GitPreRelease)
}

$auth = @{
  "Authorization"="token " + $Env:GitToken
}

$releaseAnswer = Invoke-WebRequest -Headers $auth -UseBasicParsing -Uri "$baseUri/releases" -Method Post -ContentType "application/json; charset=utf-8" -Body $($body | ConvertTo-Json -Depth 5 -Compress)
Write-Host "Release status code is $($releaseAnswer.StatusCode) $($releaseAnswer.StatusDescription)"
if (-not $releaseAnswer.StatusCode.Equals(201))
{
    Write-Error "Release status code is not 201! Aborting release creation..."
    exit 1
}

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
