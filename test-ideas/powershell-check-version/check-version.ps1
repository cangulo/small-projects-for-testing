[CmdletBinding()]
param (
    [Parameter(Mandatory)]
    [ValidatePattern("^[0-9]{2}\.[0-9]{1,2}\.[0-9]{1,2}\.[0-9]{1,2}$", 
        ErrorMessage="Please provide a full version following the XX.YY.Z.W format")]
    [string]$fullVersionNumber
)

$separator = "."
$versionArray = $fullVersionNumber.Split($separator)
$majorVersion = $versionArray[0]
$minorVersion = If ($versionArray[1].Length -eq 1) { "0" + $versionArray[1] } Else { $versionArray[1] }
