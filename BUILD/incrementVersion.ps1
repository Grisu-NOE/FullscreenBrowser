$baseUri = "https://api.github.com/repos/Grisu-NOE/FullscreenBrowser"

Write-Host "BUILD_SOURCESDIRECTORY: $Env:BUILD_SOURCESDIRECTORY"
$fileName = "Version.txt"
$file = "$Env:BUILD_SOURCESDIRECTORY\$fileName"
$fileVersion = [version](Get-Content $file | Select -First 1)
$newVersion = "{0}.{1}.{2}.{3}" -f $fileVersion.Major, $fileVersion.Minor, $fileVersion.Build, ($fileVersion.Revision + 1)
Write-Host "New version: $newVersion"
$newVersion | Set-Content $file

$getAnswer = Invoke-WebRequest -UseBasicParsing -Uri "$baseUri/contents/$fileName" -Method Get
if (-not $getAnswer.StatusCode.Equals(200))
{
    Write-Error "HTTP status code is not 200! Returned $($getAnswer.StatusCode) $($getAnswer.StatusDescription)"
    exit 1
}

$serverContent = $getAnswer.Content | ConvertFrom-Json
Write-Host "SHA: $($serverContent.sha)"

$newVersionBytes = [System.Text.Encoding]::UTF8.GetBytes($newVersion)
$body = @{
  "message"="//***NO_CI***//"
  "committer"=@{
    "name"=$Env:GitUserName
    "email"=$Env:GitUserEmail
  }
  "content"=[System.Convert]::ToBase64String($newVersionBytes)
  "sha"=$serverContent.sha
}

Write-Host "Committer name is $($body.committer.name)"
Write-Host "Committer email is $($body.committer.email)"

$auth = @{
  "Authorization"="token $Env:GitToken"
}

$postAnswer = Invoke-WebRequest -Headers $auth -UseBasicParsing -Uri $serverContent.url -Method Put -ContentType "application/json; charset=utf-8" -Body $($body | ConvertTo-Json -Depth 5 -Compress)
Write-Host "Update status code is $($postAnswer.StatusCode) $($postAnswer.StatusDescription)"
