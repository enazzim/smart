@echo off
setlocal enabledelayedexpansion

rem ============================================================
rem  Smart-Manager EC2 first-time setup (Amazon Linux 2023)
rem  Usage: setup-ec2.bat [--force-env]
rem    --force-env  regenerate /etc/smartmanager/api.env
rem ============================================================

set "ROOT=%~dp0"
set "EC2_HOST=3.36.75.21"
set "EC2_USER=ec2-user"
set "PEM_KEY=%ROOT%sindong-key.pem"
set "STAGE_REMOTE=/tmp/smartmanager-bootstrap"
set "SCRIPT_DIR=%ROOT%deploy\scripts"
set "ENV_EXAMPLE=%ROOT%deploy\env\api.env.example"
set "ENV_LOCAL=%ROOT%deploy\env\api.env"
set "SERVICE_FILE=%ROOT%deploy\systemd\smartmanager-api.service"
set "NGINX_CONF=%ROOT%deploy\nginx\smartmanager.conf"
set "CREDS_LOCAL=%ROOT%deploy\env\bootstrap-credentials.local.txt"
set "MARIADB_ROOT_LOCAL=%ROOT%deploy\env\mariadb-root.local.txt"
set "FORCE_API_ENV=0"

if /i "%~1"=="--force-env" set "FORCE_API_ENV=1"

echo.
echo ========================================
echo  Smart-Manager EC2 Setup
echo ========================================
echo.

where ssh >nul 2>&1
if errorlevel 1 (
    echo ERROR: ssh.exe not found.
    exit /b 1
)
where scp >nul 2>&1
if errorlevel 1 (
    echo ERROR: scp.exe not found.
    exit /b 1
)
if not exist "%PEM_KEY%" (
    echo ERROR: PEM key not found.
    exit /b 1
)
if not exist "%SCRIPT_DIR%\ec2-bootstrap.sh" (
    echo ERROR: ec2-bootstrap.sh not found.
    exit /b 1
)
if not exist "%ENV_EXAMPLE%" (
    echo ERROR: api.env.example not found.
    exit /b 1
)
if not exist "%SERVICE_FILE%" (
    echo ERROR: smartmanager-api.service not found.
    exit /b 1
)
if not exist "%NGINX_CONF%" (
    echo ERROR: smartmanager.conf not found.
    exit /b 1
)

echo [1/4] Checking SSH...
ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes -o StrictHostKeyChecking=accept-new -o ConnectTimeout=15 %EC2_USER%@%EC2_HOST% "echo ok" >nul 2>&1
if errorlevel 1 (
    echo ERROR: SSH connection failed.
    exit /b 1
)
echo   OK
echo.

echo [2/4] Uploading bootstrap files...
ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "rm -rf %STAGE_REMOTE% && mkdir -p %STAGE_REMOTE%"
if errorlevel 1 (
    echo ERROR: Failed to create remote staging dir.
    exit /b 1
)

scp -i "%PEM_KEY%" -o IdentitiesOnly=yes ^
  "%SCRIPT_DIR%\ec2-bootstrap.sh" ^
  "%ENV_EXAMPLE%" ^
  "%SERVICE_FILE%" ^
  "%NGINX_CONF%" ^
  %EC2_USER%@%EC2_HOST%:%STAGE_REMOTE%/
if errorlevel 1 (
    echo ERROR: Upload failed.
    exit /b 1
)
if exist "%ENV_LOCAL%" (
    echo   - using local deploy\env\api.env
    scp -i "%PEM_KEY%" -o IdentitiesOnly=yes "%ENV_LOCAL%" %EC2_USER%@%EC2_HOST%:%STAGE_REMOTE%/api.env
    if errorlevel 1 (
        echo ERROR: api.env upload failed.
        exit /b 1
    )
)
if exist "%MARIADB_ROOT_LOCAL%" (
    echo   - using MariaDB root password file
    scp -i "%PEM_KEY%" -o IdentitiesOnly=yes "%MARIADB_ROOT_LOCAL%" %EC2_USER%@%EC2_HOST%:%STAGE_REMOTE%/mariadb-root.pass
    if errorlevel 1 (
        echo ERROR: mariadb-root password upload failed.
        exit /b 1
    )
) else (
    echo.
    echo MariaDB root password file not found:
    echo   deploy\env\mariadb-root.local.txt
    echo Put the root password as a single line in that file, then re-run.
    echo.
    exit /b 1
)
echo   OK
echo.

echo [3/4] Running bootstrap on EC2...
ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "chmod +x %STAGE_REMOTE%/ec2-bootstrap.sh && STAGE_DIR=%STAGE_REMOTE% FORCE_API_ENV=%FORCE_API_ENV% bash %STAGE_REMOTE%/ec2-bootstrap.sh"
if errorlevel 1 (
    echo ERROR: Bootstrap failed.
    exit /b 1
)
echo.

echo [4/4] Fetching credentials if newly created...
ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "test -f %STAGE_REMOTE%/bootstrap-credentials.txt"
if not errorlevel 1 (
    scp -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST%:%STAGE_REMOTE%/bootstrap-credentials.txt "%CREDS_LOCAL%"
    if errorlevel 1 (
        echo WARNING: Could not download credentials file.
    ) else (
        echo   Saved: deploy\env\bootstrap-credentials.local.txt
        echo   Open that file for DB/admin passwords. Do not commit it.
    )
) else (
    echo   No new credentials file ^(api.env already existed^).
)

ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "rm -rf %STAGE_REMOTE%" >nul 2>&1

echo.
echo Setup finished.
echo Next: deploy-ec2.bat
echo.
pause
endlocal
exit /b 0
