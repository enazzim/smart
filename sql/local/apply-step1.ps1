# Apply Step 1 SQL to local kit_erp
param(
  [string]$Mysql = 'C:\Program Files\MariaDB 10.11\bin\mysql.exe',
  [string]$User = 'root',
  [string]$Password = '1111',
  [string]$Database = 'kit_erp'
)

$root = Split-Path (Split-Path $PSScriptRoot -Parent) -Parent
$files = @(
  (Join-Path $root 'sql\step1\V001__smartmanager_core.sql'),
  (Join-Path $root 'sql\step1\V002__seed_code_group_public_code.sql')
)

foreach ($f in $files) {
  if (-not (Test-Path $f)) { throw "Missing: $f" }
  Write-Host "Applying $f ..."
  cmd /c "`"$Mysql`" -u $User -p$Password --force $Database < `"$f`""
  if ($LASTEXITCODE -ne 0) { throw "Failed: $f" }
}

Write-Host "Done. Verify:"
& $Mysql -u $User -p$Password $Database -e @"
SELECT 'code_group' AS tbl, COUNT(*) AS cnt FROM code_group
UNION ALL SELECT 'public_code', COUNT(*) FROM public_code
UNION ALL SELECT 'company', COUNT(*) FROM company;
"@
