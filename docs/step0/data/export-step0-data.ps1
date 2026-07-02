# Step 0 Tier B2/B3 export
# Usage:
#   $env:SMARTMANAGER_ERP_DSN = "Server=...;Database=Sindong_ERP;User Id=...;Password=...;TrustServerCertificate=True"
#   .\export-step0-data.ps1
# Without DSN: writes *-from-code-audit.csv / *-draft.csv from static mapping.

param(
  [string]$OutDir = $PSScriptRoot
)

function Write-Utf8Csv {
  param([string]$Path, [object[]]$Rows)
  $cols = ($Rows[0].PSObject.Properties | ForEach-Object Name)
  $sb = New-Object System.Text.StringBuilder
  [void]$sb.AppendLine(($cols -join ','))
  foreach ($r in $Rows) {
    $line = ($cols | ForEach-Object {
      $v = [string]$r.$_
      if ($v -match '[,"\r\n]') { '"' + ($v -replace '"','""') + '"' } else { $v }
    }) -join ','
    [void]$sb.AppendLine($line)
  }
  [System.IO.File]::WriteAllText($Path, $sb.ToString().TrimEnd(), [System.Text.UTF8Encoding]::new($false))
}

function Export-PucFromCodeAudit {
  $rows = @(
    [pscustomobject]@{ large_code='0110'; large_name='거래처분류1'; source='hypothesis'; confidence='low'; code_group_key='COMPANY_TRADE_CLASS_1'; usage_type='GENERIC'; note='LargeClassificationName 조회 — DB 검증 필수' }
    [pscustomobject]@{ large_code='0120'; large_name='거래처분류2'; source='hypothesis'; confidence='low'; code_group_key='COMPANY_TRADE_CLASS_2'; usage_type='GENERIC'; note='동일' }
    [pscustomobject]@{ large_code='0130'; large_name='거래처분류3'; source='hypothesis'; confidence='low'; code_group_key='COMPANY_TRADE_CLASS_3'; usage_type='GENERIC'; note='동일' }
    [pscustomobject]@{ large_code='0210'; large_name='품목분류1'; source='code-audit'; confidence='high'; code_group_key='ITEM_CLASSIFICATION_1'; usage_type='GENERIC'; note='ItemInfo.aspx.cs' }
    [pscustomobject]@{ large_code='0220'; large_name='품목분류2'; source='code-audit'; confidence='high'; code_group_key='ITEM_CLASSIFICATION_2'; usage_type='GENERIC'; note='' }
    [pscustomobject]@{ large_code='0230'; large_name='품목분류3'; source='code-audit'; confidence='high'; code_group_key='ITEM_CLASSIFICATION_3'; usage_type='GENERIC'; note='' }
    [pscustomobject]@{ large_code='0240'; large_name='품목분류4'; source='code-audit'; confidence='high'; code_group_key='ITEM_CLASSIFICATION_4'; usage_type='GENERIC'; note='' }
    [pscustomobject]@{ large_code='0330'; large_name='구매의뢰원천'; source='code-audit'; confidence='high'; code_group_key=''; usage_type='GENERIC'; note='Phase2' }
    [pscustomobject]@{ large_code='0400'; large_name='단위'; source='code-audit'; confidence='high'; code_group_key='UNIT_GENERAL'; usage_type='UNIT'; note='' }
    [pscustomobject]@{ large_code='0410'; large_name='규격단위1'; source='code-audit'; confidence='high'; code_group_key='UNIT_SPEC_1'; usage_type='UNIT'; note='' }
    [pscustomobject]@{ large_code='0420'; large_name='규격단위2'; source='code-audit'; confidence='high'; code_group_key='UNIT_SPEC_2'; usage_type='UNIT'; note='' }
    [pscustomobject]@{ large_code='0430'; large_name='규격단위3'; source='code-audit'; confidence='high'; code_group_key='UNIT_SPEC_3'; usage_type='UNIT'; note='' }
    [pscustomobject]@{ large_code='0440'; large_name='규격단위4'; source='code-audit'; confidence='medium'; code_group_key=''; usage_type='UNIT'; note='ItemInfo 주석' }
    [pscustomobject]@{ large_code='0510'; large_name='재고단위'; source='code-audit'; confidence='high'; code_group_key='UNIT_STOCK'; usage_type='UNIT'; note='' }
    [pscustomobject]@{ large_code='0520'; large_name='BOM단위'; source='code-audit'; confidence='high'; code_group_key='UNIT_BOM'; usage_type='UNIT'; note='' }
    [pscustomobject]@{ large_code='0530'; large_name='구매단위'; source='code-audit'; confidence='high'; code_group_key='UNIT_PURCHASE'; usage_type='UNIT'; note='' }
    [pscustomobject]@{ large_code='0540'; large_name='판매단위'; source='code-audit'; confidence='high'; code_group_key='UNIT_SALE'; usage_type='UNIT'; note='' }
    [pscustomobject]@{ large_code='0610'; large_name='품목타입'; source='code-audit'; confidence='high'; code_group_key='ITEM_TYPE'; usage_type='GENERIC'; note='' }
    [pscustomobject]@{ large_code='0620'; large_name='재질'; source='code-audit'; confidence='high'; code_group_key='ITEM_MATERIAL'; usage_type='GENERIC'; note='' }
    [pscustomobject]@{ large_code='0630'; large_name='품목상태'; source='code-audit'; confidence='high'; code_group_key='ITEM_STATE'; usage_type='GENERIC'; note='exclude 06300010' }
    [pscustomobject]@{ large_code='0640'; large_name='조달구분'; source='code-audit'; confidence='high'; code_group_key='BOM_SUPPLY_DIVISION'; usage_type='GENERIC'; note='' }
    [pscustomobject]@{ large_code='0720'; large_name='설비분류'; source='code-audit'; confidence='high'; code_group_key='EQUIPMENT_CLASS'; usage_type='GENERIC'; note='exclude 07200010,07200020' }
    [pscustomobject]@{ large_code='0800'; large_name='설비위치'; source='code-audit'; confidence='high'; code_group_key='EQUIPMENT_LOCATION'; usage_type='GENERIC'; note='d4-equipment Drop' }
    [pscustomobject]@{ large_code='0910'; large_name='지급계획'; source='code-audit'; confidence='high'; code_group_key='PAYMENT_PLAN_TYPE'; usage_type='GENERIC'; note='' }
    [pscustomobject]@{ large_code='0920'; large_name='지급실적'; source='code-audit'; confidence='high'; code_group_key='PAYMENT_RESULT_TYPE'; usage_type='GENERIC'; note='' }
    [pscustomobject]@{ large_code='1000'; large_name='부적합현상'; source='code-audit'; confidence='medium'; code_group_key='QC_DEFECT_PHENOMENON'; usage_type='NC_DETAIL'; note='PageLoad_GetDataSource' }
    [pscustomobject]@{ large_code='1010'; large_name='부적합원인'; source='code-audit'; confidence='high'; code_group_key='QC_DEFECT_CAUSE'; usage_type='NC_REASON'; note='' }
    [pscustomobject]@{ large_code='1210'; large_name='비작업사유'; source='code-audit'; confidence='high'; code_group_key=''; usage_type='GENERIC'; note='Phase2' }
    [pscustomobject]@{ large_code='1310'; large_name='검사판정'; source='code-audit'; confidence='high'; code_group_key='QC_INSPECTION_DECISION'; usage_type='GENERIC'; note='' }
    [pscustomobject]@{ large_code='1400'; large_name='공정명'; source='code-audit'; confidence='high'; code_group_key='PROCESS_CODE'; usage_type='PROCESS'; note='exclude 14000000,14009999' }
    [pscustomobject]@{ large_code='1500'; large_name='입출고사유'; source='code-audit'; confidence='high'; code_group_key=''; usage_type='GENERIC'; note='Phase2' }
    [pscustomobject]@{ large_code='1700'; large_name='보용품입고사유'; source='code-audit'; confidence='high'; code_group_key=''; usage_type='GENERIC'; note='Phase2' }
    [pscustomobject]@{ large_code='1800'; large_name='기타구매'; source='code-audit'; confidence='high'; code_group_key=''; usage_type='GENERIC'; note='Phase2' }
    [pscustomobject]@{ large_code='1900'; large_name='업무일지그룹'; source='to-be-new'; confidence='high'; code_group_key='WORK_DIARY_GROUP'; usage_type='WORK_DIARY_GROUP'; note='레거시 UserInfo.aspx 00~16 그룹 → Step1 시드' }
  )
  $path = Join-Path $OutDir 'puc-large-distinct-from-code-audit.csv'
  Write-Utf8Csv -Path $path -Rows $rows
  Copy-Item $path (Join-Path $OutDir 'puc-large-distinct.csv') -Force
  return $path
}

function Export-CtTBasisMenuDraft {
  $rows = @(
    [pscustomobject]@{ Num=0; Title='공용코드'; EngTitle='B12_PublicUseCode'; LegacyPage='SystemInfoManagement/PublicUseCode.aspx'; source='step0-plan-D2'; note='선행' }
    [pscustomobject]@{ Num=1; Title='거래처'; EngTitle='B01_CompanyInfo'; LegacyPage='BasisInformation/CompanyInfo.aspx'; source='step0-plan-D2'; note='' }
    [pscustomobject]@{ Num=2; Title='품목'; EngTitle='B02_ItemInfo'; LegacyPage='BasisInformation/ItemInfo.aspx'; source='step0-plan-D2'; note='' }
    [pscustomobject]@{ Num=3; Title='품목구성'; EngTitle='B03_ItemOrganizationInfo'; LegacyPage='BasisInformation/ItemOrganizationInfo.aspx'; source='step0-plan-D2'; note='' }
    [pscustomobject]@{ Num=4; Title='작업장'; EngTitle='B04_WCInfo'; LegacyPage='BasisInformation/WCInfo.aspx'; source='step0-plan-D2'; note='' }
    [pscustomobject]@{ Num=5; Title='공정'; EngTitle='B05_ProcessSequence'; LegacyPage='BasisInformation/ProcessSequenceInfo.aspx'; source='step0-plan-D2'; note='' }
    [pscustomobject]@{ Num=6; Title='설비'; EngTitle='B06_EquipmentInfo'; LegacyPage='BasisInformation/EquipmentInfo.aspx'; source='step0-plan-D2'; note='' }
    [pscustomobject]@{ Num=7; Title='작업표준'; EngTitle='B07_WorkStandardInfo'; LegacyPage='BasisInformation/WorkStandardInfo.aspx'; source='step0-plan-D2'; note='' }
    [pscustomobject]@{ Num=8; Title='판매단가'; EngTitle='B08_SaleUnitCodeInfo'; LegacyPage='BasisInformation/SaleUnitCodeInfo.aspx'; source='step0-plan-D2'; note='단가 8a' }
    [pscustomobject]@{ Num=9; Title='구매단가'; EngTitle='B10_BuyingUnitCodeInfo'; LegacyPage='BasisInformation/BuyingUnitCodeInfo.aspx'; source='step0-plan-D2'; note='단가 8b' }
    [pscustomobject]@{ Num=10; Title='외주단가'; EngTitle='B09_OutSideOrderUnitCodeInfo'; LegacyPage='BasisInformation/OutSideOrderUnitCodeInfo.aspx'; source='step0-plan-D2'; note='단가 8c' }
    [pscustomobject]@{ Num=11; Title='기본생산달력'; EngTitle='B13_StandardProductionCalendarInfo'; LegacyPage='BasisInformation/StandardProductionCalendarInfo.aspx'; source='step0-plan-D2'; note='' }
    [pscustomobject]@{ Num=12; Title='WC생산달력'; EngTitle='B14_WCProductionCalendarInf0'; LegacyPage='BasisInformation/WCProductionCalendarInfo.aspx'; source='step0-plan-D2'; note='Base.cs 오타 Inf0' }
    [pscustomobject]@{ Num=13; Title='사용자'; EngTitle='B11_UserInfo'; LegacyPage='BasisInformation/UserInfo.aspx'; source='step0-plan-D2'; note='' }
    [pscustomobject]@{ Num=90; Title='진품목구성'; EngTitle=''; LegacyPage='BasisInformation/RealItemOrganizationInfo.aspx'; source='step0-plan-D2'; note='Real Num TBD' }
    [pscustomobject]@{ Num=91; Title='진공정'; EngTitle=''; LegacyPage='BasisInformation/RealProcessSequenceInfo.aspx'; source='step0-plan-D2'; note='Real Num TBD' }
    [pscustomobject]@{ Num=92; Title='진작업표준'; EngTitle=''; LegacyPage='BasisInformation/RealWorkStandardInfo.aspx'; source='step0-plan-D2'; note='Real Num TBD' }
  )
  $path = Join-Path $OutDir 'ct-t-basis-menu-draft.csv'
  Write-Utf8Csv -Path $path -Rows $rows
  Copy-Item $path (Join-Path $OutDir 'ct-t-basis-menu.csv') -Force
  return $path
}

function Export-PucFromDatabase {
  param([string]$ConnectionString)
  $conn = New-Object System.Data.SqlClient.SqlConnection($ConnectionString)
  $conn.Open()
  try {
    $cmd = $conn.CreateCommand()
    $cmd.CommandText = "SELECT DISTINCT LargeClassificationCode, LargeClassificationName FROM PUC_MT WHERE RecodingState = 1 ORDER BY LargeClassificationCode"
    $r1 = New-Object System.Data.DataTable
    [void](New-Object System.Data.SqlClient.SqlDataAdapter $cmd).Fill($r1)
    $pucPath = Join-Path $OutDir 'puc-large-distinct-from-db.csv'
    $lines = @('large_code,large_name,source')
    foreach ($row in $r1.Rows) { $lines += "$($row[0]),$($row[1]),db-export" }
    [System.IO.File]::WriteAllLines($pucPath, $lines, [System.Text.UTF8Encoding]::new($false))
    Copy-Item $pucPath (Join-Path $OutDir 'puc-large-distinct.csv') -Force

    $cmd.CommandText = "SELECT Num, Title, EngTitle, [Group] FROM CT_T WHERE [Group] = N'기준정보' ORDER BY Num"
    $r2 = New-Object System.Data.DataTable
    [void](New-Object System.Data.SqlClient.SqlDataAdapter $cmd).Fill($r2)
    $ctPath = Join-Path $OutDir 'ct-t-basis-menu-from-db.csv'
    $lines2 = @('Num,Title,EngTitle,Group,source')
    foreach ($row in $r2.Rows) { $lines2 += "$($row[0]),$($row[1]),$($row[2]),$($row[3]),db-export" }
    [System.IO.File]::WriteAllLines($ctPath, $lines2, [System.Text.UTF8Encoding]::new($false))
    Copy-Item $ctPath (Join-Path $OutDir 'ct-t-basis-menu.csv') -Force
    Write-Host "DB export OK"
  } finally { $conn.Close() }
}

if ($env:SMARTMANAGER_ERP_DSN) {
  Export-PucFromDatabase -ConnectionString $env:SMARTMANAGER_ERP_DSN
} else {
  Export-PucFromCodeAudit | Out-Null
  Export-CtTBasisMenuDraft | Out-Null
  Write-Host "Interim export (no DSN). Set SMARTMANAGER_ERP_DSN for live DB."
}
