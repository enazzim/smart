# E2E P0-1: full TX chain (FG-01 default, legacy abc via SM_E2E_PRODUCT_ITEM=abc)
# Run: powershell -NoProfile -ExecutionPolicy Bypass -File scripts/e2e-full-tx-chain-verify.ps1

$ErrorActionPreference = 'Stop'
. "$PSScriptRoot/e2e-common.ps1"

$ProductItemNo = Get-E2EProductItemNo
$TxDate = if ($env:SM_E2E_TX_DATE) { $env:SM_E2E_TX_DATE } else { Today-Iso }
$OrderQty = if ($env:SM_E2E_ORDER_QTY) { [decimal]$env:SM_E2E_ORDER_QTY } else { [decimal]10 }
$SkipCancelPhase = ($env:SM_E2E_SKIP_CANCEL -eq '1')
$SkipOutsourcePhase = ($env:SM_E2E_SKIP_OUTSOURCE -eq '1')
$tracker = New-E2EResultTracker

if (-not (Test-ApiHealth)) { throw "API not reachable: $Script:E2E_BaseUrl" }

$headers = Login-E2E
$companies = Get-E2ECompanies $headers
$items = Get-E2EItems $headers
$RawItemNos = Get-E2ERawItemNos $headers $ProductItemNo

$salesPartner = Find-CompanyByRole $companies 'SALES' @('OUTSOURCE')
$purchasePartner = Find-CompanyByRole $companies 'PURCHASE' @('OUTSOURCE')
$productItem = Resolve-E2EItem $headers $items $ProductItemNo

if (-not $salesPartner) { throw 'SALES partner not found' }
if (-not $purchasePartner) { throw 'PURCHASE partner not found' }
if (-not $productItem) { throw "Product item not found: $ProductItemNo" }
foreach ($rawNo in $RawItemNos) {
    if (-not (Resolve-E2EItem $headers $items $rawNo)) { throw "Raw item not found: $rawNo" }
}

Write-Host "E2E product=$ProductItemNo raws=$($RawItemNos -join ',')" -ForegroundColor DarkGray

Invoke-RestJson -Method PUT -Path '/api/v1/system/settings/production.material_issue.enabled' `
    -Headers $headers -Body @{ value = 'NO' } | Out-Null

$fiscal = Get-FiscalPeriod $headers $TxDate

Write-Host "`n=== Phase 1: Sales order + production plan ===" -ForegroundColor Cyan
$salesOrder = Invoke-RestJson -Method POST -Path '/api/v1/sales/orders' -Headers $headers -Body @{
    partnerId = [long]$salesPartner.id; orderDate = $TxDate; requestedDeliveryDate = $TxDate
    remark = 'E2E full-tx-chain'
    lines = @(@{ itemId = [long]$productItem.id; orderQty = [double]$OrderQty; unitPrice = 1000; deliveryDate = $TxDate })
}
$salesLineId = [long]$salesOrder.lines[0].id
Add-E2EPass $tracker "SO $($salesOrder.orderNo)"

$salesOrder = Invoke-RestJson -Method POST -Path "/api/v1/sales/orders/$($salesOrder.id)/confirm" -Headers $headers
if ($salesOrder.status -eq 'CONFIRMED') { Add-E2EPass $tracker 'SO confirmed' } else { Add-E2EFail $tracker "SO confirm failed: $($salesOrder.status)" }

$plans = Invoke-RestJson -Method POST -Path '/api/v1/production/plans' -Headers $headers -Body @{
    lines = @(@{ salesOrderLineId = $salesLineId; plannedQty = [double]$OrderQty })
}
$planId = [long]$plans[0].id
$planNo = $plans[0].planNo
Add-E2EPass $tracker "Plan $planNo"

Write-Host "`n=== Phase 2: MRP + purchase order ===" -ForegroundColor Cyan
$mrpRun = Invoke-RestJson -Method POST -Path '/api/v1/production/mrp/calculate' -Headers $headers -Body @{ productionPlanIds = @($planId) }
Add-E2EPass $tracker "MRP $($mrpRun.runNo)"

$mrpCandidates = Unwrap-ApiArray (Invoke-RestJson -Path "/api/v1/purchase/orders/mrp-candidates?orderDate=$TxDate" -Headers $headers)
$poLines = @()
foreach ($candidate in $mrpCandidates) {
    if ($candidate.planNo -ne $planNo) { continue }
    $vendor = $candidate.vendors | Select-Object -First 1
    if (-not $vendor) { continue }
    $qty = [double]$candidate.suggestedQty
    if ($qty -le 0) { $qty = [double]$candidate.grossQty }
    if ($qty -le 0) { continue }
    $poLines += @{
        requirementLineId = [long]$candidate.requirementLineId; orderQty = $qty
        unitPrice = [double]$vendor.unitPrice; requestedDeliveryDate = $TxDate
    }
}
if ($poLines.Count -eq 0) { throw 'No MRP PO lines' }

$po = Invoke-RestJson -Method POST -Path '/api/v1/purchase/orders/from-mrp' -Headers $headers -Body @{
    partnerId = [long]$purchasePartner.id; orderDate = $TxDate; lines = $poLines
}
Add-E2EPass $tracker "PO $($po.orderNo) lines=$($po.lines.Count)"

Write-Host "`n=== Phase 3: Purchase receipt ===" -ForegroundColor Cyan
$receiptLines = @($po.lines | ForEach-Object { @{ purchaseOrderLineId = [long]$_.id; receiptQty = [double]$_.orderQty } })
$receipt = Invoke-RestJson -Method POST -Path '/api/v1/purchase/receipts' -Headers $headers -Body @{
    receiptDate = $TxDate; fiscalYear = [int]$fiscal.fiscalYear; fiscalMonth = [int]$fiscal.fiscalMonth; lines = $receiptLines
}
Add-E2EPass $tracker "Receipt $($receipt.receiptNo)"

foreach ($rawNo in $RawItemNos) {
    $qty = Get-LocationQty $headers $rawNo 'RAW'
    if ($qty -gt 0) { Add-E2EPass $tracker "$rawNo RAW=$qty" } else { Add-E2EFail $tracker "$rawNo RAW empty" }
}

Write-Host "`n=== Phase 4: Work plan / order / report ===" -ForegroundColor Cyan
$workPlans = Invoke-RestJson -Method POST -Path '/api/v1/production/work-plans' -Headers $headers -Body @{ productionPlanIds = @($planId) }
if ($workPlans.Count -lt 1) { throw 'No work plans' }
Add-E2EPass $tracker "WorkPlans $($workPlans.Count)"

$outsourcePlanCount = @($workPlans | Where-Object { $_.workDistinction -eq 'OUTSOURCE' }).Count
$outsourceChains = @()
if (-not $SkipOutsourcePhase -and $outsourcePlanCount -gt 0) {
    Write-Host "`n=== Phase 4a: Outsource OO / shipment / receipt ===" -ForegroundColor Cyan
    $outsourceChains = @(Complete-E2EOutsourceWorkPlans $headers $workPlans $TxDate $fiscal $tracker)
    if ($outsourceChains.Count -lt $outsourcePlanCount) {
        Add-E2EFail $tracker "Outsource chain $($outsourceChains.Count)/$outsourcePlanCount"
    } else {
        Add-E2EPass $tracker "Outsource chain $($outsourceChains.Count)/$outsourcePlanCount"
    }
}

Write-Host "`n=== Phase 4b: INHOUSE work order / report ===" -ForegroundColor Cyan
$inhouseWorkPlanIds = @($workPlans | Where-Object { $_.workDistinction -eq 'INHOUSE' } | ForEach-Object { [long]$_.id })
if ($inhouseWorkPlanIds.Count -lt 1) { throw 'No INHOUSE work plans' }
$workOrders = Unwrap-ApiArray (Invoke-RestJson -Method POST -Path '/api/v1/production/work-orders' -Headers $headers -Body @{ workPlanIds = $inhouseWorkPlanIds })

foreach ($semi in (Get-BomDirectSemiNodes $headers $ProductItemNo)) {
    $semiQty = To-Decimal $semi.quantity * $OrderQty
    $semiWr = Complete-E2EItemFinalWorkReport $headers $workOrders $semi.itemNum $semiQty $TxDate
    if ($semiWr) { Add-E2EPass $tracker "Semi $($semi.itemNum) WR $($semiWr.reportNum)" }
    else { Add-E2EFail $tracker "Semi $($semi.itemNum) WR failed" }
}

$productWos = @($workOrders | Where-Object { $_.itemNo -ceq $ProductItemNo })
if ($productWos.Count -eq 0) { throw "No work orders for $ProductItemNo" }
$finalWo = $productWos | Sort-Object { $_.processSequenceNum } | Select-Object -Last 1
Add-E2EPass $tracker "WO $($finalWo.orderNum) ($ProductItemNo)"

$salesBefore = Get-LocationQty $headers $ProductItemNo 'SALES'
$wr = Complete-E2EItemFinalWorkReport $headers $workOrders $ProductItemNo $OrderQty $TxDate
if (-not $wr) { throw 'Product work report failed' }
Add-E2EPass $tracker "WR $($wr.reportNum)"

$salesAfter = Get-LocationQty $headers $ProductItemNo 'SALES'
$salesDelta = $salesAfter - $salesBefore
if ($salesDelta -eq $OrderQty) { Add-E2EPass $tracker "SALES +$salesDelta" } else { Add-E2EFail $tracker "SALES delta=$salesDelta expected=$OrderQty" }

Write-Host "`n=== Phase 5: Shipment + revenue ===" -ForegroundColor Cyan
$shipLine = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/sales/shipments/candidates' -Headers $headers) |
    Where-Object { $_.orderLineId -eq $salesLineId } | Select-Object -First 1
if (-not $shipLine) { throw 'No shipment candidate' }

$shipment = Invoke-RestJson -Method POST -Path '/api/v1/sales/shipments' -Headers $headers -Body @{
    shipmentDate = $TxDate; lines = @(@{ salesOrderLineId = $salesLineId; shipmentQty = [double]$OrderQty })
}
Add-E2EPass $tracker "Shipment $($shipment.shipmentNo)"
$salesAfterShipment = Get-LocationQty $headers $ProductItemNo 'SALES'

$revLine = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/sales/revenues/candidates' -Headers $headers) |
    Where-Object { $_.orderLineId -eq $salesLineId } | Select-Object -First 1
if (-not $revLine) { throw 'No revenue candidate' }

$revenue = Invoke-RestJson -Method POST -Path '/api/v1/sales/revenues' -Headers $headers -Body @{
    revenueDate = $TxDate; lines = @(@{ salesShipmentLineId = [long]$revLine.shipmentLineId; revenueQty = [double]$OrderQty })
}
Add-E2EPass $tracker "Revenue $($revenue.revenueNo)"

if (-not $SkipCancelPhase) {
    Write-Host "`n=== Phase 6: Cancel reverse ===" -ForegroundColor Cyan
    Invoke-RestJson -Method POST -Path "/api/v1/sales/revenues/$($revenue.id)/cancel" -Headers $headers | Out-Null
    if ((Get-LocationQty $headers $ProductItemNo 'SALES') -eq $salesAfterShipment) { Add-E2EPass $tracker 'Revenue cancel ok' } else { Add-E2EFail $tracker 'Revenue cancel SALES mismatch' }

    Invoke-RestJson -Method POST -Path "/api/v1/sales/shipments/$($shipment.id)/cancel" -Headers $headers | Out-Null
    if ((Get-LocationQty $headers $ProductItemNo 'SALES') -ge $salesAfter) { Add-E2EPass $tracker 'Shipment cancel ok' } else { Add-E2EFail $tracker 'Shipment cancel SALES drop' }

    if ($wr.cancellable) {
        Invoke-RestJson -Method POST -Path "/api/v1/production/work-reports/$($wr.id)/cancel" -Headers $headers | Out-Null
        if ((Get-LocationQty $headers $ProductItemNo 'SALES') -lt $salesAfter) { Add-E2EPass $tracker 'WR cancel SALES restored' } else { Add-E2EFail $tracker 'WR cancel SALES not restored' }
    } else { Add-E2EFail $tracker 'WR not cancellable' }

    if ($outsourceChains.Count -gt 0) {
        Cancel-E2EOutsourceChains $headers $outsourceChains $tracker
    }
}

Write-E2ESummary $tracker 'P0-1 full TX chain E2E'
