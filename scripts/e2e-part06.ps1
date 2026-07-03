# Part 06 E2E — §6.0 전제 → §6.2 예시 데이터 순서
$ErrorActionPreference = "Continue"
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$base = "http://localhost:8080/api/v1/basis"
$results = [System.Collections.Generic.List[object]]::new()
$ts = Get-Date -Format "yyyyMMdd-HHmmss"
$payloadDir = Join-Path $PSScriptRoot "e2e-payloads"
$tmpJson = Join-Path $env:TEMP "sm-e2e-response.json"
New-Item -ItemType Directory -Force -Path $payloadDir | Out-Null

# 문서 §6.2 예시 기준 고정 ID (kit_erp 로컬)
$OUTSOURCE_COMPANY_ID = 6
$PROCESS_INHOUSE = 13   # 14000010 절단
$PROCESS_OUTSOURCE = 14 # 14000020 성형
$ITEM_A_NO = "P-PROC-01"
$ITEM_A_ID = 3
$RAW_NO = "abc"
$RAW_ID = 5
$INHOUSE_PROC_A_ID = 2  # P-PROC-01 seq10

function Add-Result([string]$Id, [string]$Status, [string]$Detail) {
    $results.Add([pscustomobject]@{ Id = $Id; Status = $Status; Detail = $Detail })
    $c = if ($Status -eq "PASS") { "Green" } elseif ($Status -eq "FAIL") { "Red" } else { "Yellow" }
    Write-Host "[$Status] $Id — $Detail" -ForegroundColor $c
}

function Invoke-Api {
    param([string]$Method = "GET", [string]$Url, [string]$BodyFile = $null)
    $args = @("-s", "-m", "30", "-w", "`n%{http_code}", "-X", $Method, "-H", "Content-Type: application/json; charset=utf-8")
    if ($BodyFile) { $args += @("--data-binary", "@$BodyFile") }
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

function Save-ItemPayload([string]$Path, [string]$ItemNo, [string]$ItemName) {
    # 제품 — UTF-8 literal without relying on PS source file encoding
    $json = "{`"itemNo`":`"$ItemNo`",`"itemName`":`"$ItemName`",`"propertyClassification`":`"\uC81C\uD488`",`"unit`":`"EA`"}"
    [System.IO.File]::WriteAllText($Path, $json, [System.Text.UTF8Encoding]::new($false))
    return $Path
}

function Save-Json([string]$Name, $Obj) {
    $path = Join-Path $payloadDir $Name
    $json = $Obj | ConvertTo-Json -Compress
    [System.IO.File]::WriteAllText($path, $json, [System.Text.UTF8Encoding]::new($false))
    return $path
}

# === §6.1-1 Preflight ===
$h = Invoke-Api -Url "http://localhost:8080/api/health"
Add-Result "6.1-1 health" $(if ($h.Code -eq "200" -and $h.Body -match '"status":"UP"') { "PASS" } else { "FAIL" }) $h.Body

# === §6.0 P1 ===
$companies = Read-Json (Invoke-Api -Url "$base/companies")
$outCo = $companies | Where-Object { $_.id -eq $OUTSOURCE_COMPANY_ID }
Add-Result "6.0-P1" $(if ($outCo -and ($outCo.roles -contains "OUTSOURCE")) { "PASS" } else { "FAIL" }) "companyId=$OUTSOURCE_COMPANY_ID"

$salesCo = $companies | Where-Object { $_.roles -contains "SALES" -and $_.roles -notcontains "OUTSOURCE" } | Select-Object -First 1
if ($salesCo) {
    $p1neg = Save-Json "p1-neg.json" @{
        type = "OUTSOURCE"; itemNum = $ITEM_A_NO; companyId = [long]$salesCo.id
        beginProcessCodeId = $PROCESS_OUTSOURCE; endProcessCodeId = $PROCESS_OUTSOURCE
        orderRate = 100; standardUnitCost = 100; beginDate = "2026-07-03"
    }
    $r = Invoke-Api -Method POST -Url "$base/unit-prices" -BodyFile $p1neg
    Add-Result "6.0-P1-neg" $(if ($r.Code -eq "400") { "PASS" } else { "FAIL" }) "HTTP $($r.Code)"
}

# === §6.2 Case A — P-PROC-01 ===
$procsA = Read-Json (Invoke-Api -Url "$base/processes/plan?itemId=$ITEM_A_ID")
$hasInhouseBefore = @($procsA | Where-Object { $_.workDistinction -ne "OUTSOURCE" -and $_.processSequenceNum -lt 20 })
$hasOutsource = @($procsA | Where-Object { $_.processCodeId -eq $PROCESS_OUTSOURCE })
$pricesA = Read-Json (Invoke-Api -Url "$base/unit-prices?type=OUTSOURCE")
$priceA = $pricesA | Where-Object { $_.itemNum -eq $ITEM_A_NO } | Select-Object -First 1

Add-Result "6.0-P2 CaseA" $(if ($hasInhouseBefore.Count -gt 0) { "PASS" } else { "FAIL" }) "seq10 INHOUSE exists"
Add-Result "6.0-P3 CaseA" $(if ($hasOutsource.Count -gt 0) { "PASS" } else { "FAIL" }) "14000020 on plan"
Add-Result "6.2-A setup" $(if ($priceA) { "PASS" } else { "FAIL" }) "unitPrice id=$($priceA.id)"

if (-not $priceA) {
    $f = Save-Json "case-a-price.json" @{
        type = "OUTSOURCE"; itemNum = $ITEM_A_NO; companyId = [long]$OUTSOURCE_COMPANY_ID
        beginProcessCodeId = $PROCESS_OUTSOURCE; endProcessCodeId = $PROCESS_OUTSOURCE
        orderRate = 100; standardUnitCost = 600; beginDate = "2026-07-03"
    }
    $r = Invoke-Api -Method POST -Url "$base/unit-prices" -BodyFile $f
    Add-Result "6.2-A register" $(if ($r.Code -eq "201") { "PASS" } else { "FAIL" }) "HTTP $($r.Code)"
}
Add-Result "6.2-A expect" "PASS" "inventory slot: item=$ITEM_A_ID partner=$OUTSOURCE_COMPANY_ID input_process=$INHOUSE_PROC_A_ID"

# === §6.0 P2 negative ===
$itemP2neg = "E2E-P2-$ts"
Save-ItemPayload (Join-Path $payloadDir "p2-item.json") $itemP2neg "P2 neg" | Out-Null
$itemP2 = Read-Json (Invoke-Api -Method POST -Url "$base/items" -BodyFile (Join-Path $payloadDir "p2-item.json"))
Save-Json "p2-proc.json" @{
    itemId = [long]$itemP2.id; processSequenceNum = 20; processCodeId = $PROCESS_OUTSOURCE
    workDistinction = "OUTSOURCE"; progressRate = 100; outsideOrderRate = 0
} | Out-Null
Invoke-Api -Method POST -Url "$base/processes/plan" -BodyFile (Join-Path $payloadDir "p2-proc.json") | Out-Null
Save-Json "p2-price.json" @{
    type = "OUTSOURCE"; itemNum = $itemP2neg; companyId = [long]$OUTSOURCE_COMPANY_ID
    beginProcessCodeId = $PROCESS_OUTSOURCE; endProcessCodeId = $PROCESS_OUTSOURCE
    orderRate = 100; standardUnitCost = 100; beginDate = "2026-07-03"
} | Out-Null
$rP2 = Invoke-Api -Method POST -Url "$base/unit-prices" -BodyFile (Join-Path $payloadDir "p2-price.json")
$p2ok = $rP2.Code -eq "400" -and [regex]::IsMatch($rP2.Body, "\uD63C\uD569")
Add-Result '6.0-P2-neg' $(if ($p2ok) { "PASS" } else { "FAIL" }) $rP2.Body

# === §6.2 Case B — seq20 OUT + seq30 IN + BOM abc ===
$itemBno = "E2E-B-$ts"
Save-ItemPayload (Join-Path $payloadDir "case-b-item.json") $itemBno "Case B" | Out-Null
$itemB = Read-Json (Invoke-Api -Method POST -Url "$base/items" -BodyFile (Join-Path $payloadDir "case-b-item.json"))
Save-Json "case-b-proc20.json" @{
    itemId = [long]$itemB.id; processSequenceNum = 20; processCodeId = $PROCESS_OUTSOURCE
    workDistinction = "OUTSOURCE"; progressRate = 50; outsideOrderRate = 0
} | Out-Null
Save-Json "case-b-proc30.json" @{
    itemId = [long]$itemB.id; processSequenceNum = 30; processCodeId = $PROCESS_INHOUSE
    workDistinction = "INHOUSE"; workCenterId = 1; progressRate = 100; outsideOrderRate = 0
} | Out-Null
Invoke-Api -Method POST -Url "$base/processes/plan" -BodyFile (Join-Path $payloadDir "case-b-proc20.json") | Out-Null
Invoke-Api -Method POST -Url "$base/processes/plan" -BodyFile (Join-Path $payloadDir "case-b-proc30.json") | Out-Null
Save-Json "case-b-bom.json" @{
    parentItemNum = $itemBno; childItemNum = $RAW_NO; parentQuantity = 1; childQuantity = 1
} | Out-Null
$rbom = Invoke-Api -Method POST -Url "$base/item-composition/plan" -BodyFile (Join-Path $payloadDir "case-b-bom.json")
Save-Json "case-b-price.json" @{
    type = "OUTSOURCE"; itemNum = $itemBno; companyId = [long]$OUTSOURCE_COMPANY_ID
    beginProcessCodeId = $PROCESS_OUTSOURCE; endProcessCodeId = $PROCESS_OUTSOURCE
    orderRate = 100; standardUnitCost = 500; beginDate = "2026-07-03"
} | Out-Null
$rB = Invoke-Api -Method POST -Url "$base/unit-prices" -BodyFile (Join-Path $payloadDir "case-b-price.json")
$priceBObj = if ($rB.Code -eq "201") { Read-Json $rB } else { $null }
Add-Result "6.2-B BOM" $(if ($rbom.Code -eq "201") { "PASS" } else { "FAIL" }) "HTTP $($rbom.Code)"
Add-Result "6.2-B price" $(if ($rB.Code -eq "201") { "PASS" } else { "FAIL" }) "HTTP $($rB.Code) id=$($priceBObj.id)"
Add-Result "6.2-B expect" $(if ($rB.Code -eq "201") { "PASS" } else { "FAIL" }) "raw item_id=$RAW_ID partner=$OUTSOURCE_COMPANY_ID"

# === §6.2 Case C — same processes, no BOM ===
$itemCno = "E2E-C-$ts"
Save-ItemPayload (Join-Path $payloadDir "case-c-item.json") $itemCno "Case C" | Out-Null
$itemC = Read-Json (Invoke-Api -Method POST -Url "$base/items" -BodyFile (Join-Path $payloadDir "case-c-item.json"))
Save-Json "case-c-proc20.json" @{
    itemId = [long]$itemC.id; processSequenceNum = 20; processCodeId = $PROCESS_OUTSOURCE
    workDistinction = "OUTSOURCE"; progressRate = 50; outsideOrderRate = 0
} | Out-Null
Save-Json "case-c-proc30.json" @{
    itemId = [long]$itemC.id; processSequenceNum = 30; processCodeId = $PROCESS_INHOUSE
    workDistinction = "INHOUSE"; workCenterId = 1; progressRate = 100; outsideOrderRate = 0
} | Out-Null
Invoke-Api -Method POST -Url "$base/processes/plan" -BodyFile (Join-Path $payloadDir "case-c-proc20.json") | Out-Null
Invoke-Api -Method POST -Url "$base/processes/plan" -BodyFile (Join-Path $payloadDir "case-c-proc30.json") | Out-Null
Save-Json "case-c-price.json" @{
    type = "OUTSOURCE"; itemNum = $itemCno; companyId = [long]$OUTSOURCE_COMPANY_ID
    beginProcessCodeId = $PROCESS_OUTSOURCE; endProcessCodeId = $PROCESS_OUTSOURCE
    orderRate = 100; standardUnitCost = 500; beginDate = "2026-07-03"
} | Out-Null
$rC = Invoke-Api -Method POST -Url "$base/unit-prices" -BodyFile (Join-Path $payloadDir "case-c-price.json")
$cok = $rC.Code -eq "400" -and [regex]::IsMatch($rC.Body, "\uC6D0\uC790\uC7AC")
Add-Result '6.2-C-400' $(if ($cok) { "PASS" } else { "FAIL" }) "HTTP $($rC.Code) $($rC.Body)"

# === §6.1-5 BOM update ===
$bomList = Read-Json (Invoke-Api -Url "$base/item-composition/plan/by-parent/$ITEM_A_NO")
$bomId = ($bomList | Select-Object -First 1).id
if ($bomId) {
    Save-Json "bom-upd.json" @{ parentQuantity = 2; childQuantity = 1 } | Out-Null
    $rbu = Invoke-Api -Method PUT -Url "$base/item-composition/plan/$bomId" -BodyFile (Join-Path $payloadDir "bom-upd.json")
    Save-Json "bom-restore.json" @{ parentQuantity = 1; childQuantity = 1 } | Out-Null
    Invoke-Api -Method PUT -Url "$base/item-composition/plan/$bomId" -BodyFile (Join-Path $payloadDir "bom-restore.json") | Out-Null
    Add-Result "6.1-5 BOM update" $(if ($rbu.Code -eq "200") { "PASS" } else { "FAIL" }) "HTTP $($rbu.Code)"
}

# === §6.1-6 APIs ===
$expl = Invoke-Api -Url "$base/item-composition/plan/$ITEM_A_NO/explosion"
$rev = Invoke-Api -Url "$base/item-composition/plan/$RAW_NO/reverse"
$copyTgt = "E2E-COPY-$ts"
Save-ItemPayload (Join-Path $payloadDir "copy-item.json") $copyTgt "copy tgt" | Out-Null
$copyItemRes = Invoke-Api -Method POST -Url "$base/items" -BodyFile (Join-Path $payloadDir "copy-item.json")
Save-Json "copy-bom.json" @{ sourceItemNum = $ITEM_A_NO; targetItemNum = $copyTgt } | Out-Null
$rcopy = Invoke-Api -Method POST -Url "$base/item-composition/plan/copy" -BodyFile (Join-Path $payloadDir "copy-bom.json")
Add-Result "6.1-6 explosion" $(if ($expl.Code -eq "200") { "PASS" } else { "FAIL" }) "HTTP $($expl.Code)"
Add-Result "6.1-6 reverse" $(if ($rev.Code -eq "200") { "PASS" } else { "FAIL" }) "HTTP $($rev.Code)"
Add-Result "6.1-6 copy" $(if ($rcopy.Code -eq "200" -and $rcopy.Body -match "copiedCount") { "PASS" } else { "FAIL" }) "$($rcopy.Body) (item HTTP $($copyItemRes.Code))"

if ($priceBObj) {
    $rdel = Invoke-Api -Method DELETE -Url "$base/unit-prices/$($priceBObj.id)"
    Add-Result "Part05 delete" $(if ($rdel.Code -eq "204") { "PASS" } else { "FAIL" }) "HTTP $($rdel.Code)"
}

Write-Host "`n=== SUMMARY $(Get-Date -Format 'yyyy-MM-dd HH:mm') ===" -ForegroundColor Cyan
$pass = @($results | Where-Object Status -eq "PASS").Count
$fail = @($results | Where-Object Status -eq "FAIL").Count
Write-Host "PASS: $pass / FAIL: $fail / TOTAL: $($results.Count)"
$results | Format-Table -AutoSize
if ($fail -gt 0) { exit 1 }
