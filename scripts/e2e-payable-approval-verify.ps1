# E2E P0-2: payable approval (receipt PENDING -> approve -> ledger -> cancel)
# Run: powershell -NoProfile -ExecutionPolicy Bypass -File scripts/e2e-payable-approval-verify.ps1

$ErrorActionPreference = 'Stop'
. "$PSScriptRoot/e2e-common.ps1"

$TxDate = if ($env:SM_E2E_TX_DATE) { $env:SM_E2E_TX_DATE } else { Today-Iso }
$ReceiptQty = if ($env:SM_E2E_RECEIPT_QTY) { [decimal]$env:SM_E2E_RECEIPT_QTY } else { [decimal]5 }
$RawItemNo = Get-E2ERawItemNo

$tracker = New-E2EResultTracker

if (-not (Test-ApiHealth)) {
    throw "API not responding: $Script:E2E_BaseUrl"
}

$headers = Login-E2E
$companies = Get-E2ECompanies $headers
$items = Get-E2EItems $headers
$purchasePartner = Find-CompanyByRole $companies 'PURCHASE' @('OUTSOURCE')
$rawItem = Resolve-E2EItem $headers $items $RawItemNo

if (-not $purchasePartner) { throw 'No PURCHASE partner found.' }
if (-not $rawItem) { throw "Raw item '$RawItemNo' not found." }

$fiscal = Get-FiscalPeriod $headers $TxDate

function Get-UnpaidAmount {
    param([long]$PartnerId)
    $candidates = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/purchase/payments/candidates' -Headers $headers)
    $row = $candidates | Where-Object { $_.partnerId -eq $PartnerId } | Select-Object -First 1
    if (-not $row) { return [decimal]0 }
    return To-Decimal $row.unpaidAmount
}

function Find-PendingByReceiptNo {
    param([string]$ReceiptNo)
    $pending = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/purchase/payable-approvals/pending' -Headers $headers)
    return $pending | Where-Object { $_.receiptDate -eq $TxDate -and $_.itemNo -ceq $RawItemNo } | Select-Object -First 1
}

Write-Host "`n=== Phase 1: PO + receipt (payable PENDING) ===" -ForegroundColor Cyan

$candidates = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/purchase/receipt-candidates' -Headers $headers)
$line = $candidates | Where-Object { $_.itemNum -ceq $RawItemNo -and $_.remainQty -gt 0 } | Select-Object -First 1

if (-not $line) {
    Write-Host 'No receipt candidate - creating manual PO' -ForegroundColor Yellow
    $po = Invoke-RestJson -Method POST -Path '/api/v1/purchase/orders' -Headers $headers -Body @{
        partnerId = [long]$purchasePartner.id
        orderDate = $TxDate
        sourceType = 'MANUAL'
        lines = @(@{
            itemId = [long]$rawItem.id
            orderQty = [double]$ReceiptQty
            unitPrice = 100
        })
    }
    $line = @{
        purchaseOrderLineId = [long]$po.lines[0].id
        remainQty = [double]$ReceiptQty
    }
}

$unpaidBefore = Get-UnpaidAmount ([long]$purchasePartner.id)

$receipt = Invoke-RestJson -Method POST -Path '/api/v1/purchase/receipts' -Headers $headers -Body @{
    receiptDate = $TxDate
    fiscalYear = [int]$fiscal.fiscalYear
    fiscalMonth = [int]$fiscal.fiscalMonth
    lines = @(@{
        purchaseOrderLineId = [long]$line.purchaseOrderLineId
        receiptQty = [double][Math]::Min($ReceiptQty, [decimal]$line.remainQty)
    })
}
Add-E2EPass $tracker "Receipt $($receipt.receiptNo)"

Start-Sleep -Milliseconds 300
$pendingRow = Find-PendingByReceiptNo $receipt.receiptNo
if ($pendingRow) {
    Add-E2EPass $tracker "Pending list historyId=$($pendingRow.historyId)"
} else {
    Add-E2EFail $tracker 'Pending list missing receipt history'
    Write-E2ESummary $tracker 'P0-2 payable approval E2E'
}

$unpaidAfterReceipt = Get-UnpaidAmount ([long]$purchasePartner.id)
if ($unpaidAfterReceipt -eq $unpaidBefore) {
    Add-E2EPass $tracker 'Unpaid unchanged after receipt (PENDING)'
} else {
    Add-E2EFail $tracker "Unpaid changed after receipt: $unpaidBefore -> $unpaidAfterReceipt"
}

Write-Host "`n=== Phase 2: Approve -> unpaid increase ===" -ForegroundColor Cyan
Invoke-RestJson -Method POST -Path '/api/v1/purchase/payable-approvals/approve' -Headers $headers -Body @{
    items = @(@{
        ledgerKind = $pendingRow.ledgerKind
        historyId = [long]$pendingRow.historyId
    })
} | Out-Null

$unpaidAfterApprove = Get-UnpaidAmount ([long]$purchasePartner.id)
if ($unpaidAfterApprove -gt $unpaidBefore) {
    Add-E2EPass $tracker "Unpaid increased $unpaidBefore -> $unpaidAfterApprove"
} else {
    Add-E2EFail $tracker "Unpaid not increased: $unpaidAfterApprove"
}

$approved = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/purchase/payable-approvals/approved' -Headers $headers)
$approvedRow = $approved | Where-Object { $_.historyId -eq $pendingRow.historyId } | Select-Object -First 1
if ($approvedRow) { Add-E2EPass $tracker 'Approved list updated' } else { Add-E2EFail $tracker 'Approved list missing row' }

Write-Host "`n=== Phase 3: Cancel approval -> unpaid restore ===" -ForegroundColor Cyan
Invoke-RestJson -Method POST -Path '/api/v1/purchase/payable-approvals/cancel-approval' -Headers $headers -Body @{
    items = @(@{
        ledgerKind = $pendingRow.ledgerKind
        historyId = [long]$pendingRow.historyId
    })
} | Out-Null

$unpaidAfterCancel = Get-UnpaidAmount ([long]$purchasePartner.id)
if ($unpaidAfterCancel -eq $unpaidBefore) {
    Add-E2EPass $tracker "Unpaid restored $unpaidAfterCancel"
} else {
    Add-E2EFail $tracker "Unpaid after cancel: $unpaidAfterCancel (expected $unpaidBefore)"
}

$pendingAgain = Find-PendingByReceiptNo $receipt.receiptNo
if ($pendingAgain) { Add-E2EPass $tracker 'Pending list restored after cancel' } else { Add-E2EFail $tracker 'Pending list missing after cancel' }

Write-E2ESummary $tracker 'P0-2 payable approval E2E'
