@echo off

setlocal



rem Fresh DB + seed + P0 E2E + Part06 (no duplicate P0)

set "ROOT=%~dp0"

set "FRESH=%ROOT%run-fresh-p0-pipeline.bat"

set "PART06=%ROOT%scripts\e2e-part06-outsource-verify.ps1"



echo.

echo ========================================

echo  SmartManager FULL E2E pipeline

echo   1. fresh P0 (drop+api+seed+P0)

echo   2. Part06 outsource verify

echo ========================================

echo.



call "%FRESH%"

if errorlevel 1 (

    echo Full pipeline stopped at fresh P0 step.

    exit /b 1

)



echo.

echo ========================================

echo  Part06 outsource verify

echo ========================================

echo.



powershell -NoProfile -ExecutionPolicy Bypass -File "%PART06%"

set "RC=%ERRORLEVEL%"



echo.

if "%RC%"=="0" (

    echo Full E2E pipeline: ALL PASS

) else (

    echo Full E2E pipeline: FAILED at Part06 ^(exit %RC%^)

)

echo.



endlocal

exit /b %RC%


