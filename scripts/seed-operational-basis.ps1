# SmartManager operational basis seed (ASCII-safe source)
# Run: powershell -NoProfile -ExecutionPolicy Bypass -File scripts/seed-operational-basis.ps1

$ErrorActionPreference = 'Stop'
. "$PSScriptRoot/e2e-common.ps1"

function Get-ItemClassName {
    param([ValidateSet('RAW','FG','SF')][string]$Kind)
    switch ($Kind) {
        'RAW' { return -join (0xC6D0, 0xC790, 0xC7AC | ForEach-Object { [char]$_ }) }
        'FG'  { return -join (0xC81C, 0xD488 | ForEach-Object { [char]$_ }) }
        'SF'  { return -join (0xACF5, 0xC815, 0xD488 | ForEach-Object { [char]$_ }) }
    }
}

$SeedTag = 'SEED-OP'
$Today = (Get-Date).ToString('yyyy-MM-dd')
$tracker = New-E2EResultTracker
$clsRaw = Get-ItemClassName 'RAW'
$clsFg = Get-ItemClassName 'FG'
$clsSf = Get-ItemClassName 'SF'

$SalesCompanies = 1..5 | ForEach-Object {
    [PSCustomObject]@{ Name = "Sales-$('{0:D2}' -f $_)"; RegNo = "101-01-{0:D5}" -f $_; Role = @('SALES') }
}
$PurchaseCompanies = 1..10 | ForEach-Object {
    [PSCustomObject]@{ Name = "Purchase-$('{0:D2}' -f $_)"; RegNo = "201-01-{0:D5}" -f $_; Role = @('PURCHASE') }
}
$OutsourceCompanies = 1..2 | ForEach-Object {
    [PSCustomObject]@{ Name = "Outsource-$('{0:D2}' -f $_)"; RegNo = "301-01-{0:D5}" -f $_; Role = @('OUTSOURCE') }
}

$ProductItems = 1..5 | ForEach-Object {
    $n = '{0:D2}' -f $_
    [PSCustomObject]@{ No = "FG-$n"; Name = "Product-$n"; Class = $clsFg }
}
$SemiItems = 1..5 | ForEach-Object {
    $n = '{0:D2}' -f $_
    [PSCustomObject]@{ No = "SF-$n"; Name = "Semi-$n"; Class = $clsSf }
}
$ProductRawItems = @(
    [PSCustomObject]@{ No = 'RM-PA'; Name = 'Raw-Product-A'; Class = $clsRaw }
    [PSCustomObject]@{ No = 'RM-PB'; Name = 'Raw-Product-B'; Class = $clsRaw }
)
$SemiRawItems = 1..5 | ForEach-Object {
    $n = '{0:D2}' -f $_
    [PSCustomObject]@{ No = "RM-W$n"; Name = "Raw-Semi-$n"; Class = $clsRaw }
}

$BomLines = @(
    @{ Parent = 'FG-01'; Child = 'SF-01'; PQ = 1; CQ = 1 }
    @{ Parent = 'FG-01'; Child = 'RM-PA'; PQ = 1; CQ = 2 }
    @{ Parent = 'FG-01'; Child = 'RM-PB'; PQ = 1; CQ = 1 }
    @{ Parent = 'FG-02'; Child = 'SF-02'; PQ = 1; CQ = 1 }
    @{ Parent = 'FG-02'; Child = 'SF-03'; PQ = 2; CQ = 1 }
    @{ Parent = 'FG-02'; Child = 'RM-PA'; PQ = 1; CQ = 1 }
    @{ Parent = 'FG-03'; Child = 'SF-03'; PQ = 1; CQ = 1 }
    @{ Parent = 'FG-03'; Child = 'SF-04'; PQ = 1; CQ = 1 }
    @{ Parent = 'FG-03'; Child = 'RM-PB'; PQ = 1; CQ = 3 }
    @{ Parent = 'FG-04'; Child = 'SF-04'; PQ = 1; CQ = 2 }
    @{ Parent = 'FG-04'; Child = 'SF-05'; PQ = 1; CQ = 1 }
    @{ Parent = 'FG-04'; Child = 'RM-PA'; PQ = 1; CQ = 1 }
    @{ Parent = 'FG-04'; Child = 'RM-PB'; PQ = 1; CQ = 1 }
    @{ Parent = 'FG-05'; Child = 'SF-05'; PQ = 1; CQ = 1 }
    @{ Parent = 'FG-05'; Child = 'SF-01'; PQ = 2; CQ = 1 }
    @{ Parent = 'FG-05'; Child = 'RM-PB'; PQ = 1; CQ = 2 }
    @{ Parent = 'SF-01'; Child = 'RM-W01'; PQ = 1; CQ = 3 }
    @{ Parent = 'SF-01'; Child = 'RM-W02'; PQ = 1; CQ = 1 }
    @{ Parent = 'SF-02'; Child = 'RM-W02'; PQ = 1; CQ = 2 }
    @{ Parent = 'SF-02'; Child = 'RM-W03'; PQ = 1; CQ = 1 }
    @{ Parent = 'SF-03'; Child = 'RM-W03'; PQ = 1; CQ = 4 }
    @{ Parent = 'SF-03'; Child = 'RM-W04'; PQ = 1; CQ = 1 }
    @{ Parent = 'SF-04'; Child = 'RM-W04'; PQ = 1; CQ = 2 }
    @{ Parent = 'SF-04'; Child = 'RM-W05'; PQ = 1; CQ = 2 }
    @{ Parent = 'SF-05'; Child = 'RM-W01'; PQ = 1; CQ = 1 }
    @{ Parent = 'SF-05'; Child = 'RM-W05'; PQ = 1; CQ = 3 }
)

$WorkCenterDefs = @(
    @{ Name = 'WC-Cut-01'; Code = '14000010' }
    @{ Name = 'WC-Cut-02'; Code = '14000010' }
    @{ Name = 'WC-Form-01'; Code = '14000020' }
    @{ Name = 'WC-Form-02'; Code = '14000020' }
    @{ Name = 'WC-Weld-01'; Code = '14000030' }
)

$ProcessTemplates3 = @(
    @{ Seq = 10; Code = '14000030'; Dist = 'OUTSOURCE'; Wc = $null }
    @{ Seq = 20; Code = '14000020'; Dist = 'OUTSOURCE'; Wc = $null }
    @{ Seq = 30; Code = '14000010'; Dist = 'INHOUSE'; Wc = 0 }
)
$ProcessTemplates2Mixed = @(
    @{ Seq = 10; Code = '14000030'; Dist = 'OUTSOURCE'; Wc = $null }
    @{ Seq = 20; Code = '14000010'; Dist = 'INHOUSE'; Wc = 1 }
)

$UserDefs = @(
    @{ Login = 'worker01'; Name = 'Prod-Op-1'; Roles = @('PRODUCTION_OPERATOR'); Group = '19000010' }
    @{ Login = 'worker02'; Name = 'Prod-Op-2'; Roles = @('PRODUCTION_OPERATOR'); Group = '19000010' }
    @{ Login = 'worker03'; Name = 'Prod-Op-3'; Roles = @('PRODUCTION_OPERATOR'); Group = '19000020' }
    @{ Login = 'worker04'; Name = 'Purch-Op-1'; Roles = @('PURCHASE_OPERATOR'); Group = '19000030' }
    @{ Login = 'worker05'; Name = 'Purch-Op-2'; Roles = @('PURCHASE_OPERATOR'); Group = '19000030' }
    @{ Login = 'worker06'; Name = 'Sales-Op-1'; Roles = @('SALES_OPERATOR'); Group = '19000040' }
    @{ Login = 'worker07'; Name = 'Sales-Op-2'; Roles = @('SALES_OPERATOR'); Group = '19000040' }
    @{ Login = 'worker08'; Name = 'Basis-Mgr'; Roles = @('BASIS_MANAGER'); Group = '19000050' }
    @{ Login = 'worker09'; Name = 'Viewer-1'; Roles = @('VIEWER'); Group = '19000060' }
    @{ Login = 'worker10'; Name = 'Viewer-2'; Roles = @('VIEWER'); Group = '19000060' }
)

function Write-SeedStep { param([string]$Message); Write-Host "`n=== $Message ===" -ForegroundColor Cyan }

function Ensure-Company {
    param($Headers, $Def, [array]$Existing)
    $found = $Existing | Where-Object { $_.businessRegNo -eq $Def.RegNo } | Select-Object -First 1
    if ($found) { return $found }
    return Invoke-RestJson -Method POST -Path '/api/v1/basis/companies' -Headers $Headers -Body @{
        companyName = $Def.Name; presidentName = 'CEO'; businessRegNo = $Def.RegNo
        businessAddress = 'Seoul Teheran-ro 100'; telephone = '02-1000-0000'; roles = $Def.Role
    }
}

function Ensure-Item {
    param($Headers, $Def, [array]$Existing)
    $found = $Existing | Where-Object { $_.itemNo -ceq $Def.No } | Select-Object -First 1
    if ($found) { return $found }
    return Invoke-RestJson -Method POST -Path '/api/v1/basis/items' -Headers $Headers -Body @{
        itemNo = $Def.No; itemName = $Def.Name; propertyClassification = $Def.Class
        unit = 'EA'; checkDistinction = 'NONE'; standardUnitCost = 1000
        leadTime = 3; safetyStockQuantity = 10; minOrderQuantity = 1
    }
}

function Ensure-WorkCenter {
    param($Headers, $Def, $ProcessCodes, [array]$Existing)
    $found = $Existing | Where-Object { $_.wcName -eq $Def.Name } | Select-Object -First 1
    if ($found) { return $found }
    $pc = $ProcessCodes | Where-Object { $_.smallCode -eq $Def.Code } | Select-Object -First 1
    if (-not $pc) { throw "Process code $($Def.Code) not found" }
    return Invoke-RestJson -Method POST -Path '/api/v1/basis/work-centers' -Headers $Headers -Body @{
        wcName = $Def.Name; mainProcessCodeId = [long]$pc.id; operationTime = 480
    }
}

function Ensure-BomLine {
    param($Headers, $Line, [array]$Existing)
    $found = $Existing | Where-Object { $_.parentItemNo -ceq $Line.Parent -and $_.childItemNo -ceq $Line.Child } | Select-Object -First 1
    if ($found) { return $found }
    return Invoke-RestJson -Method POST -Path '/api/v1/basis/item-composition/plan' -Headers $Headers -Body @{
        parentItemNum = $Line.Parent; childItemNum = $Line.Child
        parentQuantity = [decimal]$Line.PQ; childQuantity = [decimal]$Line.CQ
    }
}

function Ensure-Process {
    param($Headers, $ItemId, $Template, $ProcessCodes, $WorkCenters, [array]$Existing)
    $pc = $ProcessCodes | Where-Object { $_.smallCode -eq $Template.Code } | Select-Object -First 1
    $wcId = $null
    if ($Template.Dist -eq 'INHOUSE') { $wcId = [long]$WorkCenters[$Template.Wc].id }
    $found = $Existing | Where-Object { $_.itemId -eq $ItemId -and $_.processSequenceNum -eq $Template.Seq } | Select-Object -First 1
    if ($found) { return $found }
    return Invoke-RestJson -Method POST -Path '/api/v1/basis/processes/plan' -Headers $Headers -Body @{
        itemId = [long]$ItemId; processSequenceNum = [int]$Template.Seq; processCodeId = [long]$pc.id
        workDistinction = $Template.Dist; workCenterId = $wcId; outsideOrderRate = 0; progressRate = 33
    }
}

function Ensure-UnitPrice {
    param($Headers, $Type, $ItemNo, $CompanyId, $StdCost, $BeginPcId, $EndPcId, $OrderRate, [array]$Existing)
    $found = $Existing | Where-Object {
        ($_.itemNum -ceq $ItemNo) -and ([long]$_.companyId -eq [long]$CompanyId) `
            -and (($null -eq $BeginPcId) -or ([long]$_.beginProcessCodeId -eq [long]$BeginPcId)) `
            -and (($null -eq $EndPcId) -or ([long]$_.endProcessCodeId -eq [long]$EndPcId))
    } | Select-Object -First 1
    if ($found) { return $found }
    $body = @{ type = $Type; itemNum = $ItemNo; companyId = [long]$CompanyId; standardUnitCost = [decimal]$StdCost; beginDate = $Today }
    if ($null -ne $OrderRate) { $body.orderRate = [decimal]$OrderRate }
    if ($BeginPcId) { $body.beginProcessCodeId = [long]$BeginPcId }
    if ($EndPcId) { $body.endProcessCodeId = [long]$EndPcId }
    try {
        return Invoke-RestJson -Method POST -Path '/api/v1/basis/unit-prices' -Headers $Headers -Body $body
    } catch {
        $retry = $Existing | Where-Object {
            ($_.itemNum -ceq $ItemNo) -and ([long]$_.companyId -eq [long]$CompanyId) `
                -and (($null -eq $BeginPcId) -or ([long]$_.beginProcessCodeId -eq [long]$BeginPcId)) `
                -and (($null -eq $EndPcId) -or ([long]$_.endProcessCodeId -eq [long]$EndPcId))
        } | Select-Object -First 1
        if ($retry) { return $retry }
        if ($_.Exception.Response) {
            $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
            $msg = $reader.ReadToEnd()
            if ($msg -match 'INVALID_REQUEST') { return $null }
        }
        throw
    }
}

function Ensure-WorkStandard {
    param($Headers, $ItemNo, $ProcessId, $WcId, $WorkerId, [array]$Existing)
    $found = $Existing | Where-Object { $_.itemNum -ceq $ItemNo -and $_.processSequenceId -eq $ProcessId } | Select-Object -First 1
    if ($found) { return $found }
    return Invoke-RestJson -Method POST -Path '/api/v1/basis/work-standards/plan' -Headers $Headers -Body @{
        itemNum = $ItemNo; processSequenceId = [long]$ProcessId; workCenterId = [long]$WcId
        priorityOrder = 1; mainWorkerId = [long]$WorkerId; setupTime = 10; standardTime = 60
    }
}

function Ensure-User {
    param($Headers, $Def, $RoleMap, $DiaryGroups, [array]$Existing)
    $found = $Existing | Where-Object { $_.loginId -ceq $Def.Login } | Select-Object -First 1
    if ($found) { return $found }
    $roleIds = @($Def.Roles | ForEach-Object { [long]$RoleMap[$_] })
    $group = $DiaryGroups | Where-Object { $_.smallCode -eq $Def.Group } | Select-Object -First 1
    $body = @{ loginId = $Def.Login; password = 'Oper1234!'; name = $Def.Name; email = "$($Def.Login)@example.com"; roleIds = $roleIds }
    if ($group) { $body.workDiaryGroupId = [long]$group.id }
    return Invoke-RestJson -Method POST -Path '/api/v1/basis/users' -Headers $Headers -Body $body
}

function Count-BomLeaves { param($Node)
    if (-not $Node.children -or $Node.children.Count -eq 0) { return 1 }
    $sum = 0; foreach ($child in $Node.children) { $sum += Count-BomLeaves $child }; return $sum
}

if (-not (Test-ApiHealth)) { throw "API not reachable: $Script:E2E_BaseUrl" }

$headers = Login-E2E
$processCodes = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/public-codes?usageType=PROCESS' -Headers $headers)
$diaryGroups = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/public-codes?largeCode=1900' -Headers $headers)
$roles = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/system/roles' -Headers $headers)
$roleMap = @{}; foreach ($r in $roles) { $roleMap[$r.roleCode] = $r.id }

Write-SeedStep '1. Companies (Sales5 Purchase10 Outsource2)'
$companies = Get-E2ECompanies $headers
$salesCo = @(); $purchaseCo = @(); $outsourceCo = @()
foreach ($d in $SalesCompanies) { $salesCo += Ensure-Company $headers $d $companies }
foreach ($d in $PurchaseCompanies) { $purchaseCo += Ensure-Company $headers $d $companies }
foreach ($d in $OutsourceCompanies) { $outsourceCo += Ensure-Company $headers $d $companies }
Add-E2EPass $tracker "Companies $($salesCo.Count + $purchaseCo.Count + $outsourceCo.Count)"

Write-SeedStep '2. Items (FG5 SF5 Raw7)'
$items = Get-E2EItems $headers
$itemMap = @{}
foreach ($d in ($ProductItems + $SemiItems + $ProductRawItems + $SemiRawItems)) {
    $itemMap[$d.No] = Ensure-Item $headers $d $items
}
Add-E2EPass $tracker "Items $($itemMap.Count)"

Write-SeedStep '3. BOM'
$existingBom = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/item-composition/plan' -Headers $headers)
$bomCount = 0
foreach ($line in $BomLines) { Ensure-BomLine $headers $line $existingBom | Out-Null; $bomCount++ }
Add-E2EPass $tracker "BOM lines $bomCount"

Write-SeedStep '4. BOM explosion check'
foreach ($p in $ProductItems) {
    $tree = Invoke-RestJson -Path "/api/v1/basis/item-composition/plan/$($p.No)/explosion" -Headers $headers
    if ($tree) { Add-E2EPass $tracker "$($p.No) explosion leaf=$(Count-BomLeaves $tree)" }
    else { Add-E2EFail $tracker "$($p.No) explosion failed" }
}

Write-SeedStep '5. Work centers (5)'
$existingWc = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/work-centers' -Headers $headers)
$workCenters = @(); foreach ($d in $WorkCenterDefs) { $workCenters += Ensure-WorkCenter $headers $d $processCodes $existingWc }
Add-E2EPass $tracker "Work centers $($workCenters.Count)"

Write-SeedStep '6. Processes (2-3 per item, 2 outsource)'
$existingProc = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/processes/plan' -Headers $headers)
$procCount = 0
foreach ($p in $ProductItems) {
    foreach ($t in $ProcessTemplates3) { Ensure-Process $headers $itemMap[$p.No].id $t $processCodes $workCenters $existingProc | Out-Null; $procCount++ }
}
foreach ($sf in $SemiItems) {
    $tpl = if ($sf.No -in @('SF-04', 'SF-05')) { $ProcessTemplates2Mixed } else { $ProcessTemplates3 }
    foreach ($t in $tpl) { Ensure-Process $headers $itemMap[$sf.No].id $t $processCodes $workCenters $existingProc | Out-Null; $procCount++ }
}
Add-E2EPass $tracker "Processes $procCount"

Write-SeedStep '7. Unit prices (1 partner per item/process)'
$saleExisting = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/unit-prices?type=SALE' -Headers $headers)
$purchaseExisting = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/unit-prices?type=PURCHASE' -Headers $headers)
$outsourceExisting = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/unit-prices?type=OUTSOURCE' -Headers $headers)
$priceCount = 0; $saleBase = 50000

# SALE: each FG -> one sales partner (Sales-01..05)
$i = 0
foreach ($p in $ProductItems) {
    $co = $salesCo[$i]
    Ensure-UnitPrice $headers 'SALE' $p.No $co.id ($saleBase + $i * 1000) $null $null $null $saleExisting | Out-Null
    $priceCount++; $i++
}

# PURCHASE: each raw -> one purchase partner (Purchase-01..07)
$rawNos = @($ProductRawItems.No) + @($SemiRawItems.No); $ri = 0
foreach ($raw in $rawNos) {
    $co = $purchaseCo[$ri]
    Ensure-UnitPrice $headers 'PURCHASE' $raw $co.id (500 + $ri * 100) $null $null 100 $purchaseExisting | Out-Null
    $priceCount++; $ri++
}

# OUTSOURCE: each OUTSOURCE process -> one partner, begin=end=process code, orderRate 100%
$allProcsForPrice = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/processes/plan' -Headers $headers)
$outsourceProcIdx = 0
foreach ($proc in ($allProcsForPrice | Where-Object { $_.workDistinction -eq 'OUTSOURCE' } | Sort-Object itemNo, processSequenceNum)) {
    $pcId = [long]$proc.processCodeId
    $co = $outsourceCo[$outsourceProcIdx % $outsourceCo.Count]
    Ensure-UnitPrice $headers 'OUTSOURCE' $proc.itemNo $co.id (3000 + $outsourceProcIdx * 150) $pcId $pcId 100 $outsourceExisting | Out-Null
    $priceCount++; $outsourceProcIdx++
}
Add-E2EPass $tracker "Unit prices $priceCount"

Write-SeedStep '8. Users (10)'
$users = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/users' -Headers $headers)
$userCount = 0; foreach ($d in $UserDefs) { Ensure-User $headers $d $roleMap $diaryGroups $users | Out-Null; $userCount++ }
Add-E2EPass $tracker "Users $userCount"

Write-SeedStep '9. Work standards (INHOUSE)'
$existingWs = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/work-standards/plan' -Headers $headers)
$allProcs = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/processes/plan' -Headers $headers)
$users = Unwrap-ApiArray (Invoke-RestJson -Path '/api/v1/basis/users' -Headers $headers)
$workerId = ($users | Where-Object { $_.loginId -ceq 'worker01' } | Select-Object -First 1).id
if (-not $workerId) { $workerId = ($users | Where-Object { $_.loginId -ceq 'admin' } | Select-Object -First 1).id }
$wsCount = 0
foreach ($proc in ($allProcs | Where-Object { $_.workDistinction -eq 'INHOUSE' -and $_.workCenterId })) {
    Ensure-WorkStandard $headers $proc.itemNo $proc.id $proc.workCenterId $workerId $existingWs | Out-Null; $wsCount++
}
Add-E2EPass $tracker "Work standards $wsCount"

Write-Host "`n--- Seed summary ---" -ForegroundColor Yellow
Write-Host "Sales:$($salesCo.Count) Purchase:$($purchaseCo.Count) Outsource:$($outsourceCo.Count) Items:$($itemMap.Count) BOM:$bomCount Prices:$priceCount WS:$wsCount Users:$userCount"
Write-Host "User password: Oper1234!" -ForegroundColor DarkGray
Write-E2ESummary $tracker "$SeedTag operational basis seed"
