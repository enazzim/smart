@echo off
setlocal

rem Truncate TX tables only (keep basis master data)
set "ROOT=%~dp0"
set "PS1=%ROOT%scripts\clean-test-db.ps1"

if not exist "%PS1%" (
    echo ERROR: %PS1% not found.
    exit /b 1
)

echo.
echo ========================================
echo  SmartManager clean test DB (TX only)
echo ========================================
echo   Mode: truncate (basis kept)
echo   SQL:  scripts\clean-test-db-except-basis.sql
echo.

powershell -NoProfile -ExecutionPolicy Bypass -File "%PS1%" -Mode truncate
set "RC=%ERRORLEVEL%"

echo.
if "%RC%"=="0" (
    echo Clean: OK
) else (
    echo Clean: FAILED ^(exit %RC%^)
)
echo.

endlocal
exit /b %RC%

