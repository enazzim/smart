@echo off
setlocal

rem Full fresh: stop API -^> DROP DB -^> restart API -^> wait health -^> P0 pipeline
set "ROOT=%~dp0"
set "STOP_BAT=%ROOT%smartmanager_backend\stop-api.bat"
set "RESTART_BAT=%ROOT%restart-api.bat"
set "PIPELINE_BAT=%ROOT%run-p0-pipeline.bat"
set "DROP_PS1=%ROOT%scripts\clean-test-db.ps1"
set "WAIT_PS1=%ROOT%scripts\wait-api-health.ps1"

echo.
echo ========================================
echo  SmartManager FRESH P0 pipeline
echo   1. stop API
echo   2. DROP database
echo   3. restart API (new window)
echo   4. wait /api/health
echo   5. seed + E2E 1-3
echo ========================================
echo.

if not exist "%DROP_PS1%" (
    echo ERROR: %DROP_PS1% not found.
    exit /b 1
)
if not exist "%WAIT_PS1%" (
    echo ERROR: %WAIT_PS1% not found.
    exit /b 1
)
if not exist "%PIPELINE_BAT%" (
    echo ERROR: %PIPELINE_BAT% not found.
    exit /b 1
)

echo [1/5] Stopping API...
if exist "%STOP_BAT%" (
    call "%STOP_BAT%"
) else (
    echo   stop-api.bat not found, skipping
)
echo.

echo [2/5] Dropping database...
powershell -NoProfile -ExecutionPolicy Bypass -File "%DROP_PS1%" -Mode drop
if errorlevel 1 (
    echo Fresh pipeline stopped at DROP step.
    exit /b 1
)
echo.

echo [3/5] Restarting API...
if not exist "%RESTART_BAT%" (
    echo ERROR: %RESTART_BAT% not found.
    exit /b 1
)
call "%RESTART_BAT%"
if errorlevel 1 (
    echo Fresh pipeline stopped at API restart step.
    exit /b 1
)
echo.

echo [4/5] Waiting for API health...
powershell -NoProfile -ExecutionPolicy Bypass -File "%WAIT_PS1%"
if errorlevel 1 (
    echo Fresh pipeline stopped: API did not become healthy.
    exit /b 1
)
echo.

echo [5/5] Running P0 pipeline (seed + E2E)...
call "%PIPELINE_BAT%"
set "RC=%ERRORLEVEL%"

echo.
if "%RC%"=="0" (
    echo Fresh P0 pipeline: ALL PASS
) else (
    echo Fresh P0 pipeline: FAILED ^(exit %RC%^)
)
echo.

endlocal
exit /b %RC%

