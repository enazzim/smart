# Clean or drop local SmartManager DB (dev only)
# Usage:
#   powershell -File scripts/clean-test-db.ps1 -Mode truncate
#   powershell -File scripts/clean-test-db.ps1 -Mode drop

param(
    [ValidateSet('truncate', 'drop')]
    [string]$Mode = 'truncate',
    [string]$EnvFile = '',
    [string]$MysqlExe = ''
)

$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent

if (-not $EnvFile) {
    $EnvFile = Join-Path $root 'config\local-db.env'
}

function Read-DbConfig {
    param([string]$Path)
    $cfg = @{
        Host = 'localhost'
        Port = '3306'
        Name = 'smartmanager'
        User = 'root'
        Password = '1111'
    }
    if (-not (Test-Path $Path)) { return $cfg }
    foreach ($line in Get-Content -Path $Path -Encoding UTF8) {
        if ($line -match '^\s*#' -or $line -match '^\s*$') { continue }
        if ($line -match '^SMARTMANAGER_DB_HOST=(.+)$') { $cfg.Host = $Matches[1].Trim() }
        elseif ($line -match '^SMARTMANAGER_DB_PORT=(.+)$') { $cfg.Port = $Matches[1].Trim() }
        elseif ($line -match '^SMARTMANAGER_DB_NAME=(.+)$') { $cfg.Name = $Matches[1].Trim() }
        elseif ($line -match '^SMARTMANAGER_DB_USER=(.+)$') { $cfg.User = $Matches[1].Trim() }
        elseif ($line -match '^SMARTMANAGER_DB_PASSWORD=(.+)$') { $cfg.Password = $Matches[1].Trim() }
    }
    return $cfg
}

function Resolve-MysqlExe {
    param([string]$Override)
    if ($Override -and (Test-Path $Override)) { return $Override }
    $candidates = @(
        'C:\Program Files\MariaDB 11.4\bin\mysql.exe',
        'C:\Program Files\MariaDB 11.11\bin\mysql.exe',
        'C:\Program Files\MariaDB 10.11\bin\mysql.exe',
        'C:\Program Files\MariaDB 10.6\bin\mysql.exe'
    )
    foreach ($path in $candidates) {
        if (Test-Path $path) { return $path }
    }
    $cmd = Get-Command mysql -ErrorAction SilentlyContinue
    if ($cmd) { return $cmd.Source }
    throw 'mysql.exe not found. Install MariaDB or pass -MysqlExe path.'
}

function Invoke-Mysql {
    param(
        [string]$Exe,
        [hashtable]$Cfg,
        [string]$Database,
        [string[]]$ExtraArgs
    )
    $args = @(
        "-h$($Cfg.Host)",
        "-P$($Cfg.Port)",
        "-u$($Cfg.User)",
        "-p$($Cfg.Password)",
        '--default-character-set=utf8mb4'
    )
    if ($Database) { $args += $Database }
    if ($ExtraArgs) { $args += $ExtraArgs }
    & $Exe @args
    if ($LASTEXITCODE -ne 0) {
        throw "mysql failed (exit $LASTEXITCODE)"
    }
}

$db = Read-DbConfig $EnvFile
$mysql = Resolve-MysqlExe $MysqlExe
$sqlFile = Join-Path $PSScriptRoot 'clean-test-db-except-basis.sql'

Write-Host "`n=== SmartManager DB clean ($Mode) ===" -ForegroundColor Cyan
Write-Host "  mysql: $($mysql)"
Write-Host "  host : $($db.Host):$($db.Port)"
Write-Host "  db   : $($db.Name)"
Write-Host "  env  : $EnvFile"
Write-Host ''

if ($Mode -eq 'drop') {
    Write-Host 'Dropping database...' -ForegroundColor Yellow
    $dropSql = "DROP DATABASE IF EXISTS ``$($db.Name)``;"
    Invoke-Mysql -Exe $mysql -Cfg $db -Database '' -ExtraArgs @('-e', $dropSql)
    Write-Host "Dropped database '$($db.Name)'." -ForegroundColor Green
    Write-Host 'Restart API to recreate schema via Flyway.' -ForegroundColor DarkGray
    exit 0
}

if (-not (Test-Path $sqlFile)) {
    throw "SQL file not found: $sqlFile"
}

Write-Host 'Truncating transaction tables (basis kept)...' -ForegroundColor Yellow
$sqlPath = $sqlFile -replace '\\', '/'
Invoke-Mysql -Exe $mysql -Cfg $db -Database $db.Name -ExtraArgs @("-e", "source $sqlPath")
Write-Host 'Transaction data cleared.' -ForegroundColor Green
exit 0
