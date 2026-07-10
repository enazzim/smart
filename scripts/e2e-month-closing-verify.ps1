# E2E P0-3: month closing - block receipt/approve in closed month
# Run: powershell -NoProfile -ExecutionPolicy Bypass -File scripts/e2e-month-closing-verify.ps1
# Note: only months before current fiscal month can be closed. Reopen latest closed month only.

$ErrorActionPreference = 'Stop'
. "$PSScriptRoot/e2e-common.ps1"

$now = Get-Date
$prevMonthDate = $now.AddMonths(-1)
$ClosingYear = if ($env:SM_E2E_CLOSE_YEAR) { [int]$env:SM_E2E_CLOSE_YEAR } else { $prevMonthDate.Year }
$ClosingMonth = if ($env:SM_E2E_CLOSE_MONTH) { [int]$env:SM_E2E_CLOSE_MONTH } else { $prevMonthDate.Month }
$ClosedTxDate = if ($env:SM_E2E_CLOSED_TX_DATE) {
    $env:SM_E2E_CLOSED_TX_DATE
} else {
    '{0:D4}-{1:D2}-15' -f $ClosingYear, $ClosingMonth
}

$RawItemNo = Get-E2ERawItemNo
$ReceiptQty = [decimal]1
$tracker = New-E2EResultTracker

if (-not (Test-ApiHealth)) { throw "API not reachable: $Script:E2E_BaseUrl" }

$headers = Login-E2E

function Get-ClosedMonths {
    return Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/system/month-closings' -Headers $headers)
}

function Test-MonthClosed {
    param([int]$Year, [int]$Month)
    return [bool](Get-ClosedMonths | Where-Object { $_.fiscalYear -eq $Year -and $_.fiscalMonth -eq $Month })
}

function Ensure-MonthOpen {
    param([int]$Year, [int]$Month)
    while (Test-MonthClosed $Year $Month) {
        $latest = (Get-ClosedMonths | Sort-Object { $_.fiscalYear * 100 + $_.fiscalMonth } | Select-Object -Last 1)
        if ($latest.fiscalYear -ne $Year -or $latest.fiscalMonth -ne $Month) {
            throw "Reopen only latest closed month. target=$Year-$Month latest=$($latest.fiscalYear)-$($latest.fiscalMonth)"
        }
        Write-Host "Reopen month: $Year-$Month" -ForegroundColor Yellow
        Invoke-RestJson -Method DELETE -Path "/api/v1/system/month-closings/$Year/$Month" -Headers $headers | Out-Null
    }
}

function Close-MonthWithPrerequisite {
    param([int]$Year, [int]$Month)
    if (Test-MonthClosed $Year $Month) {
        Write-Host "Already closed: $Year-$Month" -ForegroundColor DarkGray
        return
    }
    $prev = if ($Month -eq 1) {
        [PSCustomObject]@{ fiscalYear = $Year - 1; fiscalMonth = 12 }
    } else {
        [PSCustomObject]@{ fiscalYear = $Year; fiscalMonth = $Month - 1 }
    }
    if ((Get-ClosedMonths).Count -gt 0 -and -not (Test-MonthClosed $prev.fiscalYear $prev.fiscalMonth)) {
        Close-MonthWithPrerequisite $prev.fiscalYear $prev.fiscalMonth
    }
    Invoke-RestJson -Method POST -Path '/api/v1/system/month-closings' -Headers $headers -Body @{
        fiscalYear = $Year; fiscalMonth = $Month
    } | Out-Null
    Add-E2EPass $tracker "Closed $Year-$($Month.ToString('00'))"
}

Write-Host "`n=== Phase 0: Ensure target month open ===" -ForegroundColor Cyan
Ensure-MonthOpen $ClosingYear $ClosingMonth

$companies = Get-E2ECompanies $headers
$items = Get-E2EItems $headers
$purchasePartner = Find-CompanyByRole $companies 'PURCHASE' @('OUTSOURCE')
$rawItem = Resolve-E2EItem $headers $items $RawItemNo
if (-not $purchasePartner -or -not $rawItem) { throw 'Purchase partner or raw item missing' }

$openDate = (Get-Date).ToString('yyyy-MM-dd')
$candidates = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/purchase/receipt-candidates' -Headers $headers)
$line = $candidates | Where-Object { $_.itemNum -ceq $RawItemNo -and $_.remainQty -gt 0 } | Select-Object -First 1
if (-not $line) {
    $po = Invoke-RestJson -Method POST -Path '/api/v1/purchase/orders' -Headers $headers -Body @{
        partnerId = [long]$purchasePartner.id; orderDate = $openDate; sourceType = 'MANUAL'
        lines = @(@{ itemId = [long]$rawItem.id; orderQty = 1; unitPrice = 100 })
    }
    $line = @{ purchaseOrderLineId = [long]$po.lines[0].id }
}

$prepReceipt = Invoke-RestJson -Method POST -Path '/api/v1/purchase/receipts' -Headers $headers -Body @{
    receiptDate = $ClosedTxDate; fiscalYear = $ClosingYear; fiscalMonth = $ClosingMonth
    lines = @(@{ purchaseOrderLineId = [long]$line.purchaseOrderLineId; receiptQty = [double]$ReceiptQty })
}
Add-E2EPass $tracker "Prep receipt $($prepReceipt.receiptNo) date=$ClosedTxDate"

$pending = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/purchase/payable-approvals/pending' -Headers $headers)
$pendingRow = $pending | Where-Object {
    $_.historyId -gt 0 -and $_.itemNo -ceq $RawItemNo -and $_.fiscalYear -eq $ClosingYear -and $_.fiscalMonth -eq $ClosingMonth
} | Select-Object -First 1
if (-not $pendingRow) { Add-E2EFail $tracker 'No prep PENDING row' }

Write-Host "`n=== Phase 1: Close month ===" -ForegroundColor Cyan
Close-MonthWithPrerequisite $ClosingYear $ClosingMonth

Write-Host "`n=== Phase 2: Block TX in closed month ===" -ForegroundColor Cyan

$blockedReceipt = Invoke-RestJsonExpectFailure -Method POST -Path '/api/v1/purchase/receipts' -Headers $headers -Body @{
    receiptDate = $ClosedTxDate; fiscalYear = $ClosingYear; fiscalMonth = $ClosingMonth
    lines = @(@{ purchaseOrderLineId = [long]$line.purchaseOrderLineId; receiptQty = [double]$ReceiptQty })
}
if ($blockedReceipt.Failed) {
    Add-E2EPass $tracker "Receipt blocked HTTP $($blockedReceipt.StatusCode)"
} else {
    Add-E2EFail $tracker 'Receipt allowed in closed month'
}

if ($pendingRow) {
    $blockedApprove = Invoke-RestJsonExpectFailure -Method POST -Path '/api/v1/purchase/payable-approvals/approve' -Headers $headers -Body @{
        items = @(@{ ledgerKind = $pendingRow.ledgerKind; historyId = [long]$pendingRow.historyId })
    }
    if ($blockedApprove.Failed) {
        Add-E2EPass $tracker "Approve blocked HTTP $($blockedApprove.StatusCode)"
    } else {
        Add-E2EFail $tracker 'Approve allowed in closed month'
    }
}

Write-Host "`n=== Phase 3: Cleanup reopen ===" -ForegroundColor Cyan
try {
    if (Test-MonthClosed $ClosingYear $ClosingMonth) {
        $latest = (Get-ClosedMonths | Sort-Object { $_.fiscalYear * 100 + $_.fiscalMonth } | Select-Object -Last 1)
        if ($latest.fiscalYear -eq $ClosingYear -and $latest.fiscalMonth -eq $ClosingMonth) {
            Invoke-RestJson -Method DELETE -Path "/api/v1/system/month-closings/$ClosingYear/$ClosingMonth" -Headers $headers | Out-Null
            Add-E2EPass $tracker 'Reopened (cleanup)'
        } else {
            Write-Host "Cleanup skip: not latest closed month" -ForegroundColor Yellow
        }
    }
} catch {
    Add-E2EFail $tracker "Reopen failed: $($_.Exception.Message)"
}

Write-E2ESummary $tracker 'P0-3 month closing E2E'
