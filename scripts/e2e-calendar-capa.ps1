# Calendar · Capa E2E — EffectiveMinutes + 주말/공휴일 자동 휴무
$ErrorActionPreference = "Continue"
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$base = "http://localhost:8080"
$basis = "$base/api/v1/basis"
$auth = "$base/api/v1/auth"
$results = [System.Collections.Generic.List[object]]::new()
$tmpJson = Join-Path $env:TEMP "sm-e2e-calendar.json"
$token = $null

function Add-Result([string]$Id, [string]$Status, [string]$Detail) {
    $results.Add([pscustomobject]@{ Id = $Id; Status = $Status; Detail = $Detail })
    $c = if ($Status -eq "PASS") { "Green" } elseif ($Status -eq "FAIL") { "Red" } else { "Yellow" }
    Write-Host "[$Status] $Id — $Detail" -ForegroundColor $c
}

function Invoke-Api {
    param([string]$Method = "GET", [string]$Url, [string]$Body = $null)
    $args = @("-s", "-m", "30", "-w", "`n%{http_code}", "-X", $Method, "-H", "Content-Type: application/json; charset=utf-8")
    if ($token) { $args += @("-H", "Authorization: Bearer $token") }
    if ($Body) { $args += @("-d", $Body) }
    $args += $Url
    $raw = & curl.exe @args
    $lines = $raw -split "`n"
    $code = $lines[-1].Trim()
    $body = ($lines[0..([Math]::Max(0, $lines.Length - 2))] -join "`n").Trim()
    if ($body) {
        [System.IO.File]::WriteAllText($tmpJson, $body, [System.Text.UTF8Encoding]::new($false))
    }
    return @{ Code = $code; Body = $body; JsonFile = $tmpJson }
}

function Read-Json([hashtable]$Response) {
    if (-not $Response.Body) { return $null }
    try { return Get-Content -Path $Response.JsonFile -Raw -Encoding UTF8 | ConvertFrom-Json }
    catch { return $null }
}

# Login
$loginBody = '{"loginId":"admin","password":"Admin123!"}'
$login = Invoke-Api -Method POST -Url "$auth/login" -Body $loginBody
$loginJson = Read-Json $login
if ($login.Code -eq "200" -and $loginJson.accessToken) {
    $token = $loginJson.accessToken
    Add-Result "AUTH" "PASS" "admin login"
} else {
    Add-Result "AUTH" "FAIL" "HTTP $($login.Code)"
    exit 1
}

# CAL-1: Saturday auto off (2026-07-04 is Saturday)
$eff = Read-Json (Invoke-Api -Url "$basis/production-calendars/effective?year=2026&month=7")
$sat = $eff | Where-Object { $_.calendarDate -eq "2026-07-04" } | Select-Object -First 1
Add-Result "CAL-1 weekend" $(if ($sat -and $sat.effectiveWorkTime -eq 0 -and $sat.autoOffDay) { "PASS" } else { "FAIL" }) "effective=$($sat.effectiveWorkTime)"

# CAL-2: Weekday default 480 (2026-07-06 Monday)
$mon = $eff | Where-Object { $_.calendarDate -eq "2026-07-06" } | Select-Object -First 1
Add-Result "CAL-2 weekday" $(if ($mon -and $mon.effectiveWorkTime -eq 480) { "PASS" } else { "FAIL" }) "effective=$($mon.effectiveWorkTime)"

# CAL-3: Public holiday (2026-08-15)
$effAug = Read-Json (Invoke-Api -Url "$basis/production-calendars/effective?year=2026&month=8")
$holiday = $effAug | Where-Object { $_.calendarDate -eq "2026-08-15" } | Select-Object -First 1
Add-Result "CAL-3 holiday" $(if ($holiday -and $holiday.effectiveWorkTime -eq 0) { "PASS" } else { "FAIL" }) "effective=$($holiday.effectiveWorkTime)"

# CAL-4: Explicit upsert overrides auto off
$put = Invoke-Api -Method PUT -Url "$basis/production-calendars/by-date/2026-07-04" -Body '{"workTime":720,"content":"특근"}'
$eff2 = Read-Json (Invoke-Api -Url "$basis/production-calendars/effective?year=2026&month=7")
$sat2 = $eff2 | Where-Object { $_.calendarDate -eq "2026-07-04" } | Select-Object -First 1
Add-Result "CAL-4 override" $(if ($put.Code -eq "200" -and $sat2.effectiveWorkTime -eq 720) { "PASS" } else { "FAIL" }) "effective=$($sat2.effectiveWorkTime)"

# Cleanup CAL-4
Invoke-Api -Method DELETE -Url "$basis/production-calendars/by-date/2026-07-04" | Out-Null

# CAL-5: Work center effective inherits auto off
$wcs = Read-Json (Invoke-Api -Url "$basis/work-centers")
$wcId = ($wcs | Select-Object -First 1).id
if ($wcId) {
    $wcEff = Read-Json (Invoke-Api -Url "$basis/work-center-calendars/effective?workCenterId=$wcId&year=2026&month=7")
    $wcSat = $wcEff | Where-Object { $_.calendarDate -eq "2026-07-04" } | Select-Object -First 1
    Add-Result "CAL-5 wc-effective" $(if ($wcSat -and $wcSat.effectiveWorkTime -eq 0) { "PASS" } else { "FAIL" }) "wc=$wcId effective=$($wcSat.effectiveWorkTime)"
} else {
    Add-Result "CAL-5 wc-effective" "SKIP" "no work center"
}

# CAPA-1: Capa equals effective on TIME
if ($wcId) {
    $capa = Read-Json (Invoke-Api -Url "$basis/work-centers/$wcId/capa?date=2026-07-06")
    Add-Result "CAPA-1 time" $(if ($capa -and $capa.effectiveMinutes -eq 480 -and $capa.capaMinutes -eq 480) { "PASS" } else { "FAIL" }) "eff=$($capa.effectiveMinutes) capa=$($capa.capaMinutes)"
} else {
    Add-Result "CAPA-1 time" "SKIP" "no work center"
}

# CAPA-2: Capa zero on auto off day
if ($wcId) {
    $capa0 = Read-Json (Invoke-Api -Url "$basis/work-centers/$wcId/capa?date=2026-07-04")
    Add-Result "CAPA-2 off" $(if ($capa0 -and $capa0.capaMinutes -eq 0) { "PASS" } else { "FAIL" }) "capa=$($capa0.capaMinutes)"
} else {
    Add-Result "CAPA-2 off" "SKIP" "no work center"
}

$pass = ($results | Where-Object { $_.Status -eq "PASS" }).Count
$fail = ($results | Where-Object { $_.Status -eq "FAIL" }).Count
Write-Host "`nSummary: PASS=$pass FAIL=$fail TOTAL=$($results.Count)"
