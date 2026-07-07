# 데이터 백업·복구 설계서

> **문서 버전:** 1.0  
> **작성일:** 2026-07-07  
> **Wave:** S1 (로컬·초기 구축)  
> **관련:** `results/sample/system-information-spec.md` §3.7

---

## 1. 목적

로컬·초기 구축 환경에서 MariaDB **스키마+데이터** 스냅샷을 파일로 보관하고, 필요 시 **복구(적용)** 한다.

| 구분 | 내용 |
|------|------|
| UI 위치 | 시스템정보 → **시스템 설정** 하단 「데이터 백업 및 복구」 |
| 저장 경로 | `{SMARTMANAGER}/backup/*.sql` |
| 권한 | `system:backup:read`, `system:backup:execute` (SYSTEM_ADMIN) |

> 운영 배포 시 UI 복구는 위험하므로 Runbook 수동 복구를 권장. 로컬 개발용으로 UI **적용** 버튼을 제공한다.

---

## 2. API

| 메서드 | 경로 | 설명 |
|--------|------|------|
| GET | `/api/v1/system/backups` | backup 폴더 `.sql` 목록 |
| POST | `/api/v1/system/backups` | `mariadb-dump` 실행 → 새 파일 생성 |
| DELETE | `/api/v1/system/backups/{fileName}` | 파일 삭제 |
| POST | `/api/v1/system/backups/{fileName}/restore` | `mariadb < file.sql` 복구 |

### 목록 응답 예

```json
[
  {
    "fileName": "smartmanager_20260707_231500.sql",
    "fileSizeBytes": 12582912,
    "createdAt": "2026-07-07T23:15:00+09:00"
  }
]
```

---

## 3. 파일 규칙

| 항목 | 규칙 |
|------|------|
| 디렉터리 | `smartmanager.backup.dir` (기본 `../../backup`) |
| 파일명 | `smartmanager_yyyyMMdd_HHmmss.sql` |
| 확장자 | `.sql` 만 허용 |
| path traversal | `..`, `/`, `\` 금지 — 파일명만 |

`bootRun` 기준 `user.dir`이 `smartmanager-api`일 때 `../../backup` → 프로젝트 루트 `SMARTMANAGER/backup`.

---

## 4. 실행 방식

### 백업

```bash
mariadb-dump -h {host} -P {port} -u {user} -p{password} \
  --single-transaction --routines --events {database} > backup/file.sql
```

### 복구

```bash
mariadb -h {host} -P {port} -u {user} -p{password} {database} < backup/file.sql
```

- JDBC URL에서 host, port, database 파싱
- Windows: `mariadb-dump`/`mariadb` PATH 또는 `smartmanager.backup.mariadb-bin-dir` 지정

---

## 5. UI

| 버튼 | 동작 |
|------|------|
| **백업 저장** | POST → 목록 갱신 |
| **삭제** | confirm 후 DELETE |
| **적용** | 강한 confirm («현재 DB가 덮어씌워집니다») 후 POST restore |

복구 성공 시 **재로그인** 안내.

---

## 6. DB 마이그레이션

`V061__system_backup_permissions.sql`

```sql
INSERT INTO permission ... ('system:backup:read', ...), ('system:backup:execute', ...)
-- SYSTEM_ADMIN role_permission
```

---

## 7. 보안·운영

1. `backup/`은 `.gitignore` 대상 (실데이터 포함)
2. 복구 중 동시 API 사용 금지 권장
3. Flyway `flyway_schema_history`도 백업 시점으로 복원됨

---

## 8. 구현 패키지

```text
infrastructure/config/BackupProperties.java
infrastructure/system/MariaDbBackupService.java
infrastructure/application/DatabaseBackupApplicationService.java
api/web/system/backup/SystemBackupController.java
frontend: api/systemBackup.ts, SystemSettingsPage.tsx (섹션 추가)
```

---

## 9. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-07-07 | 초안 — 로컬 backup 폴더·API·UI |
