$fontDir = Join-Path $PSScriptRoot "..\fonts"
$fontDir = Resolve-Path $fontDir

$windowsFontDir = "$env:WINDIR\Fonts"
$regPath = "HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts"

function Test-FontInstalled($name) {
    $fonts = Get-ItemProperty -Path $regPath -ErrorAction SilentlyContinue
    return $fonts -and ($fonts.PSObject.Properties.Name -contains $name)
}

# REGULAR
if (-not (Test-FontInstalled "Noto Sans JP (TrueType)")) {
    Copy-Item "$fontDir\NotoSansJP-Regular.otf" $windowsFontDir -Force

    New-ItemProperty -Path $regPath `
        -Name "Noto Sans JP (TrueType)" `
        -Value "NotoSansJP-Regular.otf" `
        -PropertyType String -Force
}

# BOLD
if (-not (Test-FontInstalled "Noto Sans JP Bold (TrueType)")) {
    Copy-Item "$fontDir\NotoSansJP-Bold.otf" $windowsFontDir -Force

    New-ItemProperty -Path $regPath `
        -Name "Noto Sans JP Bold (TrueType)" `
        -Value "NotoSansJP-Bold.otf" `
        -PropertyType String -Force
}

Write-Host "Fonts installed."