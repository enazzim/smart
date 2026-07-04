@echo off
setlocal enabledelayedexpansion

rem SmartManager API: stop existing server on 8080, rebuild, restart
set "ROOT=%~dp0"
set "BACKEND_DIR=%ROOT%smartmanager_backend"
set "PORT=8080"
set "WINDOW_TITLE=SmartManager API"

if not exist "%BACKEND_DIR%\gradlew.bat" (
    echo ERROR: smartmanager_backend folder not found.
    echo   Expected: %BACKEND_DIR%
    exit /b 1
)

echo.
echo ========================================
echo  SmartManager API restart
echo ========================================
echo   Backend: %BACKEND_DIR%
echo.

echo [1/3] Stopping process on port %PORT%...
set "ATTEMPT=0"
:kill_port
set "FOUND=0"
for /f "tokens=5" %%p in ('netstat -ano ^| findstr ":%PORT%" ^| findstr "LISTENING"') do (
    if not "%%p"=="0" (
        echo   - taskkill /PID %%p /F
        taskkill /PID %%p /F >nul 2>&1
        set "FOUND=1"
    )
)
if "!FOUND!"=="1" (
    set /a ATTEMPT+=1
    if !ATTEMPT! GEQ 5 (
        echo   - ERROR: Port %PORT% is still in use. Close other bootRun windows and retry.
        exit /b 1
    )
    echo   - Waiting for port release...
    timeout /t 2 /nobreak >nul
    goto kill_port
) else (
    echo   - Port %PORT% is free
)

echo.
echo [2/3] Building backend...
cd /d "%BACKEND_DIR%"
call gradlew.bat compileJava --no-daemon
if errorlevel 1 (
    echo.
    echo BUILD FAILED. Server was not restarted.
    exit /b 1
)

echo.
echo [3/3] Starting API server (bootRun)...
start "%WINDOW_TITLE%" cmd /k "cd /d ""%BACKEND_DIR%"" && gradlew.bat :smartmanager-api:bootRun"

echo.
echo Done. Check the "%WINDOW_TITLE%" window for startup logs.
echo Health: http://localhost:%PORT%/api/health
echo.

endlocal
