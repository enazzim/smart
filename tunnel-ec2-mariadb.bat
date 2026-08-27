@echo off
setlocal enabledelayedexpansion

rem ============================================================
rem  EC2 MariaDB SSH Local Port Forward (for HeidiSQL)
rem  PC:127.0.0.1:LOCAL_PORT  -->  EC2:127.0.0.1:3306
rem ============================================================

set "ROOT=%~dp0"
set "EC2_HOST=3.36.75.21"
set "EC2_USER=ec2-user"
set "PEM_KEY=%ROOT%sindong-key.pem"
set "LOCAL_PORT=3307"
set "REMOTE_HOST=127.0.0.1"
set "REMOTE_PORT=3306"

if not "%~1"=="" set "LOCAL_PORT=%~1"
if not "%~2"=="" set "PEM_KEY=%~2"

echo.
echo ========================================
echo  EC2 MariaDB SSH Tunnel
echo ========================================
echo   Local : 127.0.0.1:%LOCAL_PORT%
echo.

where ssh >nul 2>&1
if errorlevel 1 (
    echo ERROR: OpenSSH client ^(ssh.exe^) not found.
    echo   Install "OpenSSH Client" from Windows Optional Features.
    exit /b 1
)

if not exist "%PEM_KEY%" (
    echo ERROR: PEM key not found.
    echo   Usage: %~nx0 [localPort] [pemPath]
    exit /b 1
)

rem Local MariaDB often uses 3306 — default tunnel port is 3307
netstat -ano | findstr ":%LOCAL_PORT% " | findstr "LISTENING" >nul 2>&1
if not errorlevel 1 (
    echo ERROR: Local port %LOCAL_PORT% is already in use.
    echo   Close the other tunnel, or run: %~nx0 3308
    exit /b 1
)

echo Keep this window open while using HeidiSQL.
echo Press Ctrl+C to close the tunnel.
echo.
echo Connecting...
echo.

ssh -i "%PEM_KEY%" ^
  -N ^
  -L %LOCAL_PORT%:%REMOTE_HOST%:%REMOTE_PORT% ^
  -o IdentitiesOnly=yes ^
  -o ExitOnForwardFailure=yes ^
  -o ServerAliveInterval=60 ^
  -o ServerAliveCountMax=3 ^
  %EC2_USER%@%EC2_HOST%

set "RC=%ERRORLEVEL%"
echo.
if not "%RC%"=="0" (
    echo Tunnel ended with exit code %RC%.
    echo   - Check Security Group inbound TCP 22 from your IP
    echo   - Check PEM key permissions / path
    echo   - First connect may ask to accept host key: type yes
) else (
    echo Tunnel closed.
)
echo.
pause
endlocal
exit /b %RC%
