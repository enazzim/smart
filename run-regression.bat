@echo off
setlocal

rem Regression: truncate TX -^> seed -^> P0+P1 E2E (basis kept)
set "ROOT=%~dp0"
set "CLEAN_BAT=%ROOT%run-clean-test-db.bat"
set "SEED_BAT=%ROOT%run-seed-operational-basis.bat"
set "E2E_BAT=%ROOT%run-e2e-p1-verify.bat"

echo.
echo ========================================
echo  SmartManager regression
echo   1. clean-test-db (truncate TX)
echo   2. seed-operational-basis
echo   3. P0+P1 E2E verify
echo ========================================
echo.

if not exist "%CLEAN_BAT%" (
    echo ERROR: %CLEAN_BAT% not found.
    exit /b 1
)
if not exist "%SEED_BAT%" (
    echo ERROR: %SEED_BAT% not found.
    exit /b 1
)
if not exist "%E2E_BAT%" (
    echo ERROR: %E2E_BAT% not found.
    exit /b 1
)

call "%CLEAN_BAT%"
if errorlevel 1 (
    echo Regression stopped at clean step.
    exit /b 1
)

call "%SEED_BAT%"
if errorlevel 1 (
    echo Regression stopped at seed step.
    exit /b 1
)

call "%E2E_BAT%"
set "RC=%ERRORLEVEL%"

echo.
if "%RC%"=="0" (
    echo Regression: ALL PASS
) else (
    echo Regression: FAILED ^(exit %RC%^)
)
echo.

endlocal
exit /b %RC%

