@echo off
setlocal enabledelayedexpansion

rem Stop SmartManager API server listening on port 8080
set "PORT=8080"

echo Stopping process on port %PORT%...
set "FOUND=0"
for /f "tokens=5" %%p in ('netstat -ano ^| findstr ":%PORT%" ^| findstr "LISTENING"') do (
    if not "%%p"=="0" (
        echo   - taskkill /PID %%p /F
        taskkill /PID %%p /F >nul 2>&1
        set "FOUND=1"
    )
)

if "!FOUND!"=="0" (
    echo No listening process on port %PORT%.
) else (
    echo Server stopped.
)

endlocal
