# ============================================================
#  InsureTrust — Start Both Projects (API + Web)
#  Usage: Right-click → "Run with PowerShell"
#         OR: .\start-dev.ps1 from the solution root
# ============================================================

$root = Split-Path -Parent $MyInvocation.MyCommand.Path

$apiPath = Join-Path $root "InsureTrust.API"
$webPath = Join-Path $root "InsureTrust.Web"

# ---- Kill any stale dotnet processes on the target ports ----
Write-Host ""
Write-Host "  🔄  Checking for stale processes on ports 5000 / 5100 / 5101 ..." -ForegroundColor Cyan

@(5000, 5100, 5101, 7000) | ForEach-Object {
    $port = $_
    $pids = (netstat -ano | Select-String ":$port ") -replace '.*\s+', '' | Sort-Object -Unique
    foreach ($p in $pids) {
        if ($p -match '^\d+$') {
            try {
                Stop-Process -Id ([int]$p) -Force -ErrorAction SilentlyContinue
                Write-Host "  ✅  Freed port $port  (PID $p)" -ForegroundColor Green
            } catch {}
        }
    }
}

Start-Sleep -Milliseconds 800

# ---- Launch API in a new coloured PowerShell window ----
Write-Host ""
Write-Host "  🚀  Starting InsureTrust.API  →  https://localhost:7000" -ForegroundColor Yellow

Start-Process powershell -ArgumentList @(
    "-NoExit",
    "-Command",
    "& { `$host.UI.RawUI.WindowTitle = 'InsureTrust API  |  :7000'; `$host.UI.RawUI.BackgroundColor = 'DarkBlue'; Clear-Host; Write-Host ' ⬡  InsureTrust API' -ForegroundColor Yellow; Write-Host '    https://localhost:7000' -ForegroundColor Cyan; Write-Host ''; Set-Location '$apiPath'; dotnet watch run }"
)

Start-Sleep -Milliseconds 1500

# ---- Launch Web in a new coloured PowerShell window ----
Write-Host "  🌐  Starting InsureTrust.Web  →  https://localhost:5101" -ForegroundColor Magenta

Start-Process powershell -ArgumentList @(
    "-NoExit",
    "-Command",
    "& { `$host.UI.RawUI.WindowTitle = 'InsureTrust Web  |  :5101'; `$host.UI.RawUI.BackgroundColor = 'DarkMagenta'; Clear-Host; Write-Host ' ⬡  InsureTrust Web' -ForegroundColor Yellow; Write-Host '    https://localhost:5101' -ForegroundColor Cyan; Write-Host ''; Set-Location '$webPath'; dotnet watch run }"
)

Write-Host ""
Write-Host "  ✅  Both windows launched! Close this window now." -ForegroundColor Green
Write-Host ""
Write-Host "  API  →  https://localhost:7000   (Swagger: /swagger)" -ForegroundColor Cyan
Write-Host "  Web  →  https://localhost:5101" -ForegroundColor Cyan
Write-Host ""

# Keep this window open briefly so the user can read it
Start-Sleep -Seconds 5
