#!/usr/bin/env bash
# Smart-Manager EC2 bootstrap (Amazon Linux 2023)
# Idempotent: safe to re-run. Does NOT overwrite existing /etc/smartmanager/api.env
# unless FORCE_API_ENV=1.
#
# MariaDB root auth:
#   Prefer staged file STAGE_DIR/mariadb-root.pass (password only, one line)
#   Else try sudo socket (passwordless). Else fail with clear message.
set -euo pipefail

STAGE_DIR="${STAGE_DIR:-/tmp/smartmanager-bootstrap}"
APP_ROOT="/var/smartmanager"
ETC_DIR="/etc/smartmanager"
API_ENV="${ETC_DIR}/api.env"
SERVICE_NAME="smartmanager-api"
WEB_SUBDIR="${APP_ROOT}/www/smartmanager"
MYSQL_DEFAULTS=""

log()  { echo "[bootstrap] $*"; }
fail() { echo "[bootstrap] ERROR: $*" >&2; exit 1; }
sql_escape() { printf '%s' "$1" | sed "s/'/'\\\\''/g"; }

need_cmd() {
  command -v "$1" >/dev/null 2>&1 || fail "required command not found: $1"
}

cleanup() {
  if [[ -n "${MYSQL_DEFAULTS}" && -f "${MYSQL_DEFAULTS}" ]]; then
    rm -f "${MYSQL_DEFAULTS}"
  fi
}
trap cleanup EXIT

mysql_admin() {
  if [[ -n "${MYSQL_DEFAULTS}" ]]; then
    "${MYSQL_CLI}" --defaults-extra-file="${MYSQL_DEFAULTS}" "$@"
  else
    sudo "${MYSQL_CLI}" "$@"
  fi
}

log "=== Smart-Manager EC2 bootstrap ==="

need_cmd sudo
need_cmd java
need_cmd nginx
need_cmd openssl

if ! command -v mariadb >/dev/null 2>&1 && ! command -v mysql >/dev/null 2>&1; then
  fail "mariadb/mysql client not found"
fi
MYSQL_CLI="$(command -v mariadb || command -v mysql)"

JAVA_VER="$(java -version 2>&1 | head -n1 || true)"
log "Java: ${JAVA_VER}"

# ---------- MariaDB root client ----------
if [[ -f "${STAGE_DIR}/mariadb-root.pass" ]]; then
  ROOT_PASS="$(tr -d '\r\n' < "${STAGE_DIR}/mariadb-root.pass")"
  [[ -n "${ROOT_PASS}" ]] || fail "mariadb-root.pass is empty"
  MYSQL_DEFAULTS="$(mktemp)"
  chmod 600 "${MYSQL_DEFAULTS}"
  ROOT_PASS_CFG="$(printf '%s' "${ROOT_PASS}" | sed 's/\\/\\\\/g; s/"/\\"/g')"
  cat > "${MYSQL_DEFAULTS}" <<EOF
[client]
user=root
password="${ROOT_PASS_CFG}"
EOF
  log "MariaDB: using root password from staging file"
  mysql_admin -e "SELECT 1;" >/dev/null || fail "MariaDB root login failed — check deploy/env/mariadb-root.local.txt"
else
  log "MariaDB: trying sudo socket (no password)"
  if ! mysql_admin -e "SELECT 1;" >/dev/null 2>&1; then
    fail "MariaDB root access denied. Create deploy/env/mariadb-root.local.txt with root password (one line) and re-run setup-ec2.bat"
  fi
fi

# ---------- OS user ----------
if id smartmanager >/dev/null 2>&1; then
  log "user smartmanager: exists"
else
  log "user smartmanager: creating"
  sudo useradd -r -s /sbin/nologin smartmanager
fi

# ---------- Directories ----------
log "directories: ensuring under ${APP_ROOT}"
sudo mkdir -p \
  "${APP_ROOT}/app" \
  "${APP_ROOT}/www" \
  "${WEB_SUBDIR}" \
  "${APP_ROOT}/drawing-storage/pdf" \
  "${APP_ROOT}/filedownload" \
  "${APP_ROOT}/backup" \
  "${APP_ROOT}/logs" \
  "${ETC_DIR}"
sudo chown -R smartmanager:smartmanager "${APP_ROOT}"

# ---------- App DB password ----------
DB_PASS=""
if [[ -f "${STAGE_DIR}/api.env" ]] && { [[ ! -f "${API_ENV}" ]] || [[ "${FORCE_API_ENV:-0}" == "1" ]]; }; then
  DB_PASS="$(grep -E '^SMARTMANAGER_DB_PASSWORD=' "${STAGE_DIR}/api.env" | head -n1 | cut -d= -f2- || true)"
elif [[ -f "${API_ENV}" ]] && [[ "${FORCE_API_ENV:-0}" != "1" ]]; then
  DB_PASS="$(sudo grep -E '^SMARTMANAGER_DB_PASSWORD=' "${API_ENV}" | head -n1 | cut -d= -f2- || true)"
fi
if [[ -z "${DB_PASS}" ]] || [[ "${DB_PASS}" == "CHANGE_ME_DB_PASSWORD" ]]; then
  DB_PASS="$(openssl rand -base64 24 | tr -d '/+=' | head -c 24)"
fi
DB_PASS_SQL="$(sql_escape "${DB_PASS}")"

log "MariaDB: ensuring database/user"
mysql_admin -e "CREATE DATABASE IF NOT EXISTS smartmanager CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;"
mysql_admin -e "CREATE USER IF NOT EXISTS 'smartmanager'@'localhost' IDENTIFIED BY '${DB_PASS_SQL}';"
mysql_admin -e "ALTER USER 'smartmanager'@'localhost' IDENTIFIED BY '${DB_PASS_SQL}';" || true
mysql_admin -e "GRANT ALL PRIVILEGES ON smartmanager.* TO 'smartmanager'@'localhost'; FLUSH PRIVILEGES;"
log "MariaDB: database smartmanager ready"

# ---------- api.env ----------
if [[ -f "${API_ENV}" ]] && [[ "${FORCE_API_ENV:-0}" != "1" ]]; then
  log "api.env: keep existing ${API_ENV}"
  if sudo grep -q 'CHANGE_ME_DB_PASSWORD' "${API_ENV}"; then
    log "api.env: replacing CHANGE_ME_DB_PASSWORD"
    TMP_ENV="$(mktemp)"
    sudo cp "${API_ENV}" "${TMP_ENV}"
    sudo chmod u+w "${TMP_ENV}"
    sudo sed -i "s|CHANGE_ME_DB_PASSWORD|${DB_PASS}|g" "${TMP_ENV}"
    sudo install -m 600 -o root -g smartmanager "${TMP_ENV}" "${API_ENV}"
    rm -f "${TMP_ENV}"
  fi
elif [[ -f "${STAGE_DIR}/api.env" ]]; then
  log "api.env: installing provided staging file"
  sudo install -m 600 -o root -g smartmanager "${STAGE_DIR}/api.env" "${API_ENV}"
else
  log "api.env: generating from example"
  JWT_SECRET="$(openssl rand -hex 32)"
  ADMIN_PASS="$(openssl rand -base64 18 | tr -d '/+=' | head -c 16)"
  if [[ ! -f "${STAGE_DIR}/api.env.example" ]]; then
    fail "api.env.example not found in ${STAGE_DIR}"
  fi
  TMP_ENV="$(mktemp)"
  cp "${STAGE_DIR}/api.env.example" "${TMP_ENV}"
  sed -i "s|CHANGE_ME_DB_PASSWORD|${DB_PASS}|g" "${TMP_ENV}"
  sed -i "s|CHANGE_ME_TO_RANDOM_STRING_AT_LEAST_32_CHARS|${JWT_SECRET}|g" "${TMP_ENV}"
  sed -i "s|CHANGE_ME_ADMIN_PASSWORD|${ADMIN_PASS}|g" "${TMP_ENV}"
  sudo install -m 600 -o root -g smartmanager "${TMP_ENV}" "${API_ENV}"
  rm -f "${TMP_ENV}"

  CREDS="${STAGE_DIR}/bootstrap-credentials.txt"
  umask 077
  cat > "${CREDS}" <<EOF
# Generated by ec2-bootstrap.sh — store securely, then delete
SMARTMANAGER_DB_USER=smartmanager
SMARTMANAGER_DB_PASSWORD=${DB_PASS}
SMARTMANAGER_ADMIN_LOGIN_ID=admin
SMARTMANAGER_ADMIN_INITIAL_PASSWORD=${ADMIN_PASS}
EOF
  log "api.env: created (credentials file staged for download)"
fi

# ---------- systemd ----------
if [[ ! -f "${STAGE_DIR}/smartmanager-api.service" ]]; then
  fail "smartmanager-api.service not found in ${STAGE_DIR}"
fi
log "systemd: installing ${SERVICE_NAME}"
sudo cp "${STAGE_DIR}/smartmanager-api.service" "/etc/systemd/system/${SERVICE_NAME}.service"
sudo systemctl daemon-reload
sudo systemctl enable "${SERVICE_NAME}"
if [[ -f "${APP_ROOT}/app/smartmanager-api.jar" ]]; then
  log "systemd: JAR present — restarting service"
  sudo systemctl restart "${SERVICE_NAME}" || log "systemd: start failed (check journalctl -u ${SERVICE_NAME})"
else
  log "systemd: JAR not present yet — run deploy-ec2.bat next"
fi

# ---------- Nginx ----------
if [[ ! -f "${STAGE_DIR}/smartmanager.conf" ]]; then
  fail "smartmanager.conf not found in ${STAGE_DIR}"
fi
log "nginx: installing conf.d/smartmanager.conf"
sudo cp "${STAGE_DIR}/smartmanager.conf" /etc/nginx/conf.d/smartmanager.conf
sudo nginx -t || fail "nginx -t failed — fix conf conflict then re-run"
sudo systemctl enable nginx >/dev/null 2>&1 || true
sudo systemctl reload nginx || sudo systemctl restart nginx
log "nginx: reloaded"

echo
log "=== Bootstrap complete ==="
log "app root : ${APP_ROOT}"
log "web dir  : ${WEB_SUBDIR}"
log "api.env  : ${API_ENV}"
log "systemd  : ${SERVICE_NAME} (enabled)"
log "nginx    : /etc/nginx/conf.d/smartmanager.conf"
log "Next     : run deploy-ec2.bat from your PC"
echo