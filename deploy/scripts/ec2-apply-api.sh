#!/usr/bin/env bash
# EC2: replace API jar, start once with Flyway repair-on-migrate, then clear the flag.
# Usage: ec2-apply-api.sh <path-to-smartmanager-api.jar>
set -euo pipefail

JAR_SRC="${1:?jar path required}"
APP_DIR="/var/smartmanager/app"
DROPIN_DIR="/etc/systemd/system/smartmanager-api.service.d"
DROPIN_FILE="${DROPIN_DIR}/90-flyway-repair.conf"
HEALTH_URL="http://127.0.0.1:8080/api/health"

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
sudo systemctl daemon-reload

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
  sudo systemctl daemon-reload
  echo "API apply completed."
  exit 0
fi

echo "WARNING: health not ready yet. Keeping repair-on-migrate=true for systemd restarts." >&2
echo "  Check: sudo journalctl -u smartmanager-api -n 80 --no-pager -l" >&2
echo "  After UP, remove: sudo rm -f ${DROPIN_FILE} && sudo systemctl daemon-reload" >&2
exit 0
