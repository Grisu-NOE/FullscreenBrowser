if($BUILD_SOURCESDIRECTORY)
{
	$file = $BUILD_SOURCESDIRECTORY
}
else
{
	$file = "C:\Git\grisu-noe-infoscreen"
}

$file = $file + "\Version.txt"
$fileVersion = [version](Get-Content $file | Select -First 1)
$newVersion = "{0}.{1}.{2}.{3}" -f $fileVersion.Major, $fileVersion.Minor, $fileVersion.Build, ($fileVersion.Revision + 1)
$newVersion | Set-Content $file