# Wait until SmartManager API health is UP
param(
    [string]$BaseUrl = 'http://localhost:8080',
    [int]$TimeoutSec = 180,
    [int]$IntervalSec = 3
)

$ErrorActionPreference = 'Stop'
$uri = "$BaseUrl/api/health"
$deadline = (Get-Date).AddSeconds($TimeoutSec)

Write-Host "Waiting for API: $uri (timeout ${TimeoutSec}s)" -ForegroundColor Cyan

while ((Get-Date) -lt $deadline) {
    try {
        $resp = Invoke-RestMethod -Uri $uri -Method GET -TimeoutSec 5
        if ($resp.status -eq 'UP') {
            Write-Host 'API is UP' -ForegroundColor Green
            exit 0
        }
    } catch {
        # not ready yet
    }
    Start-Sleep -Seconds $IntervalSec
}

Write-Host "API health timeout after ${TimeoutSec}s" -ForegroundColor Red
exit 1
