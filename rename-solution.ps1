$newName = Read-Host "Please input the new name in PascalCase"

$oldName = "AwesomeAspApp"
$oldNameCamelCase = "awesomeAspApp"
$oldNameKebabCase = "awesome-asp-app"
$oldNameTitleCase = "Awesome Asp App"

$newNameCamelCase = [char]::ToLower($newName[0]) + $newName.Substring(1)
$newNameKebabCase = ($newName -creplace ('(?<!^)[A-Z]', '-$&')).ToLower()
$newNameTitleCase = $newName -creplace ('(?<!^)[A-Z]', ' $&')

function Replace-Name([String] $value) {
    $value.
    Replace($oldName, $newName).
    Replace($oldNameCamelCase, $newNameCamelCase).
    Replace($oldNameKebabCase, $newNameKebabCase).
    Replace($oldNameTitleCase, $newNameTitleCase);
}

$currentPath = (Get-Location).Path

Write-Host "Working in $currentPath"

Get-ChildItem -Path "." -Recurse -File | % {
    if ($_.Name -eq "rename-solution.ps1") {
        Return
    }

    Write-Host "Processing $_..."

    # Read file, replace name and delete file
    $content = [System.IO.File]::ReadAllText($_.FullName)
    $content = Replace-Name $content
    Remove-Item -Path $_.FullName

    # create destination directory
    $relevantPath = $_.FullName.Substring($currentPath.Length)
    $newPath = Replace-Name $relevantPath
    $newPath = "$currentPath$newPath"

    $directory = [System.IO.Path]::GetDirectoryName($newPath)
    New-Item -ItemType Directory -Force -Path $directory | Out-Null

    # write new file
    [System.IO.File]::WriteAllText($newPath, $content)
    
    Write-Host "Moved to $newPath"
}

Get-ChildItem -Path "." -Recurse -Directory | % {
    if ($_.GetFiles("**", [System.IO.SearchOption]::AllDirectories).Length -eq 0) {
        Write-Host "Delete $_"
        Remove-Item -Path $_.FullName -Force -Recurse
    }
}