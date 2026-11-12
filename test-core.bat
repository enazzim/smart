@echo off
setlocal

rem ============================================================
rem  Smart-Manager — core unit tests (local)
rem  Inventory / closing / purchase status tagged @Tag("core")
rem  Usage: test-core.bat
rem ============================================================

set "ROOT=%~dp0"
set "BACKEND_DIR=%ROOT%smartmanager_backend"
set "FRONTEND_DIR=%ROOT%smartmanager_frontend"

if not exist "%BACKEND_DIR%\gradlew.bat" (
    echo ERROR: smartmanager_backend\gradlew.bat not found.
    exit /b 1
)

echo.
echo ========================================
echo  Smart-Manager core tests
echo ========================================
echo.

echo [1/2] Backend core ^(JUnit @Tag core^)...
cd /d "%BACKEND_DIR%"
call gradlew.bat test -PcoreTests=true --no-daemon
if errorlevel 1 (
    echo.
    echo BACKEND CORE TESTS FAILED.
    exit /b 1
)

echo.
echo [2/2] Frontend utils ^(vitest^)...
cd /d "%FRONTEND_DIR%"
call npm test
if errorlevel 1 (
    echo.
    echo FRONTEND TESTS FAILED.
    exit /b 1
)

echo.
echo CORE TESTS OK.
exit /b 0
