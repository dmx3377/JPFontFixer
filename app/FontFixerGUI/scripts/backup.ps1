$regPath = "HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes"

reg export "HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes" `
"$PSScriptRoot\backup.reg" /y

Write-Host "Backup saved."