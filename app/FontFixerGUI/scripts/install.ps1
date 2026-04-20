$ErrorActionPreference = "Stop"

$fontDir = Resolve-Path "$PSScriptRoot\..\fonts"
$winFonts = "$env:WINDIR\Fonts"

Write-Host "Installing fonts..."

# Install all fonts automatically (TTF or OTF)
Get-ChildItem $fontDir -Include *.ttf, *.otf | ForEach-Object {
    $dest = Join-Path $winFonts $_.Name

    if (-not (Test-Path $dest)) {
        Copy-Item $_.FullName $dest
    }
}

# Registry path
$regPath = "HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes"

# Ensure key exists
if (-not (Test-Path $regPath)) {
    New-Item $regPath | Out-Null
}

# Apply substitutions safely
Set-ItemProperty $regPath "MS Gothic" "Noto Sans JP"
Set-ItemProperty $regPath "MS UI Gothic" "Noto Sans JP"
Set-ItemProperty $regPath "MS PGothic" "Noto Sans JP"

Write-Host "Done. Please restart."