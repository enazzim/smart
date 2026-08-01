# Smart-Manager EC2 배포 (Amazon Linux 2023)

상세 체크리스트: [docs/deploy/ec2-checklist.md](../docs/deploy/ec2-checklist.md)

**대상 OS**: Amazon Linux 2023 (`ec2-user`, 패키지 관리자 `dnf`)

**기설치**: JDK 17, MariaDB 11.4, Nginx

## 구성 파일

| 경로 | 용도 |
|------|------|
| [nginx/smartmanager.conf](nginx/smartmanager.conf) | Nginx (`/etc/nginx/conf.d/`) |
| [systemd/smartmanager-api.service](systemd/smartmanager-api.service) | API 데몬 |
| [env/api.env.example](env/api.env.example) | 운영 환경변수 예시 |
| [application-prod.yml](../smartmanager_backend/smartmanager-api/src/main/resources/application-prod.yml) | Spring `prod` 프로필 |

## 빠른 설치 순서

### 0. JDK 확인 (이미 설치됨)

```bash
java -version
# 17.x 인지 확인. 미충족 시에만: sudo dnf install -y java-17-amazon-corretto-devel
```

### 1. 유저·디렉터리

```bash
sudo useradd -r -s /sbin/nologin smartmanager
sudo mkdir -p /var/smartmanager/{app,www,drawing-storage/pdf,filedownload,backup,logs}
sudo mkdir -p /etc/smartmanager
sudo chown -R smartmanager:smartmanager /var/smartmanager
```

### 2. MariaDB

```sql
CREATE DATABASE smartmanager CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
CREATE USER 'smartmanager'@'localhost' IDENTIFIED BY '<STRONG_PASSWORD>';
GRANT ALL PRIVILEGES ON smartmanager.* TO 'smartmanager'@'localhost';
FLUSH PRIVILEGES;
```

### 3. 환경변수

```bash
sudo cp deploy/env/api.env.example /etc/smartmanager/api.env
sudo nano /etc/smartmanager/api.env   # 시크릿 입력
sudo chmod 600 /etc/smartmanager/api.env
sudo chown root:smartmanager /etc/smartmanager/api.env
```

### 4. 빌드·배치

**백엔드** (개발 PC 또는 EC2):

```bash
cd smartmanager_backend
./gradlew :smartmanager-api:bootJar -x test
sudo cp smartmanager-api/build/libs/smartmanager-api-*.jar /var/smartmanager/app/smartmanager-api.jar
sudo chown smartmanager:smartmanager /var/smartmanager/app/smartmanager-api.jar
```

**프론트**:

```bash
cd smartmanager_frontend
npm ci && npm run build
sudo rsync -a --delete dist/ /var/smartmanager/www/
sudo chown -R smartmanager:smartmanager /var/smartmanager/www
```

### 5. systemd

```bash
sudo cp deploy/systemd/smartmanager-api.service /etc/systemd/system/
sudo systemctl daemon-reload
sudo systemctl enable --now smartmanager-api
sudo systemctl status smartmanager-api
journalctl -u smartmanager-api -f
```

### 6. Nginx (Amazon Linux 2023)

```bash
sudo cp deploy/nginx/smartmanager.conf /etc/nginx/conf.d/smartmanager.conf
# default_server 충돌 시 /etc/nginx/nginx.conf 또는 기존 conf.d 파일 조정
sudo nginx -t && sudo systemctl reload nginx
```

### 7. 확인

```bash
curl -sS http://127.0.0.1:8080/api/health
curl -sS http://52.78.102.6/api/health
```

브라우저: `http://52.78.102.6/`

## 재배포

```bash
# JAR 교체 후
sudo systemctl restart smartmanager-api

# 프론트만
sudo rsync -a --delete dist/ /var/smartmanager/www/
```

## HTTPS (추후)

도메인 확보 후 Let's Encrypt(certbot)로 443을 열고 `server_name`을 도메인으로 바꾼다. 현재는 HTTP(IP) 전제.