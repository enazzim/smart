@echo off
setlocal

rem Step1 seed then Step2 E2E 1-3
set "ROOT=%~dp0"

echo.
echo ========================================
echo  SmartManager P0 pipeline
echo   1. seed-operational-basis
echo   2. e2e-full-tx-chain
echo   3. e2e-payable-approval
echo   4. e2e-month-closing
echo ========================================
echo.

call "%ROOT%run-seed-operational-basis.bat"
if errorlevel 1 (
    echo Pipeline stopped at seed step.
    exit /b 1
)

call "%ROOT%run-e2e-p0-verify.bat"
set "RC=%ERRORLEVEL%"

echo.
if "%RC%"=="0" (
    echo Pipeline: ALL PASS
) else (
    echo Pipeline: FAILED at E2E step ^(exit %RC%^)
)
echo.

endlocal
exit /b %RC%

