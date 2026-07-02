# Run SmartManager API (Gradle)
$backend = Join-Path (Split-Path (Split-Path $PSScriptRoot -Parent) -Parent) "smartmanager_backend"
Set-Location $backend
.\gradlew.bat :smartmanager-api:bootRun
