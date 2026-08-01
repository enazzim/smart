#!/usr/bin/env bash
# EC2: replace API jar, start once with Flyway repair-on-migrate, then clear the flag.
# Usage: ec2-apply-api.sh <path-to-smartmanager-api.jar>
set -euo pipefail

JAR_SRC="${1:?jar path required}"
APP_DIR="/var/smartmanager/app"
DROPIN_DIR="/etc/systemd/system/smartmanager-api.service.d"
DROPIN_FILE="${DROPIN_DIR}/90-flyway-repair.conf"
HEALTH_URL="http://127.0.0.1:8080/api/health"

daemon_reload_retry() {
  local attempt
  for attempt in 1 2 3 4 5; do
    if sudo systemctl daemon-reload; then
      return 0
    fi
    echo "  daemon-reload attempt ${attempt} failed; retrying..." >&2
    sleep $((attempt * 2))
  done
  return 1
}

if [[ ! -f "${JAR_SRC}" ]]; then
  echo "ERROR: jar not found: ${JAR_SRC}" >&2
  exit 1
fi

echo "Stopping smartmanager-api..."
sudo systemctl stop smartmanager-api || true

echo "Installing jar..."
sudo cp "${JAR_SRC}" "${APP_DIR}/smartmanager-api.jar"
sudo chown smartmanager:smartmanager "${APP_DIR}/smartmanager-api.jar"

echo "Enabling one-shot Flyway repair-on-migrate..."
sudo mkdir -p "${DROPIN_DIR}"
sudo tee "${DROPIN_FILE}" >/dev/null <<'EOF'
[Service]
Environment=SMARTMANAGER_FLYWAY_REPAIR_ON_MIGRATE=true
EOF
if ! daemon_reload_retry; then
  echo "ERROR: systemctl daemon-reload failed before start." >&2
  exit 1
fi

echo "Starting smartmanager-api..."
sudo systemctl start smartmanager-api

echo "Waiting for health..."
HEALTH_OK=0
for _ in $(seq 1 40); do
  if curl -sf -m 3 "${HEALTH_URL}" 2>/dev/null | grep -qi UP; then
    HEALTH_OK=1
    echo "Health: OK"
    break
  fi
  sleep 3
done

if [[ "${HEALTH_OK}" -eq 1 ]]; then
  echo "Clearing Flyway repair-on-migrate flag..."
  sudo rm -f "${DROPIN_FILE}"
  # JVM 기동 직후 dbus/systemd 부하로 daemon-reload가 자주 타임아웃된다.
  # drop-in 파일은 이미 지웠으므로, reload 실패해도 API 배포 자체는 성공으로 본다.
  sleep 3
  if daemon_reload_retry; then
    echo "API apply completed."
  else
    echo "WARNING: daemon-reload timed out after Health OK (API is running)." >&2
    echo "  Drop-in already removed. Later run: sudo systemctl daemon-reload" >&2
    echo "API apply completed (with daemon-reload warning)."
  fi
  exit 0
fi

echo "WARNING: health not ready yet. Keeping repair-on-migrate=true for systemd restarts." >&2
echo "  Check: sudo journalctl -u smartmanager-api -n 80 --no-pager -l" >&2
echo "  After UP, remove: sudo rm -f ${DROPIN_FILE} && sudo systemctl daemon-reload" >&2
exit 0
