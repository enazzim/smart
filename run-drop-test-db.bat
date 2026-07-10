@echo off
setlocal

rem DROP entire database (Flyway recreates on next API start)
set "ROOT=%~dp0"
set "PS1=%ROOT%scripts\clean-test-db.ps1"

if not exist "%PS1%" (
    echo ERROR: %PS1% not found.
    exit /b 1
)

echo.
echo ========================================
echo  SmartManager DROP test database
echo ========================================
echo   WARNING: all data in DB will be removed
echo   Next: restart API, then run seed/E2E
echo.

powershell -NoProfile -ExecutionPolicy Bypass -File "%PS1%" -Mode drop
set "RC=%ERRORLEVEL%"

echo.
if "%RC%"=="0" (
    echo Drop: OK
) else (
    echo Drop: FAILED ^(exit %RC%^)
)
echo.

endlocal
exit /b %RC%

