# Calendar / Capa E2E — EffectiveMinutes + weekend/holiday auto off
# Run: powershell -NoProfile -ExecutionPolicy Bypass -File scripts/e2e-calendar-capa.ps1

$ErrorActionPreference = 'Stop'
. "$PSScriptRoot/e2e-common.ps1"

$tracker = New-E2EResultTracker

if (-not (Test-ApiHealth)) {
    throw "API not responding: $Script:E2E_BaseUrl"
}

$headers = Login-E2E
Add-E2EPass $tracker 'AUTH admin login'

function Remove-E2EProductionCalendarDate {
    param([hashtable]$Hdr, [string]$Date)
    $year = [int]$Date.Substring(0, 4)
    $month = [int]$Date.Substring(5, 2)
    $eff = Unwrap-ApiArray (Invoke-RestJson -Path "/api/v1/basis/production-calendars/effective?year=$year&month=$month" -Headers $Hdr)
    $day = $eff | Where-Object { $_.calendarDate -eq $Date } | Select-Object -First 1
    if ($day -and $day.registered) {
        Invoke-RestJson -Method DELETE -Path "/api/v1/basis/production-calendars/by-date/$Date" -Headers $Hdr | Out-Null
    }
}

# Ensure 2026-07-04 has no explicit override from prior E2E runs
Remove-E2EProductionCalendarDate $headers '2026-07-04'

Write-Host "`n=== Calendar / Capa checks ===" -ForegroundColor Cyan

# CAL-1: Saturday auto off (2026-07-04)
$eff = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/production-calendars/effective?year=2026&month=7' -Headers $headers)
$sat = $eff | Where-Object { $_.calendarDate -eq '2026-07-04' } | Select-Object -First 1
if ($sat -and $sat.effectiveWorkTime -eq 0 -and $sat.autoOffDay) {
    Add-E2EPass $tracker 'CAL-1 weekend'
} else {
    Add-E2EFail $tracker "CAL-1 weekend effective=$($sat.effectiveWorkTime)"
}

# CAL-2: Weekday default 480 (2026-07-06 Monday)
$mon = $eff | Where-Object { $_.calendarDate -eq '2026-07-06' } | Select-Object -First 1
if ($mon -and $mon.effectiveWorkTime -eq 480) {
    Add-E2EPass $tracker 'CAL-2 weekday'
} else {
    Add-E2EFail $tracker "CAL-2 weekday effective=$($mon.effectiveWorkTime)"
}

# CAL-3: Public holiday (2026-08-15)
$effAug = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/production-calendars/effective?year=2026&month=8' -Headers $headers)
$holiday = $effAug | Where-Object { $_.calendarDate -eq '2026-08-15' } | Select-Object -First 1
if ($holiday -and $holiday.effectiveWorkTime -eq 0) {
    Add-E2EPass $tracker 'CAL-3 holiday'
} else {
    Add-E2EFail $tracker "CAL-3 holiday effective=$($holiday.effectiveWorkTime)"
}

# CAL-4: Explicit upsert overrides auto off
Invoke-RestJson -Method PUT -Path '/api/v1/basis/production-calendars/by-date/2026-07-04' -Headers $headers -Body @{
    workTime = 720
    content = 'overtime'
} | Out-Null
$eff2 = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/production-calendars/effective?year=2026&month=7' -Headers $headers)
$sat2 = $eff2 | Where-Object { $_.calendarDate -eq '2026-07-04' } | Select-Object -First 1
if ($sat2 -and $sat2.effectiveWorkTime -eq 720) {
    Add-E2EPass $tracker 'CAL-4 override'
} else {
    Add-E2EFail $tracker "CAL-4 override effective=$($sat2.effectiveWorkTime)"
}
Remove-E2EProductionCalendarDate $headers '2026-07-04'

# CAL-5 + CAPA: work center effective / capa
$wcs = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/work-centers' -Headers $headers)
$wcId = ($wcs | Select-Object -First 1).id
if ($wcId) {
    $wcEff = Unwrap-ApiArray (Invoke-RestJson -Path "/api/v1/basis/work-center-calendars/effective?workCenterId=$wcId&year=2026&month=7" -Headers $headers)
    $wcSat = $wcEff | Where-Object { $_.calendarDate -eq '2026-07-04' } | Select-Object -First 1
    if ($wcSat -and $wcSat.effectiveWorkTime -eq 0) {
        Add-E2EPass $tracker 'CAL-5 wc-effective'
    } else {
        Add-E2EFail $tracker "CAL-5 wc-effective effective=$($wcSat.effectiveWorkTime)"
    }

    $capa = Invoke-RestJson -Path "/api/v1/basis/work-centers/$wcId/capa?date=2026-07-06" -Headers $headers
    if ($capa -and $capa.effectiveMinutes -eq 480 -and $capa.capaMinutes -eq 480) {
        Add-E2EPass $tracker 'CAPA-1 time'
    } else {
        Add-E2EFail $tracker "CAPA-1 time eff=$($capa.effectiveMinutes) capa=$($capa.capaMinutes)"
    }

    $capa0 = Invoke-RestJson -Path "/api/v1/basis/work-centers/$wcId/capa?date=2026-07-04" -Headers $headers
    if ($capa0 -and $capa0.capaMinutes -eq 0) {
        Add-E2EPass $tracker 'CAPA-2 off'
    } else {
        Add-E2EFail $tracker "CAPA-2 off capa=$($capa0.capaMinutes)"
    }
} else {
    Add-E2EFail $tracker 'CAL-5/CAPA no work center'
}

Write-E2ESummary $tracker 'Calendar / Capa E2E'
