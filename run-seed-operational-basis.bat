@echo off
setlocal

rem Operational basis seed
set "ROOT=%~dp0"
set "PS1=%ROOT%scripts\seed-operational-basis.ps1"

if not exist "%PS1%" (
    echo ERROR: %PS1% not found.
    exit /b 1
)

echo.
echo ========================================
echo  SmartManager operational basis seed
echo ========================================
echo   Script: %PS1%
echo   API:    http://localhost:8080
echo.

powershell -NoProfile -ExecutionPolicy Bypass -File "%PS1%"
set "RC=%ERRORLEVEL%"

echo.
if "%RC%"=="0" (
    echo Seed: OK
) else (
    echo Seed: FAILED ^(exit %RC%^)
)
echo.

endlocal
exit /b %RC%

