# P0 + Part06 E2E batch runner
# Run: powershell -NoProfile -ExecutionPolicy Bypass -File scripts/e2e-p1-verify.ps1

$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot

$scripts = @(
    'e2e-full-tx-chain-verify.ps1',
    'e2e-payable-approval-verify.ps1',
    'e2e-month-closing-verify.ps1',
    'e2e-part06-outsource-verify.ps1',
    'e2e-calendar-capa.ps1'
)

$failed = @()
foreach ($name in $scripts) {
    Write-Host "`n########################################" -ForegroundColor Magenta
    Write-Host "# $name" -ForegroundColor Magenta
    Write-Host "########################################`n" -ForegroundColor Magenta
    & "$root\$name"
    $code = $LASTEXITCODE
    if ($code -ne 0) {
        $failed += $name
    }
}

Write-Host "`n=== P0+P1 E2E batch result ===" -ForegroundColor Cyan
if ($failed.Count -eq 0) {
    Write-Host 'ALL PASS' -ForegroundColor Green
    exit 0
}
Write-Host "FAILED: $($failed -join ', ')" -ForegroundColor Red
exit 1
