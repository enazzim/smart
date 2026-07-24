@echo off
setlocal enabledelayedexpansion

rem ============================================================
rem  Smart-Manager EC2 deploy (Amazon Linux 2023)
rem  Usage: deploy-ec2.bat [all^|api^|web] [--skip-build]
rem ============================================================

set "ROOT=%~dp0"
set "EC2_HOST=3.36.75.21"
set "EC2_USER=ec2-user"
set "PEM_KEY=%ROOT%sindong-key.pem"
set "BACKEND_DIR=%ROOT%smartmanager_backend"
set "FRONTEND_DIR=%ROOT%smartmanager_frontend"
set "REMOTE_APP=/var/smartmanager/app"
set "REMOTE_WWW=/var/smartmanager/www"
set "REMOTE_WEB_DIR=/var/smartmanager/www/smartmanager"
set "REMOTE_TMP=/tmp/smartmanager-deploy"
set "NGINX_CONF=%ROOT%deploy\nginx\smartmanager.conf"
set "APPLY_API_SH=%ROOT%deploy\scripts\ec2-apply-api.sh"
set "TARGET=all"
set "SKIP_BUILD=0"
set "DO_API=0"
set "DO_WEB=0"

:parse_args
if "%~1"=="" goto args_done
if /i "%~1"=="all" set "TARGET=all" & shift & goto parse_args
if /i "%~1"=="api" set "TARGET=api" & shift & goto parse_args
if /i "%~1"=="web" set "TARGET=web" & shift & goto parse_args
if /i "%~1"=="--skip-build" set "SKIP_BUILD=1" & shift & goto parse_args
echo ERROR: Unknown argument: %~1
echo Usage: %~nx0 [all^|api^|web] [--skip-build]
exit /b 1
:args_done

if /i "%TARGET%"=="all" set "DO_API=1" & set "DO_WEB=1"
if /i "%TARGET%"=="api" set "DO_API=1"
if /i "%TARGET%"=="web" set "DO_WEB=1"

echo.
echo ========================================
echo  Smart-Manager EC2 Deploy
echo ========================================
echo   Mode : %TARGET%
if "%SKIP_BUILD%"=="1" (echo   Build: skipped) else (echo   Build: enabled)
echo.

where ssh >nul 2>&1
if errorlevel 1 (
    echo ERROR: ssh.exe not found. Install OpenSSH Client.
    exit /b 1
)
where scp >nul 2>&1
if errorlevel 1 (
    echo ERROR: scp.exe not found. Install OpenSSH Client.
    exit /b 1
)
if not exist "%PEM_KEY%" (
    echo ERROR: PEM key not found.
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

rem ---------- Build ----------
if "%SKIP_BUILD%"=="1" (
    echo [1/4] Build skipped.
    echo.
    goto resolve_artifacts
)

echo [1/4] Building...
if "%DO_API%"=="1" (
    echo   - API bootJar
    cd /d "%BACKEND_DIR%"
    call gradlew.bat :smartmanager-api:bootJar -x test --no-daemon
    if errorlevel 1 (
        echo ERROR: API build failed.
        exit /b 1
    )
)
if "%DO_WEB%"=="1" (
    echo   - Frontend npm run build
    cd /d "%FRONTEND_DIR%"
    call npm run build
    if errorlevel 1 (
        echo ERROR: Frontend build failed.
        exit /b 1
    )
    if not exist "%FRONTEND_DIR%\dist\index.html" (
        echo ERROR: dist\index.html not found after build.
        exit /b 1
    )
)
echo   OK
echo.

:resolve_artifacts
set "JAR_FILE="
if "%DO_API%"=="1" (
    for %%f in ("%BACKEND_DIR%\smartmanager-api\build\libs\smartmanager-api-*.jar") do (
        echo %%~nxf | findstr /i /c:"-plain.jar" >nul
        if errorlevel 1 set "JAR_FILE=%%f"
    )
    if "!JAR_FILE!"=="" (
        echo ERROR: bootJar not found under smartmanager-api\build\libs\
        exit /b 1
    )
)
if "%DO_WEB%"=="1" (
    if not exist "%FRONTEND_DIR%\dist\index.html" (
        echo ERROR: Frontend dist not found. Build first or omit --skip-build.
        exit /b 1
    )
)

rem ---------- Upload ----------
echo [2/4] Uploading artifacts...
ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "rm -rf %REMOTE_TMP% && mkdir -p %REMOTE_TMP%/www"
if errorlevel 1 (
    echo ERROR: Failed to prepare remote temp dir.
    exit /b 1
)

if "%DO_API%"=="1" (
    echo   - API jar
    scp -i "%PEM_KEY%" -o IdentitiesOnly=yes "!JAR_FILE!" %EC2_USER%@%EC2_HOST%:%REMOTE_TMP%/smartmanager-api.jar
    if errorlevel 1 (
        echo ERROR: JAR upload failed.
        exit /b 1
    )
    if not exist "%APPLY_API_SH%" (
        echo ERROR: Missing %APPLY_API_SH%
        exit /b 1
    )
    echo   - Apply script
    scp -i "%PEM_KEY%" -o IdentitiesOnly=yes "%APPLY_API_SH%" %EC2_USER%@%EC2_HOST%:%REMOTE_TMP%/ec2-apply-api.sh
    if errorlevel 1 (
        echo ERROR: Apply script upload failed.
        exit /b 1
    )
)

if "%DO_WEB%"=="1" (
    echo   - Frontend dist
    if exist "%ROOT%_deploy-www.tgz" del /f /q "%ROOT%_deploy-www.tgz" >nul 2>&1
    cd /d "%FRONTEND_DIR%\dist"
    tar -czf "%ROOT%_deploy-www.tgz" .
    if errorlevel 1 (
        echo ERROR: Failed to pack dist.
        exit /b 1
    )
    scp -i "%PEM_KEY%" -o IdentitiesOnly=yes "%ROOT%_deploy-www.tgz" %EC2_USER%@%EC2_HOST%:%REMOTE_TMP%/www.tgz
    if errorlevel 1 (
        echo ERROR: Frontend upload failed.
        del /f /q "%ROOT%_deploy-www.tgz" >nul 2>&1
        exit /b 1
    )
    ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "tar -xzf %REMOTE_TMP%/www.tgz -C %REMOTE_TMP%/www && rm -f %REMOTE_TMP%/www.tgz"
    if errorlevel 1 (
        echo ERROR: Failed to extract frontend on remote.
        del /f /q "%ROOT%_deploy-www.tgz" >nul 2>&1
        exit /b 1
    )
    del /f /q "%ROOT%_deploy-www.tgz" >nul 2>&1
)
echo   OK
echo.

rem ---------- Apply on EC2 ----------
echo [3/4] Applying on server...

if "%DO_API%"=="1" (
    rem Flyway repair-on-migrate one-shot then start (see deploy/scripts/ec2-apply-api.sh)
    ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "sed -i 's/\r$//' %REMOTE_TMP%/ec2-apply-api.sh && chmod +x %REMOTE_TMP%/ec2-apply-api.sh && bash %REMOTE_TMP%/ec2-apply-api.sh %REMOTE_TMP%/smartmanager-api.jar"
    if errorlevel 1 (
        echo ERROR: API deploy failed on server.
        exit /b 1
    )
    echo   - API restarted ^(Flyway repair-on-migrate once^)
)

if "%DO_WEB%"=="1" (
    rem smartmanager 폴더 없으면 생성, 있으면 내부만 비운 뒤 업로드
    ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "sudo mkdir -p %REMOTE_WEB_DIR% && sudo find %REMOTE_WEB_DIR% -mindepth 1 -delete && sudo cp -a %REMOTE_TMP%/www/. %REMOTE_WEB_DIR%/ && sudo chown -R smartmanager:smartmanager %REMOTE_WWW%"
    if errorlevel 1 (
        echo ERROR: Frontend deploy failed.
        exit /b 1
    )
    echo   - Frontend synced to /smartmanager/
    if exist "%NGINX_CONF%" (
        scp -i "%PEM_KEY%" -o IdentitiesOnly=yes "%NGINX_CONF%" %EC2_USER%@%EC2_HOST%:%REMOTE_TMP%/smartmanager.conf
        if errorlevel 1 (
            echo ERROR: Nginx conf upload failed.
            exit /b 1
        )
        ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "sudo cp %REMOTE_TMP%/smartmanager.conf /etc/nginx/conf.d/smartmanager.conf && sudo nginx -t && sudo systemctl reload nginx"
        if errorlevel 1 (
            echo ERROR: Nginx reload failed.
            exit /b 1
        )
        echo   - Nginx conf applied
    ) else (
        ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "sudo nginx -t && sudo systemctl reload nginx" >nul 2>&1
    )
)

ssh -i "%PEM_KEY%" -o IdentitiesOnly=yes %EC2_USER%@%EC2_HOST% "rm -rf %REMOTE_TMP%"
echo   OK
echo.

rem ---------- Health ----------
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
)
echo.
echo Deploy finished. Mode=%TARGET%
echo.
pause
endlocal
exit /b 0
