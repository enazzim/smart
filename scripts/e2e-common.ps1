# SmartManager E2E — 공통 헬퍼 (다른 e2e-*.ps1 에서 dot-source)
# Usage: . "$PSScriptRoot/e2e-common.ps1"

$Script:E2E_BaseUrl = if ($env:SM_E2E_BASE_URL) { $env:SM_E2E_BASE_URL } else { 'http://localhost:8080' }
$Script:E2E_LoginId = if ($env:SM_E2E_LOGIN_ID) { $env:SM_E2E_LOGIN_ID } else { 'admin' }
$Script:E2E_Password = if ($env:SM_E2E_PASSWORD) { $env:SM_E2E_PASSWORD } else { 'Admin123!' }

function Unwrap-ApiArray {
    param($Result)
    if ($null -eq $Result) { return @() }
    if ($Result -is [System.Array]) { return $Result }
    if ($null -ne $Result.value) { return @($Result.value) }
    return @($Result)
}

function To-Decimal {
    param($Value)
    if ($null -eq $Value) { return [decimal]0 }
    if ($Value -is [array]) { $Value = $Value[0] }
    return [decimal]$Value
}

function Login-E2E {
    $login = Invoke-RestMethod -Uri "$Script:E2E_BaseUrl/api/v1/auth/login" -Method POST `
        -ContentType 'application/json' -Body (@{
            loginId = $Script:E2E_LoginId
            password = $Script:E2E_Password
        } | ConvertTo-Json)
    return @{
        Authorization = "Bearer $($login.accessToken)"
        'Content-Type' = 'application/json'
    }
}

function Invoke-RestJson {
    param(
        [string]$Method = 'GET',
        [string]$Path,
        [hashtable]$Headers,
        [object]$Body
    )
    $uri = if ($Path.StartsWith('http')) { $Path } else { "$Script:E2E_BaseUrl$Path" }
    $iwrParams = @{
        Uri = $uri
        Method = $Method
        Headers = $Headers
        UseBasicParsing = $true
    }
    if ($null -ne $Body) {
        $json = ($Body | ConvertTo-Json -Depth 12)
        $iwrParams.Body = [System.Text.Encoding]::UTF8.GetBytes($json)
        if (-not $iwrParams.Headers.ContainsKey('Content-Type')) {
            $iwrParams.Headers['Content-Type'] = 'application/json; charset=utf-8'
        }
    }
    $response = Invoke-WebRequest @iwrParams
    if ($response.StatusCode -eq 204) { return $null }
    if ($null -eq $response.Content -or $response.Content.Length -eq 0) { return $null }
    $text = if ($response.RawContentStream) {
        $response.RawContentStream.Position = 0
        [System.IO.StreamReader]::new($response.RawContentStream, [System.Text.Encoding]::UTF8).ReadToEnd()
    } else {
        [System.Text.Encoding]::UTF8.GetString($response.Content)
    }
    if ([string]::IsNullOrWhiteSpace($text)) { return $null }
    return $text | ConvertFrom-Json
}

function Invoke-RestJsonExpectFailure {
    param(
        [string]$Method = 'POST',
        [string]$Path,
        [hashtable]$Headers,
        [object]$Body
    )
    try {
        $null = Invoke-RestJson -Method $Method -Path $Path -Headers $Headers -Body $Body
        return @{ Failed = $false; StatusCode = 200; Message = 'request succeeded (expected failure)' }
    } catch {
        $status = 0
        $message = $_.Exception.Message
        if ($_.Exception.Response) {
            $status = [int]$_.Exception.Response.StatusCode
            try {
                $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
                $message = $reader.ReadToEnd()
                $reader.Close()
            } catch {
                # ignore stream read errors
            }
        }
        return @{ Failed = $true; StatusCode = $status; Message = $message }
    } finally {
        $global:LASTEXITCODE = 0
    }
}

function Test-ApiHealth {
    try {
        Invoke-RestMethod -Uri "$Script:E2E_BaseUrl/api/health" -Method GET | Out-Null
        return $true
    } catch {
        return $false
    }
}

function Get-E2ECompanies {
    param([hashtable]$Headers)
    return Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/companies' -Headers $Headers)
}

function Get-E2EItems {
    param([hashtable]$Headers)
    return Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/items' -Headers $Headers)
}

function Find-CompanyByRole {
    param(
        [array]$Companies,
        [string]$Role,
        [string[]]$ExcludeRoles = @()
    )
    return $Companies | Where-Object {
        $company = $_
        $company.roles -contains $Role -and (
            $ExcludeRoles.Count -eq 0 -or -not ($ExcludeRoles | Where-Object { $company.roles -contains $_ })
        )
    } | Select-Object -First 1
}

function Find-ItemByNo {
    param([array]$Items, [string]$ItemNo)
    return $Items | Where-Object { $_.itemNo -ceq $ItemNo } | Select-Object -First 1
}

function Get-E2EItemByNo {
    param(
        [hashtable]$Headers,
        [string]$ItemNo
    )
    try {
        return Invoke-RestJson -Path "/api/v1/basis/items/by-no/$ItemNo" -Headers $Headers
    } catch {
        return $null
    }
}

function Resolve-E2EItem {
    param(
        [hashtable]$Headers,
        [array]$Items,
        [string]$ItemNo
    )
    $found = Find-ItemByNo $Items $ItemNo
    if ($found) { return $found }
    return Get-E2EItemByNo $Headers $ItemNo
}

function Get-FiscalPeriod {
    param(
        [hashtable]$Headers,
        [string]$Date
    )
    $query = if ($Date) { "?date=$Date" } else { '' }
    return Invoke-RestJson -Path "/api/v1/system/month-closings/period$query" -Headers $Headers
}

function Get-LocationQty {
    param(
        [hashtable]$Headers,
        [string]$ItemNo,
        [string]$LocationCode
    )
    $rows = Unwrap-ApiArray (Invoke-RestJson -Path "/api/v1/inventory/balances?itemNo=$ItemNo" -Headers $Headers)
    $sum = [decimal]0
    foreach ($row in $rows | Where-Object { $_.locationCode -eq $LocationCode }) {
        $sum += (To-Decimal $row.stockQty)
    }
    return $sum
}

function New-E2EResultTracker {
    return @{
        Pass = [System.Collections.Generic.List[string]]::new()
        Fail = [System.Collections.Generic.List[string]]::new()
    }
}

function Add-E2EPass {
    param($Tracker, [string]$Message)
    $Tracker.Pass.Add($Message)
    Write-Host "PASS $Message" -ForegroundColor Green
}

function Add-E2EFail {
    param($Tracker, [string]$Message)
    $Tracker.Fail.Add($Message)
    Write-Host "FAIL $Message" -ForegroundColor Red
}

function Write-E2ESummary {
    param($Tracker, [string]$Title)
    Write-Host "`n=== $Title ===" -ForegroundColor Cyan
    Write-Host "PASS: $($Tracker.Pass.Count)" -ForegroundColor Green
    Write-Host "FAIL: $($Tracker.Fail.Count)" -ForegroundColor $(if ($Tracker.Fail.Count -gt 0) { 'Red' } else { 'Green' })
    if ($Tracker.Fail.Count -gt 0) {
        exit 1
    }
    exit 0
}

function Today-Iso {
    return (Get-Date).ToString('yyyy-MM-dd')
}

# --- E2E 기본 품목 (seed-operational-basis FG-01 세트) ---
function Get-E2EPropertyClass {
    param([ValidateSet('RAW', 'FG', 'SF')][string]$Kind)
    switch ($Kind) {
        'RAW' { return -join (0xC6D0, 0xC790, 0xC7AC | ForEach-Object { [char]$_ }) }
        'FG'  { return -join (0xC81C, 0xD488 | ForEach-Object { [char]$_ }) }
        'SF'  { return -join (0xACF5, 0xC815, 0xD488 | ForEach-Object { [char]$_ }) }
    }
}

function Get-E2EProductItemNo {
    if ($env:SM_E2E_PRODUCT_ITEM) { return $env:SM_E2E_PRODUCT_ITEM }
    return 'FG-01'
}

function Get-E2ERawItemNo {
    if ($env:SM_E2E_RAW_ITEM) { return $env:SM_E2E_RAW_ITEM }
    return 'RM-PA'
}

function Get-E2ERawItemNos {
    param(
        [hashtable]$Headers,
        [string]$ProductItemNo
    )
    if ($env:SM_E2E_RAW_ITEMS) {
        return @($env:SM_E2E_RAW_ITEMS -split ',' | ForEach-Object { $_.Trim() } | Where-Object { $_ })
    }
    if ($ProductItemNo -ceq 'abc') {
        return @('abc-r', 'abc-t')
    }
    return Get-BomLeafRawItemNos $Headers $ProductItemNo
}

function Get-BomExplosion {
    param(
        [hashtable]$Headers,
        [string]$ItemNo
    )
    return Invoke-RestJson -Path "/api/v1/basis/item-composition/plan/$ItemNo/explosion" -Headers $Headers
}

function Get-BomLeafNodes {
    param($Node)
    $leaves = [System.Collections.Generic.List[object]]::new()
    function Walk($n) {
        if (-not $n.children -or $n.children.Count -eq 0) {
            $leaves.Add($n)
            return
        }
        foreach ($child in $n.children) { Walk $child }
    }
    Walk $Node
    return $leaves
}

function Get-BomLeafRawItemNos {
    param(
        [hashtable]$Headers,
        [string]$ProductItemNo
    )
    $tree = Get-BomExplosion $Headers $ProductItemNo
    $clsRaw = Get-E2EPropertyClass 'RAW'
    return @(
        Get-BomLeafNodes $tree |
            Where-Object { $_.propertyClassification -eq $clsRaw } |
            ForEach-Object { $_.itemNum }
    )
}

function Get-BomDirectSemiNodes {
    param(
        [hashtable]$Headers,
        [string]$ProductItemNo
    )
    $tree = Get-BomExplosion $Headers $ProductItemNo
    $clsSf = Get-E2EPropertyClass 'SF'
    if (-not $tree.children) { return @() }
    return @($tree.children | Where-Object { $_.propertyClassification -eq $clsSf })
}

function Submit-E2EWorkReport {
    param(
        [hashtable]$Headers,
        [long]$WorkOrderId,
        [double]$GoodQty,
        [string]$TxDate
    )
    $cons = Invoke-RestJson -Path "/api/v1/production/work-reports/consumption-status?workOrderId=$WorkOrderId&pendingGoodQty=$GoodQty" -Headers $Headers
    $issueLines = @($cons.lines | ForEach-Object {
        @{ itemCompositionId = $_.itemCompositionId; itemId = $_.itemId; issueQty = [double]$_.requiredQty }
    })
    return Invoke-RestJson -Method POST -Path '/api/v1/production/work-reports' -Headers $Headers -Body @{
        workOrderId = $WorkOrderId
        reportDate = $TxDate
        goodQty = $GoodQty
        scrapQty = 0
        workerName = 'e2e'
        issueLines = $issueLines
    }
}

function Complete-E2EItemFinalWorkReport {
    param(
        [hashtable]$Headers,
        [array]$WorkOrders,
        [string]$ItemNo,
        [decimal]$GoodQty,
        [string]$TxDate
    )
    $wos = @($WorkOrders | Where-Object { $_.itemNo -ceq $ItemNo })
    if ($wos.Count -eq 0) { return $null }
    $finalWo = $wos | Sort-Object { $_.processSequenceNum } | Select-Object -Last 1
    $qty = [double][Math]::Min($GoodQty, [decimal]$finalWo.remainingQty)
    if ($qty -le 0) { return $null }
    return Submit-E2EWorkReport $Headers ([long]$finalWo.id) $qty $TxDate
}

function Complete-E2EOutsourceWorkPlans {
    param(
        [hashtable]$Headers,
        [array]$WorkPlans,
        [string]$TxDate,
        $Fiscal,
        $Tracker
    )
    $sorted = @(
        $WorkPlans |
            Where-Object { $_.workDistinction -eq 'OUTSOURCE' } |
            Sort-Object @{ Expression = { if ($_.itemNo -match '^SF-') { 0 } else { 1 } } }, processSequenceNum
    )
    if ($sorted.Count -eq 0) { return @() }

    $chains = [System.Collections.Generic.List[object]]::new()
    foreach ($wp in $sorted) {
        $candidates = Unwrap-ApiArray (
            Invoke-RestJson -Path "/api/v1/outsource/orders/work-plan-candidates?orderDate=$TxDate" -Headers $Headers
        )
        $candidate = $candidates | Where-Object { $_.workPlanId -eq $wp.id } | Select-Object -First 1
        if (-not $candidate) {
            Add-E2EFail $Tracker "OO candidate missing $($wp.itemNo) seq$($wp.processSequenceNum)"
            continue
        }
        if (-not $candidate.orderable) {
            Add-E2EFail $Tracker "OO blocked $($wp.itemNo) seq$($wp.processSequenceNum): $($candidate.orderableMessage)"
            continue
        }
        $vendor = $candidate.vendors | Select-Object -First 1
        if (-not $vendor) {
            Add-E2EFail $Tracker "OO no vendor $($wp.itemNo) seq$($wp.processSequenceNum)"
            continue
        }

        $qty = [double]$candidate.remainingQty
        if ($qty -le 0) { $qty = [double]$candidate.plannedQty }

        $oo = Invoke-RestJson -Method POST -Path '/api/v1/outsource/orders/from-work-plan' -Headers $Headers -Body @{
            partnerId = [long]$vendor.partnerId
            orderDate = $TxDate
            lines = @(@{
                workPlanId = [long]$wp.id
                beginProcessCodeId = [long]$vendor.beginProcessCodeId
                endProcessCodeId = [long]$vendor.endProcessCodeId
                orderQty = $qty
                unitPrice = [double]$vendor.unitPrice
                requestedDeliveryDate = $TxDate
            })
        }
        $orderLineId = [long]$oo.lines[0].id
        Add-E2EPass $Tracker "OO $($oo.orderNo) $($wp.itemNo) seq$($wp.processSequenceNum)"

        $shipment = Invoke-RestJson -Method POST -Path '/api/v1/outsource/shipments' -Headers $Headers -Body @{
            shipmentDate = $TxDate
            lines = @(@{ orderLineId = $orderLineId; shipmentQty = $qty })
        }
        Add-E2EPass $Tracker "OSh $($shipment.shipmentNo) $($wp.itemNo)"

        Start-Sleep -Milliseconds 300
        $receiptCandidates = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/outsource/receipts/candidates' -Headers $Headers)
        $rc = $receiptCandidates | Where-Object { $_.outsourcingOrderLineId -eq $orderLineId } | Select-Object -First 1
        if (-not $rc) {
            Add-E2EFail $Tracker "OR candidate missing $($wp.itemNo) line=$orderLineId"
            continue
        }
        $receiptQty = [double][Math]::Min($qty, [decimal]$rc.remainQty)
        $or = Invoke-RestJson -Method POST -Path '/api/v1/outsource/receipts' -Headers $Headers -Body @{
            receiptDate = $TxDate
            fiscalYear = [int]$Fiscal.fiscalYear
            fiscalMonth = [int]$Fiscal.fiscalMonth
            lines = @(@{ outsourcingOrderLineId = $orderLineId; receiptQty = $receiptQty })
        }
        Add-E2EPass $Tracker "OR $($or.receiptNo) $($wp.itemNo)"
        $chains.Add([PSCustomObject]@{
            itemNo = $wp.itemNo
            seq = [int]$wp.processSequenceNum
            order = $oo
            shipment = $shipment
            receipt = $or
        }) | Out-Null
    }
    return $chains.ToArray()
}

function Cancel-E2EOutsourceChains {
    param(
        [hashtable]$Headers,
        [array]$Chains,
        $Tracker
    )
    if (-not $Chains -or $Chains.Count -eq 0) { return }

    for ($i = $Chains.Count - 1; $i -ge 0; $i--) {
        $chain = $Chains[$i]
        $label = "$($chain.itemNo) seq$($chain.seq)"

        if ($chain.receipt -and $chain.receipt.id) {
            try {
                Invoke-RestJson -Method POST -Path "/api/v1/outsource/receipts/$($chain.receipt.id)/cancel" -Headers $Headers | Out-Null
                Add-E2EPass $Tracker "OR cancel $label"
            } catch {
                Add-E2EFail $Tracker "OR cancel failed $label"
            }
        }

        if ($chain.shipment -and $chain.shipment.id) {
            try {
                Invoke-RestJson -Method POST -Path "/api/v1/outsource/shipments/$($chain.shipment.id)/cancel" -Headers $Headers | Out-Null
                Add-E2EPass $Tracker "OSh cancel $label"
            } catch {
                Add-E2EFail $Tracker "OSh cancel failed $label"
            }
        }

        if ($chain.order -and $chain.order.id) {
            try {
                $detail = Invoke-RestJson -Path "/api/v1/outsource/orders/$($chain.order.id)" -Headers $Headers
                if ($detail.cancelable) {
                    Invoke-RestJson -Method POST -Path "/api/v1/outsource/orders/$($chain.order.id)/cancel" -Headers $Headers | Out-Null
                    Add-E2EPass $Tracker "OO cancel $label"
                } else {
                    Add-E2EPass $Tracker "OO cancel skip $label (not cancelable)"
                }
            } catch {
                Add-E2EFail $Tracker "OO cancel failed $label"
            }
        }
    }
}
