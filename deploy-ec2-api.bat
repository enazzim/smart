@echo off
setlocal enabledelayedexpansion

rem ============================================================
rem  Smart-Manager EC2 — Backend(API) only
rem  Usage: deploy-ec2-api.bat [--skip-build]
rem  - Builds bootJar (unless --skip-build), uploads jar, restarts API
rem ============================================================

set "ROOT=%~dp0"
set "EC2_HOST=3.36.75.21"
set "EC2_USER=ec2-user"
set "PEM_KEY=%ROOT%sindong-key.pem"
set "BACKEND_DIR=%ROOT%smartmanager_backend"
set "REMOTE_TMP=/tmp/smartmanager-deploy-api"
set "APPLY_API_SH=%ROOT%deploy\scripts\ec2-apply-api.sh"
set "SKIP_BUILD=0"

:parse_args
if "%~1"=="" goto args_done
if /i "%~1"=="--skip-build" set "SKIP_BUILD=1" & shift & goto parse_args
echo ERROR: Unknown argument: %~1
echo Usage: %~nx0 [--skip-build]
exit /b 1
:args_done

echo.
echo ========================================
echo  Smart-Manager EC2 API Deploy
echo ========================================
echo   Host : %EC2_HOST%
if "%SKIP_BUILD%"=="1" (echo   Build: skipped) else (echo   Build: enabled)
echo.

where ssh >nul 2>&1
if errorlevel 1 (
    echo ERROR: ssh not found. Install OpenSSH Client.
    exit /b 1
)
where scp >nul 2>&1
if errorlevel 1 (
    echo ERROR: scp not found. Install OpenSSH Client.
    exit /b 1
)
if not exist "%PEM_KEY%" (
    echo ERROR: PEM key not found: %PEM_KEY%
    exit /b 1
)
if not exist "%APPLY_API_SH%" (
    echo ERROR: Missing %APPLY_API_SH%
    exit /b 1
)

echo [0/4] Checking SSH...
ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes -o StrictHostKeyChecking=accept-new -o ConnectTimeout=15 %EC2_USER%@%EC2_HOST% "echo ok" >nul 2>&1
if errorlevel 1 (
    echo ERROR: SSH connection failed.
    echo   Check Security Group TCP 22 and PEM key.
    exit /b 1
)
echo   OK
echo.

if "%SKIP_BUILD%"=="1" (
    echo [1/4] Build skipped.
    echo.
) else (
    echo [1/4] Building API bootJar...
    cd /d "%BACKEND_DIR%"
    call gradlew.bat :smartmanager-api:bootJar -x test --no-daemon
    if errorlevel 1 (
        echo ERROR: API build failed.
        exit /b 1
    )
    echo   OK
    echo.
)

set "JAR_FILE="
for %%f in ("%BACKEND_DIR%\smartmanager-api\build\libs\smartmanager-api-*.jar") do (
    echo %%~nxf | findstr /i /c:"-plain.jar" >nul
    if errorlevel 1 set "JAR_FILE=%%f"
)
if "!JAR_FILE!"=="" (
    echo ERROR: bootJar not found under smartmanager-api\build\libs\
    echo        Build first or omit --skip-build after a successful build.
    exit /b 1
)
echo   Jar: !JAR_FILE!
echo.

echo [2/4] Uploading artifacts...
ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "rm -rf %REMOTE_TMP% && mkdir -p %REMOTE_TMP%"
if errorlevel 1 (
    echo ERROR: Failed to prepare remote temp dir.
    exit /b 1
)

echo   - API jar
scp -i "%PEM_KEY%" -o IdentitiesOnly=yes "!JAR_FILE!" %EC2_USER%@%EC2_HOST%:%REMOTE_TMP%/smartmanager-api.jar
if errorlevel 1 (
    echo ERROR: JAR upload failed.
    exit /b 1
)

echo   - Apply script
scp -i "%PEM_KEY%" -o IdentitiesOnly=yes "%APPLY_API_SH%" %EC2_USER%@%EC2_HOST%:%REMOTE_TMP%/ec2-apply-api.sh
if errorlevel 1 (
    echo ERROR: Apply script upload failed.
    exit /b 1
)
echo   OK
echo.

echo [3/4] Applying on server...
ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes -o ServerAliveInterval=30 -o ServerAliveCountMax=20 %EC2_USER%@%EC2_HOST% "sed -i 's/\r$//' %REMOTE_TMP%/ec2-apply-api.sh && chmod +x %REMOTE_TMP%/ec2-apply-api.sh && bash %REMOTE_TMP%/ec2-apply-api.sh %REMOTE_TMP%/smartmanager-api.jar && rm -rf %REMOTE_TMP%"
if errorlevel 1 (
    echo ERROR: API deploy failed on server.
    exit /b 1
)
echo   OK
echo.

echo [4/4] Health check...
set "HEALTH_OK=0"
for /l %%i in (1,1,20) do (
    curl -sS -m 5 "http://%EC2_HOST%/api/health" 2>nul | findstr /i "UP" >nul 2>&1
    if not errorlevel 1 (
        set "HEALTH_OK=1"
        goto health_done
    )
    timeout /t 3 /nobreak >nul
)
:health_done
if "%HEALTH_OK%"=="1" (
    echo   Health: OK
) else (
    echo   Health: not ready yet
    echo   Check: http://%EC2_HOST%/api/health
)
echo.
echo API deploy finished.
echo.
exit /b 0
