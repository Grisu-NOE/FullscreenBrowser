$file = $Env:BUILD_SOURCESDIRECTORY + "\Version.txt"
$fileVersion = [version](Get-Content $file | Select -First 1)
Write-Host "Version is $fileVersion"

# Regular expression pattern to find the version in the build number 
# and then apply it to the assemblies
$VersionRegex = "\d+\.\d+\.\d+\.\d+"

$solutionInfo = $Env:BUILD_SOURCESDIRECTORY + "\SolutionInfo.cs"
$filecontent = Get-Content($solutionInfo)
attrib $solutionInfo -r
$filecontent -replace $VersionRegex, $fileVersion | Out-File $solutionInfo
Write-Verbose "$solutionInfo.FullName - version applied"