@echo off
setlocal

rem P0 E2E 1-3 sequential
set "ROOT=%~dp0"
set "PS1=%ROOT%scripts\e2e-p0-verify.ps1"

if not exist "%PS1%" (
    echo ERROR: %PS1% not found.
    exit /b 1
)

echo.
echo ========================================
echo  SmartManager P0 E2E verify (1-3)
echo ========================================
echo   Script: %PS1%
echo   API:    http://localhost:8080
echo.

powershell -NoProfile -ExecutionPolicy Bypass -File "%PS1%"
set "RC=%ERRORLEVEL%"

echo.
if "%RC%"=="0" (
    echo P0 E2E: ALL PASS
) else (
    echo P0 E2E: FAILED ^(exit %RC%^)
)
echo.

endlocal
exit /b %RC%

