# Step 1 E2E: Purchase receipt -> Work report -> Inventory -> Cancel
$ErrorActionPreference = 'Stop'
$base = 'http://localhost:8080'
$mysql = 'C:\Program Files\MariaDB 11.4\bin\mysql.exe'
$reportDate = '2026-07-06'
$targetOrderNum = 'WO-20260706-2'

function Login {
    $login = Invoke-RestMethod -Uri "$base/api/v1/auth/login" -Method POST -ContentType 'application/json' -Body '{"loginId":"admin","password":"Admin123!"}'
    return @{ Authorization = "Bearer $($login.accessToken)"; 'Content-Type' = 'application/json' }
}

function Get-OnHand($headers, $workOrderId) {
    Invoke-RestMethod -Uri "$base/api/v1/production/work-reports/issue-on-hand?workOrderId=$workOrderId&reportDate=$reportDate" -Headers $headers
}

function Get-InventoryDb {
    $sql = "SELECT i.item_no, il.location_code, ib.stock_qty, IFNULL(ib.output_process_id,'NULL') FROM inventory_balance ib JOIN item i ON i.id=ib.item_id JOIN inventory_location il ON il.id=ib.location_id WHERE ib.recording_state=1 AND i.item_no IN ('abc','abc-r','abc-t') ORDER BY i.item_no, il.location_code"
    & $mysql -uroot -p1111 smartmanager -N -e $sql 2>$null
}

$results = [ordered]@{}
$headers = Login

Write-Host "`n=== E1 baseline: issue-on-hand & DB inventory ===" -ForegroundColor Cyan
$targets = Invoke-RestMethod -Uri "$base/api/v1/production/work-reports/report-targets" -Headers $headers
$reports = Invoke-RestMethod -Uri "$base/api/v1/production/work-reports" -Headers $headers
$receipts = Invoke-RestMethod -Uri "$base/api/v1/purchase/receipts" -Headers $headers

$wo = $targets | Where-Object { $_.orderNum -eq $targetOrderNum } | Select-Object -First 1
if (-not $wo) {
    $orders = Invoke-RestMethod -Uri "$base/api/v1/production/work-orders" -Headers $headers
    $wo = $orders | Where-Object { $_.orderNum -eq $targetOrderNum } | Select-Object -First 1
}

if (-not $wo) { throw "No work order $targetOrderNum found for E2E" }
$woId = $wo.id
Write-Host "Work order: $($wo.orderNum) id=$woId remaining=$($wo.remainingQty) reported=$($wo.reportedQty)"

# E1: purchase receipts posted
$postedReceipts = @($receipts | Where-Object { $_.status -match 'POSTED|반영|COMPLETE|REGISTERED' -or $_.lines.Count -gt 0 })
$e1ReceiptOk = ($receipts.Count -ge 1)
Write-Host "Purchase receipts count: $($receipts.Count) (E1 receipts exist: $e1ReceiptOk)"

$onHandBefore = Get-OnHand $headers $woId
Write-Host "On-hand before:" ($onHandBefore | ConvertTo-Json -Compress)
$dbBefore = Get-InventoryDb
Write-Host "DB inventory before:`n$dbBefore"

# Cancel existing registered report on this WO if full (to allow partial test)
$existing = @($reports | Where-Object { $_.workOrderId -eq $woId -and $_.cancellable -eq $true })
if ($existing.Count -gt 0) {
    $existing = $existing[0]
    Write-Host "`n=== E5 prep: cancel existing report $($existing.reportNum) ===" -ForegroundColor Cyan
    Invoke-RestMethod -Uri "$base/api/v1/production/work-reports/$($existing.id)/cancel" -Method POST -Headers $headers | Out-Null
    $onHandAfterCancel = Get-OnHand $headers $woId
    $wo = @(Invoke-RestMethod -Uri "$base/api/v1/production/work-orders" -Headers $headers | Where-Object { $_.id -eq $woId })[0]
    $results['E5_cancel_on_hand'] = $onHandAfterCancel
    Write-Host "On-hand after cancel:" ($onHandAfterCancel | ConvertTo-Json -Compress)
    Write-Host "WO after cancel remaining=$($wo.remainingQty) reported=$($wo.reportedQty)"
}

$onHandPreReport = Get-OnHand $headers $woId
$cons = Invoke-RestMethod -Uri "$base/api/v1/production/work-reports/consumption-status?workOrderId=$woId&pendingGoodQty=100" -Headers $headers
$lineR = $cons.lines | Where-Object { $_.itemNo -eq 'abc-r' } | Select-Object -First 1
$lineT = $cons.lines | Where-Object { $_.itemNo -eq 'abc-t' } | Select-Object -First 1

Write-Host "`n=== E2 partial report: good=100 scrap=10, only abc-r checked ===" -ForegroundColor Cyan
$payload = @{
    workOrderId = $woId
    reportDate = $reportDate
    goodQty = 100
    scrapQty = 10
    workerName = 'admin'
    issueLines = @(@{
        itemCompositionId = $lineR.itemCompositionId
        itemId = $lineR.itemId
        issueQty = 200
    })
} | ConvertTo-Json -Depth 5

$wr1 = Invoke-RestMethod -Uri "$base/api/v1/production/work-reports" -Method POST -Headers $headers -Body $payload
Write-Host "Created $($wr1.reportNum) good=$($wr1.goodQty) scrap=$($wr1.scrapQty)"

$onHandPostPartial = Get-OnHand $headers $woId
$woAfter = @(Invoke-RestMethod -Uri "$base/api/v1/production/work-orders" -Headers $headers | Where-Object { $_.id -eq $woId })[0]
$rOnHand = ($onHandPostPartial | Where-Object { $_.itemId -eq $lineR.itemId }).onHandQty
$tOnHand = ($onHandPostPartial | Where-Object { $_.itemId -eq $lineT.itemId }).onHandQty
$rBefore = ($onHandPreReport | Where-Object { $_.itemId -eq $lineR.itemId }).onHandQty
$tBefore = ($onHandPreReport | Where-Object { $_.itemId -eq $lineT.itemId }).onHandQty

$results['E2_r_delta'] = [decimal]$rBefore - [decimal]$rOnHand
$results['E2_t_delta'] = [decimal]$tBefore - [decimal]$tOnHand
$results['E3_reported'] = $woAfter.reportedQty
$results['E3_remaining'] = $woAfter.remainingQty

Write-Host "abc-r on-hand: $rBefore -> $rOnHand (delta $($results['E2_r_delta']))"
Write-Host "abc-t on-hand: $tBefore -> $tOnHand (delta $($results['E2_t_delta']))"
Write-Host "WO reported=$($woAfter.reportedQty) remaining=$($woAfter.remainingQty)"

Write-Host "`n=== E4 full remaining report ===" -ForegroundColor Cyan
$remaining = [decimal]$woAfter.remainingQty
$cons2 = Invoke-RestMethod -Uri "$base/api/v1/production/work-reports/consumption-status?workOrderId=$woId&pendingGoodQty=$remaining" -Headers $headers
$lineR2 = $cons2.lines | Where-Object { $_.itemNo -eq 'abc-r' } | Select-Object -First 1
$lineT2 = $cons2.lines | Where-Object { $_.itemNo -eq 'abc-t' } | Select-Object -First 1
$dbPreSales = Get-InventoryDb

$payload2 = @{
    workOrderId = $woId
    reportDate = $reportDate
    goodQty = $remaining
    scrapQty = 0
    issueLines = @(
        @{ itemCompositionId = $lineR2.itemCompositionId; itemId = $lineR2.itemId; issueQty = [double]($lineR2.unitRatio * $remaining) },
        @{ itemCompositionId = $lineT2.itemCompositionId; itemId = $lineT2.itemId; issueQty = [double]($lineT2.unitRatio * $remaining) }
    )
} | ConvertTo-Json -Depth 5

$wr2 = Invoke-RestMethod -Uri "$base/api/v1/production/work-reports" -Method POST -Headers $headers -Body $payload2
Write-Host "Created $($wr2.reportNum) good=$($wr2.goodQty)"
$dbPostSales = Get-InventoryDb
Write-Host "DB inventory after final report:`n$dbPostSales"

Write-Host "`n=== E5 cancel final report ===" -ForegroundColor Cyan
Invoke-RestMethod -Uri "$base/api/v1/production/work-reports/$($wr2.id)/cancel" -Method POST -Headers $headers | Out-Null
$onHandFinal = Get-OnHand $headers $woId
$dbAfterCancel2 = Get-InventoryDb
Write-Host "On-hand after cancel wr2:" ($onHandFinal | ConvertTo-Json -Compress)

Write-Host "`n=== SUMMARY ===" -ForegroundColor Green
$pass = @()
$fail = @()
if ($e1ReceiptOk) { $pass += 'E1 purchase receipts exist' } else { $fail += 'E1 no purchase receipts' }
$onHandR = ($onHandBefore | Where-Object { $_.itemId -eq 4 })
if ($onHandR -and [decimal]$onHandR.onHandQty -ge 0) { $pass += "E1 issue-on-hand API ok (abc-r=$($onHandR.onHandQty))" }
if ([decimal]$results['E2_r_delta'] -eq 200) { $pass += 'E2 abc-r -200' } else { $fail += "E2 abc-r expected -200 got $($results['E2_r_delta'])" }
if ([decimal]$results['E2_t_delta'] -eq 0) { $pass += 'E2 abc-t unchanged' } else { $fail += "E2 abc-t expected 0 got $($results['E2_t_delta'])" }
if ([decimal]$results['E3_reported'] -eq 100) { $pass += 'E3 reported=100' } else { $fail += "E3 reported=$($results['E3_reported'])" }
if ($dbPostSales -match 'abc\tSALES\t500') { $pass += 'E4 SALES 500' }
elseif ($dbPostSales -match 'abc\tSALES\t400') { $pass += 'E4 SALES 400 (partial+full)' }
else { $fail += "E4 SALES: $dbPostSales" }

$rAfterCancel = ($onHandFinal | Where-Object { $_.itemId -eq $lineR.itemId }).onHandQty
if ([decimal]$rAfterCancel -eq [decimal]$rBefore) { $pass += 'E5 abc-r restored after cancel wr2' }
else { $fail += "E5 abc-r after cancel=$rAfterCancel expected=$rBefore" }

$pass | ForEach-Object { Write-Host "PASS $_" -ForegroundColor Green }
$fail | ForEach-Object { Write-Host "FAIL $_" -ForegroundColor Red }
