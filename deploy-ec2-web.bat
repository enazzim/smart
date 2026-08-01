@echo off
setlocal enabledelayedexpansion

rem ============================================================
rem  Smart-Manager EC2 — Frontend(dist) only → Nginx
rem  Usage: deploy-ec2-web.bat
rem  - Builds nothing. Uploads smartmanager_frontend\dist as-is.
rem ============================================================

set "ROOT=%~dp0"
set "EC2_HOST=3.36.75.21"
set "EC2_USER=ec2-user"
set "PEM_KEY=%ROOT%sindong-key.pem"
set "FRONTEND_DIR=%ROOT%smartmanager_frontend"
set "DIST_DIR=%FRONTEND_DIR%\dist"
set "REMOTE_WWW=/var/smartmanager/www"
set "REMOTE_WEB_DIR=/var/smartmanager/www/smartmanager"
set "REMOTE_TMP=/tmp/smartmanager-deploy-web"
set "TGZ=%ROOT%_deploy-www.tgz"

echo.
echo ========================================
echo  Smart-Manager EC2 Web Deploy (dist only)
echo ========================================
echo   Host: %EC2_HOST%
echo   Dist: %DIST_DIR%
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
where tar >nul 2>&1
if errorlevel 1 (
    echo ERROR: tar not found.
    exit /b 1
)

if not exist "%PEM_KEY%" (
    echo ERROR: PEM key not found: %PEM_KEY%
    exit /b 1
)
if not exist "%DIST_DIR%\index.html" (
    echo ERROR: dist\index.html not found.
    echo        Run "npm run build" under smartmanager_frontend first.
    exit /b 1
)

echo [1/3] Checking SSH...
ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes -o StrictHostKeyChecking=accept-new -o ConnectTimeout=15 %EC2_USER%@%EC2_HOST% "echo ok" >nul 2>&1
if errorlevel 1 (
    echo ERROR: SSH connection failed.
    exit /b 1
)
echo   OK
echo.

echo [2/3] Packing and uploading dist...
if exist "%TGZ%" del /f /q "%TGZ%" >nul 2>&1
cd /d "%DIST_DIR%"
tar -czf "%TGZ%" .
if errorlevel 1 (
    echo ERROR: Failed to pack dist.
    exit /b 1
)

ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "rm -rf %REMOTE_TMP% && mkdir -p %REMOTE_TMP%/www"
if errorlevel 1 (
    echo ERROR: Failed to prepare remote temp dir.
    del /f /q "%TGZ%" >nul 2>&1
    exit /b 1
)

scp -i "%PEM_KEY%" -o IdentitiesOnly=yes "%TGZ%" %EC2_USER%@%EC2_HOST%:%REMOTE_TMP%/www.tgz
if errorlevel 1 (
    echo ERROR: Upload failed.
    del /f /q "%TGZ%" >nul 2>&1
    exit /b 1
)
del /f /q "%TGZ%" >nul 2>&1

ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "tar -xzf %REMOTE_TMP%/www.tgz -C %REMOTE_TMP%/www && rm -f %REMOTE_TMP%/www.tgz"
if errorlevel 1 (
    echo ERROR: Failed to extract on remote.
    exit /b 1
)
echo   OK
echo.

echo [3/3] Applying to Nginx www...
ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "sudo mkdir -p %REMOTE_WEB_DIR% && sudo find %REMOTE_WEB_DIR% -mindepth 1 -delete && sudo cp -a %REMOTE_TMP%/www/. %REMOTE_WEB_DIR%/ && sudo chown -R smartmanager:smartmanager %REMOTE_WWW% && rm -rf %REMOTE_TMP% && sudo nginx -t && sudo systemctl reload nginx"
if errorlevel 1 (
    echo ERROR: Frontend apply / nginx reload failed.
    exit /b 1
)

echo   OK
echo.
echo Web deploy finished.
echo   URL: http://%EC2_HOST%/smartmanager/
echo.
exit /b 0
