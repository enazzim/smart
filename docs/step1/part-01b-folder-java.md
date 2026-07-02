# Step 1 Part 01 보완 — 폴더·Java 버전 정리

> **일자:** 2026-07-02  
> **선행:** [part-01-boilerplate.md](./part-01-boilerplate.md)

---

## 변경 사항

| 항목 | 이전 | 이후 |
|------|------|------|
| 백엔드 위치 | 저장소 루트 (`smartmanager-*` 모듈) | **`smartmanager_backend/`** |
| 프론트엔드 | (없음) | **`smartmanager_frontend/`** (Part 02+ 착수) |
| Java | toolchain 21 | **source/target 17** (JDK **17 이상** 필요) |
| Gradle 실행 | 루트 `gradlew.bat` | `smartmanager_backend/gradlew.bat` |

## 검증

```
cd smartmanager_backend
.\gradlew.bat clean build -x test
→ BUILD SUCCESSFUL
```

## 실행

```powershell
cd smartmanager_backend
.\gradlew.bat :smartmanager-api:bootRun
```

또는 `sql\local\run-api.ps1` (루트에서 실행 시 backend로 이동 후 기동)
