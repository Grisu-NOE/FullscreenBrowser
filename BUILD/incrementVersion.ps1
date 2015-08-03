Write-Host "BUILD_SOURCESDIRECTORY: $Env:BUILD_SOURCESDIRECTORY"
$file = $Env:BUILD_SOURCESDIRECTORY + "\Version.txt"
$fileVersion = [version](Get-Content $file | Select -First 1)
$newVersion = "{0}.{1}.{2}.{3}" -f $fileVersion.Major, $fileVersion.Minor, $fileVersion.Build, ($fileVersion.Revision + 1)
Write-Host "Calculated version: $newVersion"
#$newVersion | Set-Content $file
#$newFileVersion = [version](Get-Content $file | Select -First 1)
#Write-Host "Stored version: $newFileVersion"

$getAnswer = Invoke-WebRequest -UseBasicParsing -Uri https://api.github.com/repos/Grisu-NOE/Infoscreen/contents/Version.txt -Method Get
if (-not $getAnswer.StatusCode.Equals(200))
{
    Write-Error "HTTP status code is not 200! Returned $($getAnswer.StatusCode) $($getAnswer.StatusDescription)"
    exit 1
}

$serverContent = $getAnswer.Content | ConvertFrom-Json
Write-Host "SHA: $($serverContent.sha)"
$body = @{
  "message"="//***NO_CI***//"
  "committer"=@{
    "name"=$Env:GitUserEmail
    "email"=$Env:GitUserName
  }
  "content"=$newVersion
  "sha"=$serverContent.sha
}

$postAnswer = Invoke-WebRequest -UseBasicParsing -Uri https://api.github.com/repos/Grisu-NOE/Infoscreen/contents/Version.txt -Method Put -ContentType "application/json; charset=utf-8" -Body $($body | ConvertTo-Json -Depth 5 -Compress)
Write-Host "Update status code is $($postAnswer.StatusCode) $($postAnswer.StatusDescription)"