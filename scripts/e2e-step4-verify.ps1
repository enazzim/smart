# Step 4 E2E: production.material_issue.enabled NO (backflush) / YES (material issue TX)
$ErrorActionPreference = 'Stop'
$base = 'http://localhost:8080'
$reportDate = '2026-07-06'
$settingKey = 'production.material_issue.enabled'
$targetOrderNum = 'WO-20260706-2'
$goodQty = 10

function Unwrap-ApiArray($result) {
    if ($null -eq $result) { return @() }
    if ($result -is [System.Array]) { return $result }
    if ($null -ne $result.value) { return @($result.value) }
    return @($result)
}

function Login {
    $login = Invoke-RestMethod -Uri "$base/api/v1/auth/login" -Method POST -ContentType 'application/json' -Body '{"loginId":"admin","password":"Admin123!"}'
    return @{ Authorization = "Bearer $($login.accessToken)"; 'Content-Type' = 'application/json' }
}

function Set-MaterialIssueSetting($headers, [string]$value) {
    Invoke-RestMethod -Uri "$base/api/v1/system/settings/$settingKey" -Method PUT -Headers $headers -Body (@{ value = $value } | ConvertTo-Json) | Out-Null
}

function Get-SettingValue($headers) {
    $rows = Unwrap-ApiArray (Invoke-RestMethod -Uri "$base/api/v1/system/settings" -Headers $headers)
    return ($rows | Where-Object { $_.settingKey -eq $settingKey } | Select-Object -First 1).value
}

function To-Decimal($value) {
    if ($null -eq $value) { return [decimal]0 }
    if ($value -is [array]) { $value = $value[0] }
    return [decimal]$value
}

function Get-LocationQty($headers, [string]$itemNo, [string]$locationCode) {
    $rows = Unwrap-ApiArray (Invoke-RestMethod -Uri "$base/api/v1/inventory/balances?itemNo=$itemNo" -Headers $headers)
    $sum = [decimal]0
    foreach ($row in $rows | Where-Object { $_.locationCode -eq $locationCode }) {
        $sum += (To-Decimal $row.stockQty)
    }
    return $sum
}

function Get-WorkOrder($headers, [string]$orderNum) {
    $orders = Unwrap-ApiArray (Invoke-RestMethod -Uri "$base/api/v1/production/work-orders" -Headers $headers)
    return $orders | Where-Object { $_.orderNum -ceq $orderNum } | Select-Object -First 1
}

function Cancel-ActiveMaterialIssues($headers, [long]$workOrderId) {
    $issues = Unwrap-ApiArray (Invoke-RestMethod -Uri "$base/api/v1/production/material-issues" -Headers $headers)
    foreach ($issue in ($issues | Where-Object { $_.workOrderId -eq $workOrderId -and $_.cancellable -eq $true })) {
        Invoke-RestMethod -Uri "$base/api/v1/production/material-issues/$($issue.id)/cancel" -Method POST -Headers $headers | Out-Null
        Write-Host "  cancelled MI $($issue.issueNum)"
    }
}

function Cancel-ActiveWorkReports($headers, [long]$workOrderId) {
    $reports = Unwrap-ApiArray (Invoke-RestMethod -Uri "$base/api/v1/production/work-reports" -Headers $headers)
    foreach ($report in ($reports | Where-Object { $_.workOrderId -eq $workOrderId -and $_.cancellable -eq $true })) {
        Invoke-RestMethod -Uri "$base/api/v1/production/work-reports/$($report.id)/cancel" -Method POST -Headers $headers | Out-Null
        Write-Host "  cancelled WR $($report.reportNum)"
    }
}

$pass = [System.Collections.Generic.List[string]]::new()
$fail = [System.Collections.Generic.List[string]]::new()
$headers = Login

Write-Host "`n=== Prep: cleanup ===" -ForegroundColor Cyan
Set-MaterialIssueSetting $headers 'YES'
$wo = Get-WorkOrder $headers $targetOrderNum
if (-not $wo) { throw "Work order $targetOrderNum not found" }
$woId = [long]$wo.id
Cancel-ActiveWorkReports $headers $woId
Cancel-ActiveMaterialIssues $headers $woId
Set-MaterialIssueSetting $headers 'NO'
$wo = Get-WorkOrder $headers $targetOrderNum
$reportedBefore = To-Decimal $wo.reportedQty

Write-Host "`n=== Phase A: NO (backflush) — $targetOrderNum ($($wo.itemNo)) ===" -ForegroundColor Cyan
if ((Get-SettingValue $headers) -eq 'NO') { $pass.Add('E1 setting NO') } else { $fail.Add('E1 setting NO') }

try {
    Invoke-RestMethod -Uri "$base/api/v1/production/material-issues" -Method POST -Headers $headers -Body (@{
        workOrderId = $woId; issueDate = $reportDate; lines = @(@{ itemCompositionId = 1; issueQty = 1 })
    } | ConvertTo-Json -Depth 5) | Out-Null
    $fail.Add('E2a material issue blocked when NO')
} catch { $pass.Add('E2a material issue blocked when NO') }

$onHand = Unwrap-ApiArray (Invoke-RestMethod -Uri "$base/api/v1/production/work-reports/issue-on-hand?workOrderId=$woId&reportDate=$reportDate" -Headers $headers)
if ($onHand.Count -ge 1) { $pass.Add("E2b issue-on-hand ($($onHand.Count) lines)") } else { $fail.Add('E2b issue-on-hand empty') }

$cons = Invoke-RestMethod -Uri "$base/api/v1/production/work-reports/consumption-status?workOrderId=$woId&pendingGoodQty=$goodQty" -Headers $headers
if ($cons.materialIssueEnabled -eq $false) { $pass.Add('E2b materialIssueEnabled=false') } else { $fail.Add('E2b materialIssueEnabled not false') }

$issueLines = @($cons.lines | ForEach-Object {
    @{ itemCompositionId = $_.itemCompositionId; itemId = $_.itemId; issueQty = [double]$_.requiredQty }
})
$rawRBefore = Get-LocationQty $headers 'abc-r' 'RAW'
$rawTBefore = Get-LocationQty $headers 'abc-t' 'RAW'
$salesBefore = Get-LocationQty $headers 'abc' 'SALES'
$wipBefore = Get-LocationQty $headers 'abc' 'WIP'
Write-Host "Before: abc-r RAW=$rawRBefore abc-t RAW=$rawTBefore abc SALES=$salesBefore WIP=$wipBefore"

$wr = Invoke-RestMethod -Uri "$base/api/v1/production/work-reports" -Method POST -Headers $headers -Body (@{
    workOrderId = $woId; reportDate = $reportDate; goodQty = $goodQty; scrapQty = 0; issueLines = $issueLines
} | ConvertTo-Json -Depth 5)
Write-Host "WR $($wr.reportNum) good=$goodQty"

$rawRAfter = Get-LocationQty $headers 'abc-r' 'RAW'
$rawTAfter = Get-LocationQty $headers 'abc-t' 'RAW'
$salesAfter = Get-LocationQty $headers 'abc' 'SALES'
$wipAfter = Get-LocationQty $headers 'abc' 'WIP'
$rDelta = $rawRBefore - $rawRAfter
$tDelta = $rawTBefore - $rawTAfter
$salesDelta = $salesAfter - $salesBefore
$wipDelta = $wipAfter - $wipBefore
Write-Host "After: rDelta=$rDelta tDelta=$tDelta salesDelta=$salesDelta wipDelta=$wipDelta"

$expR = [decimal]($cons.lines | Where-Object { $_.itemNo -eq 'abc-r' } | Select-Object -First 1).requiredQty
$expT = [decimal]($cons.lines | Where-Object { $_.itemNo -eq 'abc-t' } | Select-Object -First 1).requiredQty
if ($rDelta -eq $expR) { $pass.Add("E2c abc-r RAW -$expR") } else { $fail.Add("E2c abc-r delta=$rDelta expected $expR") }
if ($tDelta -eq $expT) { $pass.Add("E2c abc-t RAW -$expT") } else { $fail.Add("E2c abc-t delta=$tDelta expected $expT") }

if ($salesDelta -eq $goodQty -and $wipDelta -eq 0) { $pass.Add("E3 final process SALES +$goodQty") }
else { $fail.Add("E3 salesDelta=$salesDelta wipDelta=$wipDelta") }

Invoke-RestMethod -Uri "$base/api/v1/production/work-reports/$($wr.id)/cancel" -Method POST -Headers $headers | Out-Null
$woEnd = Get-WorkOrder $headers $targetOrderNum
if ((Get-LocationQty $headers 'abc-r' 'RAW') -eq $rawRBefore) { $pass.Add('E4 abc-r RAW restored') } else { $fail.Add('E4 abc-r RAW not restored') }
if ((Get-LocationQty $headers 'abc-t' 'RAW') -eq $rawTBefore) { $pass.Add('E4 abc-t RAW restored') } else { $fail.Add('E4 abc-t RAW not restored') }
if ((Get-LocationQty $headers 'abc' 'SALES') -eq $salesBefore) { $pass.Add('E4 SALES restored') } else { $fail.Add('E4 SALES not restored') }
if ((To-Decimal $woEnd.reportedQty) -eq $reportedBefore) { $pass.Add('E4 WO reported_qty restored') } else { $fail.Add("E4 reported=$($woEnd.reportedQty) expected $reportedBefore") }

Write-Host "`n=== Phase B: YES (PRD-W2) ===" -ForegroundColor Cyan
Set-MaterialIssueSetting $headers 'YES'
if ((Get-SettingValue $headers) -eq 'YES') { $pass.Add('E5a setting YES') } else { $fail.Add('E5a setting YES') }

$consYes = Invoke-RestMethod -Uri "$base/api/v1/production/work-reports/consumption-status?workOrderId=$woId&pendingGoodQty=$goodQty" -Headers $headers
if ($consYes.materialIssueEnabled -eq $true) { $pass.Add('E5b materialIssueEnabled=true') } else { $fail.Add('E5b materialIssueEnabled') }

try {
    Invoke-RestMethod -Uri "$base/api/v1/production/work-reports" -Method POST -Headers $headers -Body (@{
        workOrderId = $woId; reportDate = $reportDate; goodQty = $goodQty; scrapQty = 0
    } | ConvertTo-Json) | Out-Null
    $fail.Add('E5c guard missing')
} catch { $pass.Add('E5c report blocked without material issue') }

$need = [decimal]($consYes.lines | Where-Object { $_.itemNo -eq 'abc-r' } | Select-Object -First 1).remainingQty
$lineR = $consYes.lines | Where-Object { $_.itemNo -eq 'abc-r' } | Select-Object -First 1
$lineT = $consYes.lines | Where-Object { $_.itemNo -eq 'abc-t' } | Select-Object -First 1
$miLines = @(
    @{ itemCompositionId = $lineR.itemCompositionId; issueQty = [double]$lineR.remainingQty }
    @{ itemCompositionId = $lineT.itemCompositionId; issueQty = [double]$lineT.remainingQty }
)
$mi = Invoke-RestMethod -Uri "$base/api/v1/production/material-issues" -Method POST -Headers $headers -Body (@{
    workOrderId = $woId; issueDate = $reportDate; lines = $miLines
} | ConvertTo-Json -Depth 5)
Write-Host "MI $($mi.issueNum)"

$consOk = Invoke-RestMethod -Uri "$base/api/v1/production/work-reports/consumption-status?workOrderId=$woId&pendingGoodQty=$goodQty" -Headers $headers
if ($consOk.allSatisfied) { $pass.Add('E5d consumption satisfied') } else { $fail.Add('E5d consumption not satisfied') }

$wr2 = Invoke-RestMethod -Uri "$base/api/v1/production/work-reports" -Method POST -Headers $headers -Body (@{
    workOrderId = $woId; reportDate = $reportDate; goodQty = $goodQty; scrapQty = 0
} | ConvertTo-Json)
Write-Host "WR $($wr2.reportNum)"
$pass.Add('E5e report after material issue')

Invoke-RestMethod -Uri "$base/api/v1/production/work-reports/$($wr2.id)/cancel" -Method POST -Headers $headers | Out-Null
Invoke-RestMethod -Uri "$base/api/v1/production/material-issues/$($mi.id)/cancel" -Method POST -Headers $headers | Out-Null
Set-MaterialIssueSetting $headers 'NO'
Write-Host "Cleanup done (setting=NO)"

Write-Host "`n=== SUMMARY ($($pass.Count) pass / $($fail.Count) fail) ===" -ForegroundColor Green
$pass | ForEach-Object { Write-Host "PASS $_" -ForegroundColor Green }
$fail | ForEach-Object { Write-Host "FAIL $_" -ForegroundColor Red }
if ($fail.Count -gt 0) { exit 1 }
