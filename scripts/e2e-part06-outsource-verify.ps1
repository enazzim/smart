# E2E Part 06 — Outsource unit price + OutsourceInputBalanceProjector
# Requires: seed-operational-basis (FG-01, Outsource-01/02, RM-PA)
# Run: powershell -NoProfile -ExecutionPolicy Bypass -File scripts/e2e-part06-outsource-verify.ps1

$ErrorActionPreference = 'Stop'
. "$PSScriptRoot/e2e-common.ps1"

$Today = if ($env:SM_E2E_TX_DATE) { $env:SM_E2E_TX_DATE } else { Today-Iso }
$ProductNo = if ($env:SM_E2E_PRODUCT_ITEM) { $env:SM_E2E_PRODUCT_ITEM } else { 'FG-01' }
$RawNo = Get-E2ERawItemNo
$tracker = New-E2EResultTracker
$ts = Get-Date -Format 'yyyyMMddHHmmss'

if (-not (Test-ApiHealth)) { throw "API not responding: $Script:E2E_BaseUrl" }

$headers = Login-E2E
$companies = Get-E2ECompanies $headers
$items = Get-E2EItems $headers
$processCodes = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/public-codes?usageType=PROCESS' -Headers $headers)
$workCenters = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/work-centers' -Headers $headers)

$pcInhouse = ($processCodes | Where-Object { $_.smallCode -eq '14000010' } | Select-Object -First 1)
$pcOutsource = ($processCodes | Where-Object { $_.smallCode -eq '14000020' } | Select-Object -First 1)
$wc = $workCenters | Select-Object -First 1
$outCo = Find-CompanyByRole $companies 'OUTSOURCE' @()
$salesCo = Find-CompanyByRole $companies 'SALES' @('OUTSOURCE', 'PURCHASE')
$product = Resolve-E2EItem $headers $items $ProductNo
$rawItem = Resolve-E2EItem $headers $items $RawNo

if (-not $outCo) { throw 'No OUTSOURCE partner found.' }
if (-not $pcInhouse -or -not $pcOutsource) { throw 'Process codes 14000010/14000020 not found.' }
if (-not $product) { throw "Product '$ProductNo' not found." }

Write-Host "`n=== Part06 Phase 1: Preconditions ===" -ForegroundColor Cyan

$procs = Unwrap-ApiArray (Invoke-RestJson -Path "/api/v1/basis/processes/plan?itemId=$($product.id)" -Headers $headers)
$hasInhouse = @($procs | Where-Object { $_.workDistinction -eq 'INHOUSE' })
$hasOutsource = @($procs | Where-Object { $_.workDistinction -eq 'OUTSOURCE' })
if ($hasInhouse.Count -gt 0) { Add-E2EPass $tracker "Product $ProductNo has INHOUSE process" } else { Add-E2EFail $tracker "Product $ProductNo missing INHOUSE" }
if ($hasOutsource.Count -gt 0) { Add-E2EPass $tracker "Product $ProductNo has OUTSOURCE process" } else { Add-E2EFail $tracker "Product $ProductNo missing OUTSOURCE" }

$outPrices = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/unit-prices?type=OUTSOURCE' -Headers $headers)
$seedPrices = @($outPrices | Where-Object { $_.itemNum -ceq $ProductNo })
if ($seedPrices.Count -ge 1) {
    Add-E2EPass $tracker "Seed outsource prices for $ProductNo count=$($seedPrices.Count)"
} else {
    Add-E2EFail $tracker "No seed outsource price for $ProductNo"
}

Write-Host "`n=== Part06 Phase 2: P1 negative (SALES partner) ===" -ForegroundColor Cyan
if ($salesCo) {
    $bad = Invoke-RestJsonExpectFailure -Method POST -Path '/api/v1/basis/unit-prices' -Headers $headers -Body @{
        type = 'OUTSOURCE'; itemNum = $ProductNo; companyId = [long]$salesCo.id
        beginProcessCodeId = [long]$pcOutsource.id; endProcessCodeId = [long]$pcOutsource.id
        orderRate = 100; standardUnitCost = 100; beginDate = $Today
    }
    if ($bad.Failed -and $bad.StatusCode -eq 400) { Add-E2EPass $tracker 'P1 neg SALES partner blocked HTTP 400' }
    else { Add-E2EFail $tracker "P1 neg expected 400 got $($bad.StatusCode)" }
} else {
    Add-E2EFail $tracker 'No SALES partner for P1 neg test'
}

Write-Host "`n=== Part06 Phase 3: P2 negative (outsource-only item) ===" -ForegroundColor Cyan
$p2No = "E2E-P2-$ts"
Invoke-RestJson -Method POST -Path '/api/v1/basis/items' -Headers $headers -Body @{
    itemNo = $p2No; itemName = "E2E-P2-$ts"; propertyClassification = (Get-E2EPropertyClass 'FG')
    unit = 'EA'; checkDistinction = 'NONE'; standardUnitCost = 1000
} | Out-Null
$p2Item = Resolve-E2EItem $headers (Get-E2EItems $headers) $p2No
Invoke-RestJson -Method POST -Path '/api/v1/basis/processes/plan' -Headers $headers -Body @{
    itemId = [long]$p2Item.id; processSequenceNum = 20; processCodeId = [long]$pcOutsource.id
    workDistinction = 'OUTSOURCE'; progressRate = 100; outsideOrderRate = 0
} | Out-Null
$p2bad = Invoke-RestJsonExpectFailure -Method POST -Path '/api/v1/basis/unit-prices' -Headers $headers -Body @{
    type = 'OUTSOURCE'; itemNum = $p2No; companyId = [long]$outCo.id
    beginProcessCodeId = [long]$pcOutsource.id; endProcessCodeId = [long]$pcOutsource.id
    orderRate = 100; standardUnitCost = 100; beginDate = $Today
}
if ($p2bad.Failed -and $p2bad.StatusCode -eq 400) { Add-E2EPass $tracker 'P2 neg outsource-only item blocked HTTP 400' }
else { Add-E2EFail $tracker "P2 neg expected 400 got $($p2bad.StatusCode)" }

Write-Host "`n=== Part06 Phase 4: Case B (OUT+IN+BOM) ===" -ForegroundColor Cyan
$bNo = "E2E-B-$ts"
Invoke-RestJson -Method POST -Path '/api/v1/basis/items' -Headers $headers -Body @{
    itemNo = $bNo; itemName = "E2E-B-$ts"; propertyClassification = (Get-E2EPropertyClass 'FG')
    unit = 'EA'; checkDistinction = 'NONE'; standardUnitCost = 1000
} | Out-Null
$bItem = Resolve-E2EItem $headers (Get-E2EItems $headers) $bNo
Invoke-RestJson -Method POST -Path '/api/v1/basis/processes/plan' -Headers $headers -Body @{
    itemId = [long]$bItem.id; processSequenceNum = 20; processCodeId = [long]$pcOutsource.id
    workDistinction = 'OUTSOURCE'; progressRate = 50; outsideOrderRate = 0
} | Out-Null
Invoke-RestJson -Method POST -Path '/api/v1/basis/processes/plan' -Headers $headers -Body @{
    itemId = [long]$bItem.id; processSequenceNum = 30; processCodeId = [long]$pcInhouse.id
    workDistinction = 'INHOUSE'; workCenterId = [long]$wc.id; progressRate = 100; outsideOrderRate = 0
} | Out-Null
Invoke-RestJson -Method POST -Path '/api/v1/basis/item-composition/plan' -Headers $headers -Body @{
    parentItemNum = $bNo; childItemNum = $RawNo; parentQuantity = 1; childQuantity = 1
} | Out-Null
$bPrice = Invoke-RestJson -Method POST -Path '/api/v1/basis/unit-prices' -Headers $headers -Body @{
    type = 'OUTSOURCE'; itemNum = $bNo; companyId = [long]$outCo.id
    beginProcessCodeId = [long]$pcOutsource.id; endProcessCodeId = [long]$pcOutsource.id
    orderRate = 100; standardUnitCost = 500; beginDate = $Today
}
if ($bPrice.id) { Add-E2EPass $tracker "Case B price registered id=$($bPrice.id)" } else { Add-E2EFail $tracker 'Case B price register failed' }

Write-Host "`n=== Part06 Phase 5: Case C (OUT+IN no BOM) ===" -ForegroundColor Cyan
$cNo = "E2E-C-$ts"
Invoke-RestJson -Method POST -Path '/api/v1/basis/items' -Headers $headers -Body @{
    itemNo = $cNo; itemName = "E2E-C-$ts"; propertyClassification = (Get-E2EPropertyClass 'FG')
    unit = 'EA'; checkDistinction = 'NONE'; standardUnitCost = 1000
} | Out-Null
$cItem = Resolve-E2EItem $headers (Get-E2EItems $headers) $cNo
Invoke-RestJson -Method POST -Path '/api/v1/basis/processes/plan' -Headers $headers -Body @{
    itemId = [long]$cItem.id; processSequenceNum = 20; processCodeId = [long]$pcOutsource.id
    workDistinction = 'OUTSOURCE'; progressRate = 50; outsideOrderRate = 0
} | Out-Null
Invoke-RestJson -Method POST -Path '/api/v1/basis/processes/plan' -Headers $headers -Body @{
    itemId = [long]$cItem.id; processSequenceNum = 30; processCodeId = [long]$pcInhouse.id
    workDistinction = 'INHOUSE'; workCenterId = [long]$wc.id; progressRate = 100; outsideOrderRate = 0
} | Out-Null
$cbad = Invoke-RestJsonExpectFailure -Method POST -Path '/api/v1/basis/unit-prices' -Headers $headers -Body @{
    type = 'OUTSOURCE'; itemNum = $cNo; companyId = [long]$outCo.id
    beginProcessCodeId = [long]$pcOutsource.id; endProcessCodeId = [long]$pcOutsource.id
    orderRate = 100; standardUnitCost = 500; beginDate = $Today
}
if ($cbad.Failed -and $cbad.StatusCode -eq 400) { Add-E2EPass $tracker 'Case C no BOM blocked HTTP 400' }
else { Add-E2EFail $tracker "Case C expected 400 got $($cbad.StatusCode)" }

if ($bPrice.id) {
    try {
        Invoke-RestJson -Method DELETE -Path "/api/v1/basis/unit-prices/$($bPrice.id)" -Headers $headers | Out-Null
        Add-E2EPass $tracker 'Case B price cleanup delete'
    } catch {
        Add-E2EFail $tracker 'Case B price cleanup failed'
    }
}

Write-E2ESummary $tracker 'Part06 outsource projector E2E'
