$baseUri = "https://api.github.com/repos/Grisu-NOE/Infoscreen"

$latestRelease = Invoke-WebRequest -Uri "$baseUri/releases/latest" -Method Get
Write-Host "Latest release status code is $($latestRelease.StatusCode) $($latestRelease.StatusDescription)"
$latestReleaseContent = $latestRelease.Content | ConvertFrom-Json

$commits = Invoke-WebRequest -UseBasicParsing -Uri "$baseUri/commits?since=$($latestReleaseContent.created_at)" -Method Get
Write-Host "Commits status code is $($commits.StatusCode) $($commits.StatusDescription)"
$contents = $commits.Content | ConvertFrom-Json

$abort = $true
foreach ($content in $contents) {
  if($content.commit.message -ne "//***NO_CI***//")
  {
	$abort = $false
	break
  }
}

if($abort)
{
  Write-Error "No new commits. Aborting.."
  exit 1
}