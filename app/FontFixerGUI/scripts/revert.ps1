$regPath = "HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes"

Write-Host "Reverting font substitutions..."

if (Test-Path $regPath) {
    Remove-ItemProperty $regPath "MS Gothic" -ErrorAction SilentlyContinue
    Remove-ItemProperty $regPath "MS UI Gothic" -ErrorAction SilentlyContinue
    Remove-ItemProperty $regPath "MS PGothic" -ErrorAction SilentlyContinue
}

Write-Host "Reverted. Please restart your computer."